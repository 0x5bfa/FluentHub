using FluentHub.Octokit.Queries.Organizations;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using FluentHub.Uwp.ViewModels.UserControls;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;
using System.Text.RegularExpressions;

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

        private bool _oauthAppIsRestrictedByOrgSettings;
        public bool OAuthAppIsRestrictedByOrgSettings { get => _oauthAppIsRestrictedByOrgSettings; set => SetProperty(ref _oauthAppIsRestrictedByOrgSettings, value); }

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
                _logger?.Error(nameof(LoadOrganizationRepositoriesAsync), ex);

                // OAuth restriction exception
                if (Regex.IsMatch(ex.Message, @"Although you appear to have the correct authorization credentials, the `.*` organization has enabled OAuth App access restrictions, meaning that data access to third-parties is limited. For more information on these restrictions, including how to enable this app, visit https://docs.github.com/articles/restricting-access-to-your-organization-s-data/"))
                {
                    OAuthAppIsRestrictedByOrgSettings = true;
                    return;
                }
                else if (_messenger != null)
                {
                    UserNotificationMessage notification = new("Something went wrong", ex.Message, UserNotificationType.Error);
                    _messenger.Send(notification);
                }
                throw;
            }
        }

        public async Task LoadOrganizationAsync(string org)
        {
            try
            {
                OrganizationQueries queries = new();
                var response = await queries.GetAsync(org);

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

                // OAuth restriction exception
                if (Regex.IsMatch(ex.Message, @"Although you appear to have the correct authorization credentials, the `.*` organization has enabled OAuth App access restrictions, meaning that data access to third-parties is limited. For more information on these restrictions, including how to enable this app, visit https://docs.github.com/articles/restricting-access-to-your-organization-s-data/"))
                {
                    OAuthAppIsRestrictedByOrgSettings = true;
                }
                else if (_messenger != null)
                {
                    UserNotificationMessage notification = new("Something went wrong", ex.Message, UserNotificationType.Error);
                    _messenger.Send(notification);
                }
                throw;
            }
        }
    }
}
