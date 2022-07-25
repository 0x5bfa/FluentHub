using FluentHub.Octokit.Queries.Repositories;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.ViewModels.UserControls;
using FluentHub.Uwp.Utils;

namespace FluentHub.Uwp.ViewModels.Repositories.Codes.Layouts
{
    public class DetailsLayoutViewModel : ObservableObject
    {
        public DetailsLayoutViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;
            _messenger = messenger;

            _items = new();
            Items = new(_items);

            RefreshDetailsLayoutPageCommand = new AsyncRelayCommand<string>(LoadRepositoryContentsAsync);
            LoadRepositoryCommand = new AsyncRelayCommand<string>(LoadRepositoryAsync);
        }

        #region Fields and Properties
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;

        private RepoContextViewModel contextViewModel;
        public RepoContextViewModel ContextViewModel { get => contextViewModel; set => SetProperty(ref contextViewModel, value); }

        private Repository _repository;
        public Repository Repository { get => _repository; set => SetProperty(ref _repository, value); }

        private RepositoryOverviewViewModel _repositoryOverviewViewModel;
        public RepositoryOverviewViewModel RepositoryOverviewViewModel { get => _repositoryOverviewViewModel; set => SetProperty(ref _repositoryOverviewViewModel, value); }

        private readonly ObservableCollection<DetailsLayoutListViewModel> _items;
        public ReadOnlyObservableCollection<DetailsLayoutListViewModel> Items { get; }

        public IAsyncRelayCommand RefreshDetailsLayoutPageCommand { get; }
        public IAsyncRelayCommand LoadRepositoryCommand { get; }
        #endregion

        private async Task LoadRepositoryContentsAsync(string url, CancellationToken token)
        {
            try
            {
                _messenger?.Send(new LoadingMessaging(true));

                if (string.IsNullOrEmpty(ContextViewModel.Repository.DefaultBranchRef.Name))
                    return;

                if (ContextViewModel.IsFile) return;
                ContextViewModel.IsDir = true;

                CommitQueries queries = new();
                var response = await queries.GetWithObjectNameAsync(
                    ContextViewModel.Repository.Name,
                    ContextViewModel.Repository.Owner.Login,
                    ContextViewModel.BranchName,
                    ContextViewModel.Path);

                if (string.IsNullOrEmpty(ContextViewModel.Path))
                    ContextViewModel.IsRootDir = true;
                else ContextViewModel.IsSubDir = true;

                var zippedResponse =
                    response.Files.Zip(
                        response.Commits,
                        (file, commit) => new
                        {
                            File = file,
                            Commit = commit
                        });

                foreach (var item in zippedResponse)
                {
                    DetailsLayoutListViewModel listItem = new()
                    {
                        Type = item.File.Type,
                        Name = item.File.Name,
                        LatestCommitMessage = item.Commit.Message.Split('\n', 2).FirstOrDefault(),
                        UpdatedAt = item.Commit.CommittedDate,
                    };

                    if (item.File.Type == "tree")
                        listItem.IconGlyph = "\uE9A0";
                    else
                        listItem.IconGlyph = "\uE996";

                    _items.Add(listItem);
                }

                var orderedByItemType =
                    new ObservableCollection<DetailsLayoutListViewModel>
                    (Items.OrderByDescending(x => x.IconGlyph));

                _items.Clear();
                foreach (var orderedItem in orderedByItemType) _items.Add(orderedItem);
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
                _messenger?.Send(new LoadingMessaging(false));
            }
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
