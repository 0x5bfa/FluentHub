using FluentHub.Octokit.Queries.Users;
using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;
using FluentHub.Uwp.Utils;

namespace FluentHub.Uwp.ViewModels.Users
{
    public class FollowersViewModel : ObservableObject
    {
        public FollowersViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _logger = logger;
            _messenger = messenger;
            _followersItems = new();
            FollowersItems = new(_followersItems);

            RefreshFollowersCommand = new AsyncRelayCommand<string>(LoadUserFollowersAsync);
        }

        #region Fields and Properties
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;

        private bool _displayTitle;
        public bool DisplayTitle { get => _displayTitle; set => SetProperty(ref _displayTitle, value); }

        private readonly ObservableCollection<UserButtonBlockViewModel> _followersItems;
        public ReadOnlyObservableCollection<UserButtonBlockViewModel> FollowersItems { get; }

        public IAsyncRelayCommand RefreshFollowersCommand { get; }
        #endregion

        private async Task LoadUserFollowersAsync(string login, CancellationToken token)
        {
            try
            {
                FollowersQueries queries = new();
                var items = await queries.GetAllAsync(login);
                if (items == null) return;

                _followersItems.Clear();
                foreach (var item in items)
                {
                    UserButtonBlockViewModel viewModel = new()
                    {
                        User = item,
                    };

                    _followersItems.Add(viewModel);
                }
            }
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadUserFollowersAsync), ex);
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
