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

            LoadReadmeContentBlockCommand = new AsyncRelayCommand<WebView>(LoadRepositoryReadmeAsync);
        }

        #region Fields and Properties
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;

        private string _htmlText;
        public string HtmlText { get => _htmlText; set => SetProperty(ref _htmlText, value); }

        private RepoContextViewModel contextViewModel;
        public RepoContextViewModel ContextViewModel { get => contextViewModel; set => SetProperty(ref contextViewModel, value); }

        public IAsyncRelayCommand LoadReadmeContentBlockCommand { get; }
        #endregion

        public async Task LoadRepositoryReadmeAsync(WebView2 webView)
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

                webView.NavigateToString(HtmlText);
            }
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadRepositoryReadmeAsync), ex);
                if (_messenger != null)
                {
                    UserNotificationMessage notification = new("Something went wrong", ex.Message, UserNotificationType.Error);
                    _messenger.Send(notification);
                }
                throw;
            }
        }
    }
}
