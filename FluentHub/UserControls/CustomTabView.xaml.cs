using FluentHub.ViewModels;
using FluentHub.Views;
using FluentHub.Views.UserPages;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FontIconSource = Microsoft.UI.Xaml.Controls.FontIconSource;

namespace FluentHub.UserControls
{
    public class TabItem : INotifyPropertyChanged
    {
        private string _header;
        public string Header
        {
            get => _header;
            set
            {
                _header = value;
                NotifyPropertyChanged(nameof(Header));
            }
        }

        private FontIconSource _iconSource;
        public FontIconSource IconSource
        {
            get => _iconSource;
            set
            {
                _iconSource = value;
                NotifyPropertyChanged(nameof(IconSource));
            }
        }

        private List<string> _pageUrl = new List<string>();
        public List<string> PageUrlList
        {
            get => _pageUrl;
            set
            {
                _pageUrl = value;
                NotifyPropertyChanged(nameof(PageUrlList));
            }
        }

        private int _naviationIndex;
        public int NavigationIndex
        {
            get => _pageUrl.Count() - 1;
            set
            {
                // Navigation control is coming soon.
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }

    public sealed partial class CustomTabView : UserControl
    {
        private bool TabItemAdding { get; set; } = false;
        private bool TabItemDeleting { get; set; } = false;

        public CustomTabView()
        {
            this.InitializeComponent();
        }

        private void MainTabView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TabItemAdding || TabItemDeleting)
            {
                TabItemAdding = false;
                TabItemDeleting = false;
                return;
            }

            int index = App.MainViewModel.SelectedTabIndex = MainTabView.SelectedIndex;

            if (App.MainViewModel.SelectedTabIndex >= 0 && App.MainViewModel.MainTabItems[index].NavigationIndex >= 0)
            {
                var pageUrlList = App.MainViewModel.MainTabItems[index].PageUrlList;
                var pageUrl = pageUrlList[App.MainViewModel.MainTabItems[index].NavigationIndex];

                App.MainViewModel.NavigateMainFrame(pageUrl);
            }
        }

        private void MainTabView_TabCloseRequested(Microsoft.UI.Xaml.Controls.TabView sender, Microsoft.UI.Xaml.Controls.TabViewTabCloseRequestedEventArgs args)
        {
            CloseTab(args.Item as TabItem);
        }

        private void MainTabView_Loaded(object sender, RoutedEventArgs e)
        {
            MainPageViewModel.AddNewTabByPath($"/{App.SignedInUserName}");
            App.MainViewModel.MainFrame.Navigate(typeof(UserHome));
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
            TabItemAdding = true;

            MainPageViewModel.AddNewTabByPath($"/{App.SignedInUserName}");
            App.MainViewModel.MainFrame.Navigate(typeof(ProfilePage), $"{App.SignedInUserName}");
            App.MainViewModel.SelectedTabIndex = MainTabView.SelectedIndex = App.MainViewModel.MainTabItems.Count() - 1;
        }
    }
}
