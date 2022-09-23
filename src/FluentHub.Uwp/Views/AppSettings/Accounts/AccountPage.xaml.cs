using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.AppSettings.Accounts;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.Views.AppSettings.Accounts
{
    public sealed partial class AccountPage : Page
    {
        public AccountPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<AccountViewModel>();
            navigationService = App.Current.Services.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;
        public AccountViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Account";
            currentItem.Description = "Account Settings";
            currentItem.Url = "fluenthub://settings/account";
            currentItem.DisplayUrl = $"Settings / Account";
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Accounts.png"))
            };

            var command = ViewModel.LoadSignedInLoginsCommand;
            if (command.CanExecute(null))
                command.Execute(null) ;
        }

        private void OnSignOutButton_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = (Frame)Window.Current.Content;
            rootFrame.Navigate(typeof(SignIn.IntroPage));
        }

        private void OnYourGitHubAccountClick(object sender, RoutedEventArgs e)
        {
        }

        private void OnYourInfoClick(object sender, RoutedEventArgs e)
        {
        }

        private void OnSecurityClick(object sender, RoutedEventArgs e)
        {
        }

        private void OnOtherUsersClick(object sender, RoutedEventArgs e)
        {
            //navigationService.Navigate<AppSettings.MainSettingsPage>($"fluenthub://settings/account/otherusers", new SuppressNavigationTransitionInfo());
        }
    }
}
