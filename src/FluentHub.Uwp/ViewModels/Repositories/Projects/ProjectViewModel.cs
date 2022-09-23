using FluentHub.Octokit.Queries.Repositories;
using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.Repositories;
using FluentHub.Uwp.ViewModels.UserControls;
using FluentHub.Uwp.ViewModels.UserControls.Overview;
using FluentHub.Uwp.ViewModels.UserControls.BlockButtons;
using FluentHub.Uwp.Utils;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Media.Imaging;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.ViewModels.Repositories.Projects
{
    public class ProjectViewModel : ObservableObject
    {
        public ProjectViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;

            LoadRepositoryProjectPageCommand = new AsyncRelayCommand(LoadRepositoryProjectPageAsync);
        }

        #region Fields and Properties
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;

        private string _login;
        public string Login { get => _login; set => SetProperty(ref _login, value); }

        private string _name;
        public string Name { get => _name; set => SetProperty(ref _name, value); }

        private int _number;
        public int Number { get => _number; set => SetProperty(ref _number, value); }

        private Repository _repository;
        public Repository Repository { get => _repository; set => SetProperty(ref _repository, value); }

        private RepositoryOverviewViewModel _repositoryOverviewViewModel;
        public RepositoryOverviewViewModel RepositoryOverviewViewModel { get => _repositoryOverviewViewModel; set => SetProperty(ref _repositoryOverviewViewModel, value); }

        private Project _project;
        public Project Project { get => _project; set => SetProperty(ref _project, value); }

        private Exception _taskException;
        public Exception TaskException { get => _taskException; set => SetProperty(ref _taskException, value); }

        public IAsyncRelayCommand LoadRepositoryProjectPageCommand { get; }
        #endregion

        private async Task LoadRepositoryProjectPageAsync()
        {
            _messenger?.Send(new TaskStateMessaging(TaskStatusType.IsStarted));
            bool faulted = false;

            string _currentTaskingMethodName = nameof(LoadRepositoryProjectPageAsync);

            try
            {
                _currentTaskingMethodName = nameof(LoadRepositoryAsync);
                await LoadRepositoryAsync(Login, Name);

                _currentTaskingMethodName = nameof(LoadProjectPageAsync);
                await LoadProjectPageAsync(Login, Name);
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

        private async Task LoadProjectPageAsync(string owner, string name)
        {
            ProjectQueries queries = new();
            var response = await queries.GetAsync(
                Repository.Owner.Login,
                Repository.Name,
                Number
                );

            Project = response;
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
                RepositoryVisibilityLabel = new()
                {
                    Name = Repository.IsPrivate ? "Private" : "Public",
                    Color = "#64000000",
                },
                ViewerSubscriptionState = Repository.ViewerSubscription?.Humanize(),

                SelectedTag = "projects",
            };
        }

        private void SetCurrentTabItem()
        {
            var provider = App.Current.Services;
            INavigationService navigationService = provider.GetRequiredService<INavigationService>();

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Project";
            currentItem.Description = "Project";
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Projects.png"))
            };
        }
    }
}
