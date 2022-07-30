using FluentHub.Octokit.Queries.Repositories;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using FluentHub.Uwp.ViewModels.UserControls;
using Windows.UI.Xaml.Media.Imaging;
using muxc = Microsoft.UI.Xaml.Controls;

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

        public async Task LoadRepositoryContentsAsync()
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
                    Repository.Name,
                    Repository.Owner.Login,
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

        public void InitializeRepositoryContext(string owner, string name, string path)
        {
            bool isRootDir = false;
            bool isFile = false;
            bool isSubDir = false;
            bool isDir = false;
            string actualPath = path;
            var pathItems = path?.Split("/").ToList();
            string branchName = "";

            // owner/name
            if (pathItems == null || pathItems.Count() == 0)
            {
                isDir = isRootDir = true;
                branchName = Repository.DefaultBranchRef?.Name;
            }
            // owner/name/tree/main
            else if (pathItems.Count() == 2)
            {
                isDir = isRootDir = true;
                branchName = pathItems.ElementAt(1);

                pathItems.RemoveRange(0, 2);
                actualPath = string.Join("/", pathItems);
            }
            // owner/name/(tree|blob)/main/path
            else if (pathItems.Count() > 2)
            {
                isRootDir = false;
                branchName = pathItems.ElementAt(1);

                isFile = pathItems.ElementAt(0).ToLower() == "blob" ? true : false;
                isSubDir = isDir = pathItems.ElementAt(0).ToLower() == "tree" ? true : false;

                pathItems.RemoveRange(0, 2);
                actualPath = string.Join("/", pathItems);
            }

            ContextViewModel = new()
            {
                Repository = Repository,

                BranchName = branchName,
                IsDir = isDir,
                IsFile = isFile,
                IsSubDir = isSubDir,
                IsRootDir = isRootDir,
                Path = actualPath,
            };
        }

        public void CreateTabHeader()
        {
            string header;
            string description;
            string url;
            muxc.ImageIconSource icon;
            
            if (ContextViewModel.IsRootDir)
            {
                if (string.IsNullOrEmpty(Repository.Description))
                {
                    header = $"{Repository.Owner.Login}/{Repository.Name}";
                }
                else
                {
                    header = $"{Repository.Owner.Login}/{Repository.Name}: {Repository.Description}";
                }
            }
            else
            {
                header = $"{Repository.Name}/{ContextViewModel.Path} at {ContextViewModel.BranchName} · {Repository.Owner.Login}/{Repository.Name}";
            }

            description = header;
            url = Repository.Url;

            icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new("ms-appx:///Assets/Icons/Repositories.png"))
            };

            NavigationHistory<Services.Navigation.PageNavigationEntry>
                .SetCurrentItem(header, description, url, icon);
        }
    }
}
