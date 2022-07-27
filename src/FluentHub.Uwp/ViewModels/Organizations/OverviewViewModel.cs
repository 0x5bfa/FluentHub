using FluentHub.Octokit.Queries.Organizations;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;

namespace FluentHub.Uwp.ViewModels.Organizations
{
    public class OverviewViewModel : ObservableObject
    {
        public OverviewViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;

            _pinnedItems = new();
            PinnedItems = new(_pinnedItems);

            _repositories = new();
            Repositories = new(_repositories);

            LoadOrganizationOverviewAsyncCommand = new AsyncRelayCommand<string>(LoadOrganizationOverviewAsync);
        }

        #region Fields and Properties
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;

        private readonly ObservableCollection<RepoButtonBlockViewModel> _pinnedItems;
        public ReadOnlyObservableCollection<RepoButtonBlockViewModel> PinnedItems { get; }

        private readonly ObservableCollection<RepoButtonBlockViewModel> _repositories;
        public ReadOnlyObservableCollection<RepoButtonBlockViewModel> Repositories { get; }

        public IAsyncRelayCommand LoadOrganizationOverviewAsyncCommand { get; }
        #endregion

        private async Task LoadOrganizationOverviewAsync(string org, CancellationToken token)
        {
            try
            {
                RepositoryQueries repoQueries = new();
                var repoItems = await repoQueries.GetAllAsync(org);

                _repositories.Clear();
                foreach (var item in repoItems)
                {
                    RepoButtonBlockViewModel viewModel = new()
                    {
                        Repository = item,
                        DisplayDetails = false,
                        DisplayStarButton = false,
                    };

                    _repositories.Add(viewModel);
                }

                PinnedItemQueries queries = new();
                var pinnedItems = await queries.GetAllAsync(org);
                if (pinnedItems == null) return;

                _pinnedItems.Clear();
                foreach (var item in pinnedItems)
                {
                    RepoButtonBlockViewModel viewModel = new()
                    {
                        Repository = item,
                        DisplayDetails = false,
                        DisplayStarButton = false,
                    };

                    _pinnedItems.Add(viewModel);
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadOrganizationOverviewAsync), ex);
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
