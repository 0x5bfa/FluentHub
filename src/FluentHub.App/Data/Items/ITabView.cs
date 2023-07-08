// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.App.Data.Items
{
	public interface ITabView
	{
		ITabViewItem SelectedItem { get; set; }

		int SelectedIndex { get; set; }

		Type NewTabPage { get; set; }

		ReadOnlyObservableCollection<ITabViewItem> TabItems { get; }

		ITabViewItem OpenTab(Type page = null, object parameter = null, bool setAsSelected = true);

		bool CloseTab(ITabViewItem tab);

		bool CloseTab(Guid tabId);

		bool CloseTab(int tabIndex);

		event EventHandler<TabViewSelectionChangedEventArgs> SelectionChanged;
	}
}
