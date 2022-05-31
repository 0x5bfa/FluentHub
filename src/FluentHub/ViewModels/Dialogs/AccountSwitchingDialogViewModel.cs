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

namespace FluentHub.ViewModels.Dialogs
{
    public class AccountSwitchingDialogViewModel : ObservableObject
    {
        public AccountSwitchingDialogViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _logger = logger;
            _messenger = messenger;

            _accountsItems = new();
            AccountsItems = new(_accountsItems);

            LoadSignedInLoginsCommand = new RelayCommand(LoadSignedInLogins);
        }

        #region fields
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;

        private readonly ObservableCollection<AccountModel> _accountsItems;
        public ReadOnlyObservableCollection<AccountModel> AccountsItems { get; }
        #endregion

        #region properties

        public IRelayCommand LoadSignedInLoginsCommand { get; }
        #endregion

        private void LoadSignedInLogins()
        {
            try
            {
                // Get logged in users from App Container
                var dividedLogins = App.Settings.SignedInUserLogins.Split(",");

                foreach (var item in dividedLogins)
                {
                    AccountModel model = new() { Login = item };
                    _accountsItems.Add(model);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
