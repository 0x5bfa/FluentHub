using CommunityToolkit.WinUI.UI;
using FluentHub.Octokit.Queries.Organizations;
using FluentHub.App.Helpers;
using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.ViewModels.Repositories;
using FluentHub.App.ViewModels.UserControls.Overview;
using FluentHub.App.ViewModels.UserControls.BlockButtons;
using FluentHub.App.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using System.Text.RegularExpressions;

namespace FluentHub.App.ViewModels.Organizations
{
    public class RepositoriesViewModel : ObservableObject
    {
        public RepositoriesViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;

            _repositories = new();
            Repositories = new(_repositories);

            LoadOrganizationRepositoriesPageCommand = new AsyncRelayCommand(LoadOrganizationRepositoriesPageAsync);
        }

        #region Fields and Properties
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;

        private string _login;
        public string Login { get => _login; set => SetProperty(ref _login, value); }

        private Organization _organization;
        public Organization Organization { get => _organization; set => SetProperty(ref _organization, value); }

        private OrganizationProfileOverviewViewModel _organizationProfileOverviewViewModel;
        public OrganizationProfileOverviewViewModel OrganizationProfileOverviewViewModel { get => _organizationProfileOverviewViewModel; set => SetProperty(ref _organizationProfileOverviewViewModel, value); }

        private bool _oauthAppIsRestrictedByOrgSettings;
        public bool OAuthAppIsRestrictedByOrgSettings { get => _oauthAppIsRestrictedByOrgSettings; set => SetProperty(ref _oauthAppIsRestrictedByOrgSettings, value); }

        private readonly ObservableCollection<RepoBlockButtonViewModel> _repositories;
        public ReadOnlyObservableCollection<RepoBlockButtonViewModel> Repositories { get; }

        private Exception _taskException;
        public Exception TaskException { get => _taskException; set => SetProperty(ref _taskException, value); }

        public IAsyncRelayCommand LoadOrganizationRepositoriesPageCommand { get; }
        #endregion

        private async Task LoadOrganizationRepositoriesPageAsync()
        {
            _messenger?.Send(new TaskStateMessaging(TaskStatusType.IsStarted));
            bool faulted = false;

            string _currentTaskingMethodName = nameof(LoadOrganizationRepositoriesPageAsync);

            try
            {
                _currentTaskingMethodName = nameof(LoadOrganizationAsync);
                await LoadOrganizationAsync(Login);

                _currentTaskingMethodName = nameof(LoadOrganizationRepositoriesAsync);
                await LoadOrganizationRepositoriesAsync(Login);
            }
            catch (Exception ex)
            {
                TaskException = ex;
                faulted = true;

                _logger?.Error(_currentTaskingMethodName, ex);
                throw;
            }
            finally
            {
                SetCurrentTabItem();
                _messenger?.Send(new TaskStateMessaging(faulted ? TaskStatusType.IsFaulted : TaskStatusType.IsCompletedSuccessfully));
            }
        }

        private async Task LoadOrganizationRepositoriesAsync(string org)
        {
            try
            {
                RepositoryQueries queries = new();
                var response = await queries.GetAllAsync(org);

                _repositories.Clear();
                foreach (var item in response)
                {
                    RepoBlockButtonViewModel viewModel = new()
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

        private async Task LoadOrganizationAsync(string org)
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

        private void SetCurrentTabItem()
        {
            INavigationService navigationService = Ioc.Default.GetRequiredService<INavigationService>();

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Repositories";
            currentItem.Description = "Repositories";
            currentItem.Icon = new ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Repositories.png"))
            };
        }
    }
}
