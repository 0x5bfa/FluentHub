using FluentHub.Helpers;
using FluentHub.ViewModels.Repositories;
using FluentHub.ViewModels.UserControls.Blocks;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
            set => SetValue(ContextViewModelProperty, value);
        }
        #endregion

        public ReadmeContentBlock()
        {
            this.InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<ReadmeContentBlockViewModel>();
        }

        public ReadmeContentBlockViewModel ViewModel { get; }

        private void ReadmeWebView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            WebViewHelpers.DisableWebViewVerticalScrolling(ref ReadmeWebView);

            // TODO: window sizw changed
        }

        private void ReadmeWebView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            if (args.Uri != null) args.Cancel = true;
        }

        private void OnReadmeContentBlockLoaded(object sender, RoutedEventArgs e)
        {
            ViewModel.ContextViewModel = ContextViewModel;

            var command = ViewModel.LoadReadmeContentBlockCommand;
            if (command.CanExecute(ReadmeWebView))
                command.Execute(ReadmeWebView);
        }
    }
}
