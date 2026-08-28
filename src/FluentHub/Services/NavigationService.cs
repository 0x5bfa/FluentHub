// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Utils;
using FluentHub.Views;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System.ComponentModel;

namespace FluentHub.Services
{
	public class NavigationService : ObservableObject, INavigationService
	{
		public NavigationService(ILogger? logger = null)
		{
			_logger = logger;
		}

		private readonly ILogger? _logger;

		private static readonly SuppressNavigationTransitionInfo _navigationMode = new();

		private ITabViewItem? _observedTab;

		public ITabView TabView { get; private set; } = default!;

		public bool IsConfigured { get; private set; }

		public bool CanGoBack
			=> _observedTab?.Frame.CanGoBack is true;

		public bool CanGoForward
			=> _observedTab?.Frame.CanGoForward is true;

		public bool CanReload
			=> _observedTab?.Frame.Content is LocatablePage &&
				_observedTab.NavigationHistory.CanReload;

		public event EventHandler? NavigationStateChanged;

		public void Configure(ITabView tabView)
		{
			if (IsConfigured)
				Disconnect();

			TabView = tabView;
			TabView.SelectionChanged += OnTabSelectionChanged;
			IsConfigured = true;
			ObserveTab(TabView.SelectedItem);
			_logger?.Info("NavigationService configured");
		}

		public void Navigate(Type page, object? parameter = null, NavigationTransitionInfo? transitionInfo = null)
		{
			EnsureConfigured();

			var tab = TabView.SelectedItem;

			if (tab is null)
				TabView.OpenTab(page, parameter, true);
			else
				tab.Frame.Navigate(page, parameter, transitionInfo);
		}

		public void Navigate<T>(object? parameter = null, NavigationTransitionInfo? transitionInfo = null) where T : Page
		{
			Navigate(typeof(T), parameter, transitionInfo ?? _navigationMode);
		}

		public Guid OpenTab(Type page, object? parameter)
		{
			EnsureConfigured();

			var item = TabView.OpenTab(page, parameter, true);

			return item.Guid;
		}

		public Guid OpenTab<T>(object? parameter = null) where T : Page
		{
			return OpenTab(typeof(T), parameter);
		}

		public void GoToTab(Guid tabId)
		{
			EnsureConfigured();

			var tab = TabView.TabItems.FirstOrDefault(x => x.Guid == tabId);
			if (tab != null)
				TabView.SelectedItem = tab;
		}

		public void CloseTab(Guid tabId)
		{
			EnsureConfigured();
			TabView.CloseTab(tabId);
		}

		public bool TryGoBack()
		{
			if (!CanGoBack || _observedTab is null)
				return false;

			_observedTab.Frame.GoBack();
			return true;
		}

		public bool TryGoForward()
		{
			if (!CanGoForward || _observedTab is null)
				return false;

			_observedTab.Frame.GoForward();
			return true;
		}

		public bool TryReload()
		{
			if (!CanReload || _observedTab?.Frame.Content is not LocatablePage locatablePage)
				return false;

			locatablePage.ReloadPage();
			return true;
		}

		private void OnTabSelectionChanged(object? sender, TabViewSelectionChangedEventArgs args)
			=> ObserveTab(args.NewSelectedItem);

		private void ObserveTab(ITabViewItem? tab)
		{
			if (_observedTab is not null)
			{
				_observedTab.Frame.Navigated -= OnFrameNavigationCompleted;
				_observedTab.Frame.NavigationFailed -= OnFrameNavigationFailed;
				_observedTab.Frame.NavigationStopped -= OnFrameNavigationCompleted;
				_observedTab.NavigationHistory.PropertyChanged -= OnNavigationHistoryPropertyChanged;
			}

			_observedTab = tab;

			if (_observedTab is not null)
			{
				_observedTab.Frame.Navigated += OnFrameNavigationCompleted;
				_observedTab.Frame.NavigationFailed += OnFrameNavigationFailed;
				_observedTab.Frame.NavigationStopped += OnFrameNavigationCompleted;
				_observedTab.NavigationHistory.PropertyChanged += OnNavigationHistoryPropertyChanged;
			}

			RaiseNavigationStateChanged();
		}

		private void OnFrameNavigationCompleted(object sender, NavigationEventArgs args)
			=> RaiseNavigationStateChanged();

		private void OnFrameNavigationFailed(object sender, NavigationFailedEventArgs args)
			=> RaiseNavigationStateChanged();

		private void OnNavigationHistoryPropertyChanged(object? sender, PropertyChangedEventArgs args)
		{
			if (args.PropertyName == nameof(NavigationHistory.CanReload))
				RaiseNavigationStateChanged();
		}

		private void RaiseNavigationStateChanged()
		{
			OnPropertyChanged(nameof(CanGoBack));
			OnPropertyChanged(nameof(CanGoForward));
			OnPropertyChanged(nameof(CanReload));
			NavigationStateChanged?.Invoke(this, System.EventArgs.Empty);
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
			if (!IsConfigured)
				return;

			TabView.SelectionChanged -= OnTabSelectionChanged;
			ObserveTab(null);
			TabView = default!;
			IsConfigured = false;
			_logger?.Info("NavigationService disconnected");
		}
	}
}
