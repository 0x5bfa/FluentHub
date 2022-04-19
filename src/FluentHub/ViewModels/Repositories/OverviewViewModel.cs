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

            LoadOverviewPageCommand = new AsyncRelayCommand(LoadOverviewPageAsync);
        }
        #endregion

        #region fields
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;
        private Repository _repositoryBlock;
        private RepositoryDetails _repository;
        private LabelControlViewModel _repositoryVisibilityLabel;
        private string _viewerSubscriptionState;
        #endregion

        #region properties
        public Repository Repository { get => _repositoryBlock; set => SetProperty(ref _repositoryBlock, value); }
        public RepositoryDetails RepositoryDetails { get => _repository; set => SetProperty(ref _repository, value); }
        public IAsyncRelayCommand LoadOverviewPageCommand { get; }
        public LabelControlViewModel RepositoryVisibilityLabel { get => _repositoryVisibilityLabel; set => SetProperty(ref _repositoryVisibilityLabel, value); }
        public string ViewerSubscriptionState { get => _viewerSubscriptionState; set => SetProperty(ref _viewerSubscriptionState, value); }
        #endregion

        #region methods
        private async Task LoadOverviewPageAsync(CancellationToken token)
        {
            try
            {
                RepositoryQueries queries = new();
                var result = await queries.GetDetailsAsync(Repository.Owner, Repository.Name);

                if (result.DefaultBranchName != null)
                {
                    RepositoryDetails = result;
                }

                ViewerSubscriptionState = RepositoryDetails?.ViewerSubscription?.Humanize();
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
