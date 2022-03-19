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
        public OverviewPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            string org = e.Parameter as string;

            Helpers.NavigationHelpers.AddPageInfoToTabItem($"{org}", $"{org}'s overview", $"https://github.com/{org}", "\uEA27", true);

            await ViewModel.GetPinnedRepos(org);

            // Avoid duplicates
            OrgPageFrame.Navigate(typeof(RepositoriesPage), org);

            UpdateVisibility();

            base.OnNavigatedTo(e);
        }

        private void UpdateVisibility()
        {
            if (ViewModel.OrgPinnedItems.Count() != 0)
            {
                UserPinnedItemsBlock.Visibility = Visibility.Visible;
            }
            else
            {
                NoOverviewTextBlock.Visibility = Visibility.Visible;
            }
        }
    }
}
