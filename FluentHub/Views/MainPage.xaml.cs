using FluentHub.DataModels;
using FluentHub.Helper;
using FluentHub.ViewModels;
using FluentHub.Views.UserPages;
using Microsoft.UI.Xaml.Controls;
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

            var CoreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            CoreTitleBar.ExtendViewIntoTitleBar = true;
            CoreTitleBar.LayoutMetricsChanged += TitleBar_LayoutMetricsChanged;

            CreateFirstTabItem();
            ContentFrame.Navigate(typeof(Home));
        }

        private void TitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
        {
            RightPaddingColumn.Width = new GridLength(sender.SystemOverlayRightInset);
        }

        private void MainTabView_AddTabButtonClick(TabView sender, object args)
        {
            CreateFirstTabItem();
            ContentFrame.Navigate(typeof(Home));

            // selection change
            MainTabView.SelectedIndex = App.MainViewModel.MainTabItems.Count() - 1;
        }

        private void CreateFirstTabItem()
        {
            TabItem item = new TabItem();

            item.PageUrl.Add($"{App.DefaultDomain}/{App.AuthedUserName}?tab=repositories");
            App.MainViewModel.FullUrl = item.PageUrl[0];
            App.MainViewModel.UnpersedUrlString = item.PageUrl[0];

            App.MainViewModel.MainTabItems.Add(item);
        }

        private void MainTabView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainTabView.SelectedIndex >= 0 && MainTabView.SelectedIndex < App.MainViewModel.MainTabItems.Count())
            {
                int index = App.MainViewModel.SelectedIndex = MainTabView.SelectedIndex;

                // frame navigation
                var list = App.MainViewModel.MainTabItems[index].PageUrl;
                var url = list[App.MainViewModel.MainTabItems[index].NavigationIndex];
                App.MainViewModel.FullUrl = url;

                UrlParseHelper paeser = new UrlParseHelper();
                var pageType = paeser.WhereShouldINavigateTo(url);

                if (pageType == UrlParseHelper.PageType.UserProfile)
                {
                    var username = url.Split("/")[3].Split("?")[0];

                    if (username == App.AuthedUserName)
                    {
                        ContentFrame.Navigate(typeof(Home));
                    }
                }
            }
        }

        private void MainTabView_TabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
        {
            CloseTab(args.Item as TabItem);
        }

        public void CloseTab(TabItem tabItem)
        {
            if (App.MainViewModel.MainTabItems.Count == 1)
            {
                // App.CloseApp();
            }
            else if (App.MainViewModel.MainTabItems.Count > 1)
            {
                App.MainViewModel.MainTabItems.Remove(tabItem);
            }

            if (App.MainViewModel.SelectedIndex > MainTabView.SelectedIndex)
            {
                App.MainViewModel.SelectedIndex--;
            }
        }

        private void DragArea_Loaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SetTitleBar(DragArea);
        }
    }
}
