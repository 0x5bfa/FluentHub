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
                _ = LoadWebViewContent();
            }
        }
        #endregion

        public CustomWebView()
        {
            InitializeComponent();
        }

        private async void OnCommentWebViewNavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
            => await Helpers.WebViewHelpers.DisableWebViewVerticalScrollingAsync(sender);

        private async Task LoadWebViewContent()
        {
            Services.MarkdownApiHandler markdown = new();
            string html = await markdown.GetHtmlAsync(Comment?.BodyHTML, Comment?.Url, Helpers.ThemeHelper.ActualTheme.ToString().ToLower());
            WebViewControl.NavigateToString(html);
        }
    }
}
