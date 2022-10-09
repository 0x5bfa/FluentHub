using FluentHub.Octokit.Queries.Repositories;
using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.Utils;
using FluentHub.Uwp.ViewModels.UserControls.Overview;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Media.Imaging;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.ViewModels.Repositories.Discussions
{
    public class DiscussionViewModel : ObservableObject
    {
        public DiscussionViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;

            LoadRepositoryDiscussionPageCommand = new AsyncRelayCommand(LoadRepositoryDiscussionPageAsync);
        }

        #region Fields and Properties
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;

        private string _login;
        public string Login { get => _login; set => SetProperty(ref _login, value); }

        private string _name;
        public string Name { get => _name; set => SetProperty(ref _name, value); }

        private Repository _repository;
        public Repository Repository { get => _repository; set => SetProperty(ref _repository, value); }

        private RepositoryOverviewViewModel _repositoryOverviewViewModel;
        public RepositoryOverviewViewModel RepositoryOverviewViewModel { get => _repositoryOverviewViewModel; set => SetProperty(ref _repositoryOverviewViewModel, value); }

        private int _number;
        public int Number { get => _number; set => SetProperty(ref _number, value); }

        private Discussion _discussion;
        public Discussion Discussion { get => _discussion; set => SetProperty(ref _discussion, value); }

        private Exception _taskException;
        public Exception TaskException { get => _taskException; set => SetProperty(ref _taskException, value); }

        public IAsyncRelayCommand LoadRepositoryDiscussionPageCommand { get; }
        #endregion

        private async Task LoadRepositoryDiscussionPageAsync()
        {
            _messenger?.Send(new TaskStateMessaging(TaskStatusType.IsStarted));
            bool faulted = false;

            string _currentTaskingMethodName = nameof(LoadRepositoryDiscussionPageAsync);

            try
            {
                _currentTaskingMethodName = nameof(LoadRepositoryAsync);
                await LoadRepositoryAsync(Login, Name);

                _currentTaskingMethodName = nameof(LoadRepositoryOneDiscussionAsync);
                await LoadRepositoryOneDiscussionAsync(Login, Name);
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

        private async Task LoadRepositoryOneDiscussionAsync(string owner, string name)
        {
            DiscussionQueries queries = new();
            var response = await queries.GetAsync(owner, name, Number);

            if (response == null) return;

            Discussion = response;
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

                SelectedTag = "discussions",
            };
        }

        private void SetCurrentTabItem()
        {
            var provider = App.Current.Services;
            INavigationService navigationService = provider.GetRequiredService<INavigationService>();

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Discussion";
            currentItem.Description = "Discussion";
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Discussions.png"))
            };
        }
    }
}
