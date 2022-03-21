using System;
using Windows.UI.Xaml.Media.Animation;

namespace FluentHub.Services.Navigation
{
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