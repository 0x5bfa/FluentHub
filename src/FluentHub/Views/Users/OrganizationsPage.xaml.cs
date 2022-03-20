using FluentHub.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Uwp;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Views.Users
{
    public sealed partial class OrganizationsPage : Page
    {
        public OrganizationsPage()
        {
            InitializeComponent();
            navigationService = App.Current.Services.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            string login = e.Parameter as string;

            //Helpers.NavigationHelpers.AddPageInfoToTabItem($"Organizations", "Viewer's organizations", $"https://github.com/organizations", "\uE737");
            var currentTab = navigationService.TabView.SelectedItem;
            currentTab.Header = "Organizations".GetLocalized();
            currentTab.Description = "Viewer's organizations";
            currentTab.Icon = new Microsoft.UI.Xaml.Controls.FontIconSource
            {
                Glyph = "\uEA27F",
                FontFamily = new Windows.UI.Xaml.Media.FontFamily("/Assets/Glyphs/Octions.ttf#octions")
            };
            await ViewModel.GetUserOrganizations(login);
        }
    }
}