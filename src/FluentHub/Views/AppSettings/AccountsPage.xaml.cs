using FluentHub.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views.AppSettings
{
    public sealed partial class AccountsPage : Page
    {
        public AccountsPage()
        {
            InitializeComponent();
            navigationService = App.Current.Services.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Accounts";
            currentItem.Description = "Users signed in FluentHub";
            currentItem.Url = "fluenthub://settings?page=accounts";
            currentItem.Icon = new muxc.FontIconSource
            {
                Glyph = "\uE713",
            };
        }
    }
}