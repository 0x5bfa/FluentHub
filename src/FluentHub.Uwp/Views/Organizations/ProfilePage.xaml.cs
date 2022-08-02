using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.Organizations;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.Views.Organizations
{
    public sealed partial class ProfilePage : Page
    {
        private string OrganizationName { get; set; }

        public ProfilePage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<ProfileViewModel>();
            navigationService = provider.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;
        public ProfileViewModel ViewModel { get; }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            OrganizationName = e.Parameter as string;

            await ViewModel.LoadOrganizationAsync(OrganizationName);
            UpdateVisibility();

            base.OnNavigatedTo(e);
        }

        private void UpdateVisibility()
        {
            if (string.IsNullOrEmpty(OrgDescriptionTextBlock.Text) is false)
            {
                OrgDescriptionTextBlock.Visibility = Visibility.Visible;
            }

            if (string.IsNullOrEmpty(LocationTextBlock.Text) is false)
            {
                LocationBlock.Visibility = Visibility.Visible;
            }

            if (string.IsNullOrEmpty(LinkHyperlinkButton.Content as string) is false)
            {
                LinkHyperlinkButton.NavigateUri = new UriBuilder(LinkHyperlinkButton.Content as string).Uri;
                LinkBlock.Visibility = Visibility.Visible;
            }

            if (string.IsNullOrEmpty(MailHyperlinkButton.Content as string) is false)
            {
                MailHyperlinkButton.NavigateUri = new Uri("mailto:" + MailHyperlinkButton.Content);
                MailBlock.Visibility = Visibility.Visible;
            }

            OrgNavViewContent.Navigate(typeof(OverviewPage), OrganizationName);
        }

        private void OrgNavView_ItemInvoked(muxc.NavigationView sender, muxc.NavigationViewItemInvokedEventArgs args)
        {
            _ = args.InvokedItemContainer.Tag.ToString() switch
            {
                "Overview" =>     OrgNavViewContent.Navigate(typeof(OverviewPage), OrganizationName),
                "Repositories" => OrgNavViewContent.Navigate(typeof(RepositoriesPage), OrganizationName),
                _ =>              OrgNavViewContent.Navigate(typeof(OverviewPage), OrganizationName),
            };
        }
    }
}
