using FluentHub.App.Services;
using FluentHub.App.ViewModels.AppSettings.Accounts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.AppSettings.Accounts
{
    public sealed partial class AccountPage : Page
    {
        public AccountPage()
        {
            InitializeComponent();

            ViewModel = Ioc.Default.GetRequiredService<AccountViewModel>();
            navigationService = Ioc.Default.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;
        public AccountViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Account";
            currentItem.Description = "Account Settings";
            currentItem.Icon = new ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Accounts.png"))
            };

            var command = ViewModel.LoadSignedInLoginsCommand;
            if (command.CanExecute(null))
                command.Execute(null) ;
        }

        private void OnSignOutButton_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = (Frame)App.Window.Content;
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
