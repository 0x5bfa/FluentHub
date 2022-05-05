using FluentHub.Backend;
using FluentHub.Models;
using FluentHub.Octokit.Models;
using FluentHub.Octokit.Queries.Repositories;
using FluentHub.ViewModels.UserControls.Labels;
using Humanizer;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml;

namespace FluentHub.ViewModels.Repositories
{
    public class OverviewViewModel : ObservableObject
    {
        #region constructor
        public OverviewViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;
            _messenger = messenger;

            RepositoryVisibilityLabel = new()
            {
                Color = "#64000000",
            };

            LoadOverviewPageCommand = new AsyncRelayCommand(LoadOverviewPageAsync);
        }
        #endregion

        #region fields and properties
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;

        private Repository _repository;
        public Repository Repository { get => _repository; set => SetProperty(ref _repository, value); }

        private LabelControlViewModel _repositoryVisibilityLabel;
        public LabelControlViewModel RepositoryVisibilityLabel { get => _repositoryVisibilityLabel; set => SetProperty(ref _repositoryVisibilityLabel, value); }

        private string _viewerSubscriptionState;
        public string ViewerSubscriptionState { get => _viewerSubscriptionState; set => SetProperty(ref _viewerSubscriptionState, value); }

        public IAsyncRelayCommand LoadOverviewPageCommand { get; }
        #endregion

        #region methods
        private async Task LoadOverviewPageAsync(CancellationToken token)
        {
            try
            {
                RepositoryQueries queries = new();
                Repository = await queries.GetDetailsAsync(Repository.Owner.Login, Repository.Name);

                ViewerSubscriptionState = Repository.ViewerSubscription?.Humanize();
                RepositoryVisibilityLabel.Name = Repository.IsPrivate ? "Private" : "Public";
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                _logger?.Error("LoadOverviewPageAsync", ex);
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
