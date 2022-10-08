using FluentHub.Octokit.Queries.Users;
using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.UserControls.Overview;
using FluentHub.Uwp.ViewModels.UserControls.BlockButtons;
using FluentHub.Uwp.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Media.Imaging;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.ViewModels.Users
{
    public class OrganizationsViewModel : ObservableObject
    {
        public OrganizationsViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;

            _organizations = new();
            Organizations = new(_organizations);

            LoadUserOrganizationsPageCommand = new AsyncRelayCommand(LoadUserOrganizationsPageAsync);
        }

        #region Fields and Properties
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;

        private string _login;
        public string Login { get => _login; set => SetProperty(ref _login, value); }

        private User _user;
        public User User { get => _user; set => SetProperty(ref _user, value); }

        private UserProfileOverviewViewModel _userProfileOverviewViewModel;
        public UserProfileOverviewViewModel UserProfileOverviewViewModel { get => _userProfileOverviewViewModel; set => SetProperty(ref _userProfileOverviewViewModel, value); }

        private bool _displayTitle;
        public bool DisplayTitle { get => _displayTitle; set => SetProperty(ref _displayTitle, value); }

        private readonly ObservableCollection<OrgBlockButtonViewModel> _organizations;
        public ReadOnlyObservableCollection<OrgBlockButtonViewModel> Organizations { get; }

        private Exception _taskException;
        public Exception TaskException { get => _taskException; set => SetProperty(ref _taskException, value); }

        public IAsyncRelayCommand LoadUserOrganizationsPageCommand { get; }
        #endregion

        private async Task LoadUserOrganizationsPageAsync()
        {
            _messenger?.Send(new TaskStateMessaging(TaskStatusType.IsStarted));
            bool faulted = false;

            string _currentTaskingMethodName = nameof(LoadUserOrganizationsPageAsync);

            try
            {
                _currentTaskingMethodName = nameof(LoadUserAsync);
                await LoadUserAsync(Login);

                _currentTaskingMethodName = nameof(LoadUserOrganizationsAsync);
                await LoadUserOrganizationsAsync(Login);
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

        private async Task LoadUserOrganizationsAsync(string login)
        {
            OrganizationQueries queries = new();
            var items = await queries.GetAllAsync(login);
            if (items == null) return;

            _organizations.Clear();
            foreach (var item in items)
            {
                OrgBlockButtonViewModel viewModel = new()
                {
                    OrgItem = item
                };

                _organizations.Add(viewModel);
            }
        }

        public async Task LoadUserAsync(string login)
        {
            UserQueries queries = new();
            var response = await queries.GetAsync(login);

            User = response ?? new();

            UserProfileOverviewViewModel = new()
            {
                User = User,
                SelectedTag = "organizations"
            };

            if (string.IsNullOrEmpty(User.WebsiteUrl) is false)
            {
                UserProfileOverviewViewModel.BuiltWebsiteUrl = new UriBuilder(User.WebsiteUrl).Uri;
            }
        }

        private void SetCurrentTabItem()
        {
            var provider = App.Current.Services;
            INavigationService navigationService = provider.GetRequiredService<INavigationService>();

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Organizations";
            currentItem.Description = $"{User?.Login}'s organizations";
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Organizations.png"))
            };
        }
    }
}
