using FluentHub.Services.Auth;
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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Views.SignIn
{
    public sealed partial class SignInPage : Page
    {
        public SignInPage()
        {
            this.InitializeComponent();
            Window.Current.SetTitleBar(AppTitleBar);
        }

        private async void CancelButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private async void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            RequestAuthorization request = new RequestAuthorization();
            _ = await request.RequestGitHubIdentity();

            App.Settings.SetupProgress = true;
            SetupProgressRing.IsActive = true;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(IntroPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
        }
    }
}
