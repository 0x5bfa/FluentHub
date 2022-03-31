using FluentHub.Backend;
using FluentHub.Models;
using FluentHub.Octokit.Queries.Users;
using FluentHub.ViewModels.UserControls.ButtonBlocks;
using Humanizer;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
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

            RefreshDiscussionsCommand = new AsyncRelayCommand<string>(RefreshDiscussionsAsync, CanRefreshDiscussions);
        }
        #endregion

        #region fields
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;
        private readonly ObservableCollection<DiscussionButtonBlockViewModel> _discussions;
        #endregion

        #region properties        
        public ReadOnlyObservableCollection<DiscussionButtonBlockViewModel> DiscussionItems { get; }
        public IAsyncRelayCommand RefreshDiscussionsCommand { get; }
        #endregion

        #region methods        
        private bool CanRefreshDiscussions(string username) => !string.IsNullOrEmpty(username);

        private async Task RefreshDiscussionsAsync(string username, CancellationToken token)
        {
            try
            {
                DiscussionQueries queries = new();
                var items = await queries.GetOverviewAllAsync(username);

                if (items == null) return;

                _discussions.Clear();
                foreach (var item in items)
                {
                    DiscussionButtonBlockViewModel viewModel = new()
                    {
                        DiscussionItem = item,
                        NameWithOwner = $"{item.Owner} / {item.Name} #{item.Number}",
                        UpdatedAtHumanized = item.UpdatedAt.Humanize()
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