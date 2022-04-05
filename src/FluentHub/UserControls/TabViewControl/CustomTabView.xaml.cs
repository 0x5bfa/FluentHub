#pragma warning disable IDE0045
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
            InternalItemsList = new ObservableCollection<ITabViewItem>();
            Items = new ReadOnlyObservableCollection<ITabViewItem>(InternalItemsList);
        }
        #endregion

        #region fields
        private ObservableCollection<ITabViewItem> InternalItemsList { get; }
        private Type _page;
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

        public Type NewTabPage
        {
            get => _page;
            set
            {
                _page = value == null || value.IsSubclassOf(typeof(Page))
                    ? value
                    : throw new ArgumentException("NewTabPage must be a subclass of Page");
            }
        }
        #endregion

        #region public methods
        public ITabViewItem OpenTab(Type page = null, object parameter = null, bool setAsSelected = true)
        {
            ITabViewItem tab = new TabItem();
            InternalItemsList.Add(tab);
            if (setAsSelected)
            {
                SelectedItem = tab;
            }
            page ??= NewTabPage;
            if (page != null)
            {
                tab.Frame.Navigate(page, parameter, new SuppressNavigationTransitionInfo());
            }
            return tab;
        }

        public bool CloseTab(ITabViewItem tabItem) => tabItem is not null && CloseTab(InternalItemsList.IndexOf(tabItem));

        public bool CloseTab(Guid guid) => CloseTab(InternalItemsList.FirstOrDefault(x => x.Guid == guid));

        public bool CloseTab(int index)
        {
            if (index >= 0 && index < InternalItemsList.Count)
            {
                int newSelectedItemIndex = -1;

                if (index == SelectedIndex) // Removing the current tab
                {
                    if (index == InternalItemsList.Count - 1) // Select the previous tab if the current item is the last tab
                    {
                        newSelectedItemIndex = index - 1;
                    }
                    else // Select the next tab
                    {
                        newSelectedItemIndex = index;
                    }
                }

                InternalItemsList.RemoveAt(index);

                if (InternalItemsList.Count == 0)
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
            => CloseTab((ITabViewItem)args.Item);

        private void OnAddNewTabButtonClick(object sender, RoutedEventArgs e) => OpenTab();
        #endregion

        #region events
        public event EventHandler<TabViewSelectionChangedEventArgs> SelectionChanged;
        #endregion
    }
}