// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using CommunityToolkit.WinUI;
using FluentHub.Core.Application.Navigation;
using FluentHub.Services.Navigation;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Controls.Tabs;

public sealed partial class CustomTabView : UserControl, ITabView
{
	private readonly ObservableCollection<ITabViewItem> _tabItems = [];

	public CustomTabView()
	{
		InitializeComponent();
		TabItems = new(_tabItems);
	}

	public ITabViewItem? SelectedItem
	{
		get => (ITabViewItem?)GetValue(SelectedItemProperty);
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

	public ReadOnlyObservableCollection<ITabViewItem> TabItems { get; }

	public event EventHandler<TabViewSelectionChangedEventArgs>? SelectionChanged;

	public ITabViewItem CreateTab(bool setAsSelected = true)
	{
		ITabViewItem tab = new Data.Tabs.TabViewItem();
		_tabItems.Add(tab);

		if (setAsSelected)
			SelectedItem = tab;

		return tab;
	}

	public bool RemoveTab(ITabViewItem tab)
	{
		var index = _tabItems.IndexOf(tab);
		if (index < 0)
			return false;

		var wasSelected = index == SelectedIndex;
		_tabItems.RemoveAt(index);

		if (_tabItems.Count == 0)
		{
			SelectedItem = null;
			AppLifecycleHelper.CloseApp();
		}
		else if (wasSelected)
		{
			SelectedIndex = Math.Min(index, _tabItems.Count - 1);
		}

		return true;
	}

	public bool RemoveTab(Guid tabId)
	{
		var tab = _tabItems.FirstOrDefault(item => item.Id == tabId);
		return tab is not null && RemoveTab(tab);
	}

	private static void OnSelectedItemChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
	{
		var control = (CustomTabView)dependencyObject;
		control.SelectionChanged?.Invoke(
			control,
			new(args.NewValue as ITabViewItem, args.OldValue as ITabViewItem));
	}

	private static void OnTitleChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
	{
		var window = AppLifecycleHelper.GetAppWindow(MainWindow.Instance);
		window.Title = args.NewValue?.ToString() ?? string.Empty;
	}

	private async void OnMainTabViewTabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
	{
		if (args.Item is ITabViewItem tab)
			await Ioc.Default.GetRequiredService<INavigationService>().CloseTabAsync(tab.Id);
	}

	private async void OnAddNewTabButtonClick(object sender, RoutedEventArgs args)
		=> await Ioc.Default.GetRequiredService<INavigationService>().OpenTabAsync(new DashboardRoute());

	private void TabViewItem_Loaded(object sender, RoutedEventArgs args)
	{
		if (sender is Microsoft.UI.Xaml.Controls.TabViewItem senderTabViewItem &&
			senderTabViewItem.FindDescendant("IconControl") is ContentControl control)
		{
			if (senderTabViewItem.IconSource is ImageIconSource imageIconSource)
				control.Content = imageIconSource.CreateIconElement();

			senderTabViewItem.RegisterPropertyChangedCallback(Microsoft.UI.Xaml.Controls.TabViewItem.IconSourceProperty, (source, property) =>
			{
				if (source is Microsoft.UI.Xaml.Controls.TabViewItem tabViewItem &&
					tabViewItem.FindDescendant("IconControl") is ContentControl iconControl &&
					tabViewItem.IconSource is ImageIconSource newIconSource)
				{
					iconControl.Content = newIconSource.CreateIconElement();
				}
			});
		}
	}
}
