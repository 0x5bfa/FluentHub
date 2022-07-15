using FluentHub.Octokit.Queries.Users;
using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;
using FluentHub.Uwp.Utils;

namespace FluentHub.Uwp.ViewModels.Users
{
    public class RepositoriesViewModel : ObservableObject
    {
        public RepositoriesViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;
            _repositories = new();
            Repositories = new(_repositories);

            RefreshRepositoriesCommand = new AsyncRelayCommand<string>(LoadUserRepositoriesAsync);
        }

        #region Fields and Properties
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;

        private bool _displayTitle;
        public bool DisplayTitle { get => _displayTitle; set => SetProperty(ref _displayTitle, value); }

        private readonly ObservableCollection<RepoButtonBlockViewModel> _repositories;
        public ReadOnlyObservableCollection<RepoButtonBlockViewModel> Repositories { get; }

        public IAsyncRelayCommand RefreshRepositoriesCommand { get; }
        #endregion

        private async Task LoadUserRepositoriesAsync(string login, CancellationToken token)
        {
            try
            {
                RepositoryQueries queries = new();
                var items = await queries.GetAllAsync(login);
                if (items == null) return;

                _repositories.Clear();
                foreach (var item in items)
                {
                    RepoButtonBlockViewModel viewModel = new()
                    {
                        Item = item,
                        DisplayDetails = true,
                        DisplayStarButton = true,
                    };

                    _repositories.Add(viewModel);
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadUserRepositoriesAsync), ex);
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
