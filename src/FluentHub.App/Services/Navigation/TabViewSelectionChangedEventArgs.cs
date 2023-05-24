// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml.Media.Animation;

namespace FluentHub.App.Services.Navigation
{
    public class TabViewSelectionChangedEventArgs : EventArgs
    {
        public ITabViewItem NewSelectedItem { get; }

        public ITabViewItem OldSelectedItem { get; }

        public NavigationTransitionInfo RecommendedNavigationTransitionInfo { get; }

        public TabViewSelectionChangedEventArgs(ITabViewItem newSelectedItem)
        {
            NewSelectedItem = newSelectedItem;
        }

        public TabViewSelectionChangedEventArgs(ITabViewItem newSelectedItem, ITabViewItem oldSelectedItem)
        {
            NewSelectedItem = newSelectedItem;
            OldSelectedItem = oldSelectedItem;
        }

        public TabViewSelectionChangedEventArgs(ITabViewItem newSelectedItem, ITabViewItem oldSelectedItem, NavigationTransitionInfo recommendedNavigationTransitionInfo)
        {
            NewSelectedItem = newSelectedItem;
            OldSelectedItem = oldSelectedItem;
            RecommendedNavigationTransitionInfo = recommendedNavigationTransitionInfo;
        }
    }
}