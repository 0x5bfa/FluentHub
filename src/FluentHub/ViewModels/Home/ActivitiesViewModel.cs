using FluentHub.Backend;
using FluentHub.Octokit.Models;
using FluentHub.Models;
using FluentHub.Octokit.Queries.Users;
using FluentHub.ViewModels.UserControls.Blocks;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.Home
{
    public class ActivitiesViewModel : ObservableObject
    {
        #region constructor
        public ActivitiesViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;
            _messenger = messenger;
            _activities = new();
            Activities = new(_activities);

            RefreshActivitiesCommand = new AsyncRelayCommand<string>(RefreshActivitiesAsync);
        }
        #endregion

        #region fields and properties
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;

        private readonly ObservableCollection<ActivityBlockViewModel> _activities;
        public ReadOnlyObservableCollection<ActivityBlockViewModel> Activities { get; }

        public IAsyncRelayCommand RefreshActivitiesCommand { get; }
        #endregion

        #region methods
        private async Task RefreshActivitiesAsync(string login, CancellationToken token)
        {
            try
            {
                ActivityQueries queries = new();
                var items = await queries.GetAllAsync(login);
                if (items == null) return;

                foreach (var item in items)
                {
                    ActivityBlockViewModel viewModel = new()
                    {
                        Payload = item,
                    };

                    _activities.Add(viewModel);
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                _logger?.Error("RefreshActivitiesAsync", ex);
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
