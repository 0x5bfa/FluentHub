using FluentHub.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Uwp;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Views.Users
{
    public sealed partial class DiscussionsPage : Page
    {
        public DiscussionsPage()
        {
            InitializeComponent();
            navigationService = App.Current.Services.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            //Helpers.NavigationHelpers.AddPageInfoToTabItem($"Discussions", "Viewer's discussions", $"https://github.com/discussions", "\uE737");
            var currentTab = navigationService.TabView.SelectedItem;
            currentTab.Header = "Discussions".GetLocalized();
            currentTab.Description = "Viewer's discussions";
            currentTab.Icon = new Microsoft.UI.Xaml.Controls.FontIconSource
            {
                Glyph = "\uE95D",
                FontFamily = new Windows.UI.Xaml.Media.FontFamily("/Assets/Glyphs/Octions.ttf#octions")
            };

            await ViewModel.GetUserDiscussions(e.Parameter as string);
        }
    }
}