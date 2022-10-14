using FluentHub.App.Helpers;
using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.Utils;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.ViewModels.UserControls
{
    public class IssueCommentBlockViewModel : ObservableObject
    {
        public IssueCommentBlockViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;
        }

        #region Fields and Properties
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;

        private IssueComment _issueComment;
        public IssueComment IssueComment { get => _issueComment; set => SetProperty(ref _issueComment, value); }

        private int _thumbsUpCount;
        public int ThumbsUpCount { get => _thumbsUpCount; set => SetProperty(ref _thumbsUpCount, value); }

        private int _thumbsDownCount;
        public int ThumbsDownCount { get => _thumbsDownCount; set => SetProperty(ref _thumbsDownCount, value); }

        private int _laughCount;
        public int LaughCount { get => _laughCount; set => SetProperty(ref _laughCount, value); }

        private int _hoorayCount;
        public int HoorayCount { get => _hoorayCount; set => SetProperty(ref _hoorayCount, value); }

        private int _confusedCount;
        public int ConfusedCount { get => _confusedCount; set => SetProperty(ref _confusedCount, value); }

        private int _heartCount;
        public int HeartCount { get => _heartCount; set => SetProperty(ref _heartCount, value); }

        private int _rocketCount;
        public int RocketCount { get => _rocketCount; set => SetProperty(ref _rocketCount, value); }

        private int _eyesCount;
        public int EyesCount { get => _eyesCount; set => SetProperty(ref _eyesCount, value); }
        #endregion

        public async Task SetMarkdownCommentToWebViewAsync(WebView2 webView2)
        {
            var settingTheme = Helpers.ThemeHelpers.RootTheme.ToString();
            var appTheme = App.Current.RequestedTheme.ToString().ToLower();

            if (settingTheme == "default")
                settingTheme = appTheme;

            MarkdownApiHandler markdown = new();
            var html = await markdown.GetHtmlAsync(IssueComment.BodyHTML, IssueComment.Url, settingTheme);

            // WebView2 is very unstable

            //// https://github.com/microsoft/microsoft-ui-xaml/issues/3714
            //await webView2.EnsureCoreWebView2Async();

            //// https://github.com/microsoft/microsoft-ui-xaml/issues/1967
            //// It is no longer the plan for WebView2 to support ms-appx-web:/// and ms-appx-data:///.
            //// Instead of using these proprietary protocols the SetVirtualHostNameToFolderMapping API is recommended.
            //var CoreWebView2 = webView2.CoreWebView2;
            //if (CoreWebView2 != null)
            //{
            //    CoreWebView2.SetVirtualHostNameToFolderMapping(
            //        "fluenthub.app", "Assets/",
            //        Microsoft.Web.WebView2.Core.CoreWebView2HostResourceAccessKind.Allow);
            //}

            //webView2.NavigateToString(html);
        }
    }
}
