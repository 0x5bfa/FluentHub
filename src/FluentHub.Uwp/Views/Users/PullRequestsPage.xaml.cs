using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.Users;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.Views.Users
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

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var param = e.Parameter as Models.FrameNavigationArgs;
            ViewModel.Login = param.Login;
            ViewModel.DisplayTitle = true;

            await ViewModel.LoadUserAsync(param.Login);

            SetCurrentTabItem();

            var command = ViewModel.RefreshPullRequestsPageCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }

        private void SetCurrentTabItem()
        {
            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Pull Requests";
            currentItem.Description = $"{ViewModel.User.Login}'s pull requests";
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/PullRequests.png"))
            };
        }
    }
}
