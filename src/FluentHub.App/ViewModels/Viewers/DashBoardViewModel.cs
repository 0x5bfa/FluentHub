// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Octokit.Queries.Users;
using FluentHub.App.Models;
using FluentHub.App.Utils;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using FluentHub.App.ViewModels.UserControls.FeedBlocks;

namespace FluentHub.App.ViewModels.Viewers
{
    public class DashBoardViewModel : ObservableObject
    {
        private readonly IMessenger _messenger;

        private readonly ILogger _logger;

        private readonly INavigationService _navigation;

        private readonly ObservableCollection<Repository> _TopRepositories;
        public ReadOnlyObservableCollection<Repository> TopRepositories { get; }

        private readonly ObservableCollection<Notification> _RecentActivities;
        public ReadOnlyObservableCollection<Notification> RecentActivities { get; }

        private readonly ObservableCollection<ActivityBlockViewModel> _Feeds;
        public ReadOnlyObservableCollection<ActivityBlockViewModel> Feeds { get; }

        private Exception _taskException;
        public Exception TaskException
        {
            get => _taskException;
            set => SetProperty(ref _taskException, value);
        }

        public IAsyncRelayCommand LoadUserHomePageCommand { get; }

        public DashBoardViewModel()
        {
            _messenger = Ioc.Default.GetRequiredService<IMessenger>();
            _logger = Ioc.Default.GetRequiredService<ILogger>();
            _navigation = Ioc.Default.GetRequiredService<INavigationService>();

            _TopRepositories = new();
            TopRepositories = new(_TopRepositories);

            _RecentActivities = new();
            RecentActivities = new(_RecentActivities);

            _Feeds = new();
            Feeds = new(_Feeds);

            LoadUserHomePageCommand = new AsyncRelayCommand(LoadUserHomePageAsync);
        }

        private async Task LoadUserHomePageAsync()
        {
            _messenger?.Send(new TaskStateMessaging(TaskStatusType.IsStarted));
            bool faulted = false;

            string _currentTaskingMethodName = nameof(LoadUserHomePageAsync);

            try
            {
                _currentTaskingMethodName = nameof(LoadHomeContentsAsync);
                await LoadHomeContentsAsync();
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

        private async Task LoadHomeContentsAsync()
        {
            RepositoryQueries repositoryQueries = new();
            var repositoryResponse = await repositoryQueries.GetAllAsync(App.AppSettings.SignedInUserName);

            if (repositoryResponse.Count < 6)
            {
                foreach (var item in repositoryResponse)
                    _TopRepositories.Add(item);
            }
            else
            {
                foreach (var item in repositoryResponse.GetRange(0, 6))
                    _TopRepositories.Add(item);
            }

            NotificationQueries notificationQueries = new();
            var notificationResponse = await notificationQueries.GetAllAsync(
                new() { All = true },
                new()
                {
                    PageCount = 1, // Constant
                    PageSize = 30,
                    StartPage = 1,
                });

            // Filder the notifications; remove clsed Issue & Pull Requests
            notificationResponse.RemoveAll(x =>
                x.Subject.Type != NotificationSubjectType.IssueOpen &&
                x.Subject.Type != NotificationSubjectType.PullRequestOpen);

            if (notificationResponse.Count < 8)
            {
                foreach (var item in notificationResponse)
                    _RecentActivities.Add(item);
            }
            else
            {
                foreach (var item in notificationResponse.GetRange(0, 8))
                    _RecentActivities.Add(item);
            }

            ActivityQueries activityQueries = new();

            var activityResponse = await activityQueries.GetAllAsync(App.AppSettings.SignedInUserName);
            if (activityResponse == null)
                return;

            foreach (var item in activityResponse)
            {
                ActivityBlockViewModel viewModel = new()
                {
                    Payload = item,
                };

                _Feeds.Add(viewModel);
            }
        }

        private static void SetCurrentTabItem()
        {
            INavigationService navigationService = Ioc.Default.GetRequiredService<INavigationService>();

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem!;
            currentItem.Header = "Dashboard";
            currentItem.Description = "Dashboard";
            currentItem.Icon = new ImageIconSource()
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Home.png"))
            };
        }
    }
}
