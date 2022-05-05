using ColorCode;
using FluentHub.Backend;
using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Octokit.Queries.Repositories;
using FluentHub.ViewModels.Repositories;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FluentHub.ViewModels.UserControls.Blocks
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
                RepositoryQueries queries = new();
                HtmlText = await queries.GetReadmeHtml(ContextViewModel.Owner, ContextViewModel.Name, ContextViewModel.BranchName, ThemeHelper.ActualTheme.ToString().ToLower());

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
