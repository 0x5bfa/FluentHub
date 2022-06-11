using FluentHub.Services;
using FluentHub.ViewModels.AppSettings;
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

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<AccountsViewModel>();
            navigationService = App.Current.Services.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;
        public AccountsViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Accounts";
            currentItem.Description = "Users signed in FluentHub";
            currentItem.Url = "fluenthub://settings?page=accounts";
            currentItem.DisplayUrl = $"Settings / Accounts";
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Settings.targetsize-96.png"))
            };

            var command = ViewModel.LoadSignedInLoginsCommand;
            if (command.CanExecute(null))
                command.Execute(null) ;
        }
    }
}