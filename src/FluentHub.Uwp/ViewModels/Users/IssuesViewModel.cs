using FluentHub.Octokit.Queries.Users;
using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using FluentHub.Uwp.ViewModels.UserControls;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;

namespace FluentHub.Uwp.ViewModels.Users
{
    public class IssuesViewModel : ObservableObject
    {
        public IssuesViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;

            _issueItems = new();
            IssueItems = new(_issueItems);

            RefreshIssuesPageCommand = new AsyncRelayCommand(LoadUserIssuesAsync);
        }

        #region Fields and Properties
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;

        private bool _displayTitle;
        public bool DisplayTitle { get => _displayTitle; set => SetProperty(ref _displayTitle, value); }

        private User _user;
        public User User { get => _user; set => SetProperty(ref _user, value); }

        private UserProfileOverviewViewModel _userProfileOverviewViewModel;
        public UserProfileOverviewViewModel UserProfileOverviewViewModel { get => _userProfileOverviewViewModel; set => SetProperty(ref _userProfileOverviewViewModel, value); }

        private string _login;
        public string Login { get => _login; set => SetProperty(ref _login, value); }

        private readonly ObservableCollection<IssueButtonBlockViewModel> _issueItems;
        public ReadOnlyObservableCollection<IssueButtonBlockViewModel> IssueItems { get; }

        public IAsyncRelayCommand RefreshIssuesPageCommand { get; }
        #endregion

        private async Task LoadUserIssuesAsync(CancellationToken token)
        {
            try
            {
                IssueQueries queries = new();
                var items = await queries.GetAllAsync(Login);
                if (items == null) return;

                _issueItems.Clear();
                foreach (var item in items)
                {
                    IssueButtonBlockViewModel viewModel = new()
                    {
                        IssueItem = item,
                    };

                    _issueItems.Add(viewModel);
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadUserIssuesAsync), ex);
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