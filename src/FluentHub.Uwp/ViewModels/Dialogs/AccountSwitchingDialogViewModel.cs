using FluentHub.Core;
using FluentHub.Uwp.Models;
using FluentHub.Octokit.Models;
using FluentHub.Octokit.Queries.Users;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;
using Humanizer;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace FluentHub.Uwp.ViewModels.Dialogs
{
    public class AccountSwitchingDialogViewModel : ObservableObject
    {
        public AccountSwitchingDialogViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _logger = logger;
            _messenger = messenger;
        }

        #region Fields and Properties
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;
        #endregion
    }
}
