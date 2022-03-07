using FluentHub.Helpers;
using FluentHub.Services.OctokitEx;
using Octokit;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Views.Users
{
    public sealed partial class OverviewPage : Windows.UI.Xaml.Controls.Page
    {
        private string Login { get; set; }

        public OverviewPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Login = e.Parameter as string;

            base.OnNavigatedTo(e);
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var count = await ViewModel.GetPinnedRepos(Login);

                if (count != 0)
                {
                    NoOverviewBlock.Visibility = Visibility.Collapsed;
                    UserPinnedItemsBlock.Visibility = Visibility.Visible;
                }

                var repo = await App.Client.Repository.Get(Login, Login);
                UserSpecialReadmeBlock.RepositoryId = repo.Id;
            }
            catch (ApiException apiEx)
            {
                Log.Error(apiEx, apiEx.Message);
            }
        }
    }
}
