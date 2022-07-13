using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using FluentHub.Octokit.Queries.Users;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;

namespace FluentHub.Uwp.ViewModels.Users
{
    public class DiscussionsViewModel : ObservableObject
    {
        public DiscussionsViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;

            _discussions = new();
            DiscussionItems = new(_discussions);

            RefreshDiscussionsCommand = new AsyncRelayCommand<string>(RefreshDiscussionsAsync);
        }

        #region Fields and Properties
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;

        private bool _displayTitle;
        public bool DisplayTitle { get => _displayTitle; set => SetProperty(ref _displayTitle, value); }

        private readonly ObservableCollection<DiscussionButtonBlockViewModel> _discussions;
        public ReadOnlyObservableCollection<DiscussionButtonBlockViewModel> DiscussionItems { get; }

        public IAsyncRelayCommand RefreshDiscussionsCommand { get; }
        #endregion

        private bool CanRefreshDiscussions(string username) => !string.IsNullOrEmpty(username);

        private async Task RefreshDiscussionsAsync(string login, CancellationToken token)
        {
            try
            {
                DiscussionQueries queries = new();
                var items = await queries.GetAllAsync(login);
                if (items == null) return;

                _discussions.Clear();
                foreach (var item in items)
                {
                    DiscussionButtonBlockViewModel viewModel = new()
                    {
                        Item = item,
                    };

                    _discussions.Add(viewModel);
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                _logger?.Error(nameof(RefreshDiscussionsAsync), ex);
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
