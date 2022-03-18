using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Views.Users
{
    public sealed partial class PullRequestsPage : Page
    {
        public PullRequestsPage() => InitializeComponent();

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            string login = e.Parameter as string;

            //Helpers.NavigationHelpers.AddPageInfoToTabItem($"Pull requests", "Viewer's pull requests", $"https://github.com/organizations", "\uE737");

            await ViewModel.GetRepoPRs(login);
        }
    }
}