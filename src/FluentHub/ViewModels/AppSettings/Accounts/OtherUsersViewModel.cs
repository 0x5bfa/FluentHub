using FluentHub.Core;
using FluentHub.Models;
using FluentHub.Octokit.Models;
using FluentHub.Octokit.Queries.Users;
using FluentHub.ViewModels.UserControls.ButtonBlocks;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.AppSettings.Accounts
{
    public class OtherUsersViewModel : ObservableObject
    {
        public OtherUsersViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _logger = logger;
            _messenger = messenger;

            _accountsItems = new();
            AccountsItems = new(_accountsItems);

            LoadSignedInLoginsCommand = new RelayCommand(LoadSignedInLogins);
        }

        #region Fields and Properties
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;

        private readonly ObservableCollection<AccountModel> _accountsItems;
        public ReadOnlyObservableCollection<AccountModel> AccountsItems { get; }

        public IRelayCommand LoadSignedInLoginsCommand { get; }
        #endregion

        private void LoadSignedInLogins()
        {
            try
            {
                var dividedLogins = App.Settings.SignedInUserLogins.Split(",");

                foreach (var item in dividedLogins)
                {
                    var isViewer = item == App.Settings.SignedInUserName ? true : false;

                    AccountModel model = new()
                    {
                        Login = item,
                        IsViewer = isViewer,
                    };

                    _accountsItems.Add(model);
                }
            }
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadSignedInLogins), ex);
                if (_messenger != null)
                {
                    UserNotificationMessage notification = new("Something went wrong", ex.Message, UserNotificationType.Error);
                    _messenger.Send(notification);
                }
                throw;
            }
        }

        public void RemoveAccount(string login)
        {
            for (int i = 0; _accountsItems.Count() > i; i++)
            {
                if (_accountsItems[i].Login == login)
                {
                    _accountsItems.RemoveAt(i);
                    break;
                }
            }

            Services.AccountService.RemoveAccount(login);
        }
    }
}
