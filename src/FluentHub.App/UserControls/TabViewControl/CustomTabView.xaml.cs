using FluentHub.App.Services.Navigation;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;

namespace FluentHub.App.UserControls.TabViewControl
{
    public sealed partial class CustomTabView : UserControl, ITabView
    {
        #region propdp
        public ITabViewItem SelectedItem
        {
            get => (ITabViewItem)GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(
                "SelectedItem",
                typeof(ITabViewItem),
                typeof(CustomTabView),
                new PropertyMetadata(null, OnSelectedItemChanged));

        public int SelectedIndex
        {
            get => (int)GetValue(SelectedIndexProperty);
            set => SetValue(SelectedIndexProperty, value);
        }

        public static readonly DependencyProperty SelectedIndexProperty =
            DependencyProperty.Register(
                "SelectedIndex",
                typeof(int),
                typeof(CustomTabView),
                new PropertyMetadata(-1));

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                "Title",
                typeof(string),
                typeof(CustomTabView),
                new PropertyMetadata(null, OnTitleChanged));
        #endregion

        public Grid DragArea => DragAreaGrid;

        public CustomTabView()
        {
            InitializeComponent();

            InternalItemsList = new ObservableCollection<ITabViewItem>();
            Items = new ReadOnlyObservableCollection<ITabViewItem>(InternalItemsList);
        }

        #region Fields and Properties
        private Type _page;

        private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var newItem = e.NewValue as ITabViewItem;
            var oldItem = e.OldValue as ITabViewItem;
            ((CustomTabView)d).OnSelectionChanged(newItem, oldItem);
        }

        private static void OnTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Microsoft.UI.Windowing.AppWindow view = App.GetAppWindow(App.Window);
            view.Title = e.NewValue?.ToString() ?? "";
        }

        private ObservableCollection<ITabViewItem> InternalItemsList { get; }
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

        public event EventHandler<TabViewSelectionChangedEventArgs> SelectionChanged;
        #endregion

        #region Methods
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

        public bool CloseTab(ITabViewItem tabItem)
            => tabItem is not null && CloseTab(InternalItemsList.IndexOf(tabItem));

        public bool CloseTab(Guid guid)
            => CloseTab(InternalItemsList.FirstOrDefault(x => x.Guid == guid));

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

        private void OnSelectionChanged(ITabViewItem newItem, ITabViewItem oldItem)
        {
            SuppressNavigationTransitionInfo transitionInfo = new();
            TabViewSelectionChangedEventArgs args = new(newItem, oldItem, transitionInfo);
            SelectionChanged?.Invoke(this, args);
        }
        #endregion

        #region Event Handlers
        private void OnMainTabViewTabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
            => CloseTab((ITabViewItem)args.Item);

        private void OnAddNewTabButtonClick(object sender, RoutedEventArgs e)
            => OpenTab();
        #endregion
    }
}
