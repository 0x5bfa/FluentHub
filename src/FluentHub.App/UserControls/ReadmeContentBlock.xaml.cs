using FluentHub.App.Extensions;
using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.ViewModels.Repositories;
using FluentHub.App.ViewModels.UserControls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Web.WebView2.Core;

namespace FluentHub.App.UserControls
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
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<ReadmeContentBlockViewModel>();
        }

        public ReadmeContentBlockViewModel ViewModel { get; }

        private async void OnReadmeContentWebView2NavigationCompleted(WebView2 sender, CoreWebView2NavigationCompletedEventArgs args)
            => await ReadmeContentWebView2.HandleResize();

        private void OnReadmeContentWebView2NavigationStarting(WebView2 sender, CoreWebView2NavigationStartingEventArgs args)
        {
            if (args.Uri != null)
            {
                args.Cancel = true;
            }
        }

        private void OnReadmeContentBlockUserControlLoaded(object sender, RoutedEventArgs e)
        {
            ViewModel.ContextViewModel = ContextViewModel;

            var command = ViewModel.LoadReadmeContentBlockCommand;
            if (command.CanExecute(ReadmeContentWebView2))
                command.Execute(ReadmeContentWebView2);
        }
    }
}
