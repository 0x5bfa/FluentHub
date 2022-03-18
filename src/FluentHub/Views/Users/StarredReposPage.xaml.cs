using FluentHub.Services.OctokitEx;
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
    public sealed partial class StarredReposPage : Page
    {
        public StarredReposPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string login = e.Parameter as string;

            Helpers.NavigationHelpers.AddPageInfoToTabItem($"Stars", $"{login}'s stars", $"https://github.com/{login}?tab=stars", "\uEA94", true);

            ViewModel.GetUserStarredRepos(login);

            base.OnNavigatedTo(e);
        }
    }
}
