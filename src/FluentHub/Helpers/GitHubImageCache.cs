// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Application.Abstractions.Caching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using Windows.Storage.Streams;

namespace FluentHub.Helpers
{
	public enum GitHubImageLoadStatus
	{
		Empty,
		Loading,
		Loaded,
		Failed,
	}

	public static class GitHubImageCache
	{
		private const int MaximumImageSize = 20 * 1024 * 1024;

		private static readonly ConditionalWeakTable<FrameworkElement, ImageLoadState> ImageStates = new();
		private static readonly HttpClient HttpClient = CreateHttpClient();

		public static readonly DependencyProperty SourceProperty = DependencyProperty.RegisterAttached(
			"Source",
			typeof(string),
			typeof(GitHubImageCache),
			new PropertyMetadata(null, OnSourceChanged));

		public static readonly DependencyProperty LoadStatusProperty = DependencyProperty.RegisterAttached(
			"LoadStatus",
			typeof(GitHubImageLoadStatus),
			typeof(GitHubImageCache),
			new PropertyMetadata(GitHubImageLoadStatus.Empty));

		public static string? GetSource(DependencyObject dependencyObject)
			=> (string?)dependencyObject.GetValue(SourceProperty);

		public static void SetSource(DependencyObject dependencyObject, string? value)
			=> dependencyObject.SetValue(SourceProperty, value);

		public static GitHubImageLoadStatus GetLoadStatus(DependencyObject dependencyObject)
			=> (GitHubImageLoadStatus)dependencyObject.GetValue(LoadStatusProperty);

		private static void SetLoadStatus(DependencyObject dependencyObject, GitHubImageLoadStatus value)
			=> dependencyObject.SetValue(LoadStatusProperty, value);

		internal static bool IsGitHubHosted(Uri uri)
		{
			if (uri.Scheme is not ("http" or "https"))
				return false;

			var host = uri.IdnHost;
			return host.Equals("github.com", StringComparison.OrdinalIgnoreCase) ||
				host.EndsWith(".github.com", StringComparison.OrdinalIgnoreCase) ||
				host.Equals("githubusercontent.com", StringComparison.OrdinalIgnoreCase) ||
				host.EndsWith(".githubusercontent.com", StringComparison.OrdinalIgnoreCase) ||
				host.Equals("githubassets.com", StringComparison.OrdinalIgnoreCase) ||
				host.EndsWith(".githubassets.com", StringComparison.OrdinalIgnoreCase);
		}

		private static void OnSourceChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			if (dependencyObject is not Image and not PersonPicture)
				throw new ArgumentException("GitHubImageCache.Source can only be used on Image or PersonPicture controls.");

			var target = (FrameworkElement)dependencyObject;
			ImageStates.GetValue(target, static value => new ImageLoadState(value))
				.SetSource(args.NewValue as string);
		}

