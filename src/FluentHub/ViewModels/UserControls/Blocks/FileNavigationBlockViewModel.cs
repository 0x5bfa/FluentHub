using FluentHub.Backend;
using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Octokit.Queries.Repositories;
using FluentHub.ViewModels.Repositories;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;

namespace FluentHub.ViewModels.UserControls.Blocks
{
    public class FileNavigationBlockViewModel : ObservableObject
    {
        #region constructor
        public FileNavigationBlockViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;
            _messenger = messenger;

            BranchNames = new();
            LoadBranchNameAllCommand = new AsyncRelayCommand(LoadBranchNameAllAsync);
        }
        #endregion

        #region fields
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;
        private RepoContextViewModel contextViewModel;
        #endregion

        #region properties
        public ObservableCollection<string> BranchNames;
        public RepoContextViewModel ContextViewModel { get => contextViewModel; set => SetProperty(ref contextViewModel, value); }

        public IAsyncRelayCommand LoadBranchNameAllCommand { get; }
        #endregion

        #region methods
        public async Task LoadBranchNameAllAsync()
        {
            try
            {
                RepositoryQueries queries = new();

                // temp workaround
                var branchNames = await queries.GetBranchNameAllAsync(contextViewModel.Owner, contextViewModel.Name);

                // Reorder
                var alphabetic = new ObservableCollection<string>(branchNames.OrderBy(x => x));
                branchNames.Clear();
                foreach (var orderedItem in alphabetic)
                {
                    if (contextViewModel.BranchName == orderedItem)
                    {
                        branchNames.Insert(0, orderedItem);
                    }
                    else
                    {
                        branchNames.Add(orderedItem);
                    }
                }

                foreach (var branch in branchNames) BranchNames.Add(branch);
            }
            catch (Exception ex)
            {
                _logger?.Error("LoadBlobContentBlockAsync", ex);
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
