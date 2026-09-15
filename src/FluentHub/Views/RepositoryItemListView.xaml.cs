// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Application.Navigation;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views;

public sealed partial class RepositoryItemListView : UserControl
{
	private readonly Action<UIElement, AppRoute> _navigate;

	public RepositoryItemListView(
		IFluentHubGitHubClient gitHub,
		AppRoute route,
		RepositoryItemListKind kind,
		Action<UIElement, AppRoute> navigate)
	{
		ViewModel = new RepositoryItemListViewModel(gitHub, route, kind);
		_navigate = navigate ?? throw new ArgumentNullException(nameof(navigate));

		InitializeComponent();
		Loaded += OnRepositoryItemListViewLoaded;
		Unloaded += OnRepositoryItemListViewUnloaded;
	}

	public RepositoryItemListViewModel ViewModel { get; }

	private async void OnRepositoryItemListViewLoaded(object sender, RoutedEventArgs e)
		=> await ViewModel.LoadAsync();

	private void OnRepositoryItemListViewUnloaded(object sender, RoutedEventArgs e)
		=> ViewModel.CancelLoading();

	private void OnItemClick(object sender, ItemClickEventArgs e)
	{
		if (e.ClickedItem is RouteListItemViewModel item)
			_navigate(this, ViewModel.CreateRoute(item));
	}
}
