using FluentHub.Backend;
using FluentHub.Octokit.Models;
using FluentHub.Models;
using FluentHub.Octokit.Queries.Users;
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

namespace FluentHub.ViewModels.Users
{
    public class DiscussionsViewModel : ObservableObject
    {
        #region constructor        
        public DiscussionsViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;
            _discussions = new();
            DiscussionItems = new(_discussions);

            RefreshDiscussionsCommand = new AsyncRelayCommand<string>(RefreshDiscussionsAsync);
        }
        #endregion

        #region fields
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;

        private readonly ObservableCollection<DiscussionButtonBlockViewModel> _discussions;
        public ReadOnlyObservableCollection<DiscussionButtonBlockViewModel> DiscussionItems { get; }

        public IAsyncRelayCommand RefreshDiscussionsCommand { get; }
        #endregion

        #region methods
        private bool CanRefreshDiscussions(string username) => !string.IsNullOrEmpty(username);

        private async Task RefreshDiscussionsAsync(string login, CancellationToken token)
        {
            try
            {
                DiscussionQueries queries = new();
                var items = await queries.GetAllAsync(login);
                if (items == null) return;

                _discussions.Clear();
                foreach (var item in items)
                {
                    DiscussionButtonBlockViewModel viewModel = new()
                    {
                        Item = item,
                    };

                    _discussions.Add(viewModel);
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                _logger?.Error("RefreshDiscussionsAsync", ex);
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
