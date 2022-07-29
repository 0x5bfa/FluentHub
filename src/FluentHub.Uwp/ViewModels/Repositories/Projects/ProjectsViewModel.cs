using FluentHub.Octokit.Queries.Repositories;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using FluentHub.Uwp.ViewModels.UserControls;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;

namespace FluentHub.Uwp.ViewModels.Repositories.Projects
{
    public class ProjectsViewModel : ObservableObject
    {
        public ProjectsViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;

            _items = new();
            Items = new(_items);

            LoadProjectsPageCommand = new AsyncRelayCommand(LoadProjectsPageAsync);
        }

        #region Fields and Properties
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;

        private Repository _repository;
        public Repository Repository { get => _repository; set => SetProperty(ref _repository, value); }

        private RepositoryOverviewViewModel _repositoryOverviewViewModel;
        public RepositoryOverviewViewModel RepositoryOverviewViewModel { get => _repositoryOverviewViewModel; set => SetProperty(ref _repositoryOverviewViewModel, value); }

        private readonly ObservableCollection<ProjectButtonBlockViewModel> _items;
        public ReadOnlyObservableCollection<ProjectButtonBlockViewModel> Items { get; }

        public IAsyncRelayCommand LoadProjectsPageCommand { get; }
        #endregion

        private async Task LoadProjectsPageAsync(CancellationToken token)
        {
            try
            {
                _messenger?.Send(new LoadingMessaging(true));

                ProjectQueries queries = new();
                var items = await queries.GetAllAsync(
                    Repository.Owner.Login,
                    Repository.Name
                    );

                _items.Clear();
                foreach (var item in items)
                {
                    ProjectButtonBlockViewModel viewmodel = new()
                    {
                        Item = item,
                    };

                    _items.Add(viewmodel);
                }
            }
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadProjectsPageAsync), ex);
                if (_messenger != null)
                {
                    UserNotificationMessage notification = new("Something went wrong", ex.Message, UserNotificationType.Error);
                    _messenger.Send(notification);
                }
                throw;
            }
            finally
            {
                _messenger?.Send(new LoadingMessaging(false));
            }
        }

        public async Task LoadRepositoryAsync(string owner, string name)
        {
            try
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

                    SelectedTag = "code",
                };
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadRepositoryAsync), ex);
                if (_messenger != null)
                {
                    UserNotificationMessage notification = new("Something went wrong", ex.Message, UserNotificationType.Error);
                    _messenger.Send(notification);
                }
                throw;
            }
        }
    }
}
