// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml.Media.Animation;

namespace FluentHub.App.Data.EventArgs
{
	public class TabViewSelectionChangedEventArgs : System.EventArgs
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