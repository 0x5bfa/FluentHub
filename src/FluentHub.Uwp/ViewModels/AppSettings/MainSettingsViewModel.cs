using FluentHub.Uwp.Utils;
using FluentHub.Uwp.Models;
using FluentHub.Octokit.Models;
using FluentHub.Octokit.Queries.Users;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace FluentHub.Uwp.ViewModels.AppSettings
{
    public class MainSettingsViewModel : ObservableObject
    {
        public MainSettingsViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _logger = logger;
            _messenger = messenger;

            _accountsItems = new();
            AccountsItems = new(_accountsItems);

            LoadSignedInLoginsCommand = new AsyncRelayCommand(LoadSignedInLogins);
        }

        #region fields and properties
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;

        private User signedInUser;
        public User SignedInUser { get => signedInUser; set => SetProperty(ref signedInUser, value); }

        private readonly ObservableCollection<AccountModel> _accountsItems;
        public ReadOnlyObservableCollection<AccountModel> AccountsItems { get; }

        public IAsyncRelayCommand LoadSignedInLoginsCommand { get; }
        #endregion

        private async Task LoadSignedInLogins()
        {
            try
            {
                UserQueries queries = new();
                var response = await queries.GetAsync(App.Settings.SignedInUserName);

                SignedInUser = response;

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
