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

          //  RefreshAccountsSavedCommand = new AsyncRelayCommand<string>(RefreshAccountsSaved);
        }


        #region fields
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;
        private readonly ObservableCollection<AccountButtonBlockViewModel> _accountsItems;
        #endregion

        #region properties
        public ReadOnlyObservableCollection<AccountButtonBlockViewModel> AccountsItems { get; }

        public IAsyncRelayCommand RefreshAccountsSavedCommand { get; }
        #endregion

        private async Task RefreshAccountsSaved() {
        }
    }
}
