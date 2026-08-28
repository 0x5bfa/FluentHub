// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Data.Tabs;

public sealed class TabViewSelectionChangedEventArgs(
	ITabViewItem? newSelectedItem,
	ITabViewItem? oldSelectedItem) : System.EventArgs
{
	public ITabViewItem? NewSelectedItem { get; } = newSelectedItem;

	public ITabViewItem? OldSelectedItem { get; } = oldSelectedItem;
}
