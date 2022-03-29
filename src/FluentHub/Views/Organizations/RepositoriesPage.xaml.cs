using FluentHub.Services;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views.Organizations
{
    public sealed partial class RepositoriesPage : Page
    {
        public RepositoriesPage()
        {
            InitializeComponent();
            navigationService = App.Current.Services.GetRequiredService<INavigationService>();
        }
        private readonly INavigationService navigationService;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string org = e.Parameter as string;

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = $"{org}'s repositories";
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