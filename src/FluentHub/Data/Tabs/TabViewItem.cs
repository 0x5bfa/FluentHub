// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Application.Navigation;
using FluentHub.Data.Navigation;
using FluentHub.Services.Navigation;

namespace FluentHub.Data.Tabs;

public sealed class TabViewItem : ObservableObject, ITabViewItem
{
	private ScreenInstance? _currentScreen;
	private CancellationTokenSource? _navigationCancellation;

	public TabViewItem()
	{
		Id = Guid.NewGuid();
		Journal = new();
		NavigationBar = new();
		Chrome = new();
		NavigationLock = new(1, 1);
	}

	public Guid Id { get; }

	public NavigationJournal<AppRoute> Journal { get; }

	public NavigationBarModel NavigationBar { get; }

	public ScreenChrome Chrome { get; }

	public ScreenInstance? CurrentScreen
	{
		get => _currentScreen;
		private set => SetProperty(ref _currentScreen, value);
	}

	public SemaphoreSlim NavigationLock { get; }

	public CancellationToken BeginNavigation(CancellationToken cancellationToken)
	{
		var replacement = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
		var previous = Interlocked.Exchange(ref _navigationCancellation, replacement);
		previous?.Cancel();
		previous?.Dispose();
		return replacement.Token;
	}

	public void SetCurrentScreen(ScreenInstance? screen)
		=> CurrentScreen = screen;
}
