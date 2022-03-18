using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;

namespace FluentHub.Services.Navigation
{
    public interface ITabView
    {
        ITabItemView SelectedItem { get; set; }
        ReadOnlyObservableCollection<ITabItemView> Items { get; }
        ITabItemView OpenTab(Type page, object parameter = null, bool setAsSelected = true);
        bool CloseTab(ITabItemView tab);
        bool CloseTab(Guid tabId);
        bool CloseTab(int tabIndex);
        event SelectionChangedEventHandler SelectionChanged;
    }
}