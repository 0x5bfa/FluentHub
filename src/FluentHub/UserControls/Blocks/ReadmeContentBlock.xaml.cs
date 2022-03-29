using FluentHub.Helpers;
using FluentHub.ViewModels.Repositories;
using FluentHub.ViewModels.UserControls.Blocks;
using Octokit;
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

namespace FluentHub.UserControls.Blocks
{
    public sealed partial class ReadmeContentBlock : UserControl
    {
        #region propdp
        public static readonly DependencyProperty ContextViewModelProperty =
            DependencyProperty.Register(
                nameof(ContextViewModel),
                typeof(RepoContextViewModel),
                typeof(ReadmeContentBlock),
                null);

        public RepoContextViewModel ContextViewModel
        {
            get => (RepoContextViewModel)GetValue(ContextViewModelProperty);
            set
            {
                SetValue(ContextViewModelProperty, value);
                if (ContextViewModel != null)
                {
                    ViewModel.RepoContextViewModel = ContextViewModel;
                    ViewModel.GetMarkdownContent(ref ReadmeWebView);
                }
            }
        }
        #endregion

        public ReadmeContentBlock()
        {
            this.InitializeComponent();
        }

        private void ReadmeWebView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            WebViewHelpers.DisableWebViewVerticalScrolling(ref ReadmeWebView);
        }

        private void ReadmeWebView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            if (args.Uri != null) args.Cancel = true;
        }

        private void OnReadmeContentBlockLoaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
