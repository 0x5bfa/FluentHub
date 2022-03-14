using FluentHub.ViewModels.Repositories;
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
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.UserControls
{
    public sealed partial class GitCloneFlyout : UserControl
    {
        public static readonly DependencyProperty CommonRepoViewModelProperty =
            DependencyProperty.Register(
                nameof(CommonRepoViewModel),
                typeof(CommonRepoViewModel),
                typeof(GitCloneFlyout),
                new PropertyMetadata(0));

        public CommonRepoViewModel CommonRepoViewModel
        {
            get { return (CommonRepoViewModel)GetValue(CommonRepoViewModelProperty); }
            set { SetValue(CommonRepoViewModelProperty, value); }
        }

        Repository Repository;

        public GitCloneFlyout()
        {
            this.InitializeComponent();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Repository = await App.Client.Repository.Get(CommonRepoViewModel.Owner, CommonRepoViewModel.Name);

            CloneUriTextBox.Text = Repository.CloneUrl;
            CloneDescriptionTextBlock.Text = "Use Git or checkout with SVN using the web URL.";
        }

        private void GitCloneFlyoutNavView_ItemInvoked(muxc.NavigationView sender, muxc.NavigationViewItemInvokedEventArgs args)
        {
            switch (args.InvokedItemContainer.Tag.ToString())
            {
                case "Https":
                    CloneUriTextBox.Text = Repository.CloneUrl;
                    CloneDescriptionTextBlock.Text = "Use Git or checkout with SVN using the web URL.";
                    break;
                case "Ssh":
                    CloneUriTextBox.Text = Repository.SshUrl;
                    CloneDescriptionTextBlock.Text = "Use a password-protected SSH key.";
                    break;
                case "GitHubCli":
                    CloneUriTextBox.Text = Repository.GitUrl;
                    CloneDescriptionTextBlock.Text = "Work fast with our official CLI.";
                    break;
            }
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            var dp = new Windows.ApplicationModel.DataTransfer.DataPackage();
            dp.SetText(CloneUriTextBox.Text);
            Windows.ApplicationModel.DataTransfer.Clipboard.SetContent(dp);
        }
    }
}
