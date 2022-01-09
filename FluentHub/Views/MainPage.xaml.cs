using FluentHub.DataModels;
using FluentHub.Helper;
using FluentHub.ViewModels;
using FluentHub.Views.UserPage;
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

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x411 を参照してください

namespace FluentHub.Views
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        MainPageViewModel vm = new MainPageViewModel();

        public MainPage()
        {
            this.InitializeComponent();

            var CoreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            CoreTitleBar.ExtendViewIntoTitleBar = true;
            CoreTitleBar.LayoutMetricsChanged += TitleBar_LayoutMetricsChanged;

            CreateTabItem("Home", "\ue80f", $"{App.DefaultDomain}/{App.AuthedUserName}");
            ContentFrame.Navigate(typeof(Home));
        }

        private void TitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
        {
            RightPaddingColumn.Width = new GridLength(sender.SystemOverlayRightInset);
        }

        private void MainTabView_AddTabButtonClick(Microsoft.UI.Xaml.Controls.TabView sender, object args)
        {
            CreateTabItem("Home", "\ue80f", $"{App.DefaultDomain}/{App.AuthedUserName}");
            ContentFrame.Navigate(typeof(Home));

            // selection change
            MainTabView.SelectedIndex = MainPageViewModel.MainTabItems.Count() - 1;
        }

        private void CreateTabItem(string header, string glyph, string url)
        {
            TabItem item = new TabItem();
            item.Header = header;
            item.IconSource = new Microsoft.UI.Xaml.Controls.FontIconSource();
            item.IconSource.Glyph = glyph;
            item.PageUrl.Add(url);
            vm.FullUrl = url;

            MainPageViewModel.MainTabItems.Add(item);
        }

        private void MainTabView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainTabView.SelectedIndex >= 0 && MainTabView.SelectedIndex < MainPageViewModel.MainTabItems.Count())
            {
                int index = MainPageViewModel.SelectedIndex = MainTabView.SelectedIndex;

                // frame navigation
                var list = MainPageViewModel.MainTabItems[index].PageUrl;
                var url = list[MainPageViewModel.MainTabItems[index].NavigationIndex];

                GitHubUrlParser paeser = new GitHubUrlParser();
                string navigateTo = paeser.WhereShouldINavigateTo(url);

                if (navigateTo == "AuthedUserHomePage")
                {
                    ContentFrame.Navigate(typeof(Home));
                }
            }
        }

        private void MainTabView_TabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
        {
            CloseTab(args.Item as TabItem);
        }

        public void CloseTab(TabItem tabItem)
        {
            if (MainPageViewModel.MainTabItems.Count == 1)
            {
                // App.CloseApp();
            }
            else if (MainPageViewModel.MainTabItems.Count > 1)
            {
                MainPageViewModel.MainTabItems.Remove(tabItem);
            }

            if (MainPageViewModel.SelectedIndex > MainTabView.SelectedIndex)
            {
                MainPageViewModel.SelectedIndex--;
            }
        }

        private void DragArea_Loaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SetTitleBar(DragArea);
        }
    }
}
