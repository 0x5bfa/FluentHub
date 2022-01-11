using FluentHub.DataModels;
using FluentHub.UserControls;
using FluentHub.ViewModels.UserPages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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


namespace FluentHub.Views.UserPages
{
    public sealed partial class Issues : Page
    {
        string username = "";

        public Issues()
        {
            this.InitializeComponent();
        }

        private void ItemsRepeater_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.GetUserIssues(username);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            username = e.Parameter as string;

            if (username == $"{App.AuthedUserName}")
            {
                App.MainViewModel.MainTabItems[App.MainViewModel.SelectedIndex].PageUrl.Add($"{App.DefaultDomain}/issues");
                App.MainViewModel.FullUrl = $"{App.DefaultDomain}/issues";
                App.MainViewModel.UnpersedUrlString = $"{App.DefaultDomain}/issues";
            }

            base.OnNavigatedTo(e);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
