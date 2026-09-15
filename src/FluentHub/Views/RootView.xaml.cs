// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Controls;
using FluentHub.Core.Application.Navigation;
using FluentHub.Services;
using FluentHub.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views
{
	public sealed partial class RootView : UserControl
	{
		private bool _repositoryItemsAdded;
		private InboxView? _currentInboxView;
		private readonly AppRouteViewFactory _routeViewFactory;

		public RootView()
		{
			var app = App.Current;
			ViewModel = new RootViewModel(app.GitHub, app.Session, app.Settings);
			_routeViewFactory = new AppRouteViewFactory(app.GitHub);

			InitializeComponent();
			Loaded += OnRootViewLoaded;
			Unloaded += OnRootViewUnloaded;
		}

		public RootViewModel ViewModel { get; }

		private async void OnRootViewLoaded(object sender, RoutedEventArgs e)
		{
			MainWindow.Instance.SetTitleBar(TitleBar);
			if (NavigationView.SelectedItem is null && NavigationView.MenuItems.Count > 0)
				NavigationView.SelectedItem = NavigationView.MenuItems[0];
			else if (NavigationView.SelectedItem is NavigationViewItem selectedItem && BladeView.Blades.Count == 0)
				ShowNavigationItem(selectedItem);

			if (_repositoryItemsAdded)
				return;

			try
			{
				await ViewModel.LoadRepositoriesAsync();

				foreach (var repository in ViewModel.Repositories)
				{
					NavigationView.MenuItems.Add(new NavigationViewItem
					{
						Content = repository.Name,
						Tag = repository.FullName,
						Icon = Octicons.CreateIcon(repository.IsPrivate ? OcticonName.Lock16 : OcticonName.Repo16),
					});
				}

				_repositoryItemsAdded = true;
			}
			catch (OperationCanceledException)
			{
				// The view was unloaded while the request was in flight.
			}
			catch (Exception)
			{
				// Keep the navigation shell usable when GitHub is unavailable.
			}
		}

		private void OnRootViewUnloaded(object sender, RoutedEventArgs e)
			=> ViewModel.CancelLoading();

		private void OnNavigationSelectionChanged(
			NavigationView sender,
			NavigationViewSelectionChangedEventArgs args)
		{
			if (args.SelectedItem is not NavigationViewItem item)
				return;

			ShowNavigationItem(item);
		}

		private void ShowNavigationItem(NavigationViewItem item)
		{

			if (_currentInboxView is not null)
			{
				_currentInboxView.NotificationSelected -= OnNotificationSelected;
				_currentInboxView = null;
			}

			var tag = item.Tag?.ToString();
			UIElement content = tag switch
			{
				"Inbox" => new InboxView(),
				_ when TryParseRepositorySlug(tag, out var repository)
					=> _routeViewFactory.Create(
						new RepositoryRoute(repository, RepositorySection.Overview),
						Navigate),
				_ => new TextBlock
				{
					Text = tag ?? item.Content?.ToString(),
					HorizontalAlignment = HorizontalAlignment.Center,
					VerticalAlignment = VerticalAlignment.Center,
				},
			};

			if (content is InboxView inboxView)
			{
				_currentInboxView = inboxView;
				_currentInboxView.NotificationSelected += OnNotificationSelected;
			}

			BladeView.Replace(content);
		}

		private void OnNotificationSelected(InboxView source, InboxItemViewModel item)
		{
			if (item.Route is { } route)
				Navigate(source, route);
		}

		private void Navigate(UIElement source, AppRoute route)
			=> BladeView.Push(source, _routeViewFactory.Create(route, Navigate));

		private static bool TryParseRepositorySlug(string? value, out RepositorySlug repository)
		{
			var parts = value?.Split('/', StringSplitOptions.RemoveEmptyEntries);
			if (parts is { Length: 2 } &&
				!string.IsNullOrWhiteSpace(parts[0]) &&
				!string.IsNullOrWhiteSpace(parts[1]))
			{
				repository = new RepositorySlug(parts[0], parts[1]);
				return true;
			}

			repository = default;
			return false;
		}
	}
}
