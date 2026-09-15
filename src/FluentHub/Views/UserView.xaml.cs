// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Application.Navigation;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.System;

namespace FluentHub.Views;

public sealed partial class UserView : UserControl
{
	private readonly Action<UIElement, AppRoute> _navigate;

	public UserView(
		IFluentHubGitHubClient gitHub,
		UserRoute route,
		Action<UIElement, AppRoute> navigate)
	{
		ViewModel = new UserViewModel(gitHub, route);
		_navigate = navigate ?? throw new ArgumentNullException(nameof(navigate));

		InitializeComponent();
		Loaded += OnUserViewLoaded;
		Unloaded += OnUserViewUnloaded;
	}

	public UserViewModel ViewModel { get; }

	private async void OnUserViewLoaded(object sender, RoutedEventArgs e)
		=> await ViewModel.LoadAsync();

	private void OnUserViewUnloaded(object sender, RoutedEventArgs e)
		=> ViewModel.CancelLoading();

	private void OnRepositoriesClick(object sender, RoutedEventArgs e)
		=> _navigate(this, new UserRoute(ViewModel.Login, UserSection.Repositories));

	private void OnIssuesClick(object sender, RoutedEventArgs e)
		=> _navigate(this, new UserRoute(ViewModel.Login, UserSection.Issues));

	private void OnPullRequestsClick(object sender, RoutedEventArgs e)
		=> _navigate(this, new UserRoute(ViewModel.Login, UserSection.PullRequests));

	private void OnDiscussionsClick(object sender, RoutedEventArgs e)
		=> _navigate(this, new UserRoute(ViewModel.Login, UserSection.Discussions));

	private async void OnWebsiteClick(object sender, RoutedEventArgs e)
	{
		if (Uri.TryCreate(ViewModel.Website, UriKind.Absolute, out var uri))
			await Launcher.LaunchUriAsync(uri);
	}
}
