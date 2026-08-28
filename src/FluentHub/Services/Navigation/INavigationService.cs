// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Application.Navigation;
using FluentHub.Data.Tabs;

namespace FluentHub.Services.Navigation;

public interface INavigationService
{
	ITabView TabView { get; }

	bool IsConfigured { get; }

	bool CanGoBack { get; }

	bool CanGoForward { get; }

	bool CanReload { get; }

	event EventHandler? NavigationStateChanged;

	void Configure(ITabView tabView);

	Task DisconnectAsync();

	Task NavigateAsync(AppRoute route, CancellationToken cancellationToken = default);

	Task<Guid?> OpenTabAsync(AppRoute route, CancellationToken cancellationToken = default);

	Task CloseTabAsync(Guid tabId);

	void GoToTab(Guid tabId);

	Task<bool> GoBackAsync(CancellationToken cancellationToken = default);

	Task<bool> GoForwardAsync(CancellationToken cancellationToken = default);

	Task<bool> ReloadAsync(CancellationToken cancellationToken = default);
}
