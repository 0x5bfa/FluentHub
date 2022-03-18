using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Views.Users
{
    public sealed partial class FollowingPage : Page
    {
        public FollowingPage() => InitializeComponent();

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            string login = e.Parameter as string;

            //Helpers.NavigationHelpers.AddPageInfoToTabItem($"Following", $"{login}'s followers", $"https://github.com/{login}?tab=following", "\uE737");

            await ViewModel.GetFollowingList(login);

            base.OnNavigatedTo(e);
        }
    }
}
