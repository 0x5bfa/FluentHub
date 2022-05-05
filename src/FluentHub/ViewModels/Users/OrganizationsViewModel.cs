using FluentHub.Backend;
using FluentHub.Octokit.Models;
using FluentHub.Models;
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
    public class OrganizationsViewModel : ObservableObject
    {
        #region constructor
        public OrganizationsViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;
            _messenger = messenger;
            _organizations = new();
            Organizations = new(_organizations);

            RefreshOrganizationsCommand = new AsyncRelayCommand<string>(RefreshOrganizationsAsync);
        }
        #endregion

        #region fields
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;
        private readonly ObservableCollection<OrgButtonBlockViewModel> _organizations;
        #endregion

        #region properties
        public ReadOnlyObservableCollection<OrgButtonBlockViewModel> Organizations { get; }

        public IAsyncRelayCommand RefreshOrganizationsCommand { get; }
        #endregion

        #region methods
        private async Task RefreshOrganizationsAsync(string login, CancellationToken token)
        {
            try
            {
                OrganizationQueries queries = new();
                var items = await queries.GetAllAsync(login);
                if (items == null) return;

                _organizations.Clear();
                foreach (var item in items)
                {
                    OrgButtonBlockViewModel viewModel = new()
                    {
                        OrgItem = item
                    };

                    _organizations.Add(viewModel);
                }                
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                _logger?.Error("RefreshOrganizationsAsync", ex);
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