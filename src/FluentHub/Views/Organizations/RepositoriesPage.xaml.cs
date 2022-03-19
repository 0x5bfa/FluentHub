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

namespace FluentHub.Views.Organizations
{
    public sealed partial class RepositoriesPage : Page
    {
        public RepositoriesPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string org = e.Parameter as string;

            Helpers.NavigationHelpers.AddPageInfoToTabItem($"{org}", $"{org}'s repositoryes", $"https://github.com/orgs/{org}/repositories", "\uEA27", true);

            ViewModel.GetUserRepos(org);

            base.OnNavigatedTo(e);
        }
    }
}
