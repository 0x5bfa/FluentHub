using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Views.Users
{
    public sealed partial class OrganizationsPage : Page
    {
        public OrganizationsPage() => InitializeComponent();

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            string login = e.Parameter as string;

            //Helpers.NavigationHelpers.AddPageInfoToTabItem($"Organizations", "Viewer's organizations", $"https://github.com/organizations", "\uE737");

            await ViewModel.GetUserOrganizations(login);
        }
    }
}