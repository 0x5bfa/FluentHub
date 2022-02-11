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
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace FluentHub.Views.UserPages
{
    public sealed partial class OverviewPage : Windows.UI.Xaml.Controls.Page
    {
        private string UserName { get; set; }

        public OverviewPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            UserName = e.Parameter as string;

            base.OnNavigatedTo(e);
        }

        private async void UserSpecialReadmeWebView_Loaded(object sender, RoutedEventArgs e)
        {
            Markdown markdown = new Markdown();
            Readme readme;

            try
            {
                readme = await App.Client.Repository.Content.GetReadme(UserName, UserName);
            }
            catch
            {
                return;
            }

            NoOverviewBlock.Visibility = Visibility.Collapsed;
            UserSpecialReadmeBlock.Visibility = Visibility.Visible;

            ReadMeLink.Content = string.Format("{0}/{1}", UserName, readme.Name);
            ReadMeLink.NavigateUri = new Uri(readme.HtmlUrl);

            string result = await markdown.GetHtml(readme.Content, "https://github.com/files-community/Files/blob/main/");
            UserSpecialReadmeWebView.NavigateToString(result);
        }

        private void UserSpecialReadmeWebView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            if (args.Uri != null) args.Cancel = true;
        }

        private void UserSpecialReadmeWebView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            WebViewHelpers.DisableWebViewVerticalScrolling(ref UserSpecialReadmeWebView);
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UserPinnedItems pinnedItems = new UserPinnedItems();

            var repoIdList = await pinnedItems.Get(UserName, true);

            var count = ViewModel.GetPinnedRepos(repoIdList);

            if (count != 0)
            {
                NoOverviewBlock.Visibility = Visibility.Collapsed;
                UserPinnedItemsBlock.Visibility = Visibility.Visible;
            }
        }
    }
}
