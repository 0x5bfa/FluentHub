using FluentHub.Helpers;
using FluentHub.Services.OctokitEx;
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
    public sealed partial class ReadmeContentBlock : UserControl
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
            set
            {
                SetValue(RepositoryIdProperty, value);
                LoadContents();
            }
        }
        #endregion

        public ReadmeContentBlock()
        {
            this.InitializeComponent();
        }

        private async void LoadContents()
        {
            if (RepositoryId == 0)
            {
                return;
            }

            var repo = await App.Client.Repository.Get(RepositoryId);

            Markdown markdown = new Markdown();

            Readme readme;

            try
            {
                readme = await App.Client.Repository.Content.GetReadme(RepositoryId);
            }
            catch
            {
                return;
            }

            string result = await markdown.GetHtml(readme.Content, repo.HtmlUrl + "/blob/main/");
            ReadmeWebView.NavigateToString(result);

            ReadmeBlock.Visibility = Visibility.Visible;
        }

        private void ReadmeWebView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            WebViewHelpers.DisableWebViewVerticalScrolling(ref ReadmeWebView);
        }

        private void ReadmeWebView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            if (args.Uri != null) args.Cancel = true;
        }
    }
}
