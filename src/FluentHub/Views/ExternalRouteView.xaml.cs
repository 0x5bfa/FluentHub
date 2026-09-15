// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.System;

namespace FluentHub.Views;

public sealed partial class ExternalRouteView : UserControl
{
	public ExternalRouteView(string title, string url)
	{
		Title = title ?? throw new ArgumentNullException(nameof(title));
		Url = url ?? throw new ArgumentNullException(nameof(url));
		InitializeComponent();
	}

	public string Title { get; }

	public string Url { get; }

	private async void OnOpenClick(object sender, RoutedEventArgs e)
	{
		if (Uri.TryCreate(Url, UriKind.Absolute, out var uri))
			await Launcher.LaunchUriAsync(uri);
	}
}
