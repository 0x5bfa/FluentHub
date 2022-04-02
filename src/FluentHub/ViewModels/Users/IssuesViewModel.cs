using FluentHub.Backend;
using FluentHub.Models;
using FluentHub.Octokit.Models;
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
    public class IssuesViewModel : ObservableObject
    {
        #region constructor
        public IssuesViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;
            _messenger = messenger;
            _issueItems = new();
            IssueItems = new(_issueItems);

            RefreshIssuesCommand = new AsyncRelayCommand<string>(RefreshIssuesAsync);
        }
        #endregion

        #region fields
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;
        private readonly ObservableCollection<IssueButtonBlockViewModel> _issueItems;
        #endregion

        #region properties
        public ReadOnlyObservableCollection<IssueButtonBlockViewModel> IssueItems { get; }
        public IAsyncRelayCommand RefreshIssuesCommand { get; }
        #endregion
        #region methods
        private async Task RefreshIssuesAsync(string login, CancellationToken token)
        {
            try
            {
                IssueQueries queries = new();
                List<Issue> items;

                items = login == null ?
                    await queries.GetAllAsync() :
                    await queries.GetAllAsync(login);

                if (items == null) return;

                _issueItems.Clear();
                foreach (var item in items)
                {
                    IssueButtonBlockViewModel viewModel = new()
                    {
                        IssueItem = item,
                        NameWithOwner = item.OwnerLogin + "/" + item.Name + " #" + item.Number,
                        UpdatedAtHumanized = item.UpdatedAt.Humanize()
                    };

                    _issueItems.Add(viewModel);
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                _logger?.Error("RefreshIssuesAsync", ex);
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