		private static HttpClient CreateHttpClient()
		{
			var client = new HttpClient();
			client.DefaultRequestHeaders.UserAgent.ParseAdd("FluentHub");
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/*"));
			return client;
		}

		private static async Task<byte[]> DownloadAsync(Uri uri, CancellationToken cancellationToken)
		{
			using var request = new HttpRequestMessage(HttpMethod.Get, uri);
			using var response = await HttpClient.SendAsync(
				request,
				HttpCompletionOption.ResponseHeadersRead,
				cancellationToken);
			response.EnsureSuccessStatusCode();

			if (response.Content.Headers.ContentLength is > MaximumImageSize)
				throw new InvalidDataException("The image exceeds the cache size limit.");

			await using var source = await response.Content.ReadAsStreamAsync(cancellationToken);
			using var destination = new MemoryStream();
			var buffer = new byte[81920];

			while (true)
			{
				var bytesRead = await source.ReadAsync(buffer, cancellationToken);
				if (bytesRead == 0)
					break;

				if (destination.Length + bytesRead > MaximumImageSize)
					throw new InvalidDataException("The image exceeds the cache size limit.");

				await destination.WriteAsync(buffer.AsMemory(0, bytesRead), cancellationToken);
			}

			return destination.ToArray();
		}

		private static async Task<BitmapImage> CreateBitmapAsync(byte[] bytes, CancellationToken cancellationToken)
		{
			using var stream = new InMemoryRandomAccessStream();
			using (var writer = new DataWriter(stream))
			{
				writer.WriteBytes(bytes);
				await writer.StoreAsync().AsTask(cancellationToken);
				writer.DetachStream();
			}

			stream.Seek(0);
			var bitmap = new BitmapImage();
			await bitmap.SetSourceAsync(stream).AsTask(cancellationToken);
			return bitmap;
		}

		private sealed class ImageLoadState
		{
			private readonly FrameworkElement _target;
			private CancellationTokenSource? _cancellationTokenSource;
			private BitmapImage? _trackedBitmap;
			private string? _source;
			private long _version;

			public ImageLoadState(FrameworkElement target)
			{
				_target = target;
				_target.Loaded += OnLoaded;
				_target.Unloaded += OnUnloaded;
			}

			public void SetSource(string? source)
			{
				_source = source;
				CancelCurrentLoad();
				SetTargetSource(null);

				if (string.IsNullOrWhiteSpace(source))
				{
					SetLoadStatus(_target, GitHubImageLoadStatus.Empty);
					return;
				}

				SetLoadStatus(_target, GitHubImageLoadStatus.Loading);

				if (_target.IsLoaded)
					StartLoad();
			}

			private void OnLoaded(object sender, RoutedEventArgs args)
			{
				if (!string.IsNullOrWhiteSpace(_source))
					StartLoad();
			}

			private void OnUnloaded(object sender, RoutedEventArgs args)
				=> CancelCurrentLoad();

			private void OnBitmapOpened(object sender, RoutedEventArgs args)
			{
				if (ReferenceEquals(sender, _trackedBitmap))
				{
					SetLoadStatus(_target, GitHubImageLoadStatus.Loaded);
					StopTrackingBitmap();
				}
			}

			private void OnBitmapFailed(object sender, ExceptionRoutedEventArgs args)
			{
				if (ReferenceEquals(sender, _trackedBitmap))
				{
					SetTargetSource(null);
					SetLoadStatus(_target, GitHubImageLoadStatus.Failed);
					StopTrackingBitmap();
				}
			}

			private void StartLoad()
			{
				CancelCurrentLoad();
				var source = _source;
				if (string.IsNullOrWhiteSpace(source))
					return;

				SetTargetSource(null);
				SetLoadStatus(_target, GitHubImageLoadStatus.Loading);

				var version = ++_version;
				_cancellationTokenSource = new CancellationTokenSource();
				_ = LoadAsync(source, version, _cancellationTokenSource.Token);
			}

			private async Task LoadAsync(string source, long version, CancellationToken cancellationToken)
			{
				if (!Uri.TryCreate(source, UriKind.RelativeOrAbsolute, out var uri))
				{
					if (version == _version)
						SetLoadStatus(_target, GitHubImageLoadStatus.Failed);
					return;
				}

				if (!uri.IsAbsoluteUri || !IsGitHubHosted(uri))
				{
					if (version == _version)
						SetUriSource(uri);
					return;
				}

				try
				{
					var cache = Ioc.Default.GetService<ICacheService>();
					var bytes = cache is null
						? await DownloadAsync(uri, cancellationToken)
						: await cache.GetOrCreateBytesAsync(
							CacheKey.Shared("images", uri.AbsoluteUri),
							CachePolicies.Image,
							token => DownloadAsync(uri, token),
							cancellationToken);
					var bitmap = await CreateBitmapAsync(bytes, cancellationToken);

					if (version == _version && !cancellationToken.IsCancellationRequested)
					{
						SetTargetSource(bitmap);
						SetLoadStatus(_target, GitHubImageLoadStatus.Loaded);
					}
				}
				catch (OperationCanceledException)
				{
				}
				catch
				{
					if (version == _version)
						SetUriSource(uri);
				}
			}

			private void SetUriSource(Uri uri)
			{
				StopTrackingBitmap();
				try
				{
					var bitmap = new BitmapImage();
					bitmap.ImageOpened += OnBitmapOpened;
					bitmap.ImageFailed += OnBitmapFailed;
					_trackedBitmap = bitmap;
					SetTargetSource(bitmap);
					bitmap.UriSource = uri;
				}
				catch
				{
					StopTrackingBitmap();
					SetTargetSource(null);
					SetLoadStatus(_target, GitHubImageLoadStatus.Failed);
				}
			}

			private void SetTargetSource(BitmapImage? source)
			{
				switch (_target)
				{
					case Image image:
						image.Source = source;
						break;
					case PersonPicture personPicture:
						personPicture.ProfilePicture = source;
						break;
				}
			}

			private void StopTrackingBitmap()
			{
				if (_trackedBitmap is null)
					return;

				_trackedBitmap.ImageOpened -= OnBitmapOpened;
				_trackedBitmap.ImageFailed -= OnBitmapFailed;
				_trackedBitmap = null;
			}

			private void CancelCurrentLoad()
			{
				_version++;
				_cancellationTokenSource?.Cancel();
				_cancellationTokenSource?.Dispose();
				_cancellationTokenSource = null;
				StopTrackingBitmap();
			}
		}
	}
}
