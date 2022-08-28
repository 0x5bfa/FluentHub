using FluentHub.Octokit.Queries.Users;
using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.ViewModels.Repositories;
using FluentHub.Uwp.ViewModels.UserControls;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;
using FluentHub.Uwp.Utils;

namespace FluentHub.Uwp.ViewModels.Users
{
    public class OverviewViewModel : ObservableObject
    {
        public OverviewViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _logger = logger;
            _messenger = messenger;

            _pinnedRepositories = new();
            PinnedRepositories = new(_pinnedRepositories);

            _pinnableRepositories = new();
            PinnableRepositories = new(_pinnableRepositories);

            RefreshRepositoryCommand = new AsyncRelayCommand<string>(LoadUserOverviewAsync);
        }

        #region Fields and Properties
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;

        private readonly ObservableCollection<RepoButtonBlockViewModel> _pinnedRepositories;
        public ReadOnlyObservableCollection<RepoButtonBlockViewModel> PinnedRepositories { get; }

        private readonly ObservableCollection<RepoButtonBlockViewModel> _pinnableRepositories;
        public ReadOnlyObservableCollection<RepoButtonBlockViewModel> PinnableRepositories { get; }

        private User _user;
        public User User { get => _user; set => SetProperty(ref _user, value); }

        private UserProfileOverviewViewModel _userProfileOverviewViewModel;
        public UserProfileOverviewViewModel UserProfileOverviewViewModel { get => _userProfileOverviewViewModel; set => SetProperty(ref _userProfileOverviewViewModel, value); }

        private string _login;
        public string Login { get => _login; set => SetProperty(ref _login, value); }

        private RepoContextViewModel _contextViewModel;
        public RepoContextViewModel ContextViewModel { get => _contextViewModel; set => SetProperty(ref _contextViewModel, value); }

        public IAsyncRelayCommand RefreshRepositoryCommand { get; }
        #endregion

        private async Task LoadUserOverviewAsync(string login)
        {
            try
            {
                _pinnableRepositories.Clear();
                _pinnedRepositories.Clear();

                PinnedItemQueries queries = new();
                var pinnedItemsRes = await queries.GetAllAsync(login);
                if (pinnedItemsRes == null) return;

                if (pinnedItemsRes.Count == 0)
                {
                    var pinnableItemsRes = await queries.GetAllPinnableItems(login);
                    if (pinnableItemsRes == null) return;

                    foreach (var item in pinnableItemsRes)
                    {
                        RepoButtonBlockViewModel viewModel = new()
                        {
                            Repository = item,
                            DisplayDetails = false,
                            DisplayStarButton = false,
                        };

                        _pinnableRepositories.Add(viewModel);
                    }
                }
                else
                {
                    foreach (var item in pinnedItemsRes)
                    {
                        RepoButtonBlockViewModel viewModel = new()
                        {
                            Repository = item,
                            DisplayDetails = false,
                            DisplayStarButton = false,
                        };

                        _pinnedRepositories.Add(viewModel);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadUserOverviewAsync), ex);
                if (_messenger != null)
                {
                    UserNotificationMessage notification = new("Something went wrong", ex.Message, UserNotificationType.Error);
                    _messenger.Send(notification);
                }
                throw;
            }
        }

        public async Task LoadUserAsync(string login)
        {
            try
            {
                UserQueries queries = new();
                var response = await queries.GetAsync(login);

                User = response ?? new();

                // View model
                UserProfileOverviewViewModel = new()
                {
                    User = User,
                };

                if (string.IsNullOrEmpty(User.WebsiteUrl) is false)
                {
                    UserProfileOverviewViewModel.BuiltWebsiteUrl = new UriBuilder(User.WebsiteUrl).Uri;
                }
            }
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadUserAsync), ex);
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
