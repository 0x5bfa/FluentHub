using FluentHub.Backend;
using FluentHub.Octokit.Models;
using FluentHub.Models;
using FluentHub.Octokit.Queries.Organizations;
using FluentHub.ViewModels.UserControls.ButtonBlocks;
using Humanizer;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.Organizations
{
    public class RepositoriesViewModel : ObservableObject
    {
        #region constructor
        public RepositoriesViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;
            _repositories = new();
            Repositories = new(_repositories);

            RefreshRepositoriesCommand = new AsyncRelayCommand<string>(RefreshRepositoriesAsync);
        }
        #endregion

        #region fields
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;
        private readonly ObservableCollection<RepoButtonBlockViewModel> _repositories;
        #endregion

        #region properties
        public ReadOnlyObservableCollection<RepoButtonBlockViewModel> Repositories { get; }
        public IAsyncRelayCommand RefreshRepositoriesCommand { get; }
        #endregion

        #region methods
        private async Task RefreshRepositoriesAsync(string login, CancellationToken token)
        {
            try
            {
                RepositoryQueries queries = new();
                var items = await queries.GetAllAsync(login);
                if (items == null) return;

                _repositories.Clear();
                foreach (var item in items)
                {
                    RepoButtonBlockViewModel viewModel = new()
                    {
                        Item = item,
                        DisplayDetails = true,
                        DisplayStarButton = true,
                    };

                    _repositories.Add(viewModel);
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                if (!ex.Message.Contains("has enabled OAuth App access restrictions, meaning that data access to third-parties is limited."))
                {
                    _logger?.Error("RefreshRepositoriesAsync", ex);
                    if (_messenger != null)
                    {
                        UserNotificationMessage notification = new("Something went wrong", ex.Message, UserNotificationType.Error);
                        _messenger.Send(notification);
                    }
                    throw;
                }
            }
        }
        #endregion
    }
}
