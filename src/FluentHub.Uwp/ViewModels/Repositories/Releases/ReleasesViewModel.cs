using FluentHub.Octokit.Queries.Repositories;
using FluentHub.Uwp.Extensions;
using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.Repositories;
using FluentHub.Uwp.ViewModels.UserControls;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;
using FluentHub.Uwp.Utils;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Media.Imaging;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.ViewModels.Repositories.Releases
{
    public class ReleasesViewModel : ObservableObject
    {
        public ReleasesViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;

            _items = new();
            Items = new(_items);

            LoadRepositoryReleasesPageCommand = new AsyncRelayCommand(LoadRepositoryReleasesPageAsync);
        }

        #region Fields and Properties
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;

        private string _login;
        public string Login { get => _login; set => SetProperty(ref _login, value); }

        private string _name;
        public string Name { get => _name; set => SetProperty(ref _name, value); }

        private Repository _repository;
        public Repository Repository { get => _repository; set => SetProperty(ref _repository, value); }

        private RepoContextViewModel contextViewModel;
        public RepoContextViewModel ContextViewModel { get => contextViewModel; set => SetProperty(ref contextViewModel, value); }

        private RepositoryOverviewViewModel _repositoryOverviewViewModel;
        public RepositoryOverviewViewModel RepositoryOverviewViewModel { get => _repositoryOverviewViewModel; set => SetProperty(ref _repositoryOverviewViewModel, value); }

        private readonly ObservableCollection<Release> _items;
        public ReadOnlyObservableCollection<Release> Items { get; }

        private Release _latestRelease;
        public Release LatestRelease { get => _latestRelease; set => SetProperty(ref _latestRelease, value); }

        private Exception _taskException;
        public Exception TaskException { get => _taskException; set => SetProperty(ref _taskException, value); }

        public Windows.UI.Xaml.Controls.WebView LatestReleaseDescriptionWebView;

        public IAsyncRelayCommand LoadRepositoryReleasesPageCommand { get; }
        #endregion

        private async Task LoadRepositoryReleasesPageAsync()
        {
            _messenger?.Send(new TaskStateMessaging(TaskStatusType.IsStarted));
            bool faulted = false;

            string _currentTaskingMethodName = nameof(LoadRepositoryReleasesPageAsync);

            try
            {
                _currentTaskingMethodName = nameof(LoadRepositoryAsync);
                await LoadRepositoryAsync(Login, Name);

                _currentTaskingMethodName = nameof(LoadRepositoryReleasesAsync);
                await LoadRepositoryReleasesAsync(Login, Name);
            }
            catch (Exception ex)
            {
                TaskException = ex;

                _logger?.Error(_currentTaskingMethodName, ex);
                _messenger?.Send(new TaskStateMessaging(TaskStatusType.IsFaulted));
                throw;
            }
            finally
            {
                SetCurrentTabItem();
                _messenger?.Send(new TaskStateMessaging(faulted ? TaskStatusType.IsFaulted : TaskStatusType.IsCompletedSuccessfully));
            }
        }

        private async Task LoadRepositoryReleasesAsync(string login, string name)
        {
            ReleaseQueries queries = new();
            var items = await queries.GetAllAsync(Repository.Owner.Login, Repository.Name);

            if (items.Any())
            {
                LatestRelease = items.FirstOrDefault();
                //await LoadRepositoryLatestReleaseContent();
            }

            _items.Clear();
            foreach (var item in items) _items.Add(item);
        }

        public async Task LoadRepositoryAsync(string owner, string name)
        {
            RepositoryQueries queries = new();
            Repository = await queries.GetDetailsAsync(owner, name);

            RepositoryOverviewViewModel = new()
            {
                Repository = Repository,
                RepositoryName = Repository.Name,
                RepositoryOwnerLogin = Repository.Owner.Login,
                RepositoryVisibilityLabel = new()
                {
                    Name = Repository.IsPrivate ? "Private" : "Public",
                    Color = "#64000000",
                },
                ViewerSubscriptionState = Repository.ViewerSubscription?.Humanize(),

                SelectedTag = "code",
            };
        }

        private async Task LoadRepositoryLatestReleaseContent()
        {
            if (LatestReleaseDescriptionWebView is null)
                return;

            string missedPath = "https://raw.githubusercontent.com/" + Repository.Owner.Login + "/" + Repository.Name + "/" + Repository.DefaultBranchRef.Name + "/";

            MarkdownApiHandler mdHandler = new();
            var html = await mdHandler.GetHtmlAsync(LatestRelease.DescriptionHTML ?? "<span>No description</span>", missedPath, ThemeHelper.ActualTheme.ToString().ToLower());

            LatestReleaseDescriptionWebView.NavigateToString(html);
            await LatestReleaseDescriptionWebView.HandleResize();

        }

        private void SetCurrentTabItem()
        {
            var provider = App.Current.Services;
            INavigationService navigationService = provider.GetRequiredService<INavigationService>();

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = $"Releases · {Login}/{Name}";
            currentItem.Description = $"Releases · {Login}/{Name}";
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Repositories.png"))
            };
        }
    }
}
