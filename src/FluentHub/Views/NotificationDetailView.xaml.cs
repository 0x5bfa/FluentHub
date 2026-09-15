// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.System;

namespace FluentHub.Views;

public sealed partial class NotificationDetailView : UserControl
{
	public NotificationDetailView(InboxItemViewModel viewModel)
	{
		ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
		InitializeComponent();
	}

	public InboxItemViewModel ViewModel { get; }

	private async void OnOpenOnGitHubClick(object sender, RoutedEventArgs e)
	{
		if (!Uri.TryCreate(ViewModel.Url, UriKind.Absolute, out var uri) ||
			(!string.Equals(uri.Scheme, Uri.UriSchemeHttp, StringComparison.OrdinalIgnoreCase) &&
			 !string.Equals(uri.Scheme, Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase)))
		{
			return;
		}

		await Launcher.LaunchUriAsync(uri);
	}
}
