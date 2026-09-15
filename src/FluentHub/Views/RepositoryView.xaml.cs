// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Application.Navigation;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views;

public sealed partial class RepositoryView : UserControl
{
	private readonly Action<UIElement, AppRoute> _navigate;

	public RepositoryView(
		IFluentHubGitHubClient gitHub,
		RepositoryRoute route,
		Action<UIElement, AppRoute> navigate)
	{
		ViewModel = new RepositoryViewModel(gitHub, route);
		_navigate = navigate ?? throw new ArgumentNullException(nameof(navigate));

		InitializeComponent();
		Loaded += OnRepositoryViewLoaded;
		Unloaded += OnRepositoryViewUnloaded;
	}

	public RepositoryViewModel ViewModel { get; }

	private async void OnRepositoryViewLoaded(object sender, RoutedEventArgs e)
		=> await ViewModel.LoadAsync();

	private void OnRepositoryViewUnloaded(object sender, RoutedEventArgs e)
		=> ViewModel.CancelLoading();

	private void OnIssuesClick(object sender, RoutedEventArgs e)
		=> _navigate(this, new RepositoryRoute(ViewModel.Repository, RepositorySection.Issues));

	private void OnPullRequestsClick(object sender, RoutedEventArgs e)
		=> _navigate(this, new RepositoryRoute(ViewModel.Repository, RepositorySection.PullRequests));

	private void OnDiscussionsClick(object sender, RoutedEventArgs e)
		=> _navigate(this, new RepositoryRoute(ViewModel.Repository, RepositorySection.Discussions));

	private void OnActionsClick(object sender, RoutedEventArgs e)
		=> _navigate(this, new RepositoryRoute(ViewModel.Repository, RepositorySection.Actions));
}
