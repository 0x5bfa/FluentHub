using FluentHub.Helpers;
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

namespace FluentHub.UserControls.Blocks
{
    public sealed partial class ReadmeContentBlock : UserControl
    {
        #region RepositoryIdProperty
        public static readonly DependencyProperty RepositoryIdProperty
        = DependencyProperty.Register(
              nameof(RepositoryId),
              typeof(long),
              typeof(ReadmeContentBlock),
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

        #region RepositoryPathProperty
        public static readonly DependencyProperty RepositoryPathProperty
        = DependencyProperty.Register(
              nameof(RepositoryPath),
              typeof(string),
              typeof(ReadmeContentBlock),
              new PropertyMetadata(null)
            );

        public string RepositoryPath
        {
            get => (string)GetValue(RepositoryPathProperty);
            set => SetValue(RepositoryPathProperty, value);
        }
        #endregion

        public ReadmeContentBlock()
        {
            this.InitializeComponent();
        }

        private async void LoadContents()
        {
            if (RepositoryId == 0) return;

            var repo = await App.Client.Repository.Get(RepositoryId);

            Octokit.Queries.MarkdownQueries markdown = new();

            Readme readme;

            try
            {
                readme = await App.Client.Repository.Content.GetReadme(RepositoryId);
            }
            catch
            {
                return;
            }

            string missedPath = "https://raw.githubusercontent.com/" + repo.Owner.Login + "/" + repo.Name + "/" + repo.DefaultBranch + "/";

            string html = await markdown.GetHtmlAsync(readme.Content, missedPath, ThemeHelper.ActualTheme.ToString().ToLower());

            ReadmeWebView.NavigateToString(html);

            ReadmeBlock.Visibility = Visibility.Visible;
        }

        private void ReadmeWebView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            WebViewHelpers.DisableWebViewVerticalScrolling(ref ReadmeWebView);

            ReadmeLoadingProgressBar.IsIndeterminate = false;
            ReadmeLoadingProgressBar.Visibility = Visibility.Collapsed;
        }

        private void ReadmeWebView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            if (args.Uri != null) args.Cancel = true;
        }
    }
}
