using FluentHub.Services;
using FluentHub.ViewModels;
using FluentHub.ViewModels.Repositories.PullRequests;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views.Repositories.PullRequests
{
    public sealed partial class PullRequestPage : Page
    {
        public PullRequestPage()
        {
            InitializeComponent();
            MainPageViewModel.PullRequestContentFrame.Navigating += OnPullRequestContentFrameNavigating;

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<PullRequestViewModel>();
            navigationService = provider.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;
        public PullRequestViewModel ViewModel { get; }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var url = e.Parameter as string;
            var uri = new Uri(url);
            var pathSegments = uri.AbsolutePath.Split("/").ToList();
            pathSegments.RemoveAt(0);

            var command = ViewModel.RefreshPullRequestPageCommand;
            if (command.CanExecute(url))
                await command.ExecuteAsync(url);

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = $"{ViewModel.PullItem.Title} · Pull request #{ViewModel.PullItem.Number} · {ViewModel.PullItem.OwnerLogin}/{ViewModel.PullItem.Name}";
            currentItem.Description = currentItem.Header;
            currentItem.Url = url;
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/PullRequests.png"))
            };
        }

        private void OnPullRequestNavigationViewSelectionChanged(muxc.NavigationView sender, muxc.NavigationViewSelectionChangedEventArgs args)
        {
            switch (args.SelectedItemContainer?.Tag?.ToString())
            {
                case "conversation":
                    PullRequestContentFrame.Navigate(typeof(ConversationPage), ViewModel.PullItem);
                    break;
                case "commits":
                    PullRequestContentFrame.Navigate(typeof(CommitsPage), ViewModel.PullItem);
                    break;
                case "filechanges":
                    PullRequestContentFrame.Navigate(typeof(FileChangesPage), ViewModel.PullItem);
                    break;
            }
        }

        private void OnPullRequestContentFrameNavigating(object sender, NavigatingCancelEventArgs e)
        {
            e.Cancel = true;
            PullRequestContentFrame.Navigate(e.SourcePageType, e.Parameter);
        }
    }
}
