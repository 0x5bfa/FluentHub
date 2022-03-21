using FluentHub.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views.Organizations
{
    public sealed partial class OverviewPage : Page
    {
        public OverviewPage()
        {
            InitializeComponent();
            navigationService = App.Current.Services.GetRequiredService<INavigationService>();
        }
        private readonly INavigationService navigationService;

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            string org = e.Parameter as string;

            //Helpers.NavigationHelpers.AddPageInfoToTabItem($"{org}", $"{org}'s overview", $"https://github.com/{org}", "\uEA27", true);
            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = $"{org}";
            currentItem.Description = $"{org}'s overview";
            currentItem.Url = $"https://github.com/{org}";
            currentItem.Icon = new muxc.FontIconSource
            {
                Glyph = "\uEA27",
                FontFamily = new Windows.UI.Xaml.Media.FontFamily("/Assets/Glyphs/Octions.ttf#octions")
            };

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