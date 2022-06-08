using FluentHub.Core;
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
    public class OverviewViewModel : ObservableObject
    {
        #region constructor
        public OverviewViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;

            _pinnedItems = new();
            PinnedItems = new(_pinnedItems);

            _repositories = new();
            Repositories = new(_repositories);

            LoadOrganizationOverviewAsyncCommand = new AsyncRelayCommand<string>(LoadOrganizationOverviewAsync);
        }
        #endregion

        #region fields
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;

        private readonly ObservableCollection<RepoButtonBlockViewModel> _pinnedItems;
        public ReadOnlyObservableCollection<RepoButtonBlockViewModel> PinnedItems { get; }

        private readonly ObservableCollection<RepoButtonBlockViewModel> _repositories;
        public ReadOnlyObservableCollection<RepoButtonBlockViewModel> Repositories { get; }

        public IAsyncRelayCommand LoadOrganizationOverviewAsyncCommand { get; }
        #endregion

        #region methods
        private async Task LoadOrganizationOverviewAsync(string org, CancellationToken token)
        {
            try
            {
                RepositoryQueries repoQueries = new();
                var repoItems = await repoQueries.GetAllAsync(org);

                _repositories.Clear();
                foreach (var item in repoItems)
                {
                    RepoButtonBlockViewModel viewModel = new()
                    {
                        Item = item,
                        DisplayDetails = false,
                        DisplayStarButton = false,
                    };

                    _repositories.Add(viewModel);
                }

                PinnedItemQueries queries = new();
                var pinnedItems = await queries.GetAllAsync(org);
                if (pinnedItems == null) return;

                _pinnedItems.Clear();
                foreach (var item in pinnedItems)
                {
                    RepoButtonBlockViewModel viewModel = new()
                    {
                        Item = item,
                        DisplayDetails = false,
                        DisplayStarButton = false,
                    };

                    _pinnedItems.Add(viewModel);
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                _logger?.Error("LoadOrganizationOverviewAsync", ex);
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
