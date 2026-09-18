// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.Collections.Generic;
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
		private bool _isSignInDialogOpen;
		private NavigationViewItemSeparator? _repositorySeparator;
		private NavigationViewItemHeader? _repositoryHeader;
		private readonly List<NavigationViewItem> _repositoryNavigationItems = [];
		private InboxView? _currentInboxView;
		private NavigationViewItem? _lastNavigationItem;
		private readonly AppRouteViewFactory _routeViewFactory;

		public RootView()
		{
			var app = App.Current;
			ViewModel = new RootViewModel(app.GitHub, app.Session, app.Settings);
			_routeViewFactory = new AppRouteViewFactory(app.GitHub);

			InitializeComponent();
			UpdateProfileButtonVisual();
			Loaded += OnRootViewLoaded;
			Unloaded += OnRootViewUnloaded;
		}

		public RootViewModel ViewModel { get; }

		private async void OnRootViewLoaded(object sender, RoutedEventArgs e)
		{
			MainWindow.Instance.SetTitleBar(MainTitleBar);
			if (NavigationView.SelectedItem is null && NavigationView.MenuItems.Count > 0)
				NavigationView.SelectedItem = NavigationView.MenuItems[0];
			else if (NavigationView.SelectedItem is NavigationViewItem selectedItem && BladeView.Blades.Count == 0)
				ShowNavigationItem(selectedItem);

			await Task.WhenAll(
				AddRepositoryNavigationItemsAsync(),
				ViewModel.LoadProfileAsync());
		}

		private void OnRootViewUnloaded(object sender, RoutedEventArgs e)
			=> ViewModel.CancelLoading();

		private void OnNavigationSelectionChanged(
			NavigationView sender,
			NavigationViewSelectionChangedEventArgs args)
		{
			if (args.SelectedItem is not NavigationViewItem item)
				return;

			_lastNavigationItem = item;
			ShowNavigationItem(item);
		}

		private void OnTitleBarPaneToggleRequested(TitleBar sender, object args)
			=> NavigationView.IsPaneOpen = !NavigationView.IsPaneOpen;

		private void OnNavigationViewDisplayModeChanged(
			NavigationView sender,
			NavigationViewDisplayModeChangedEventArgs args)
			=> MainTitleBar.IsPaneToggleButtonVisible = sender.PaneDisplayMode != NavigationViewPaneDisplayMode.Top;

		private void OnProfileFlyoutOpening(object sender, object args)
		{
			var isAuthenticated = ViewModel.IsAuthenticated;
			UpdateProfileButtonVisual();
			SignInMenuFlyoutItem.Visibility = isAuthenticated ? Visibility.Collapsed : Visibility.Visible;
			ProfileMenuFlyoutItem.Visibility = isAuthenticated ? Visibility.Visible : Visibility.Collapsed;
			ProfileFlyoutSeparator.Visibility = Visibility.Visible;
			AppSettingsMenuFlyoutItem.Visibility = Visibility.Visible;
		}

		private async void OnProfileActionClick(object sender, RoutedEventArgs e)
		{
			ProfileButton.Flyout?.Hide();

			try
			{
				if (!ViewModel.IsAuthenticated)
				{
					await ShowSignInDialogAsync();
					return;
				}

				await ViewModel.SignOutAsync();
				UpdateProfileButtonVisual();
				RemoveRepositoryNavigationItems();

				if (NavigationView.MenuItems.Count > 0 &&
					NavigationView.MenuItems[0] is NavigationViewItem firstItem)
				{
					NavigationView.SelectedItem = firstItem;
					ShowNavigationItem(firstItem);
				}
				else
				{
					ShowContent(CreateCenteredText(Strings.Root_AuthenticationRequiredMessage.GetLocalized()));
				}
			}
			catch (OperationCanceledException)
			{
				// The account action was cancelled while the view was unloading.
			}
			catch (Exception)
			{
				// Keep the current session visible if the settings file could not be cleared.
			}
		}

		private void OnAppSettingsClick(object sender, RoutedEventArgs e)
			=> ShowContent(CreateCenteredText(Strings.Root_SettingsText.GetLocalized()));

		private void ShowNavigationItem(NavigationViewItem item)
		{
			var tag = item.Tag?.ToString();
			if (!ViewModel.IsAuthenticated && RequiresAuthentication(tag))
			{
				ShowContent(CreateCenteredText(Strings.Root_AuthenticationRequiredMessage.GetLocalized()));
				return;
			}

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

			ShowContent(content);
		}

		private void ShowContent(UIElement content)
		{
			if (_currentInboxView is not null)
			{
				_currentInboxView.NotificationSelected -= OnNotificationSelected;
				_currentInboxView = null;
			}

			if (content is InboxView inboxView)
			{
				_currentInboxView = inboxView;
				_currentInboxView.NotificationSelected += OnNotificationSelected;
			}

			BladeView.Replace(content);
		}

		private async Task AddRepositoryNavigationItemsAsync()
		{
			if (_repositoryItemsAdded || !ViewModel.IsAuthenticated)
				return;

			try
			{
				await ViewModel.LoadRepositoriesAsync();

				if (ViewModel.Repositories.Count > 0)
				{
					_repositorySeparator = new NavigationViewItemSeparator();
					_repositoryHeader = new NavigationViewItemHeader
					{
						Content = Strings.Root_RepositoriesHeader.GetLocalized(),
					};
					NavigationView.MenuItems.Add(_repositorySeparator);
					NavigationView.MenuItems.Add(_repositoryHeader);

					foreach (var repository in ViewModel.Repositories)
					{
						var item = new NavigationViewItem
						{
							Content = repository.Name,
							Tag = repository.FullName,
							Icon = Octicons.CreateIcon(repository.IsPrivate ? OcticonName.Lock16 : OcticonName.Repo16),
						};
						_repositoryNavigationItems.Add(item);
						NavigationView.MenuItems.Add(item);
					}
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

		private void RemoveRepositoryNavigationItems()
		{
			if (_lastNavigationItem is not null && _repositoryNavigationItems.Contains(_lastNavigationItem))
				_lastNavigationItem = null;

			foreach (var item in _repositoryNavigationItems)
				NavigationView.MenuItems.Remove(item);

			if (_repositoryHeader is not null)
				NavigationView.MenuItems.Remove(_repositoryHeader);

			if (_repositorySeparator is not null)
				NavigationView.MenuItems.Remove(_repositorySeparator);

			_repositoryNavigationItems.Clear();
			_repositoryHeader = null;
			_repositorySeparator = null;
			_repositoryItemsAdded = false;
		}

		private async Task ShowSignInDialogAsync()
		{
			if (_isSignInDialogOpen || XamlRoot is null)
				return;

			_isSignInDialogOpen = true;
			var loginView = new LoginView();
			var dialog = new ContentDialog
			{
				Title = Strings.Root_SignInDialogTitle.GetLocalized(),
				Content = loginView,
				CloseButtonText = Strings.Root_CancelButtonText.GetLocalized(),
				DefaultButton = ContentDialogButton.Close,
				XamlRoot = XamlRoot,
			};

			void OnSignInCompleted(object? sender, EventArgs args)
				=> dialog.Hide();

			loginView.SignInCompleted += OnSignInCompleted;
			try
			{
				await dialog.ShowAsync();

				if (ViewModel.IsAuthenticated)
				{
					ViewModel.RefreshAuthenticationState();
					UpdateProfileButtonVisual();
					await Task.WhenAll(
						AddRepositoryNavigationItemsAsync(),
						ViewModel.LoadProfileAsync());
				}
			}
			finally
			{
				loginView.SignInCompleted -= OnSignInCompleted;
				_isSignInDialogOpen = false;
				RestoreNavigationSelection();
			}
		}

		private void RestoreNavigationSelection()
		{
			var item = _lastNavigationItem;
			if ((item is null || !NavigationView.MenuItems.Contains(item)) && NavigationView.MenuItems.Count > 0)
				item = NavigationView.MenuItems[0] as NavigationViewItem;

			if (item is null)
				return;

			if (!ReferenceEquals(NavigationView.SelectedItem, item))
				NavigationView.SelectedItem = item;
			else
				ShowNavigationItem(item);
		}

		private static bool RequiresAuthentication(string? tag)
			=> tag is "Inbox" or "MyPulls" or "Reviews" or "Assigned";

		private void UpdateProfileButtonVisual()
		{
			var isAuthenticated = ViewModel.IsAuthenticated;
			SignedOutProfileIcon.Visibility = isAuthenticated ? Visibility.Collapsed : Visibility.Visible;
			ProfileAvatar.Visibility = isAuthenticated ? Visibility.Visible : Visibility.Collapsed;
		}

		private static TextBlock CreateCenteredText(string text)
			=> new()
			{
				Text = text,
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center,
			};

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
