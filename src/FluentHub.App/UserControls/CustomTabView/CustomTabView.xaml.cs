// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using CommunityToolkit.WinUI.UI;
using FluentHub.App.Views;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;

namespace FluentHub.App.UserControls.CustomTabView
{
	public sealed partial class CustomTabView : UserControl, ITabView
	{
		/*
		private MainPage _mainPage;

		public MainPage MainPageInstance
		{
			get => _mainPage;
			set => _mainPage = value;
		} */

		public ITabViewItem SelectedItem
		{
			get => (ITabViewItem)GetValue(SelectedItemProperty);
			set => SetValue(SelectedItemProperty, value);
		}

		public static readonly DependencyProperty SelectedItemProperty =
			DependencyProperty.Register(nameof(SelectedItem), typeof(ITabViewItem), typeof(CustomTabView), new(null, OnSelectedItemChanged));

		public int SelectedIndex
		{
			get => (int)GetValue(SelectedIndexProperty);
			set => SetValue(SelectedIndexProperty, value);
		}

		public static readonly DependencyProperty SelectedIndexProperty =
			DependencyProperty.Register(nameof(SelectedIndex), typeof(int), typeof(CustomTabView), new(-1));

		public string Title
		{
			get => (string)GetValue(TitleProperty);
			set => SetValue(TitleProperty, value);
		}

		public static readonly DependencyProperty TitleProperty =
			DependencyProperty.Register(nameof(Title), typeof(string), typeof(CustomTabView), new(null, OnTitleChanged));

		public Grid DragArea
			=> DragAreaGrid;

		private readonly ObservableCollection<ITabViewItem> _TabItems;
		public ReadOnlyObservableCollection<ITabViewItem> TabItems { get; }

		private Type _NewTabPage = typeof(Views.Viewers.DashBoardPage);
		public Type NewTabPage
		{
			get => _NewTabPage;
			set
			{
				if (value is not null)
					_NewTabPage = value;
			}
		}

		public event EventHandler<TabViewSelectionChangedEventArgs>? SelectionChanged;

		public CustomTabView()
		{
			InitializeComponent();

			_TabItems = new();
			TabItems = new(_TabItems);
		}

		public ITabViewItem OpenTab(Type? page = null, object? parameter = null, bool setAsSelected = true)
		{
			ITabViewItem tab = new Data.Items.TabViewItem();
			_TabItems.Add(tab);

			if (setAsSelected)
				SelectedItem = tab;

			page ??= NewTabPage;
			if (page != null)
				tab.Frame.Navigate(page, parameter, new SuppressNavigationTransitionInfo());

			return tab;
		}

		public bool CloseTab(ITabViewItem tabItem)
			=> tabItem is not null && CloseTab(_TabItems.IndexOf(tabItem));

		public bool CloseTab(Guid guid)
			=> CloseTab(_TabItems.First(x => x.Guid == guid));

		public bool CloseTab(int index)
		{
			if (index >= 0 && index < _TabItems.Count)
			{
				int newSelectedItemIndex = -1;

				// Removing the current tab
				if (index == SelectedIndex)
				{
					// Select the previous tab if the current item is the last tab
					if (index == _TabItems.Count - 1)
						newSelectedItemIndex = index - 1;
					else // Select the next tab
						newSelectedItemIndex = index;
				}

				_TabItems.RemoveAt(index);

				if (_TabItems.Count == 0)
					AppLifecycleHelper.CloseApp();

				if (newSelectedItemIndex >= 0)
					SelectedIndex = newSelectedItemIndex;

				return true;
			}

			return false;
		}

		private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var newItem = (ITabViewItem)e.NewValue;
			var oldItem = (ITabViewItem)e.OldValue;
			((CustomTabView)d).OnSelectionChanged(newItem, oldItem);
		}

		private static void OnTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Microsoft.UI.Windowing.AppWindow view = AppLifecycleHelper.GetAppWindow(MainWindow.Instance);
			view.Title = e.NewValue?.ToString() ?? "";
		}

		private void OnSelectionChanged(ITabViewItem newItem, ITabViewItem oldItem)
		{
			SuppressNavigationTransitionInfo transitionInfo = new();
			TabViewSelectionChangedEventArgs args = new(newItem, oldItem, transitionInfo);

			SelectionChanged?.Invoke(this, args);
		}

		private void OnMainTabViewTabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
			=> CloseTab((ITabViewItem)args.Item);

		private void OnAddNewTabButtonClick(object sender, RoutedEventArgs e)
			=> OpenTab();

		private void TabViewItem_Loaded(object sender, RoutedEventArgs e)
		{
			if (sender is Microsoft.UI.Xaml.Controls.TabViewItem senderTabViewItem &&
				senderTabViewItem.FindDescendant("IconControl") is ContentControl control)
			{
				if (senderTabViewItem.IconSource is ImageIconSource iis)
					control.Content = iis.CreateIconElement();

				senderTabViewItem.RegisterPropertyChangedCallback(Microsoft.UI.Xaml.Controls.TabViewItem.IconSourceProperty, (s, args) =>
				{
					if (s is Microsoft.UI.Xaml.Controls.TabViewItem tabViewItem &&
						tabViewItem.FindDescendant("IconControl") is ContentControl iconControl)
					{
						if (tabViewItem.IconSource is ImageIconSource iis)
							iconControl.Content = iis.CreateIconElement();
					}
				});
			}
		}

		/*
		private void GlobalNavigationButton_Click(object sender, RoutedEventArgs e)
		{
			_mainPage.LeftSideNavigationViewOpenerButton_Click(sender, e);
		} */
	}
}
