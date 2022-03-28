using FluentHub.Octokit.Queries.Users;
using FluentHub.ViewModels.UserControls.ButtonBlocks;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Serilog;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.Users
{
    public class OrganizationsViewModel : ObservableObject
    {
        public OrganizationsViewModel(ILogger logger = null)
        {
            _logger = logger;

            _organizations = new();
            Organizations = new(_organizations);

            RefreshOrganizationsCommand = new AsyncRelayCommand<string>(RefreshOrganizationsAsync, CanRefreshOrganizations);
        }

        private readonly ILogger _logger;
        private readonly ObservableCollection<OrgButtonBlockViewModel> _organizations;
        public ReadOnlyObservableCollection<OrgButtonBlockViewModel> Organizations { get; }

        public IAsyncRelayCommand RefreshOrganizationsCommand { get; }

        private bool CanRefreshOrganizations(string username) => !string.IsNullOrEmpty(username);

        private async Task RefreshOrganizationsAsync(string username)
        {            
            try
            {
                OrganizationQueries queries = new();
                var items = await queries.GetOverviewAllAsync(username);

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
                _logger?.Error(ex, "RefreshOrganizationsAsync");
                throw;
            }
        }
    }
}