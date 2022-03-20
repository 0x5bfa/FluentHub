using FluentHub.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Uwp;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Views.Home
{
    public sealed partial class ActivitiesPage : Page
    {
        public ActivitiesPage()
        {
            InitializeComponent();
            navigationService = App.Current.Services.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;


        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            // save navigation info

            var currentTab = navigationService.TabView.SelectedItem;
            currentTab.Header = "Activities";
            currentTab.Description = "Viewer's activities";
            currentTab.Icon = new Microsoft.UI.Xaml.Controls.FontIconSource
            {
                Glyph = "\uECAD"
            };

            //"Viewer's activities", "https://github.com", "\uECAD");

            await ViewModel.GetAllActivityForCurrent(e.Parameter as string);
        }
    }
}
