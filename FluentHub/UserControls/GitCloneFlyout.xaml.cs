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

namespace FluentHub.UserControls
{
    public sealed partial class GitCloneFlyout : UserControl
    {
        #region RepositoryIdProperty
        public static readonly DependencyProperty RepositoryIdProperty
        = DependencyProperty.Register(
              nameof(RepositoryId),
              typeof(long),
              typeof(GitCloneFlyout),
              new PropertyMetadata(null)
            );

        public long RepositoryId
        {
            get => (long)GetValue(RepositoryIdProperty);
            set => SetValue(RepositoryIdProperty, value);
        }
        #endregion

        private Octokit.Repository repository { get; set; }

        public GitCloneFlyout()
        {
            this.InitializeComponent();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //repository = await App.Client.Repository.Get(RepositoryId);

            //CloneUriTextBox.Text = repository.CloneUrl;
            CloneDescriptionTextBlock.Text = "Use Git or checkout with SVN using the web URL.";
        }

        private void GitCloneFlyoutNavView_ItemInvoked(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs args)
        {
            switch (args.InvokedItemContainer.Tag.ToString())
            {
                case "Https":
                    //CloneUriTextBox.Text = repository.CloneUrl;
                    CloneDescriptionTextBlock.Text = "Use Git or checkout with SVN using the web URL.";
                    break;
                case "Ssh":
                    //CloneUriTextBox.Text = repository.SshUrl;
                    CloneDescriptionTextBlock.Text = "Use a password-protected SSH key.";
                    break;
                case "GitHubCli":
                    //CloneUriTextBox.Text = repository.GitUrl;
                    CloneDescriptionTextBlock.Text = "Work fast with our official CLI.";
                    break;
            }
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            //var dp = new Windows.ApplicationModel.DataTransfer.DataPackage();
            //dp.SetText(CloneUriTextBox.Text);
            //Windows.ApplicationModel.DataTransfer.Clipboard.SetContent(dp);
        }
    }
}
