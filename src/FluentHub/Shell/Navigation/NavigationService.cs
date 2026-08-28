// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Application.Navigation;
using FluentHub.Shell.Tabs;
using FluentHub.Utils;

namespace FluentHub.Shell.Navigation;

public sealed class NavigationService(
	IScreenFactory screenFactory,
	ILogger? logger = null) : ObservableObject, INavigationService
{
	private ITabViewItem? _observedTab;

	public ITabView TabView { get; private set; } = default!;

	public bool IsConfigured { get; private set; }

	public bool CanGoBack
		=> _observedTab?.Journal.CanGoBack is true;

	public bool CanGoForward
		=> _observedTab?.Journal.CanGoForward is true;

	public bool CanReload
		=> _observedTab?.CurrentScreen is not null;

	public event EventHandler? NavigationStateChanged;

	public void Configure(ITabView tabView)
	{
		ArgumentNullException.ThrowIfNull(tabView);

		if (IsConfigured)
			throw new InvalidOperationException("The navigation service is already configured.");

		TabView = tabView;
		TabView.SelectionChanged += OnTabSelectionChanged;
		IsConfigured = true;
		ObserveTab(TabView.SelectedItem);
		logger?.Info("NavigationService configured");
	}

	public async Task DisconnectAsync()
	{
		if (!IsConfigured)
			return;

		TabView.SelectionChanged -= OnTabSelectionChanged;
		ObserveTab(null);

		foreach (var tab in TabView.TabItems)
		{
			tab.BeginNavigation(CancellationToken.None);
			await tab.NavigationLock.WaitAsync();
			try
			{
				if (tab.CurrentScreen is { } screen)
				{
					tab.SetCurrentScreen(null);
					await screen.DisposeAsync();
				}
			}
			finally
			{
				tab.NavigationLock.Release();
			}
		}

		TabView = default!;
		IsConfigured = false;
		logger?.Info("NavigationService disconnected");
	}

	public async Task NavigateAsync(AppRoute route, CancellationToken cancellationToken = default)
	{
		EnsureConfigured();
		var tab = TabView.SelectedItem ?? TabView.CreateTab();

		try
		{
			await NavigateNewAsync(tab, route, cancellationToken);
		}
		catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
		{
			// A newer navigation superseded this request.
		}
		catch (OperationCanceledException)
		{
			throw;
		}
		catch
		{
			// ReplaceScreenAsync logged and surfaced the activation failure on the tab.
		}
	}

	public async Task<Guid?> OpenTabAsync(AppRoute route, CancellationToken cancellationToken = default)
	{
		EnsureConfigured();
		var tab = TabView.CreateTab();

		try
		{
			await NavigateNewAsync(tab, route, cancellationToken);
			return tab.Id;
		}
		catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
		{
			TabView.RemoveTab(tab);
			return null;
		}
		catch (OperationCanceledException)
		{
			TabView.RemoveTab(tab);
			throw;
		}
		catch
		{
			TabView.RemoveTab(tab);
			return null;
		}
	}

	public async Task CloseTabAsync(Guid tabId)
	{
		EnsureConfigured();
		var tab = TabView.TabItems.FirstOrDefault(item => item.Id == tabId);
		if (tab is null)
			return;

		tab.BeginNavigation(CancellationToken.None);
		await tab.NavigationLock.WaitAsync();
		try
		{
			if (tab.CurrentScreen is { } screen)
			{
				tab.SetCurrentScreen(null);
				await screen.DisposeAsync();
			}

			TabView.RemoveTab(tab);
		}
		finally
		{
			tab.NavigationLock.Release();
		}

		RaiseNavigationStateChanged();
	}

	public void GoToTab(Guid tabId)
	{
		EnsureConfigured();
		var tab = TabView.TabItems.FirstOrDefault(item => item.Id == tabId);
		if (tab is not null)
			TabView.SelectedItem = tab;
	}

	public Task<bool> GoBackAsync(CancellationToken cancellationToken = default)
		=> NavigateJournalAsync(forward: false, cancellationToken);

	public Task<bool> GoForwardAsync(CancellationToken cancellationToken = default)
		=> NavigateJournalAsync(forward: true, cancellationToken);

	public async Task<bool> ReloadAsync(CancellationToken cancellationToken = default)
	{
		if (!CanReload || _observedTab?.CurrentScreen is not { } screen)
			return false;

		try
		{
			_observedTab.Chrome.ClearError();
			using var linkedCancellation = CancellationTokenSource.CreateLinkedTokenSource(
				cancellationToken,
				screen.LifetimeToken);
			await screen.Screen.ReloadAsync(linkedCancellation.Token);
			return true;
		}
		catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
		{
			return false;
		}
		catch (OperationCanceledException)
		{
			throw;
		}
		catch (Exception ex)
		{
			_observedTab.Chrome.ShowError("The current screen could not be refreshed. Try again later.");
			logger?.Error($"Failed to reload route {screen.Route.GetType().Name}.", ex);
			return false;
		}
	}

	private async Task<bool> NavigateJournalAsync(bool forward, CancellationToken cancellationToken)
	{
		try
		{
			EnsureConfigured();
			var tab = _observedTab;
			if (tab is null)
				return false;

			var navigationToken = tab.BeginNavigation(cancellationToken);
			await tab.NavigationLock.WaitAsync(navigationToken);
			try
			{
				var snapshot = tab.Journal.CaptureSnapshot();
				var moved = forward
					? tab.Journal.TryGoForward(out var route)
					: tab.Journal.TryGoBack(out route);

				if (!moved || route is null)
					return false;

				await ReplaceScreenAsync(tab, route, snapshot, navigationToken);
				return true;
			}
			finally
			{
				tab.NavigationLock.Release();
			}
		}
		catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
		{
			return false;
		}
		catch (OperationCanceledException)
		{
			throw;
		}
		catch
		{
			return false;
		}
	}

	private async Task NavigateNewAsync(ITabViewItem tab, AppRoute route, CancellationToken cancellationToken)
	{
		var navigationToken = tab.BeginNavigation(cancellationToken);
		await tab.NavigationLock.WaitAsync(navigationToken);
		try
		{
			var snapshot = tab.Journal.CaptureSnapshot();
			tab.Journal.Navigate(route);
			await ReplaceScreenAsync(tab, route, snapshot, navigationToken);
		}
		finally
		{
			tab.NavigationLock.Release();
		}
	}

	private async Task ReplaceScreenAsync(
		ITabViewItem tab,
		AppRoute route,
		NavigationJournal<AppRoute>.NavigationJournalSnapshot snapshot,
		CancellationToken cancellationToken)
	{
		var previousScreen = tab.CurrentScreen;
		ScreenInstance? nextScreen = null;
		tab.NavigationBar.ApplyRoute(route);
		tab.Chrome.ClearError();

		try
		{
			nextScreen = await screenFactory.CreateAsync(route, cancellationToken);
			cancellationToken.ThrowIfCancellationRequested();
		}
		catch (Exception ex)
		{
			if (nextScreen is not null)
			{
				try
				{
					await nextScreen.DisposeAsync();
				}
				catch (Exception cleanupException)
				{
					logger?.Error($"Failed to dispose route {route.GetType().Name} after activation failed.", cleanupException);
				}
			}

			tab.Journal.RestoreSnapshot(snapshot);
			if (tab.Journal.Current is { } previousRoute)
				tab.NavigationBar.ApplyRoute(previousRoute);

			if (ex is not OperationCanceledException)
			{
				tab.Chrome.ShowError("The requested screen could not be opened. Try again or return to the previous screen.");
				logger?.Error($"Failed to activate route {route.GetType().Name}.", ex);
			}

			RaiseNavigationStateChanged();
			throw;
		}

		tab.SetCurrentScreen(nextScreen);

		if (previousScreen is not null)
		{
			try
			{
				await previousScreen.DisposeAsync();
			}
			catch (Exception ex)
			{
				logger?.Error($"Failed to dispose route {previousScreen.Route.GetType().Name}.", ex);
			}
		}

		RaiseNavigationStateChanged();
	}

	private void OnTabSelectionChanged(object? sender, TabViewSelectionChangedEventArgs args)
		=> ObserveTab(args.NewSelectedItem);

	private void ObserveTab(ITabViewItem? tab)
	{
		_observedTab = tab;
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
		if (IsConfigured)
			return;

		const string message = "The navigation service has not been configured.";
		logger?.Error(message);
		throw new InvalidOperationException(message);
	}
}
