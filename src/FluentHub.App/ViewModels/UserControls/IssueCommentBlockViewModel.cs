using FluentHub.App.Helpers;
using FluentHub.App.Services;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.ViewModels.UserControls
{
    public class IssueCommentBlockViewModel : ObservableObject
    {
        public IssueCommentBlockViewModel()
        {
        }

        #region Fields and Properties
        private IssueComment issueComment;
        public IssueComment IssueComment { get => issueComment; set => SetProperty(ref issueComment, value); }

        private bool _disabledScrolling;
        public bool DisabledScrolling { get => _disabledScrolling; set => SetProperty(ref _disabledScrolling, value); }

        private bool _isContentLoaded;
        public bool IsContentLoaded { get => _isContentLoaded; set => SetProperty(ref _isContentLoaded, value); }

        private WebView2 _commentWebView;
        public WebView2 CommentWebView { get => _commentWebView; set => SetProperty(ref _commentWebView, value); }
        #endregion

        public async Task SetMarkdownCommentToWebViewAsync(WebView2 webView)
        {
            MarkdownApiHandler markdown = new();
            var html = await markdown.GetHtmlAsync(IssueComment?.BodyHTML, IssueComment?.Url, ThemeHelpers.RootTheme.ToString().ToLower());

            webView.NavigateToString(html);
        }
    }
}
