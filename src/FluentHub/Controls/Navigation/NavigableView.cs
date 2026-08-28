// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.Windows.Input;

namespace FluentHub.Controls.Navigation;

/// <summary>
/// Presenter-hosted screen that participates in the shell navigation bar.
/// </summary>
public abstract class NavigableView : ScreenView
{
	protected NavigableView(NavigationPageKind pageKind, NavigationPageKey itemKey)
	{
		PageKind = pageKind;
		PageKey = itemKey;
	}

	public NavigationPageKind PageKind { get; }

	public NavigationPageKey PageKey { get; }

	protected ICommand? _pageLoadCommand
	{
		get => _screenLoadCommand;
		set => _screenLoadCommand = value;
	}
}
