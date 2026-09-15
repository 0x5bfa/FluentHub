// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Application.Navigation;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views;

public sealed partial class RepositoryListView : UserControl
{
	private readonly Action<UIElement, AppRoute> _navigate;

	public RepositoryListView(
		IFluentHubGitHubClient gitHub,
		string ownerLogin,
		RepositoryListOwnerKind ownerKind,
		Action<UIElement, AppRoute> navigate)
	{
		ViewModel = new RepositoryListViewModel(gitHub, ownerLogin, ownerKind);
		_navigate = navigate ?? throw new ArgumentNullException(nameof(navigate));

		InitializeComponent();
		Loaded += OnRepositoryListViewLoaded;
		Unloaded += OnRepositoryListViewUnloaded;
	}

	public RepositoryListViewModel ViewModel { get; }

	private async void OnRepositoryListViewLoaded(object sender, RoutedEventArgs e)
		=> await ViewModel.LoadAsync();

	private void OnRepositoryListViewUnloaded(object sender, RoutedEventArgs e)
		=> ViewModel.CancelLoading();

	private void OnItemClick(object sender, ItemClickEventArgs e)
	{
		if (e.ClickedItem is RepositoryListItemViewModel item)
			_navigate(this, new RepositoryRoute(item.Repository, RepositorySection.Overview));
	}
}
