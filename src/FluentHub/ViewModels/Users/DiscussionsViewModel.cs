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
    public class DiscussionsViewModel : ObservableObject
    {
        public DiscussionsViewModel(ILogger logger = null)
        {
            _logger = logger;
            _discussions = new();
            DiscussionItems = new(_discussions);

            RefreshDiscussionsCommand = new AsyncRelayCommand<string>(RefreshDiscussionsAsync, CanRefreshDiscussions);
        }

        private readonly ILogger _logger;
        private readonly ObservableCollection<DiscussionButtonBlockViewModel> _discussions;
        public ReadOnlyObservableCollection<DiscussionButtonBlockViewModel> DiscussionItems { get; }
        public IAsyncRelayCommand RefreshDiscussionsCommand { get; }

        private bool CanRefreshDiscussions(string username) => !string.IsNullOrEmpty(username);

        private async Task RefreshDiscussionsAsync(string username)
        {
            try
            {
                DiscussionQueries queries = new();
                var items = await queries.GetOverviewAllAsync(username);

                _discussions.Clear();
                foreach (var item in items)
                {
                    DiscussionButtonBlockViewModel viewModel = new()
                    {
                        DiscussionItem = item,
                        NameWithOwner = $"{item.Owner} / {item.Name} #{item.Number}",
                        UpdatedAtHumanized = item.UpdatedAt.Humanize()
                    };

                    _discussions.Add(viewModel);
                }
            }
            catch (Exception ex)
            {
                _logger?.Error(ex, "RefreshDiscussionsAsync");
                throw;
            }
        }
    }
}