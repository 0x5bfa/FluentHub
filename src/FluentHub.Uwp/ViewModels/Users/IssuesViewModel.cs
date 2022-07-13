using FluentHub.Octokit.Queries.Users;
using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;

namespace FluentHub.Uwp.ViewModels.Users
{
    public class IssuesViewModel : ObservableObject
    {
        public IssuesViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;
            _messenger = messenger;
            _issueItems = new();
            IssueItems = new(_issueItems);

            RefreshIssuesPageCommand = new AsyncRelayCommand<string>(RefreshIssuesPageAsync);
        }

        #region Fields and Properties
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;

        private bool _displayTitle;
        public bool DisplayTitle { get => _displayTitle; set => SetProperty(ref _displayTitle, value); }

        private readonly ObservableCollection<IssueButtonBlockViewModel> _issueItems;
        public ReadOnlyObservableCollection<IssueButtonBlockViewModel> IssueItems { get; }

        public IAsyncRelayCommand RefreshIssuesPageCommand { get; }
        #endregion

        private async Task RefreshIssuesPageAsync(string login, CancellationToken token)
        {
            try
            {
                IssueQueries queries = new();
                var items = await queries.GetAllAsync(login);
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