using FluentHub.Octokit.Queries.Users;
using FluentHub.ViewModels.UserControls.Blocks;
using Humanizer;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Octokit;
using Serilog;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.Home
{
    public class ActivitiesViewModel : ObservableObject
    {
        public ActivitiesViewModel(ILogger logger = null)
        {
            _logger = logger;
            _activities = new();
            Activities = new(_activities);

            RefreshActivitiesCommand = new AsyncRelayCommand<string>(RefreshActivitiesAsync, CanRefreshActivities);
        }

        private readonly ILogger _logger;
        private readonly ObservableCollection<ActivityBlockViewModel> _activities;
        public ReadOnlyObservableCollection<ActivityBlockViewModel> Activities { get; }
        public IAsyncRelayCommand RefreshActivitiesCommand { get; }

        private bool CanRefreshActivities(string username) => !string.IsNullOrEmpty(username);

        private async Task RefreshActivitiesAsync(string username)
        {            
            try
            {
                ActivityQueries queries = new();

                var requestResult = await queries.GetAllAsync(username);
                foreach (var item in requestResult)
                {
                    ActivityBlockViewModel viewModel = new()
                    {
                        Payload = item,
                        UpdatedAtHumanized = item.CreatedAt.Humanize()
                    };
                    _activities.Add(viewModel);
                }
            }
            catch (Exception e)
            {
                _logger?.Error(e, "RefreshActivitiesAsync");
                throw;
            }
        }
    }
}