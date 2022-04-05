using System;
using System.Collections.ObjectModel;

namespace FluentHub.Services.Navigation
{
    public interface ITabView
    {        
        ITabViewItem SelectedItem { get; set; }
        int SelectedIndex { get; set; }
        Type NewTabPage { get; set; }
        ReadOnlyObservableCollection<ITabViewItem> Items { get; }
        ITabViewItem OpenTab(Type page = null, object parameter = null, bool setAsSelected = true);
        bool CloseTab(ITabViewItem tab);
        bool CloseTab(Guid tabId);
        bool CloseTab(int tabIndex);
        event EventHandler<TabViewSelectionChangedEventArgs> SelectionChanged;
    }
}