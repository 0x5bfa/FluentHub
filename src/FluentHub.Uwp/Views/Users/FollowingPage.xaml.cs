using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.Users;
using Microsoft.Extensions.DependencyInjection;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Uwp.Views.Users
{
    public sealed partial class FollowingPage : Page
    {
        public FollowingPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<FollowingViewModel>();
            navigationService = provider.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;
        public FollowingViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // e.g.) https://github.com/onein528?tab=following
            string url = e.Parameter as string;
            var uri = new Uri(url);
            string login;

            if (url == "fluenthub://followers")
            {
                login = App.Settings.SignedInUserName;
                ViewModel.DisplayTitle = true;
            }
            else
            {
                login = uri.Segments[1];
            }

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = $"Following";
            currentItem.Description = $"People {login} is following";
            currentItem.DisplayUrl = $"{login} / Following";
            currentItem.Url = url;
            currentItem.Icon = new Microsoft.UI.Xaml.Controls.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Accounts.png"))
            };

            var command = ViewModel.RefreshFollowingCommand;
            if (command.CanExecute(login))
                command.ExecuteAsync(login);
        }
    }
}