// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using System.Windows.Input;

namespace FluentHub.Shell.Navigation;

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
