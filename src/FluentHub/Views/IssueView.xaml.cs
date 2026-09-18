// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Application.Navigation;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views;

public sealed partial class IssueView : UserControl
{
	private readonly Action<UIElement, AppRoute> _navigate;

	public IssueView(
		IFluentHubGitHubClient gitHub,
		RepositoryIssueRoute route,
		Action<UIElement, AppRoute> navigate)
	{
		ViewModel = new IssueViewModel(gitHub, route);
		_navigate = navigate ?? throw new ArgumentNullException(nameof(navigate));

		InitializeComponent();
		Loaded += OnIssueViewLoaded;
		Unloaded += OnIssueViewUnloaded;
	}

	public IssueViewModel ViewModel { get; }

	private async void OnIssueViewLoaded(object sender, RoutedEventArgs e)
	{
		await ViewModel.LoadAsync();
	}

	private void OnIssueViewUnloaded(object sender, RoutedEventArgs e)
	{
		ViewModel.CancelLoading();
	}

	private void OnAuthorClick(object sender, RoutedEventArgs e)
	{
		if (ViewModel.HasAuthor)
		{
			_navigate(this, new UserRoute(ViewModel.AuthorLogin));
		}
	}

	private void OnRepositoryClick(object sender, RoutedEventArgs e)
	{
		_navigate(this, ViewModel.RepositoryRoute);
	}
}
