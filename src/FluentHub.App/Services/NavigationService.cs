// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.Utils;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;

namespace FluentHub.App.Services
{
	public class NavigationService : ObservableObject, INavigationService
	{
		public NavigationService(ILogger logger = null)
		{
			_logger = logger;
		}

		private readonly ILogger _logger;

		private static readonly EntranceNavigationTransitionInfo _navigationMode = new();

		public Type CurrentPage { get; set; }

		public ITabView TabView { get; private set; }

		public bool IsConfigured { get; private set; }

		public void Configure(ITabView tabView)
		{
			TabView = tabView;
			IsConfigured = true;
			_logger?.Info("NavigationService configured");
		}

		public void Navigate(Type page, object parameter = null, NavigationTransitionInfo transitionInfo = null)
		{
			EnsureConfigured();

			var tab = TabView.SelectedItem;

			if (tab is null)
				TabView.OpenTab(page, parameter, true);
			else
				tab.Frame.Navigate(page, parameter, transitionInfo);

			CurrentPage = page;
		}

		public void Navigate<T>(object parameter = null, NavigationTransitionInfo transitionInfo = null) where T : Page
			=> Navigate(typeof(T), parameter, transitionInfo ?? _navigationMode);

		public Guid OpenTab(Type page, object parameter)
		{
			EnsureConfigured();

			var item = TabView.OpenTab(page, parameter, true);
			return item.Guid;
		}

		public Guid OpenTab<T>(object parameter = null) where T : Page
			=> OpenTab(typeof(T), parameter);

		public void GoToTab(Guid tabId)
		{
			var tab = TabView.TabItems.FirstOrDefault(x => x.Guid == tabId);
			if (tab != null)
			{
				TabView.SelectedItem = tab;
			}
		}

		public void CloseTab(Guid tabId)
			=> TabView.CloseTab(tabId);

		public void GoBack()
		{
			EnsureConfigured();

			var tab = TabView.SelectedItem ?? throw new InvalidOperationException("No tab selected");

			tab.Frame.GoBack();
		}

		public void GoForward()
		{
			EnsureConfigured();

			var tab = TabView.SelectedItem ?? throw new InvalidOperationException("No tab selected");

			tab.Frame.GoForward();
		}

		public bool CanGoBack()
		{
			EnsureConfigured();

			return TabView.SelectedItem?.Frame?.CanGoBack == true;
		}

		public bool CanGoForward()
		{
			EnsureConfigured();

			return TabView.SelectedItem?.Frame?.CanGoForward == true;
		}

		public NavigationHistoryItem GetCurrentTabItem()
		{
			return TabView.SelectedItem.NavigationHistory.CurrentItem;
		}

		public void SetCurrentTabItem(NavigationHistoryItem newEntry)
		{
			var currentItem = TabView.SelectedItem.NavigationHistory.CurrentItem;

			currentItem.Header = newEntry.Header ?? string.Empty;
			currentItem.Description = newEntry.Description ?? string.Empty;

			if (newEntry.Icon is not null)
				currentItem.Icon = newEntry.Icon;
		}

		public void SaveContextToCurrentTabItem(FrameNavigationParameter context)
		{
			TabView.SelectedItem.NavigationHistory.CurrentItem.Context = context;
		}

		private void EnsureConfigured()
		{
			if (!IsConfigured)
			{
				var message = "The Navigation Service has not been configured. Call INavigationService.Configure first";
				_logger?.Error(message);

				throw new InvalidOperationException(message);
			}
		}

		public void Disconnect()
		{
			TabView = null;
			IsConfigured = false;
			_logger?.Info("NavigationService disconnected");
		}
	}
}
