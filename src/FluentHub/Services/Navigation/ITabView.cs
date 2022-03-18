using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Windows.UI.Xaml.Controls;

namespace FluentHub.Services.Navigation
{
    public interface ITabView : INotifyPropertyChanged
    {
        ITabItemView SelectedItem { get; set; }
        ReadOnlyObservableCollection<ITabItemView> Items { get; }
        ITabItemView CreateTab(Type page, object parameter = null, bool setAsSelected = true);
        event SelectionChangedEventHandler SelectionChanged;
    }
}