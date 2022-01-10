using Octokit;
using FluentHub.ViewModels;
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
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FluentHub.Views.UserPage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProfilePage : Windows.UI.Xaml.Controls.Page
    {
        private string profileUsername = App.AuthedUserName;
        User user;

        public ProfilePage()
        {
            this.InitializeComponent();

        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            // Update main tab strip
            MainPageViewModel.MainTabItems[MainPageViewModel.SelectedIndex].Header = "Profile";

            profileUsername = e.Parameter as string;
            user = await App.Client.User.Get(profileUsername);

            SetUserInfo();

            base.OnNavigatedTo(e);
        }

        private void SetUserInfo()
        {
            // Avator
            BitmapImage avatorImage = new BitmapImage(new Uri(user.AvatarUrl));
            UserAvatorImage.Source = avatorImage;

            // Username
            if (user.Login != null)
            {
                Username.Text = user.Login;
            }

            // Fullname
            if (user.Name != null)
            {
                FullName.Text = user.Name;
            }

            // Bio
            if (user.Bio != null)
            {
                UserBioTextBlock.Text = user.Bio;
                UserBioBlock.Visibility = Visibility.Visible;
            }

            //Link
            if (user.Blog != null)
            {
                LinkButton.Content = user.Blog;
                LinkButton.NavigateUri = new Uri(user.Blog);
                LinkBlock.Visibility = Visibility.Visible;
            }

            // Location
            if (LocationTextBlock.Text != null)
            {
                LocationTextBlock.Text = user.Location;
                LocationBlock.Visibility = Visibility.Visible;
            }

            // FF
            FollowerCount.Text = user.Followers.ToString();
            FollowingCount.Text = user.Following.ToString();
        }

        private void UserNavView_Loaded(object sender, RoutedEventArgs e)
        {
            UserNavViewContent.Navigate(typeof(Overview));
        }

        private void UserNavView_SelectionChanged(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            string tag = args.SelectedItemContainer.Tag.ToString();

            switch (tag)
            {
                case "Overview":
                    UserNavViewContent.Navigate(typeof(Overview));
                    break;
                case "Repositories":
                    UserNavViewContent.Navigate(typeof(Repositories));
                    break;
            }
        }
    }
}
