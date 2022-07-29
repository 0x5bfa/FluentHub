using FluentHub.Uwp.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels;
using FluentHub.Uwp.ViewModels.Repositories.PullRequests;
using FluentHub.Uwp.ViewModels.Repositories.Issues;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.Views.Repositories.PullRequests
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
            var param = e.Parameter as FrameNavigationArgs;

            await ViewModel.LoadRepositoryAsync(param.Login, param.Name);

            var command = ViewModel.RefreshPullRequestPageCommand;
            if (command.CanExecute(null))
                await command.ExecuteAsync(null);

            SetCurrentTabItem();
        }

        private void SetCurrentTabItem()
        {
            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = $"{ViewModel.PullItem.Title} · #{ViewModel.PullItem.Number}";
            currentItem.Description = currentItem.Header;
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
