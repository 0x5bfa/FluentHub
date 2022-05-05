using FluentHub.Backend;
using FluentHub.Octokit.Models.Events;
using FluentHub.Octokit.Models;
using FluentHub.Models;
using FluentHub.Octokit.Queries.Repositories;
using FluentHub.UserControls.Blocks;
using FluentHub.ViewModels.UserControls.Blocks;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.Repositories.Discussions
{
    public class DiscussionViewModel : ObservableObject
    {
        #region constructor
        public DiscussionViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;

            LoadDiscussionPageCommand = new AsyncRelayCommand<string>(LoadDiscussionPageAsync);
        }
        #endregion

        #region properties
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;

        private Discussion _discussion;
        public Discussion Discussion { get => _discussion; set => SetProperty(ref _discussion, value); }

        public IAsyncRelayCommand LoadDiscussionPageCommand { get; }
        #endregion

        #region methods
        private async Task LoadDiscussionPageAsync(string url)
        {
            try
            {
                var urlSegments = url.Split("/");

                DiscussionQueries queries = new();
                var response = await queries.GetAsync(urlSegments[3], urlSegments[4], Convert.ToInt32(urlSegments[6]));

                if (response == null) return;

                Discussion = response;
            }
            catch (Exception ex)
            {
                _logger?.Error("LoadDiscussionPageAsync", ex);
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
