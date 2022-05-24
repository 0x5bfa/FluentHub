using FluentHub.Services;
using FluentHub.ViewModels.Users;
using Microsoft.Extensions.DependencyInjection;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Views.Users
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
            currentItem.Description = $"{login}'s followers";
            currentItem.Url = url;
            currentItem.Icon = new Microsoft.UI.Xaml.Controls.FontIconSource
            {
                Glyph = "\uEA36",
                FontFamily = new Windows.UI.Xaml.Media.FontFamily("/Assets/Glyphs/Octions.ttf#octions")
            };

            var command = ViewModel.RefreshFollowingCommand;
            if (command.CanExecute(login))
                command.ExecuteAsync(login);
        }
    }
}