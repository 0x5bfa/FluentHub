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
            if (e.Parameter != null)
            {
                UserName = e.Parameter as string;
            }
            else
            {
                UserName = App.SignedInUserName;
            }

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

        private async void UserSpecialReadmeWebView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            string returnStr = await UserSpecialReadmeWebView.InvokeScriptAsync("eval", new string[] { SetBodyOverFlowHiddenString });
            int heightScroll = 0;
            var heightScrollStr = await UserSpecialReadmeWebView.InvokeScriptAsync("eval", new[] { "document.body.scrollHeight.toString()" });

            if (int.TryParse(heightScrollStr, out heightScroll))
            {
                UserSpecialReadmeWebView.Height = heightScroll;
            }
        }

        string SetBodyOverFlowHiddenString
            = @"function SetBodyOverFlowHidden()
                {
                    document.body.style.overflow = 'hidden';
                }
                SetBodyOverFlowHidden();
            ";

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
