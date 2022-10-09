using FluentHub.Octokit.Queries.Users;
using FluentHub.Uwp.Utils;
using FluentHub.Uwp.Models;

namespace FluentHub.Uwp.ViewModels.AppSettings.Accounts
{
    public class AccountViewModel : ObservableObject
    {
        public AccountViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _logger = logger;
            _messenger = messenger;

            LoadSignedInLoginsCommand = new AsyncRelayCommand(LoadCurrentUserGitHubInfoAsync);
        }

        #region Fields and Properties
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;

        private User signedInUser;
        public User SignedInUser { get => signedInUser; set => SetProperty(ref signedInUser, value); }

        public IAsyncRelayCommand LoadSignedInLoginsCommand { get; }
        #endregion

        private async Task LoadCurrentUserGitHubInfoAsync()
        {
            try
            {
                UserQueries queries = new();
                var response = await queries.GetAsync(App.AppSettings.SignedInUserName);

                SignedInUser = response;
            }
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadCurrentUserGitHubInfoAsync), ex);
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
