using FluentHub.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Views.Users
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
            string login = e.Parameter as string;

            //Helpers.NavigationHelpers.AddPageInfoToTabItem($"{login}'s overview", $"https://github.com/{login}?tab=overview", $"https://github.com/{login}?tab=overview", "\uE77B");
            var currentTab = navigationService.TabView.SelectedItem;
            currentTab.Header = $"{login}'s overview";
            currentTab.Description = "";
            currentTab.Icon = new Microsoft.UI.Xaml.Controls.FontIconSource
            {
                Glyph = "\uE77B"
            };

            await ViewModel.GetPinnedRepos(login);
            await ViewModel.GetSpecialRepoId(login);
            UpdateVisibility();

            base.OnNavigatedTo(e);
        }

        private void UpdateVisibility()
        {
            if (ViewModel.PinnedRepos.Any())
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