using CommunityToolkit.WinUI.UI;
using FluentHub.Octokit.Queries.Users;
using FluentHub.App.Helpers;
using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.Utils;
using FluentHub.App.ViewModels.UserControls.FeedBlocks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.App.ViewModels.Home
{
    public class FeedsViewModel : ObservableObject
    {
        public FeedsViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;

            _userRepositories = new();
            UserRepositories = new(_userRepositories);

            _activities = new();
            Activities = new(_activities);

            LoadUserFeedsPageCommand = new AsyncRelayCommand(LoadUserFeedsPageAsync);
        }

        #region Fields and Properties
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;

        private readonly ObservableCollection<Repository> _userRepositories;
        public ReadOnlyObservableCollection<Repository> UserRepositories { get; }

        private readonly ObservableCollection<ActivityBlockViewModel> _activities;
        public ReadOnlyObservableCollection<ActivityBlockViewModel> Activities { get; }

        private Exception _taskException;
        public Exception TaskException { get => _taskException; set => SetProperty(ref _taskException, value); }

        public IAsyncRelayCommand LoadUserFeedsPageCommand { get; }
        #endregion

        private async Task LoadUserFeedsPageAsync()
        {
            _messenger?.Send(new TaskStateMessaging(TaskStatusType.IsStarted));
            bool faulted = false;

            string _currentTaskingMethodName = nameof(LoadUserFeedsPageAsync);

            try
            {
                _currentTaskingMethodName = nameof(LoadActivitiesAsync);
                await LoadActivitiesAsync();
            }
            catch (Exception ex)
            {
                TaskException = ex;
                faulted = true;

                _logger?.Error(_currentTaskingMethodName, ex);
                throw;
            }
            finally
            {
                SetCurrentTabItem();
                _messenger?.Send(new TaskStateMessaging(faulted ? TaskStatusType.IsFaulted : TaskStatusType.IsCompletedSuccessfully));
            }
        }

        private async Task LoadActivitiesAsync()
        {
            RepositoryQueries repositoryQueries = new();
            var repositoryResponse = await repositoryQueries.GetAllAsync(App.AppSettings.SignedInUserName);
            if (repositoryResponse == null) return;

            foreach (var item in repositoryResponse) _userRepositories.Add(item);

            ActivityQueries activityQueries = new();
            var activityResponse = await activityQueries.GetAllAsync(App.AppSettings.SignedInUserName);
            if (activityResponse == null) return;

            foreach (var item in activityResponse)
            {
                ActivityBlockViewModel viewModel = new()
                {
                    Payload = item,
                };

                _activities.Add(viewModel);
            }
        }

        private void SetCurrentTabItem()
        {
            var provider = App.Current.Services;
            INavigationService navigationService = provider.GetRequiredService<INavigationService>();

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Feeds";
            currentItem.Description = "Feeds";
            currentItem.Icon = new ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Activities.png"))
            };
        }
    }
}
