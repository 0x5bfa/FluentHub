using FluentHub.DataModels;
using FluentHub.ViewModels;
using FluentHub.Views.UserPage;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
        public MainPage()
        {
            this.InitializeComponent();
            Window.Current.SetTitleBar(DragArea);

            TabItem item = new TabItem();
            item.Header = "Home";
            item.IconSource = new Microsoft.UI.Xaml.Controls.FontIconSource();
            item.IconSource.Glyph = "\ue80f";

            MainPageViewModel.MainTabItems.Add(item);
            ContentFrame.Navigate(typeof(Home));
        }

        private void MainTabView_AddTabButtonClick(Microsoft.UI.Xaml.Controls.TabView sender, object args)
        {
            TabItem item = new TabItem();
            item.Header = "Home";
            item.IconSource = new Microsoft.UI.Xaml.Controls.FontIconSource();
            item.IconSource.Glyph = "\ue80f";

            MainPageViewModel.MainTabItems.Add(item);
            ContentFrame.Navigate(typeof(Home));
        }

        private void MainTabView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainPageViewModel.SelectedIndex = MainTabView.SelectedIndex;

            // frame navigation
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
        }
    }
}
