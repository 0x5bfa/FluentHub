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
using Windows.ApplicationModel.Resources;
using FluentHub.Views;
using FluentHub.Services.Auth;
using System.Threading.Tasks;
using FluentHub.ViewModels;


namespace FluentHub.Dialogs
{
    public sealed partial class SetupDialog : ContentDialog
    {
        public SetupDialog()
        {
            this.InitializeComponent();
        }

        private async void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private async void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            RequestAuthorization request = new RequestAuthorization();
            _ = await request.RequestGitHubIdentity();

            this.Hide();

            App.Settings.SetupProgress = true;
        }
    }
}