using FluentHub.Octokit.Queries.Organizations;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;

namespace FluentHub.Uwp.ViewModels.Organizations
{
    public class RepositoriesViewModel : ObservableObject
    {
        public RepositoriesViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;

            _repositories = new();
            Repositories = new(_repositories);

            RefreshRepositoriesCommand = new AsyncRelayCommand<string>(LoadOrganizationRepositoriesAsync);
        }

        #region Fields and Properties
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;

        private readonly ObservableCollection<RepoButtonBlockViewModel> _repositories;
        public ReadOnlyObservableCollection<RepoButtonBlockViewModel> Repositories { get; }

        public IAsyncRelayCommand RefreshRepositoriesCommand { get; }
        #endregion

        private async Task LoadOrganizationRepositoriesAsync(string login, CancellationToken token)
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
                        Repository = item,
                        DisplayDetails = true,
                        DisplayStarButton = true,
                    };

                    _repositories.Add(viewModel);
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                if (!ex.Message.Contains("has enabled OAuth App access restrictions, meaning that data access to third-parties is limited."))
                {
                    _logger?.Error(nameof(LoadOrganizationRepositoriesAsync), ex);
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
}
