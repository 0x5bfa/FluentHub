using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.UserControls.Labels;
using Windows.UI.Xaml.Controls;

namespace FluentHub.Uwp.ViewModels.UserControls.Blocks
{
    public class IssueCommentBlockViewModel : ObservableObject
    {
        public IssueCommentBlockViewModel()
        {
            IsEditedLabel = new()
            {
                Color = "#36000000",
                Name = "Edited",
            };

            AuthorAssociationLabel = new()
            {
                Color = "#36000000",
            };
        }

        #region Fields and Properties
        private IssueComment issueComment;
        public IssueComment IssueComment { get => issueComment; set => SetProperty(ref issueComment, value); }

        private bool _disabledScrolling;
        public bool DisabledScrolling { get => _disabledScrolling; set => SetProperty(ref _disabledScrolling, value); }

        private bool _isContentLoaded;
        public bool IsContentLoaded { get => _isContentLoaded; set => SetProperty(ref _isContentLoaded, value); }

        private LabelControlViewModel _isEditedLabel;
        public LabelControlViewModel IsEditedLabel { get => _isEditedLabel; set => SetProperty(ref _isEditedLabel, value); }

        private WebView _commentWebView;
        public WebView CommentWebView { get => _commentWebView; set => SetProperty(ref _commentWebView, value); }

        private LabelControlViewModel _authorAssociationLabel;
        public LabelControlViewModel AuthorAssociationLabel { get => _authorAssociationLabel; set => SetProperty(ref _authorAssociationLabel, value); }
        #endregion

        public async Task SetMarkdownCommentToWebViewAsync(WebView webView)
        {
            if (IsContentLoaded || CommentWebView is not null)
                return;

            CommentWebView = webView;
            //CommentWebView.NavigationCompleted += OnNavigationCompleted;

            string authorAssociation = IssueComment?.AuthorAssociation.Humanize();
            if (authorAssociation != "None") AuthorAssociationLabel.Name = authorAssociation;

            MarkdownApiHandler markdown = new();
            var html = await markdown.GetHtmlAsync(IssueComment?.BodyHTML, IssueComment?.Url, ThemeHelper.ActualTheme.ToString().ToLower());
            CommentWebView.NavigateToString(html);
            IsContentLoaded = true;
        }

        private async void OnNavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            if (DisabledScrolling)
                return;

            var heightString = await CommentWebView.InvokeScriptAsync("eval", new[] { "document.body.scrollHeight.toString()" });

            if (!int.TryParse(heightString, out int height))
            {
                throw new Exception("Unable to get page height");
            }

            CommentWebView.Height = height;
            DisabledScrolling = true;
        }
    }
}
