// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Application.Navigation;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views;

public sealed partial class PullRequestView : UserControl
{
	public PullRequestView(
		IFluentHubGitHubClient gitHub,
		RepositoryPullRequestRoute route)
	{
		ViewModel = new PullRequestViewModel(gitHub, route);

		InitializeComponent();
		Loaded += OnPullRequestViewLoaded;
		Unloaded += OnPullRequestViewUnloaded;
	}

	public PullRequestViewModel ViewModel { get; }

	private async void OnPullRequestViewLoaded(object sender, RoutedEventArgs e)
	{
		await ViewModel.LoadAsync();
	}

	private void OnPullRequestViewUnloaded(object sender, RoutedEventArgs e)
	{
		ViewModel.CancelLoading();
	}
}
