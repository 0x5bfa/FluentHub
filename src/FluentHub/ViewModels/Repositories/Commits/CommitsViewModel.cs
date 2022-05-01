using FluentHub.Backend;
using FluentHub.Models;
using FluentHub.Octokit.Models;
using FluentHub.Octokit.Queries.Repositories;
using FluentHub.ViewModels.UserControls.ButtonBlocks;
using Humanizer;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace FluentHub.ViewModels.Repositories.Commits
{
    public class CommitsViewModel : ObservableObject
    {
        #region constructor
        public CommitsViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;
            _messenger = messenger;
            _items = new();
            Items = new(_items);

            LoadCommitsPageCommand = new AsyncRelayCommand(LoadCommitsPageAsync);
        }
        #endregion

        #region fields
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;
        private RepoContextViewModel contextViewModel;
        private readonly ObservableCollection<CommitButtonBlockViewModel> _items;
        #endregion

        #region properties
        public RepoContextViewModel ContextViewModel { get => contextViewModel; set => SetProperty(ref contextViewModel, value); }
        public ReadOnlyObservableCollection<CommitButtonBlockViewModel> Items { get; }
        public IAsyncRelayCommand LoadCommitsPageCommand { get; }
        #endregion

        #region methods
        private async Task LoadCommitsPageAsync(CancellationToken token)
        {
            try
            {
                CommitQueries queries = new();
                var items = await queries.GetAllAsync(
                    ContextViewModel.Name,
                    ContextViewModel.Owner,
                    ContextViewModel.BranchName,
                    ContextViewModel.Path);

                _items.Clear();
                foreach (var item in items)
                {
                    CommitButtonBlockViewModel viewModel = new()
                    {
                        CommitItem = item,
                    };

                    _items.Add(viewModel);
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                _logger?.Error("LoadReleasesPageAsync", ex);
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
