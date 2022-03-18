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

namespace FluentHub.Views.Users
{
    public sealed partial class RepositoriesPage : Page
    {
        public RepositoriesPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            string login = e.Parameter as string;

            Helpers.NavigationHelpers.AddPageInfoToTabItem($"Repositories", $"{login}'s repositories", $"https://github.com/{login}?tab=repositories", "\uEA52", true);

            await ViewModel.GetUserRepos(login);

            base.OnNavigatedTo(e);
        }
    }
}
