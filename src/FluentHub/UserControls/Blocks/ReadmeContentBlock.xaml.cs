using FluentHub.Helpers;
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
        public static readonly DependencyProperty ViewMoedelProperty
        = DependencyProperty.Register(
              nameof(ViewModel),
              typeof(ReadmeContentBlockViewModel),
              typeof(ReadmeContentBlock),
              new PropertyMetadata(null)
            );

        public ReadmeContentBlockViewModel ViewModel
        {
            get => (ReadmeContentBlockViewModel)GetValue(ViewMoedelProperty);
            set
            {
                SetValue(ViewMoedelProperty, value);
                this.DataContext = ViewModel;
                ViewModel?.GetMarkdownContent(ref ReadmeWebView);
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
    }
}
