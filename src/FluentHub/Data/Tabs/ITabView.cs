// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Data.Tabs;

public interface ITabView
{
	ITabViewItem? SelectedItem { get; set; }

	int SelectedIndex { get; set; }

	ReadOnlyObservableCollection<ITabViewItem> TabItems { get; }

	ITabViewItem CreateTab(bool setAsSelected = true);

	bool RemoveTab(ITabViewItem tab);

	bool RemoveTab(Guid tabId);

	event EventHandler<TabViewSelectionChangedEventArgs>? SelectionChanged;
}
