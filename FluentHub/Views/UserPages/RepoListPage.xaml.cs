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
        private string UserName { get; set; }

        public RepoListPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            UserName = e.Parameter as string;

            base.OnNavigatedTo(e);
        }

        private void ItemsRepeater_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.GetUserRepos(UserName);
        }
    }
}
