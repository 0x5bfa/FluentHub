using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.Utils;
using FluentHub.Uwp.ViewModels.Repositories;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.ViewModels.UserControls
{
    public class ReadmeContentBlockViewModel : ObservableObject
    {
        public ReadmeContentBlockViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;

            LoadReadmeContentBlockCommand = new AsyncRelayCommand<WebView2>(LoadRepositoryReadmeAsync);
        }

        #region Fields and Properties
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;

        private string _htmlText;
        public string HtmlText { get => _htmlText; set => SetProperty(ref _htmlText, value); }

        private bool _isLoaded;
        public bool IsLoaded { get => _isLoaded; set => SetProperty(ref _isLoaded, value); }

        private RepoContextViewModel contextViewModel;
        public RepoContextViewModel ContextViewModel { get => contextViewModel; set => SetProperty(ref contextViewModel, value); }

        public IAsyncRelayCommand LoadReadmeContentBlockCommand { get; }
        #endregion

        public async Task LoadRepositoryReadmeAsync(WebView2 webView2)
        {
            try
            {
                MarkdownApiHandler queries = new();
                HtmlText = await queries.GetHtmlAsync(
                    ContextViewModel.Repository.Owner.Login,
                    ContextViewModel.Repository.Name,
                    ContextViewModel.BranchName,
                    ThemeHelpers.RootTheme.ToString().ToLower()
                    );

                if (HtmlText == null) return;

                // https://github.com/microsoft/microsoft-ui-xaml/issues/3714
                await webView2.EnsureCoreWebView2Async();

                // https://github.com/microsoft/microsoft-ui-xaml/issues/1967
                // It is no longer the plan for WebView2 to support ms-appx-web:/// and ms-appx-data:///.
                // Instead of using these proprietary protocols the SetVirtualHostNameToFolderMapping API is recommended.
                var core_wv2 = webView2.CoreWebView2;
                if (core_wv2 != null)
                {
                    core_wv2.SetVirtualHostNameToFolderMapping(
                        "fluenthub.app", "Assets/",
                        Microsoft.Web.WebView2.Core.CoreWebView2HostResourceAccessKind.Allow);
                }

                webView2.NavigateToString(HtmlText);

                IsLoaded = true;
            }
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadRepositoryReadmeAsync), ex);

                IsLoaded = false;
            }
        }
    }
}
