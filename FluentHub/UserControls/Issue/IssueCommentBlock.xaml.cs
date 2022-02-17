using FluentHub.Helpers;
using FluentHub.Services.OctokitEx;
using FluentHub.ViewModels.UserControls.Issue;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.UserControls.Issue
{
    public sealed partial class IssueCommentBlock : UserControl
    {
        public static readonly DependencyProperty PropertyViewModelProperty =
            DependencyProperty.Register(nameof(PropertyViewModel), typeof(IssueCommentBlockViewModel), typeof(IssueCommentBlock), new PropertyMetadata(0));

        public IssueCommentBlockViewModel PropertyViewModel
        {
            get => (IssueCommentBlockViewModel)GetValue(PropertyViewModelProperty);
            set
            {
                SetValue(PropertyViewModelProperty, value);
                ViewModel = PropertyViewModel;
                SetWebViewContents(ref CommentWebView);
            }
        }

        public IssueCommentBlock()
        {
            this.InitializeComponent();
        }

        private void SetWebViewContents(ref WebView webView)
        {
            _ = ViewModel.SetWebViewContentsAsync(webView);
        }

        private void CommentWebView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            WebViewHelpers.DisableWebViewVerticalScrolling(ref CommentWebView);
        }
    }
}
