using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Views.Users
{
    public sealed partial class StarredReposPage : Page
    {
        public StarredReposPage() => InitializeComponent();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string login = e.Parameter as string;

            //Helpers.NavigationHelpers.AddPageInfoToTabItem($"Stars", $"{login}'s stars", $"https://github.com/{login}?tab=stars", "\uE737");

            ViewModel.GetUserStarredRepos(login);

            base.OnNavigatedTo(e);
        }
    }
}