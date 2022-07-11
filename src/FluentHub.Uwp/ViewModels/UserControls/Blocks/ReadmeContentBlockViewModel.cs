using FluentHub.Uwp.Utils;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Octokit.Queries.Repositories;
using FluentHub.Uwp.ViewModels.Repositories;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FluentHub.Uwp.ViewModels.UserControls.Blocks
{
    public class ReadmeContentBlockViewModel : ObservableObject
    {
        #region constructor
        public ReadmeContentBlockViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;
            _messenger = messenger;

            LoadReadmeContentBlockCommand = new AsyncRelayCommand<WebView>(LoadReadmeContentBlockAsync);
        }
        #endregion

        #region fields
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;

        private string _htmlText;
        private RepoContextViewModel contextViewModel;
        #endregion

        #region properties
        public string HtmlText { get => _htmlText; set => SetProperty(ref _htmlText, value); }
        public RepoContextViewModel ContextViewModel { get => contextViewModel; set => SetProperty(ref contextViewModel, value); }

        public IAsyncRelayCommand LoadReadmeContentBlockCommand { get; }
        #endregion

        #region methods
        public async Task LoadReadmeContentBlockAsync(WebView webView)
        {
            try
            {
                MarkdownApiHandler queries = new();
                HtmlText = await queries.GetHtmlAsync(
                    ContextViewModel.Repository.Owner.Login,
                    ContextViewModel.Repository.Name,
                    ContextViewModel.BranchName,
                    ThemeHelper.ActualTheme.ToString().ToLower()
                    );

                if (HtmlText == null) return;

                webView.NavigateToString(HtmlText);
            }
            catch (Exception ex)
            {
                _logger?.Error("LoadReadmeContentBlockAsync", ex);
                if (_messenger != null)
                {
                    UserNotificationMessage notification = new("Something went wrong", ex.Message, UserNotificationType.Error);
                    _messenger.Send(notification);
                }
                throw;
            }
        }
        #endregion
    }
}
