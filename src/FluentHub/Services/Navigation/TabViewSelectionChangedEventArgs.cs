using System;
using Windows.UI.Xaml.Media.Animation;

namespace FluentHub.Services.Navigation
{
    public class TabViewSelectionChangedEventArgs : EventArgs
    {
        public TabViewSelectionChangedEventArgs(ITabViewItem newSelectedItem)
        {
            NewSelectedItem = newSelectedItem;
        }
        public TabViewSelectionChangedEventArgs(ITabViewItem newSelectedItem, ITabViewItem oldSelectedItem)
        {
            NewSelectedItem = newSelectedItem;
            OldSelectedItem = oldSelectedItem;
        }
        public TabViewSelectionChangedEventArgs(ITabViewItem newSelectedItem,
                                                ITabViewItem oldSelectedItem,
                                                NavigationTransitionInfo recommendedNavigationTransitionInfo)
        {
            NewSelectedItem = newSelectedItem;
            OldSelectedItem = oldSelectedItem;
            RecommendedNavigationTransitionInfo = recommendedNavigationTransitionInfo;
        }
        public ITabViewItem NewSelectedItem { get; }
        public ITabViewItem OldSelectedItem { get; }
        public NavigationTransitionInfo RecommendedNavigationTransitionInfo { get; }
    }
}