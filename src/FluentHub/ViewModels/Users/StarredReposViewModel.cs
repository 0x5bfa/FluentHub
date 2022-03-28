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
    public class StarredReposViewModel : ObservableObject
    {
        public StarredReposViewModel(ILogger logger = null)
        {
            _logger = logger;

            _repositories = new();
            Repositories = new(_repositories);

            RefreshRepositoriesCommand = new AsyncRelayCommand<string>(RefreshRepositoriesAsync, CanRefreshRepositories);
        }

        private readonly ILogger _logger;
        private readonly ObservableCollection<RepoButtonBlockViewModel> _repositories;
        public ReadOnlyObservableCollection<RepoButtonBlockViewModel> Repositories { get; }

        public IAsyncRelayCommand RefreshRepositoriesCommand { get; }

        private bool CanRefreshRepositories(string username) => !string.IsNullOrEmpty(username);

        private async Task RefreshRepositoriesAsync(string username)
        {            
            try
            {
                StarredRepoQueries queries = new();
                var items = await queries.GetOverviewAllAsync(username);

                _repositories.Clear();
                foreach (var item in items)
                {
                    RepoButtonBlockViewModel viewModel = new()
                    {
                        Item = item,
                        DisplayDetails = true,
                        DisplayStarButton = false
                    };

                    _repositories.Add(viewModel);
                }
            }
            catch (Exception ex)
            {
                _logger?.Error(ex, "RefreshRepositoriesAsync");
                throw;
            }
        }
    }
}