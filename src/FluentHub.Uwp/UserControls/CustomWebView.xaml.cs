using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.Uwp.UserControls
{
    public sealed partial class CustomWebView : UserControl
    {
        #region propdp
        public static readonly DependencyProperty CommentProperty =
            DependencyProperty.Register(
                nameof(Comment),
                typeof(IssueComment),
                typeof(CustomWebView),
                new PropertyMetadata(null)
                );

        public IssueComment Comment
        {
            get => (IssueComment)GetValue(CommentProperty);
            set
            {
                SetValue(CommentProperty, value);

                //if (string.IsNullOrEmpty(LoadedIssueCommentIds.FirstOrDefault(x => x.Key == Comment.Id.ToString()).Key))
                //{
                //    LoadedIssueCommentIds.Add(Comment.Id.ToString(), false);
                //    _ = LoadWebViewContent();
                //}
            }
        }
        #endregion

        public CustomWebView()
        {
            InitializeComponent();

            //if (LoadedIssueCommentIds is null)
            //    LoadedIssueCommentIds = new();
        }

        //private static Dictionary<string, bool> LoadedIssueCommentIds { get; set; }

        //private async void OnCommentWebViewNavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        //{
        //    if (LoadedIssueCommentIds[Comment.Id.ToString()] == false)
        //    {
        //        await DisableWebViewVerticalScrollingAsync(sender);
        //        LoadedIssueCommentIds[Comment.Id.ToString()] = true;
        //    }
        //}

        //private async Task LoadWebViewContent()
        //{
        //}

        //private async Task DisableWebViewVerticalScrollingAsync(WebView webView)
        //{
        //    _ = await webView.InvokeScriptAsync("eval", new[] { @"function SetBodyOverFlowHidden() { document.body.style.overflow = 'hidden'; } SetBodyOverFlowHidden();" });

        //    // get the total height
        //    var heightString = await webView.InvokeScriptAsync("eval", new[] { "document.body.scrollHeight.toString()" });

        //    if (!int.TryParse(heightString, out int height))
        //    {
        //        throw new Exception("Unable to get page height");
        //    }

        //    // resize the webview to the content
        //    webView.Height = height;
        //}

        private async void OnWebViewControlLoaded(object sender, RoutedEventArgs e)
        {
            Services.MarkdownApiHandler markdown = new();
            string html = await markdown.GetHtmlAsync(Comment?.BodyHTML, Comment?.Url, Helpers.ThemeHelpers.ActualTheme.ToString().ToLower());
            WebViewControl.NavigateToString(html);
        }
    }
}
