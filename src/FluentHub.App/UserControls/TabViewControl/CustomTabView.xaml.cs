// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.Services.Navigation;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;

namespace FluentHub.App.UserControls.TabViewControl
{
    public sealed partial class CustomTabView : UserControl, ITabView
    {
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

        private Type _NewTabPage = typeof(Views.Viewers.UserHomePage);
        public Type NewTabPage
        {
            get => _NewTabPage;
            set
            {
                if (value is not null && value.IsSubclassOf(typeof(Page)))
                {
                    _NewTabPage = value;
                }
                else
                {
                    throw new ArgumentException("New tab's page must be a subclass of Page");
                }
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
            ITabViewItem tab = new TabItem();
            _TabItems.Add(tab);

            if (setAsSelected)
                SelectedItem = tab;

            page ??= NewTabPage;
            if (page != null)
                tab.Frame.Navigate(page, parameter, new EntranceNavigationTransitionInfo());

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
                    App.CloseApp();

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
            Microsoft.UI.Windowing.AppWindow view = App.GetAppWindow(App.Window);
            view.Title = e.NewValue?.ToString() ?? "";
        }

        private void OnSelectionChanged(ITabViewItem newItem, ITabViewItem oldItem)
        {
            EntranceNavigationTransitionInfo transitionInfo = new();
            TabViewSelectionChangedEventArgs args = new(newItem, oldItem, transitionInfo);

            SelectionChanged?.Invoke(this, args);
        }

        private void OnMainTabViewTabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
            => CloseTab((ITabViewItem)args.Item);

        private void OnAddNewTabButtonClick(object sender, RoutedEventArgs e)
            => OpenTab();
    }
}
