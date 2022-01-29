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
        public OverviewPage()
        {
            this.InitializeComponent();
        }

        private async void UserSpecialReadmeWebView_Loaded(object sender, RoutedEventArgs e)
        {
            Markdown markdown = new Markdown();

            var readme = await App.Client.Repository.Content.GetReadme($"{App.SignedInUserName}", $"{App.SignedInUserName}");

            if (readme == null)
            {
                return;
            }
            else
            {
                UserSpecialReadmeBlock.Visibility = Visibility.Visible;
            }

            ReadMeLink.Content = string.Format("{0}/{1}", $"{App.SignedInUserName}", readme.Name);
            ReadMeLink.NavigateUri = new Uri(readme.HtmlUrl);

            string result = await markdown.FormatRenderedMarkdownToHtml(await readme.GetHtmlContent());
            UserSpecialReadmeWebView.NavigateToString(result);
        }

        private void UserSpecialReadmeWebView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            if(args.Uri != null)
            {
                args.Cancel = true;
            }
        }

        private async void ItemsRepeater_Loaded(object sender, RoutedEventArgs e)
        {
            UserPinnedItems pinnedItems = new UserPinnedItems();

            var repoIdList = await pinnedItems.Get(App.SignedInUserName);

            ViewModel.GetUserPinnedRepos(repoIdList);
        }
    }
}
