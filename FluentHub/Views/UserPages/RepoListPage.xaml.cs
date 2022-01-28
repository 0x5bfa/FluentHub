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


namespace FluentHub.Views.UserPages
{
    public sealed partial class RepoListPage : Page
    {
        public RepoListPage()
        {
            this.InitializeComponent();
        }

        private void ItemsRepeater_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.GetUserRepos(App.SignedInUserName);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
