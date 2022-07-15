using FluentHub.Octokit.Queries.Repositories;
using FluentHub.Uwp.Utils;
using FluentHub.Uwp.Models;

namespace FluentHub.Uwp.ViewModels.Repositories.Discussions
{
    public class DiscussionViewModel : ObservableObject
    {
        public DiscussionViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;

            LoadDiscussionPageCommand = new AsyncRelayCommand<string>(LoadRepositoryOneDiscussionAsync);
        }

        #region Fields and Properties
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;

        private Discussion _discussion;
        public Discussion Discussion { get => _discussion; set => SetProperty(ref _discussion, value); }

        public IAsyncRelayCommand LoadDiscussionPageCommand { get; }
        #endregion

        private async Task LoadRepositoryOneDiscussionAsync(string url)
        {
            try
            {
                var pathSegments = url.Split("/");

                DiscussionQueries queries = new();
                var response = await queries.GetAsync(pathSegments[3], pathSegments[4], Convert.ToInt32(pathSegments[6]));

                if (response == null) return;

                Discussion = response;
            }
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadRepositoryOneDiscussionAsync), ex);
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
