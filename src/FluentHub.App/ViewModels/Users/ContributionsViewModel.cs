using FluentHub.Octokit.Queries.Users;
using FluentHub.App.Helpers;
using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.ViewModels.UserControls.Overview;
using FluentHub.App.ViewModels.UserControls.BlockButtons;
using FluentHub.App.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.App.ViewModels.Users
{
    public class ContributionsViewModel : ObservableObject
    {
        public ContributionsViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;

            _discussions = new();
            DiscussionItems = new(_discussions);

            LoadUserDiscussionsPageCommand = new AsyncRelayCommand(LoadUserDiscussionsPageAsync);
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

        private readonly ObservableCollection<DiscussionBlockButtonViewModel> _discussions;
        public ReadOnlyObservableCollection<DiscussionBlockButtonViewModel> DiscussionItems { get; }

        private Exception _taskException;
        public Exception TaskException { get => _taskException; set => SetProperty(ref _taskException, value); }

        public IAsyncRelayCommand LoadUserDiscussionsPageCommand { get; }
        #endregion

        private async Task LoadUserDiscussionsPageAsync()
        {
            _messenger?.Send(new TaskStateMessaging(TaskStatusType.IsStarted));
            bool faulted = false;

            string _currentTaskingMethodName = nameof(LoadUserDiscussionsPageAsync);

            try
            {
                _currentTaskingMethodName = nameof(LoadUserAsync);
                await LoadUserAsync(Login);

                _currentTaskingMethodName = nameof(LoadUserDiscussionsAsync);
                await LoadUserDiscussionsAsync(Login);
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

        private async Task LoadUserDiscussionsAsync(string login)
        {
            DiscussionQueries queries = new();
            var items = await queries.GetAllAsync(login);
            if (items == null) return;

            _discussions.Clear();
            foreach (var item in items)
            {
                DiscussionBlockButtonViewModel viewModel = new()
                {
                    Item = item,
                };

                _discussions.Add(viewModel);
            }
        }

        private async Task LoadUserAsync(string login)
        {
            UserQueries queries = new();
            var response = await queries.GetAsync(login);

            User = response ?? new();

            UserProfileOverviewViewModel = new()
            {
                User = User,
            };

            if (string.IsNullOrEmpty(User.WebsiteUrl) is false)
            {
                UserProfileOverviewViewModel.BuiltWebsiteUrl = new UriBuilder(User.WebsiteUrl).Uri;
            }
        }

        private void SetCurrentTabItem()
        {
            INavigationService navigationService = Ioc.Default.GetRequiredService<INavigationService>();

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Discussions";
            currentItem.Description = $"{User?.Login}'s discussions";
            currentItem.Icon = new ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Discussions.png"))
            };
        }
    }
}
