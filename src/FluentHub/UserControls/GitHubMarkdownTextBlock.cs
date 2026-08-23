// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using CommunityToolkit.WinUI.Controls;
using FluentHub.Helpers;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.UserControls
{
	public sealed partial class GitHubMarkdownTextBlock : MarkdownTextBlock
	{
		public static readonly DependencyProperty BaseUrlProperty =
			DependencyProperty.Register(
				nameof(BaseUrl),
				typeof(string),
				typeof(GitHubMarkdownTextBlock),
				new PropertyMetadata(null, OnBaseUrlChanged));

		private static readonly IImageProvider ImageProvider = new CachedGitHubImageProvider();

		public string? BaseUrl
		{
			get => (string?)GetValue(BaseUrlProperty);
			set => SetValue(BaseUrlProperty, value);
		}

		public GitHubMarkdownTextBlock()
		{
			IsTextSelectionEnabled = true;
			UseAutoLinks = true;
			UseEmphasisExtras = true;
			UseListExtras = true;
			UsePipeTables = true;
			UseTaskLists = true;
			Config = CreateConfig(null);
		}

		private static void OnBaseUrlChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
		{
			if (sender is GitHubMarkdownTextBlock markdownTextBlock)
				markdownTextBlock.Config = CreateConfig(args.NewValue as string);
		}

		private static MarkdownConfig CreateConfig(string? baseUrl)
			=> new()
			{
				BaseUrl = baseUrl,
				ImageProvider = ImageProvider,
			};

		private sealed class CachedGitHubImageProvider : IImageProvider
		{
			public Task<Image> GetImage(string url)
			{
				var image = new Image();
				GitHubImageCache.SetSource(image, url);
				return Task.FromResult(image);
			}

			public bool ShouldUseThisProvider(string url)
				=> Uri.TryCreate(url, UriKind.Absolute, out var uri) && GitHubImageCache.IsGitHubHosted(uri);
		}
	}
}
