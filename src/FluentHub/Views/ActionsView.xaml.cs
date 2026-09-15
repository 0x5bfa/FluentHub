// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.System;

namespace FluentHub.Views;

public sealed partial class ActionsView : UserControl
{
	public ActionsView(ActionsViewModel viewModel)
	{
		ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
		InitializeComponent();
	}

	public ActionsViewModel ViewModel { get; }

	private async void OnOpenActionsClick(object sender, RoutedEventArgs e)
	{
		if (Uri.TryCreate(ViewModel.ActionsUrl, UriKind.Absolute, out var uri))
			await Launcher.LaunchUriAsync(uri);
	}
}
