using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media.Animation;

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
        event EventHandler<TabViewSelectionChangedEventArgs> SelectionChanged;

    }
    public class TabViewSelectionChangedEventArgs : EventArgs
    {
        public TabViewSelectionChangedEventArgs(ITabItemView newSelectedItem)
        {
            NewSelectedItem = newSelectedItem;
        }
        public TabViewSelectionChangedEventArgs(ITabItemView newSelectedItem, ITabItemView oldSelectedItem)
        {
            NewSelectedItem = newSelectedItem;
            OldSelectedItem = oldSelectedItem;
        }
        public TabViewSelectionChangedEventArgs(ITabItemView newSelectedItem,
                                                ITabItemView oldSelectedItem,
                                                NavigationTransitionInfo recommendedNavigationTransitionInfo)
        {
            NewSelectedItem = newSelectedItem;
            OldSelectedItem = oldSelectedItem;
            RecommendedNavigationTransitionInfo = recommendedNavigationTransitionInfo;
        }
        public ITabItemView NewSelectedItem { get; }
        public ITabItemView OldSelectedItem { get; }
        public NavigationTransitionInfo RecommendedNavigationTransitionInfo { get; }
    }
}