using FluentHub.Octokit.Queries.Organizations;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using FluentHub.Uwp.ViewModels.UserControls;
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

            RefreshRepositoriesCommand = new AsyncRelayCommand(LoadOrganizationRepositoriesAsync);
        }

        #region Fields and Properties
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;

        private Organization _organization;
        public Organization Organization { get => _organization; set => SetProperty(ref _organization, value); }

        private OrganizationProfileOverviewViewModel _organizationProfileOverviewViewModel;
        public OrganizationProfileOverviewViewModel OrganizationProfileOverviewViewModel { get => _organizationProfileOverviewViewModel; set => SetProperty(ref _organizationProfileOverviewViewModel, value); }

        private string _login;
        public string Login { get => _login; set => SetProperty(ref _login, value); }

        private readonly ObservableCollection<RepoButtonBlockViewModel> _repositories;
        public ReadOnlyObservableCollection<RepoButtonBlockViewModel> Repositories { get; }

        public IAsyncRelayCommand RefreshRepositoriesCommand { get; }
        #endregion

        private async Task LoadOrganizationRepositoriesAsync(CancellationToken token)
        {
            try
            {
                RepositoryQueries queries = new();
                var response = await queries.GetAllAsync(Login);

                _repositories.Clear();
                foreach (var item in response)
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

        public async Task LoadOrganizationAsync(string org)
        {
            try
            {
                OrganizationQueries queries = new();
                var response = await queries.GetOverview(org);

                Organization = response ?? new();

                // View model
                OrganizationProfileOverviewViewModel = new()
                {
                    Organization = Organization,
                    SelectedTag = "repositories",
                };
            }
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadOrganizationAsync), ex);
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
