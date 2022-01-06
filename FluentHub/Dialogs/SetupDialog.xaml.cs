using FluentHub.Services.Octiokit;
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

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FluentHub.Dialogs
{
    public sealed partial class SetupDialog : ContentDialog
    {
        public SetupDialog()
        {
            this.InitializeComponent();
            AuthOptionCombobox.Items.Add("Unauthorized access");
            AuthOptionCombobox.Items.Add("Basic authentication");
            AuthOptionCombobox.Items.Add("OAuth token authentication");
            AuthOptionCombobox.Items.Add("Web authentication");
            AuthOptionCombobox.SelectedIndex = 3;
        }

        private void AuthOptionCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BasicAuth.Visibility = Visibility.Collapsed;
            OAuthTokenAuth.Visibility = Visibility.Collapsed;

            var selectedItem = AuthOptionCombobox.SelectedItem;

            switch (selectedItem.ToString())
            {
                case "Unauthorized access":
                    ContinueButton.Content = "Continue with no personal access";
                    ContinueButton.IsEnabled = true;
                    break;
                case "Basic authentication":
                    BasicAuth.Visibility = Visibility.Visible;
                    ContinueButton.Content = "Authorize with basic loggin";

                    if (UsernameTextBox.Text != "" && PaddwordTextBox.Password != "")
                        ContinueButton.IsEnabled = true;
                    else
                        ContinueButton.IsEnabled = false;
                    break;
                case "OAuth token authentication":
                    OAuthTokenAuth.Visibility = Visibility.Visible;
                    ContinueButton.Content = "Authorize with OAuth token";

                    if (TokenTextBox.Text != "")
                        ContinueButton.IsEnabled = true;
                    else
                        ContinueButton.IsEnabled = false;
                    break;
                case "Web authentication":
                    ContinueButton.Content = "Open with your browser";
                    ContinueButton.IsEnabled = true;
                    break;
            }
        }

        private void UsernameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (UsernameTextBox.Text != "" && PaddwordTextBox.Password != "")
                ContinueButton.IsEnabled = true;
            else
                ContinueButton.IsEnabled = false;
        }

        private void PaddwordTextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (UsernameTextBox.Text != "" && PaddwordTextBox.Password != "")
                ContinueButton.IsEnabled = true;
            else
                ContinueButton.IsEnabled = false;
        }

        private void TokenTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TokenTextBox.Text != "" && TokenTextBox.Text.Length == 40)
                ContinueButton.IsEnabled = true;
            else
                ContinueButton.IsEnabled = false;
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedIndex = AuthOptionCombobox.SelectedIndex;
            var webAuthenticationBroker = new WebAuthenticationBroker();

            switch (selectedIndex)
            {
                case 0:
                    break;

                case 1:
                    UserClient.GithubClient.Credentials = new Octokit.Credentials(UsernameTextBox.Text, PaddwordTextBox.Password);
                    break;

                case 2:
                    UserClient.GithubClient.Credentials = new Octokit.Credentials(TokenTextBox.Text);
                    break;

                case 3:
                    // webAuthenticationBroker...
                    break;
            }

            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(MainPage));
            this.Hide();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
