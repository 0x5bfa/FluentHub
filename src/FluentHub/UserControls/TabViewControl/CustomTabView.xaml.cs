using FluentHub.Services.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Linq;
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
            _items = new ObservableCollection<ITabItemView>();
            Items = new ReadOnlyObservableCollection<ITabItemView>(_items);
        }
        #endregion

        #region fields
        private readonly ObservableCollection<ITabItemView> _items;
        #endregion

        #region properties
        public ITabItemView SelectedItem
        {
            get => (ITabItemView)GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem",
                                        typeof(ITabItemView),
                                        typeof(UserControl),
                                        new PropertyMetadata(null));

        public ReadOnlyObservableCollection<ITabItemView> Items { get; }
        #endregion

        #region public methods
        public ITabItemView OpenTab(Type page!!, object parameter = null, bool setAsSelected = true)
        {
            var item = new TabItem
            {
                Guid = Guid.NewGuid(),
                Icon = new muxc.FontIconSource
                {
                    Glyph = "\uE737"
                }
            };
            item.NavigationHistory.NavigateTo(new(page, parameter, new SlideNavigationTransitionInfo
            {
                Effect = SlideNavigationTransitionEffect.FromRight
            }));

            _items.Add(item);
            if (setAsSelected)
            {
                MainTabView.SelectedItem = item;
            }
            return item;
        }

        public bool CloseTab(ITabItemView tabItem) => tabItem is not null && CloseTab(_items.IndexOf(tabItem));

        public bool CloseTab(Guid guid) => CloseTab(_items.FirstOrDefault(x => x.Guid == guid));

        public bool CloseTab(int index)
        {
            if (index >= 0 && index < _items.Count)
            {
                int newSelectedItemIndex = -1;

                if (index == MainTabView.SelectedIndex) // Removing the current tab
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
                    MainTabView.SelectedIndex = newSelectedItemIndex;
                }
                return true;
            }
            return false;
        }
        #endregion

        #region event handlers
        private void OnMainTabViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var newItem = e.AddedItems.FirstOrDefault() as ITabItemView;
            var oldItem = e.RemovedItems.FirstOrDefault() as ITabItemView;
            NavigationTransitionInfo transitionInfo;

            /*
            if (newItem is null || oldItem is null)
            {
                transitionInfo = new SlideNavigationTransitionInfo
                {
                    Effect = SlideNavigationTransitionEffect.FromRight
                };
            }
            else
            {
                var oldItemIndex = _items.IndexOf(oldItem);
                var newItemIndex = _items.IndexOf(newItem);
                transitionInfo = new SlideNavigationTransitionInfo
                {
                    Effect = (oldItemIndex - newItemIndex) switch
                    {
                        > 0 => SlideNavigationTransitionEffect.FromLeft,
                        < 0 => SlideNavigationTransitionEffect.FromRight,
                        _ => SlideNavigationTransitionEffect.FromBottom
                    }
                };
            }
            */
            transitionInfo = new SuppressNavigationTransitionInfo();                                 
            var args = new TabViewSelectionChangedEventArgs(newItem, oldItem, transitionInfo);
            SelectionChanged?.Invoke(this, args);
        }

        private void OnMainTabViewTabCloseRequested(muxc.TabView sender, muxc.TabViewTabCloseRequestedEventArgs args) => CloseTab(args.Item as ITabItemView);

        private void OnMainTabViewLoaded(object sender, RoutedEventArgs e)
        {
            /*
            MainPageViewModel.AddNewTabByPath($"/{App.SignedInUserName}");
            App.MainViewModel.MainFrame.Navigate(typeof(UserHomePage));
            var iconSource = new muxc.FontIconSource();
            iconSource.Glyph = "\uE737";
            App.MainViewModel.MainTabItems[App.MainViewModel.SelectedTabIndex].IconSource = iconSource;
            App.MainViewModel.MainTabItems[App.MainViewModel.SelectedTabIndex].Header = "FluentHub";
            */
        }

        private void AddNewTabButton_Click(object sender, RoutedEventArgs e)
        {
            var item = new TabItem
            {
                Guid = Guid.NewGuid(),
                Icon = new muxc.FontIconSource
                {
                    Glyph = "\uE737"
                }
            };
            _items.Add(item);
            SelectedItem = item;
            /*
            TabItemAdding = true;            
            MainPageViewModel.AddNewTabByPath($"/{App.SignedInUserName}");
            App.MainViewModel.MainFrame.Navigate(typeof(UserHomePage));
            App.MainViewModel.SelectedTabIndex = MainTabView.SelectedIndex = App.MainViewModel.MainTabItems.Count() - 1;
            */
        }
        #endregion

        #region events
        public event EventHandler<TabViewSelectionChangedEventArgs> SelectionChanged;
        #endregion
    }
}