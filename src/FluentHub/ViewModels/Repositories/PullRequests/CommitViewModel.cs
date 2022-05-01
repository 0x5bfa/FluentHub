using FluentHub.Backend;
using FluentHub.Models;
using FluentHub.Octokit.Models;
using FluentHub.Octokit.Queries.Repositories;
using FluentHub.ViewModels.UserControls.Blocks;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.Repositories.PullRequests
{
    public class CommitViewModel : ObservableObject
    {
        #region constructor
        public CommitViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;
            _messenger = messenger;
            _diffViewModels = new();

            DiffViewModels = new(_diffViewModels);
            LoadCommitPageCommand = new AsyncRelayCommand(LoadCommitPageAsync);
        }
        #endregion

        #region properties
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;

        private Commit _commitItem;
        public Commit CommitItem { get => _commitItem; set => SetProperty(ref _commitItem, value); }

        private CommitDetails _commitDetails;
        public CommitDetails CommitDetails { get => _commitDetails; set => SetProperty(ref _commitDetails, value); }

        private ObservableCollection<DiffBlockViewModel> _diffViewModels;
        public ReadOnlyObservableCollection<DiffBlockViewModel> DiffViewModels { get; }

        public IAsyncRelayCommand LoadCommitPageCommand { get; }
        #endregion

        #region methods
        private async Task LoadCommitPageAsync(CancellationToken token)
        {
            try
            {
                DiffQueries queries = new();
                var files = await queries.GetAllAsync(
                    CommitItem.Owner,
                    CommitItem.Name,
                    CommitItem.PullRequestNumber);

                if (CommitDetails.ChangedFiles.Count() == 0) return;

                _diffViewModels.Clear();
                foreach (var item in files)
                {
                    DiffBlockViewModel viewModel = new()
                    {
                        ChangedFile = item,
                    };

                    _diffViewModels.Add(viewModel);
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                _logger?.Error("LoadCommitPageAsync", ex);
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
