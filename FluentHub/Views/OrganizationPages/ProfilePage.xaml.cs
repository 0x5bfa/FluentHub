using Octokit;
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

namespace FluentHub.Views.OrganizationPages
{
    public sealed partial class ProfilePage : Windows.UI.Xaml.Controls.Page
    {
        private string OrganizationName { get; set; }
        private Organization Organization { get; set; }


        public ProfilePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            OrganizationName = e.Parameter as string;

            base.OnNavigatedTo(e);
        }

        private void OrgNavView_SelectionChanged(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            switch (args.SelectedItemContainer.Tag.ToString())
            {
                case "Repositories":
                    OrgNavViewContent.Navigate(typeof(OverviewPage), OrganizationName);
                    break;
                case "People":
                    break;
                case "Teams":
                    break;
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Organization = await App.Client.Organization.Get(OrganizationName);

            // Avator
            UserAvatorImage.Source = new BitmapImage(new Uri(Organization.AvatarUrl));

            // Fullname
            if (!string.IsNullOrEmpty(Organization.Name))
            {
                FullName.Text = Organization.Name;
            }

            if (!string.IsNullOrEmpty(Organization.Description))
            {
                UserBioTextBlock.Text = Organization.Description;
                UserBioTextBlock.Visibility = Visibility.Visible;
            }

            if (!string.IsNullOrEmpty(Organization.Location))
            {
                LocationTextBlock.Text = Organization.Location;
                LocationBlock.Visibility = Visibility.Visible;
            }

            if (!string.IsNullOrEmpty(Organization.Blog))
            {
                LinkButton.Content = Organization.Blog;
                LinkButton.NavigateUri = new UriBuilder(Organization.Blog).Uri;
                LinkBlock.Visibility = Visibility.Visible;
            }

            if (!string.IsNullOrEmpty(Organization.Email))
            {
                MailButton.Content = Organization.Email;
                LinkButton.NavigateUri = new Uri("mailto:" + Organization.Email);
                MailBlock.Visibility = Visibility.Visible;
            }

            OrgNavViewContent.Navigate(typeof(OverviewPage), OrganizationName);
        }
    }
}
