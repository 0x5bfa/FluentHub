using FluentHub.Octokit.Queries.Repositories;
using FluentHub.App.Helpers;
using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.Utils;
using FluentHub.App.ViewModels.UserControls.Overview;
using FluentHub.App.ViewModels.UserControls.BlockButtons;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.App.ViewModels.Repositories.Issues
{
    public class IssuesViewModel : ObservableObject
    {
        public IssuesViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;

            _issueItems = new();
            IssueItems = new(_issueItems);

            _pinnedItems = new();
            PinnedItems = new(_pinnedItems);

            LoadRepositoryIssuesPageCommand = new AsyncRelayCommand(LoadRepositoryIssuesPageAsync);
        }

        #region Fields and Properties
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;

        private string _login;
        public string Login { get => _login; set => SetProperty(ref _login, value); }

        private string _name;
        public string Name { get => _name; set => SetProperty(ref _name, value); }

        private Repository _repository;
        public Repository Repository { get => _repository; set => SetProperty(ref _repository, value); }

        private RepositoryOverviewViewModel _repositoryOverviewViewModel;
        public RepositoryOverviewViewModel RepositoryOverviewViewModel { get => _repositoryOverviewViewModel; set => SetProperty(ref _repositoryOverviewViewModel, value); }

        private readonly ObservableCollection<IssueBlockButtonViewModel> _issueItems;
        public ReadOnlyObservableCollection<IssueBlockButtonViewModel> IssueItems { get; }

        private readonly ObservableCollection<IssueBlockButtonViewModel> _pinnedItems;
        public ReadOnlyObservableCollection<IssueBlockButtonViewModel> PinnedItems { get; }

        private Exception _taskException;
        public Exception TaskException { get => _taskException; set => SetProperty(ref _taskException, value); }

        public IAsyncRelayCommand LoadRepositoryIssuesPageCommand { get; }
        #endregion

        private async Task LoadRepositoryIssuesPageAsync()
        {
            _messenger?.Send(new TaskStateMessaging(TaskStatusType.IsStarted));
            bool faulted = false;

            string _currentTaskingMethodName = nameof(LoadRepositoryIssuesPageAsync);

            try
            {
                _currentTaskingMethodName = nameof(LoadRepositoryAsync);
                await LoadRepositoryAsync(Login, Name);

                _currentTaskingMethodName = nameof(LoadRepositoryIssuesAsync);
                await LoadRepositoryIssuesAsync(Login, Name);
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

        private async Task LoadRepositoryIssuesAsync(string owner, string name)
        {
            IssueQueries queries = new();
            var items = await queries.GetAllAsync(name, owner);

            _issueItems.Clear();
            foreach (var item in items)
            {
                IssueBlockButtonViewModel viewModel = new()
                {
                    IssueItem = item,
                };

                _issueItems.Add(viewModel);
            }

            var pinnedIssues = await queries.GetPinnedAllAsync(owner, name);
            if (pinnedIssues == null) return;

            _pinnedItems.Clear();
            foreach (var item in pinnedIssues)
            {
                IssueBlockButtonViewModel viewModel = new()
                {
                    IssueItem = item,
                };

                _pinnedItems.Add(viewModel);
            }
        }

        private async Task LoadRepositoryAsync(string owner, string name)
        {
            RepositoryQueries queries = new();
            Repository = await queries.GetDetailsAsync(owner, name);

            RepositoryOverviewViewModel = new()
            {
                Repository = Repository,
                RepositoryName = Repository.Name,
                RepositoryOwnerLogin = Repository.Owner.Login,
                ViewerSubscriptionState = Repository.ViewerSubscription?.Humanize(),

                SelectedTag = "issues",
            };
        }

        private void SetCurrentTabItem()
        {
            INavigationService navigationService = Ioc.Default.GetRequiredService<INavigationService>();

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.UserLogin = Repository.Owner.Login;
            currentItem.RepositoryName = Repository.Name;
            currentItem.Header = "Issues";
            currentItem.Description = "Issues";
            currentItem.Icon = new ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Issues.png"))
            };
        }
    }
}
