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

namespace FluentHub.ViewModels.Repositories.Codes
{
    public class ReleasesViewModel : ObservableObject
    {
        #region constructor
        public ReleasesViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;
            _messenger = messenger;
            _items = new();
            Items = new(_items);

            LoadReleasesPageCommand = new AsyncRelayCommand(LoadReleasesPageAsync);
        }
        #endregion

        #region fields
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;
        #endregion

        #region properties
        private RepoContextViewModel contextViewModel;
        public RepoContextViewModel ContextViewModel { get => contextViewModel; set => SetProperty(ref contextViewModel, value); }

        private readonly ObservableCollection<Release> _items;
        public ReadOnlyObservableCollection<Release> Items { get; }

        public IAsyncRelayCommand LoadReleasesPageCommand { get; }
        #endregion

        #region methods
        private async Task LoadReleasesPageAsync(CancellationToken token)
        {
            try
            {
                ReleaseQueries queries = new();
                var items = await queries.GetAllAsync(ContextViewModel.Owner, ContextViewModel.Name);

                _items.Clear();

                foreach (var item in items) _items.Add(item);
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
