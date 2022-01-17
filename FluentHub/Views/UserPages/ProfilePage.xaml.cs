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


namespace FluentHub.Views.UserPages
{
    public sealed partial class ProfilePage : Windows.UI.Xaml.Controls.Page
    {
        private string requestedUsername { get; set; } = "";
        private User user { get; set; }

        public ProfilePage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            requestedUsername = e.Parameter as string;
            user = await App.Client.User.Get(requestedUsername);

            base.OnNavigatedTo(e);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SetUserInfo();
        }


        private void SetUserInfo()
        {
            // Avator
            BitmapImage avatorImage = new BitmapImage(new Uri(user.AvatarUrl));
            UserAvatorImage.Source = avatorImage;

            // Username
            if (user.Login != "")
            {
                Username.Text = user.Login;
            }

            // Fullname
            if (user.Name != "")
            {
                FullName.Text = user.Name;
            }

            // Bio
            if (user.Bio != "")
            {
                UserBioTextBlock.Text = user.Bio;
                UserBioBlock.Visibility = Visibility.Visible;
            }

            //Link
            if (user.Blog != "")
            {
                LinkButton.Content = user.Blog;
                LinkButton.NavigateUri = new Uri(user.Blog);
                LinkBlock.Visibility = Visibility.Visible;
            }

            // Location
            if (LocationTextBlock.Text != "")
            {
                LocationTextBlock.Text = user.Location;
                LocationBlock.Visibility = Visibility.Visible;
            }

            // FF
            FollowerCount.Text = user.Followers.ToString();
            FollowingCount.Text = user.Following.ToString();
        }
    }
}
