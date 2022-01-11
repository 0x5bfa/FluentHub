using FluentHub.Services.Auth;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FluentHub.Views.UserPages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Home : Page
    {
        public Home()
        {
            this.InitializeComponent();
        }

        private void HomeNavView_ItemInvoked(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs args)
        {
            string tag = args.InvokedItemContainer.Tag.ToString();

            switch (tag)
            {
                case "Profile":
                    HomeNavViewContent.Navigate(typeof(ProfilePage), $"{App.AuthedUserName}");
                    break;
                case "Notifications":
                    break;
                case "Activities":
                    break;

                case "Issues":
                    HomeNavViewContent.Navigate(typeof(Issues), $"{App.AuthedUserName}");
                    break;
                case "PullRequests":
                    break;
                case "Discussions":
                    break;
                case "Repositories":
                    break;
                case "Organizations":
                    break;
                case "Starred":
                    break;
            }
        }

        private void HomeNavView_Loaded(object sender, RoutedEventArgs e)
        {
            HomeNavViewContent.Navigate(typeof(ProfilePage), $"{App.AuthedUserName}");
        }
    }
}
