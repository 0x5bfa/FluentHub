using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views.Organizations
{
    public sealed partial class ProfilePage : Page
    {
        private string OrganizationName { get; set; }

        public ProfilePage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            OrganizationName = e.Parameter as string;

            await ViewModel.GetOrganization(OrganizationName);
            UpdateVisibility();

            base.OnNavigatedTo(e);
        }

        private void UpdateVisibility()
        {
            if (!string.IsNullOrEmpty(OrgDescriptionTextBlock.Text))
            {
                OrgDescriptionTextBlock.Visibility = Visibility.Visible;
            }

            if (!string.IsNullOrEmpty(LocationTextBlock.Text))
            {
                LocationBlock.Visibility = Visibility.Visible;
            }

            if (!string.IsNullOrEmpty(LinkHyperlinkButton.Content as string))
            {
                LinkHyperlinkButton.NavigateUri = new UriBuilder(LinkHyperlinkButton.Content as string).Uri;
                LinkBlock.Visibility = Visibility.Visible;
            }

            if (!string.IsNullOrEmpty(MailHyperlinkButton.Content as string))
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
