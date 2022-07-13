using FluentHub.Uwp.Utils;
using FluentHub.Uwp.Models;
using FluentHub.Octokit.Queries.Users;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace FluentHub.Uwp.ViewModels.AppSettings.Accounts
{
    public class AccountViewModel : ObservableObject
    {
        #region Constructor        
        public AccountViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _logger = logger;
            _messenger = messenger;

            LoadSignedInLoginsCommand = new AsyncRelayCommand(LoadCurrentUserGitHubInfoAsync);
        }
        #endregion

        #region Fields and Properties
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;

        private User signedInUser;
        public User SignedInUser { get => signedInUser; set => SetProperty(ref signedInUser, value); }

        public IAsyncRelayCommand LoadSignedInLoginsCommand { get; }
        #endregion

        private async Task LoadCurrentUserGitHubInfoAsync()
        {
            try
            {
                UserQueries queries = new();
                var response = await queries.GetAsync(App.Settings.SignedInUserName);

                SignedInUser = response;
            }
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadCurrentUserGitHubInfoAsync), ex);
                if (_messenger != null)
                {
                    UserNotificationMessage notification = new("Something went wrong", ex.Message, UserNotificationType.Error);
                    _messenger.Send(notification);
                }
                throw;
            }
        }
    }
}
