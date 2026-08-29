// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Application.Navigation;
using FluentHub.Data.Navigation;
using FluentHub.Services.Navigation;
using System.ComponentModel;

namespace FluentHub.Data.Tabs;

public interface ITabViewItem : INotifyPropertyChanged
{
	Guid Id { get; }

	NavigationJournal<AppRoute> Journal { get; }

	NavigationBarModel NavigationBar { get; }

	ScreenChrome Chrome { get; }

	ScreenInstance? CurrentScreen { get; }

	SemaphoreSlim NavigationLock { get; }

	CancellationToken BeginNavigation(CancellationToken cancellationToken);

	void SetCurrentScreen(ScreenInstance? screen);
}
