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
using System.Threading.Tasks;

namespace FluentHub.ViewModels.Users
{
    public class OrganizationsViewModel : ObservableObject
    {
        public OrganizationsViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _logger = logger;
            _messenger = messenger;
            _organizations = new();
            Organizations = new(_organizations);

            RefreshOrganizationsCommand = new AsyncRelayCommand<string>(RefreshOrganizationsAsync);
        }

        private readonly ILogger _logger;
        private readonly IMessenger _messenger;
        private readonly ObservableCollection<OrgButtonBlockViewModel> _organizations;
        public ReadOnlyObservableCollection<OrgButtonBlockViewModel> Organizations { get; }

        public IAsyncRelayCommand RefreshOrganizationsCommand { get; }

        private async Task RefreshOrganizationsAsync(string login)
        {
            try
            {
                OrganizationQueries queries = new();
                List<Organization> items;

                items = login == null ?
                    await queries.GetAllAsync() :
                    await queries.GetAllAsync(login);

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
    }
}