using FluentHub.Uwp.Extensions;
using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.ViewModels.UserControls;
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
                ViewModel?.SetMarkdownCommentToWebViewAsync(CommentWebView);
            }
        }
        #endregion

        public IssueCommentBlock() => InitializeComponent();

        private bool WebViewIsNavigatedSuccessfully { get; set; }

        private async void OnCommentWebViewNavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
            => await((WebView)sender).HandleResize();

        private async void OnWebViewSizeChanged(object sender, SizeChangedEventArgs e)
            => await ((WebView)sender).HandleResize();

        private async void OnCommentWebViewLoaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
