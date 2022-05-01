using FluentHub.Backend;
using FluentHub.Octokit.Models;
using FluentHub.Models;
using FluentHub.Octokit.Queries.Repositories;
using FluentHub.UserControls.Blocks;
using FluentHub.ViewModels.UserControls.ButtonBlocks;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.Repositories.Discussions
{
    public class DiscussionsViewModel : ObservableObject
    {
        #region constructor
        public DiscussionsViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;
            _items = new();
            Items = new(_items);

            LoadDiscussionsPageCommand = new AsyncRelayCommand<string>(LoadDiscussionsPageAsync);
        }
        #endregion

        #region properties
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;

        private readonly ObservableCollection<DiscussionButtonBlockViewModel> _items;
        public ReadOnlyObservableCollection<DiscussionButtonBlockViewModel> Items { get; }

        public IAsyncRelayCommand LoadDiscussionsPageCommand { get; }
        #endregion

        #region methods
        private async Task LoadDiscussionsPageAsync(string nameWithOwner)
        {
            try
            {
                DiscussionQueries queries = new();
                var items = await queries.GetAllAsync(nameWithOwner.Split("/")[0], nameWithOwner.Split("/")[1]);

                _items.Clear();
                foreach (var item in items)
                {
                    DiscussionButtonBlockViewModel viewmodel = new()
                    {
                        Item = item,
                    };

                    _items.Add(viewmodel);
                }
            }
            catch (Exception ex)
            {
                _logger?.Error("LoadDiscussionsPageAsync", ex);
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
