using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.ViewModels.UserControls.Blocks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Uwp.UserControls.Blocks
{
    public sealed partial class IssueCommentBlock : UserControl
    {
        #region propdp
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                nameof(ViewModel),
                typeof(IssueCommentBlockViewModel),
                typeof(IssueCommentBlock),
                new PropertyMetadata(null)
                );

        public IssueCommentBlockViewModel ViewModel
        {
            get => (IssueCommentBlockViewModel)GetValue(ViewModelProperty);
            set
            {
                SetValue(ViewModelProperty, value);
                ViewModel?.SetWebViewContentsAsync(CommentWebView);
            }
        }
        #endregion

        public IssueCommentBlock() => InitializeComponent();

        private bool WebViewIsNavigatedSuccessfully { get; set; }

        private async void OnCommentWebViewNavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            await WebViewHelpers.DisableWebViewVerticalScrollingAsync(CommentWebView);
            WebViewIsNavigatedSuccessfully = true;
        }

        private async void OnWebViewSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (CommentWebView != null && WebViewIsNavigatedSuccessfully)
            {
                await WebViewHelpers.DisableWebViewVerticalScrollingAsync(CommentWebView);
            }
        }
    }
}
