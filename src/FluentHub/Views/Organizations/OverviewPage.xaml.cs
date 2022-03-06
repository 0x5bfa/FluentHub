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

namespace FluentHub.Views.Organizations
{
    public sealed partial class OverviewPage : Page
    {
        private string OrganizationName { get; set; }

        public OverviewPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            OrganizationName = e.Parameter as string;

            base.OnNavigatedTo(e);
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UserPinnedItems pinnedItems = new UserPinnedItems();

            var repoIdList = await pinnedItems.Get(OrganizationName, false);

            var count = ViewModel.GetPinnedRepos(repoIdList);

            if (count != 0)
            {
                UserPinnedItemsBlock.Visibility = Visibility.Visible;
            }

            OrgPageFrame.Navigate(typeof(RepoListPage), OrganizationName);
        }
    }
}
