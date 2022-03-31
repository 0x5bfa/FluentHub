using FluentHub.Services.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.UserControls.TabViewControl
{
    public sealed partial class CustomTabView : UserControl, ITabView
    {
        #region constructor
        public CustomTabView()
        {
            InitializeComponent();
            _items = new ObservableCollection<ITabViewItem>();
            Items = new ReadOnlyObservableCollection<ITabViewItem>(_items);
        }
        #endregion

        #region fields
        private readonly ObservableCollection<ITabViewItem> _items;
        #endregion

        #region properties
        public ITabViewItem SelectedItem
        {
            get => (ITabViewItem)GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem",
                                        typeof(ITabViewItem),
                                        typeof(CustomTabView),
                                        new PropertyMetadata(null, OnSelectedItemChanged));

        public int SelectedIndex
        {
            get => (int)GetValue(SelectedIndexProperty);
            set => SetValue(SelectedIndexProperty, value);
        }
        public static readonly DependencyProperty SelectedIndexProperty =
            DependencyProperty.Register("SelectedIndex",
                                        typeof(int),
                                        typeof(CustomTabView),
                                        new PropertyMetadata(-1));

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title",
                                        typeof(string),
                                        typeof(CustomTabView),
                                        new PropertyMetadata(null, OnTitleChanged));

        private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var newItem = e.NewValue as ITabViewItem;
            var oldItem = e.OldValue as ITabViewItem;
            ((CustomTabView)d).OnSelectionChanged(newItem, oldItem);
        }
        private static void OnTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var view = ApplicationView.GetForCurrentView();
            view.Title = e.NewValue?.ToString() ?? "";
        }

        public ReadOnlyObservableCollection<ITabViewItem> Items { get; }
        #endregion

        #region public methods
        public ITabViewItem OpenTab(Type page!!, object parameter = null, bool setAsSelected = true)
        {
            var transitionInfo = new SlideNavigationTransitionInfo
            {
                Effect = SlideNavigationTransitionEffect.FromRight
            };

            var item = new TabItem
            {

            };
            item.NavigationHistory.NavigateTo(new(page, parameter, transitionInfo)
            {
                Icon = new muxc.FontIconSource
                {
                    Glyph = "\uE737"
                }
            });

            _items.Add(item);
            if (setAsSelected)
            {
                SelectedItem = item;
            }
            return item;
        }

        public bool CloseTab(ITabViewItem tabItem) => tabItem is not null && CloseTab(_items.IndexOf(tabItem));

        public bool CloseTab(Guid guid) => CloseTab(_items.FirstOrDefault(x => x.Guid == guid));

        public bool CloseTab(int index)
        {
            if (index >= 0 && index < _items.Count)
            {
                int newSelectedItemIndex = -1;

                if (index == SelectedIndex) // Removing the current tab
                {
                    if (index == _items.Count - 1) // Select the previous tab if the current item is the last tab
                    {
                        newSelectedItemIndex = index - 1;
                    }
                    else // Select the next tab
                    {
                        newSelectedItemIndex = index;
                    }
                }

                _items.RemoveAt(index);

                if (_items.Count == 0)
                {
                    App.CloseApp();
                }

                if (newSelectedItemIndex >= 0)
                {
                    SelectedIndex = newSelectedItemIndex;
                }
                return true;
            }
            return false;
        }
        #endregion

        #region private methods
        private void OnSelectionChanged(ITabViewItem newItem, ITabViewItem oldItem)
        {
            SuppressNavigationTransitionInfo transitionInfo = new();
            TabViewSelectionChangedEventArgs args = new(newItem, oldItem, transitionInfo);
            SelectionChanged?.Invoke(this, args);
        }
        #endregion

        #region event handlers
        private void OnMainTabViewTabCloseRequested(muxc.TabView sender,
                                                    muxc.TabViewTabCloseRequestedEventArgs args)
            => CloseTab(args.Item as ITabViewItem);

        private void OnAddNewTabButtonClick(object sender, RoutedEventArgs e)
        {
            var item = new TabItem();
            _items.Add(item);
            SelectedItem = item;
        }
        #endregion

        #region events
        public event EventHandler<TabViewSelectionChangedEventArgs> SelectionChanged;
        #endregion
    }
}