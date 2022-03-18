using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Views.Users
{
    public sealed partial class FollowersPage : Page
    {
        public FollowersPage() => InitializeComponent();

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            string login = e.Parameter as string;

            //Helpers.NavigationHelpers.AddPageInfoToTabItem($"Followers", $"{login}'s followers", $"https://github.com/{login}?tab=followers", "\uE737");

            await ViewModel.GetFollowersList(login);

            base.OnNavigatedTo(e);
        }
    }
}