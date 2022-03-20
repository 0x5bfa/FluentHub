using FluentHub.Services.Navigation;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views.Organizations
{
    public sealed partial class RepositoriesPage : Page
    {
        public RepositoriesPage() => InitializeComponent();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string org = e.Parameter as string;

            //Helpers.NavigationHelpers.AddPageInfoToTabItem($"{org}", $"{org}'s repositoryes", $"https://github.com/orgs/{org}/repositories", "\uEA27", true);
            var currentItem = App.Current.Services.GetService<ITabItemView>().NavigationHistory.CurrentItem;
            currentItem.Header = $"{org}";
            currentItem.Description = $"{org}'s repositories";
            currentItem.Url = $"https://github.com/orgs/{org}/repositories";
            currentItem.Icon = new muxc.FontIconSource
            {
                Glyph = "\uEA27",
                FontFamily = new Windows.UI.Xaml.Media.FontFamily("/Assets/Glyphs/Octions.ttf#octions")
            };

            ViewModel.GetUserRepos(org);

            base.OnNavigatedTo(e);
        }
    }
}