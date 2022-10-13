using CommunityToolkit.WinUI.UI;
using FluentHub.Octokit.Queries.Organizations;
using FluentHub.App.Helpers;
using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.ViewModels.UserControls.Overview;
using FluentHub.App.ViewModels.UserControls.BlockButtons;
using FluentHub.App.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Media.Imaging;
using System.Text.RegularExpressions;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.ViewModels.Organizations
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

            LoadOrganizationOverviewPageCommand = new AsyncRelayCommand(LoadOrganizationOverviewPageAsync);
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

        private readonly ObservableCollection<RepoBlockButtonViewModel> _pinnedItems;
        public ReadOnlyObservableCollection<RepoBlockButtonViewModel> PinnedItems { get; }

        private readonly ObservableCollection<RepoBlockButtonViewModel> _repositories;
        public ReadOnlyObservableCollection<RepoBlockButtonViewModel> Repositories { get; }

        private Exception _taskException;
        public Exception TaskException { get => _taskException; set => SetProperty(ref _taskException, value); }

        public IAsyncRelayCommand LoadOrganizationOverviewPageCommand { get; }
        #endregion

        private async Task LoadOrganizationOverviewPageAsync()
        {
            _messenger?.Send(new TaskStateMessaging(TaskStatusType.IsStarted));
            bool faulted = false;

            string _currentTaskingMethodName = nameof(LoadOrganizationOverviewPageAsync);

            try
            {
                _currentTaskingMethodName = nameof(LoadOrganizationAsync);
                await LoadOrganizationAsync(Login);

                _currentTaskingMethodName = nameof(LoadOrganizationOverviewAsync);
                await LoadOrganizationOverviewAsync(Login);
            }
            catch (Exception ex)
            {
                TaskException = ex;
                faulted = true;

                // OAuth restriction exception
                if (Regex.IsMatch(ex.Message, @"Although you appear to have the correct authorization credentials, the `.*` organization has enabled OAuth App access restrictions, meaning that data access to third-parties is limited. For more information on these restrictions, including how to enable this app, visit https://docs.github.com/articles/restricting-access-to-your-organization-s-data/"))
                {
                    OAuthAppIsRestrictedByOrgSettings = true;
                    faulted = false;
                }
                else
                {
                    _logger?.Error(_currentTaskingMethodName, ex);
                    throw;
                }
            }
            finally
            {
                SetCurrentTabItem();
                _messenger?.Send(new TaskStateMessaging(faulted ? TaskStatusType.IsFaulted : TaskStatusType.IsCompletedSuccessfully));
            }
        }

        private async Task LoadOrganizationOverviewAsync(string org)
        {
            RepositoryQueries repoQueries = new();
            var repoItems = await repoQueries.GetAllAsync(org);

            _repositories.Clear();
            foreach (var item in repoItems)
            {
                RepoBlockButtonViewModel viewModel = new()
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
                RepoBlockButtonViewModel viewModel = new()
                {
                    Repository = item,
                    DisplayDetails = false,
                    DisplayStarButton = false,
                };

                _pinnedItems.Add(viewModel);
            }
        }

        private async Task LoadOrganizationAsync(string org)
        {
            OrganizationQueries queries = new();
            var response = await queries.GetAsync(org);

            Organization = response ?? new();

            // View model
            OrganizationProfileOverviewViewModel = new()
            {
                Organization = Organization,
                SelectedTag = "overview",
            };
        }

        private void SetCurrentTabItem()
        {
            var provider = App.Current.Services;
            INavigationService navigationService = provider.GetRequiredService<INavigationService>();

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = $"{Login}";
            currentItem.Description = $"{Login}";
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Organizations.png"))
            };
        }
    }
}
