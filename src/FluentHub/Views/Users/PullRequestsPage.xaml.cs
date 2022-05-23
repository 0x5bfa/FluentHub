using FluentHub.Services;
using FluentHub.ViewModels.Users;
using Microsoft.Extensions.DependencyInjection;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Views.Users
{
    public sealed partial class PullRequestsPage : Page
    {
        public PullRequestsPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<PullRequestsViewModel>();
            navigationService = provider.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;
        public PullRequestsViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // e.g.) https://github.com/pulls
            string url = e.Parameter as string;
            string login = App.Settings.SignedInUserName;
            ViewModel.DisplayTitle = true;

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "PullRequests";
            currentItem.Description = "Viewer's pull requests";
            currentItem.Url = url;
            currentItem.Icon = new Microsoft.UI.Xaml.Controls.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/PullRequests.png"))
            };

            var command = ViewModel.RefreshPullRequestsPageCommand;
            if (command.CanExecute(login))
                command.Execute(login);
        }
    }
}
