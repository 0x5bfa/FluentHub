using FluentHub.Services;
using FluentHub.ViewModels.Repositories.PullRequests;
using Microsoft.Extensions.DependencyInjection;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views.Repositories.PullRequests
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
            var url = e.Parameter as string;
            var uri = new Uri(url);
            var pathSegments = uri.AbsolutePath.Split("/").ToList();
            pathSegments.RemoveAt(0);

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Pull Requests";
            currentItem.Description = $"Pull Requests";
            currentItem.Url = url;
            currentItem.DisplayUrl = $"{pathSegments[0]} / {pathSegments[1]} / Pulls";
            currentItem.Icon = new Microsoft.UI.Xaml.Controls.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/PullRequests.targetsize-96.png"))
            };

            var command = ViewModel.RefreshPullRequestsPageCommand;
            if (command.CanExecute($"{pathSegments[0]}/{pathSegments[1]}"))
                command.Execute($"{pathSegments[0]}/{pathSegments[1]}");
        }
    }
}
