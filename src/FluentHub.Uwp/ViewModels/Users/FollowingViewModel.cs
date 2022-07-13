using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using FluentHub.Octokit.Queries.Users;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;

namespace FluentHub.Uwp.ViewModels.Users
{
    public class FollowingViewModel : ObservableObject
    {
        public FollowingViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _logger = logger;
            _messenger = messenger;
            _followingItems = new();
            FollowingItems = new(_followingItems);

            RefreshFollowingCommand = new AsyncRelayCommand<string>(LoadFollowingAsync);
        }

        #region Fields and Properties
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;

        private bool _displayTitle;
        public bool DisplayTitle { get => _displayTitle; set => SetProperty(ref _displayTitle, value); }

        private readonly ObservableCollection<UserButtonBlockViewModel> _followingItems;
        public ReadOnlyObservableCollection<UserButtonBlockViewModel> FollowingItems { get; }

        public IAsyncRelayCommand RefreshFollowingCommand { get; }
        #endregion

        private async Task LoadFollowingAsync(string login, CancellationToken token)
        {
            try
            {
                FollowingQueries queries = new();
                var items = await queries.GetAllAsync(login);
                if (items == null) return;

                _followingItems.Clear();
                foreach (var item in items)
                {
                    UserButtonBlockViewModel viewModel = new()
                    {
                        User = item,
                    };

                    _followingItems.Add(viewModel);
                }
            }
            catch (Exception ex)
            {
                _logger?.Error("RefreshIssuesAsync", ex);
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
