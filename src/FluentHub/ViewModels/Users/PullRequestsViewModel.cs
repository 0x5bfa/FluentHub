using FluentHub.Octokit.Queries.Users;
using FluentHub.ViewModels.UserControls.ButtonBlocks;
using Humanizer;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Serilog;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.Users
{
    public class PullRequestsViewModel : ObservableObject
    {
        public PullRequestsViewModel(ILogger logger = null)
        {
            _logger = logger;

            _pullRequests = new();
            PullItems = new(_pullRequests);

            RefreshPullRequestsCommand = new AsyncRelayCommand<string>(RefreshPullRequestsAsync, CanRefreshPullRequests);
        }

        private readonly ILogger _logger;
        private readonly ObservableCollection<PullButtonBlockViewModel> _pullRequests;
        public ReadOnlyObservableCollection<PullButtonBlockViewModel> PullItems { get; }

        public IAsyncRelayCommand RefreshPullRequestsCommand { get; }

        private bool CanRefreshPullRequests(string username) => !string.IsNullOrEmpty(username);

        private async Task RefreshPullRequestsAsync(string username)
        {
            try
            {
                PullRequestQueries queries = new();
                var items = await queries.GetOverviewAllAsync(username);

                if (items == null) return;

                _pullRequests.Clear();
                foreach (var item in items)
                {
                    PullButtonBlockViewModel viewModel = new()
                    {
                        PullItem = item,
                        NameWithOwner = $"{item.Owner} / {item.Name} #{item.Number}",
                        UpdatedAtHumanized = item.UpdatedAt.Humanize()
                    };

                    _pullRequests.Add(viewModel);
                }
            }
            catch (Exception ex)
            {
                _logger?.Error(ex, "RefreshPullRequestsAsync");
                throw;
            }
        }
    }
}