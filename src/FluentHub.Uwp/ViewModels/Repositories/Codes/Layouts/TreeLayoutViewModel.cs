using FluentHub.Octokit.Queries.Repositories;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using FluentHub.Uwp.ViewModels.UserControls;
using FluentHub.Uwp.ViewModels.UserControls.Overview;

namespace FluentHub.Uwp.ViewModels.Repositories.Code.Layouts
{
    public class TreeLayoutViewModel : ObservableObject
    {
        public TreeLayoutViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;

            _items = new();
            Items = new(_items);

            LoadTreeViewContentsCommand = new AsyncRelayCommand(LoadRepositoryContentsAsync);
            LoadRepositoryCommand = new AsyncRelayCommand<string>(LoadRepositoryAsync);
        }

        #region Fields and Properties
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;

        private bool _blobSelected;
        public bool BlobSelected { get => _blobSelected; set => SetProperty(ref _blobSelected, value); }

        private RepoContextViewModel _contextViewModel;
        public RepoContextViewModel ContextViewModel { get => _contextViewModel; set => SetProperty(ref _contextViewModel, value); }

        private Repository _repository;
        public Repository Repository { get => _repository; set => SetProperty(ref _repository, value); }

        private RepositoryOverviewViewModel _repositoryOverviewViewModel;
        public RepositoryOverviewViewModel RepositoryOverviewViewModel { get => _repositoryOverviewViewModel; set => SetProperty(ref _repositoryOverviewViewModel, value); }

        private RepoContextViewModel _selectedContextViewModel;
        public RepoContextViewModel SelectedContextViewModel { get => _selectedContextViewModel; set => SetProperty(ref _selectedContextViewModel, value); }

        private readonly ObservableCollection<TreeLayoutPageModel> _items;
        public ReadOnlyObservableCollection<TreeLayoutPageModel> Items { get; }

        public IAsyncRelayCommand LoadTreeViewContentsCommand { get; }
        public IAsyncRelayCommand LoadRepositoryCommand { get; }
        #endregion

        private async Task LoadRepositoryContentsAsync(CancellationToken token)
        {
            _messenger?.Send(new TaskStateMessaging(TaskStatusType.IsStarted));
            bool faulted = false;

            try
            {
                if (string.IsNullOrEmpty(ContextViewModel.Repository.DefaultBranchRef.Name))
                    return;

                TreeQueries queries = new();
                var response = await queries.GetAllAsync(
                    ContextViewModel.Repository.Name,
                    ContextViewModel.Repository.Owner.Login,
                    ContextViewModel.BranchName,
                    ContextViewModel.Path);

                foreach (var item in response)
                {
                    TreeLayoutPageModel model = new()
                    {
                        Name = item.Name,
                        Path = item.Path,
                        Tag = item.Type,
                        IsBolb = false,
                    };

                    if (item.Type == "tree")
                    {
                        model.Glyph = "\uE9A0";
                    }
                    else
                    {
                        model.Glyph = "\uE996";
                        model.IsBolb = true;
                    }

                    _items.Add(model);
                }

                var orderedItems =
                    new ObservableCollection<TreeLayoutPageModel>
                    (Items.OrderByDescending(x => x.Glyph));

                _items.Clear();
                foreach (var item in orderedItems) _items.Add(item);
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadRepositoryContentsAsync), ex);
                if (_messenger != null)
                {
                    UserNotificationMessage notification = new("Something went wrong", ex.Message, UserNotificationType.Error);
                    _messenger.Send(notification);
                }
                throw;
            }
            finally
            {
                _messenger?.Send(new TaskStateMessaging(faulted ? TaskStatusType.IsFaulted : TaskStatusType.IsCompletedSuccessfully));
            }
        }

        public async Task<List<TreeLayoutPageModel>> LoadSubItemsAsync(string path)
        {
            try
            {
                var pathItems = path.Split("/");
                List<TreeLayoutPageModel> subItems = new();

                if (string.IsNullOrEmpty(ContextViewModel.Repository.DefaultBranchRef.Name))
                    return null;

                TreeQueries queries = new();
                var objects = await queries.GetAllAsync(
                    ContextViewModel.Repository.Name,
                    ContextViewModel.Repository.Owner.Login,
                    ContextViewModel.BranchName,
                    path);

                foreach (var obj in objects)
                {
                    TreeLayoutPageModel model = new()
                    {
                        Name = obj.Name,
                        Path = obj.Path,
                        Tag = obj.Type,
                        IsBolb = false,
                    };

                    if (obj.Type == "tree")
                    {
                        model.Glyph = "\uE9A0";
                    }
                    else
                    {
                        model.Glyph = "\uE996";
                        model.IsBolb = true;
                    }

                    subItems.Add(model);
                }

                var orderedItems =
                    new List<TreeLayoutPageModel>
                    (subItems.OrderByDescending(x => x.Glyph));

                subItems.Clear();
                foreach (var item in orderedItems) subItems.Add(item);

                return subItems;
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadSubItemsAsync), ex);
                if (_messenger != null)
                {
                    UserNotificationMessage notification = new("Something went wrong", ex.Message, UserNotificationType.Error);
                    _messenger.Send(notification);
                }
                throw;
            }

            return null;
        }

        private async Task LoadRepositoryAsync(string url, CancellationToken token)
        {
            try
            {
                var uri = new Uri(url);
                var pathSegments = uri.AbsolutePath.Split("/").ToList();
                pathSegments.RemoveAt(0);

                RepositoryQueries queries = new();
                Repository = await queries.GetDetailsAsync(pathSegments[0], pathSegments[1]);

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
