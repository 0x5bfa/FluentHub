// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Shell.Tabs;

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
