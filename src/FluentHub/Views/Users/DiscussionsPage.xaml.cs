using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Views.Users
{
    public sealed partial class DiscussionsPage : Page
    {
        public DiscussionsPage() => InitializeComponent();        

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            //Helpers.NavigationHelpers.AddPageInfoToTabItem($"Discussions", "Viewer's discussions", $"https://github.com/discussions", "\uE737");

            await ViewModel.GetUserDiscussions(e.Parameter as string);
        }
    }
}