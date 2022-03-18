using FluentHub.Services.Navigation;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
        private bool TabItemAdding { get; set; } = false;
        private bool TabItemDeleting { get; set; } = false;
        public ITabItemView SelectedItem
        {
            get => MainTabView.SelectedItem as ITabItemView;
            set => MainTabView.SelectedItem = value;
        }

        public ReadOnlyObservableCollection<ITabItemView> Items { get; }
        #endregion

        private void MainTabView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
            SelectionChanged?.Invoke(this, e);

            /*
            if (TabItemAdding || TabItemDeleting)
            {
                TabItemAdding = false;
                TabItemDeleting = false;
                return;
            }

            int index = App.MainViewModel.SelectedTabIndex = MainTabView.SelectedIndex;

            if (App.MainViewModel.SelectedTabIndex >= 0 && App.MainViewModel.MainTabItems[index].NavigationIndex >= 0)
            {
                var pageUrlList = App.MainViewModel.MainTabItems[index].PageUrls;
                var pageUrl = pageUrlList[App.MainViewModel.MainTabItems[index].NavigationIndex];

                App.MainViewModel.NavigateMainFrame(pageUrl);
            }*/
        }

        private void MainTabView_TabCloseRequested(muxc.TabView sender, muxc.TabViewTabCloseRequestedEventArgs args)
        {
            CloseTab(args.Item as TabItem);
        }

        private void MainTabView_Loaded(object sender, RoutedEventArgs e)
        {
            /*
            MainPageViewModel.AddNewTabByPath($"/{App.SignedInUserName}");
            App.MainViewModel.MainFrame.Navigate(typeof(UserHomePage));
            var iconSource = new muxc.FontIconSource();
            iconSource.Glyph = "\uE737";
            App.MainViewModel.MainTabItems[App.MainViewModel.SelectedTabIndex].IconSource = iconSource;
            App.MainViewModel.MainTabItems[App.MainViewModel.SelectedTabIndex].Header = "FluentHub";*/
        }

        private void CloseTab(TabItem tabItem)
        {
            if (App.MainViewModel.MainTabItems.Count == 1)
            {
                App.CloseApp();
            }
            else if (App.MainViewModel.MainTabItems.Count > 1)
            {
                if (App.MainViewModel.SelectedTabIndex > MainTabView.SelectedIndex)
                {
                    App.MainViewModel.SelectedTabIndex--;
                }
                else if (App.MainViewModel.SelectedTabIndex == MainTabView.SelectedIndex)
                {
                    TabItemDeleting = true;
                }

                App.MainViewModel.MainTabItems.Remove(tabItem);
            }
        }

        private void AddNewTabButton_Click(object sender, RoutedEventArgs e)
        {
            CreateTab(null, null, true);
            //TabItemAdding = true;
            /*
            MainPageViewModel.AddNewTabByPath($"/{App.SignedInUserName}");
            App.MainViewModel.MainFrame.Navigate(typeof(UserHomePage));
            App.MainViewModel.SelectedTabIndex = MainTabView.SelectedIndex = App.MainViewModel.MainTabItems.Count() - 1;*/
        }

        public ITabItemView CreateTab(Type page, object parameter = null, bool setAsSelected = true)
        {
            var item = new TabItem
            {
                CurrentPage = page,
                Parameter = parameter,
                IconSource = new muxc.FontIconSource
                {
                    Glyph = "\uE737"
                }
            };
            _items.Add(item);
            if (setAsSelected)
            {
                MainTabView.SelectedItem = item;
            }
            return item;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public event SelectionChangedEventHandler SelectionChanged;

        private bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }
            return false;
        }
    }
}
