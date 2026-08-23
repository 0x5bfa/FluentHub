// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Caching;
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
	public static class GitHubImageCache
	{
		private const int MaximumImageSize = 20 * 1024 * 1024;

		private static readonly ConditionalWeakTable<Image, ImageLoadState> ImageStates = new();
		private static readonly HttpClient HttpClient = CreateHttpClient();

		public static readonly DependencyProperty SourceProperty = DependencyProperty.RegisterAttached(
			"Source",
			typeof(string),
			typeof(GitHubImageCache),
			new PropertyMetadata(null, OnSourceChanged));

		public static string? GetSource(DependencyObject dependencyObject)
			=> (string?)dependencyObject.GetValue(SourceProperty);

		public static void SetSource(DependencyObject dependencyObject, string? value)
			=> dependencyObject.SetValue(SourceProperty, value);

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
			if (dependencyObject is not Image image)
				throw new ArgumentException("GitHubImageCache.Source can only be used on Image controls.");

			ImageStates.GetValue(image, static value => new ImageLoadState(value))
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
			private readonly Image _image;
			private CancellationTokenSource? _cancellationTokenSource;
			private string? _source;
			private long _version;

			public ImageLoadState(Image image)
			{
				_image = image;
				_image.Loaded += OnLoaded;
				_image.Unloaded += OnUnloaded;
			}

			public void SetSource(string? source)
			{
				_source = source;
				CancelCurrentLoad();

				if (string.IsNullOrWhiteSpace(source))
				{
					_image.Source = null;
					return;
				}

				if (_image.IsLoaded)
					StartLoad();
			}

			private void OnLoaded(object sender, RoutedEventArgs args)
			{
				if (!string.IsNullOrWhiteSpace(_source))
					StartLoad();
			}

			private void OnUnloaded(object sender, RoutedEventArgs args)
				=> CancelCurrentLoad();

			private void StartLoad()
			{
				CancelCurrentLoad();
				var source = _source;
				if (string.IsNullOrWhiteSpace(source))
					return;

				var version = ++_version;
				_cancellationTokenSource = new CancellationTokenSource();
				_ = LoadAsync(source, version, _cancellationTokenSource.Token);
			}

			private async Task LoadAsync(string source, long version, CancellationToken cancellationToken)
			{
				if (!Uri.TryCreate(source, UriKind.Absolute, out var uri))
				{
					if (version == _version)
						_image.Source = new BitmapImage(new Uri(source, UriKind.RelativeOrAbsolute));
					return;
				}

				if (!IsGitHubHosted(uri))
				{
					if (version == _version)
						_image.Source = new BitmapImage(uri);
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
						_image.Source = bitmap;
				}
				catch (OperationCanceledException)
				{
				}
				catch
				{
					if (version == _version)
						_image.Source = new BitmapImage(uri);
				}
			}

			private void CancelCurrentLoad()
			{
				_version++;
				_cancellationTokenSource?.Cancel();
				_cancellationTokenSource?.Dispose();
				_cancellationTokenSource = null;
			}
		}
	}
}
