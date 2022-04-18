using FluentHub.Helpers;
using FluentHub.ViewModels.UserControls.Blocks;
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

namespace FluentHub.UserControls.Blocks
{
    public sealed partial class IssueCommentBlock : UserControl
    {
        #region propdp
        public static readonly DependencyProperty PropertyViewModelProperty =
            DependencyProperty.Register(nameof(PropertyViewModel), typeof(IssueCommentBlockViewModel), typeof(IssueCommentBlock), new PropertyMetadata(0));

        public IssueCommentBlockViewModel PropertyViewModel
        {
            get => (IssueCommentBlockViewModel)GetValue(PropertyViewModelProperty);
            set
            {
                SetValue(PropertyViewModelProperty, value);
                ViewModel = PropertyViewModel;
                ViewModel?.SetWebViewContentsAsync(CommentWebView);
            }
        }
        #endregion

        public IssueCommentBlock()
        {
            this.InitializeComponent();
        }

        private void OnCommentWebViewNavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            WebViewHelpers.DisableWebViewVerticalScrolling(ref CommentWebView);
        }
    }
}
