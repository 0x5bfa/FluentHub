using FluentHub.Models;
using FluentHub.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace FluentHub.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            App.MainViewModel.MainFrame.Navigating += ViewModelMainFrame_Navigating;
            CoreApplication.GetCurrentView().TitleBar.LayoutMetricsChanged += TitleBar_LayoutMetricsChanged;
        }

        private void TitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
        {
            RightPaddingColumn.Width = new GridLength(sender.SystemOverlayRightInset);
        }

        private void ViewModelMainFrame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            MainFrame.Navigate(e.SourcePageType);

            e.Cancel = true;
        }

        private void MainTabView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainPageViewModel.TabItemAdding || MainPageViewModel.TabItemDeleting)
            {
                MainPageViewModel.TabItemAdding = false;
                MainPageViewModel.TabItemDeleting = false;
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

        private void DragArea_Loaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SetTitleBar(DragArea);
        }

        private void MainTabView_Loaded(object sender, RoutedEventArgs e)
        {
            MainPageViewModel.AddNewTabByPath($"/{App.SignedInUserName}");
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
                    MainPageViewModel.TabItemDeleting = true;
                }

                App.MainViewModel.MainTabItems.Remove(tabItem);
            }
        }

        private void AddNewTabButton_Click(object sender, RoutedEventArgs e)
        {
            MainPageViewModel.AddNewTabByPath($"/{App.SignedInUserName}");
            App.MainViewModel.SelectedTabIndex = MainTabView.SelectedIndex = App.MainViewModel.MainTabItems.Count() - 1;
        }
    }
}
