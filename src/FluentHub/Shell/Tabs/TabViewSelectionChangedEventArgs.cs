// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Shell.Tabs;

public sealed class TabViewSelectionChangedEventArgs(
	ITabViewItem? newSelectedItem,
	ITabViewItem? oldSelectedItem) : System.EventArgs
{
	public ITabViewItem? NewSelectedItem { get; } = newSelectedItem;

	public ITabViewItem? OldSelectedItem { get; } = oldSelectedItem;
}
