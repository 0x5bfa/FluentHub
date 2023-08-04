using FluentHub.Octokit.Queries.Repositories;
using FluentHub.App.Extensions;
using FluentHub.App.Helpers;
using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.Utils;
using FluentHub.App.ViewModels.UserControls.Overview;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.App.ViewModels.Repositories.Releases
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

        public WebView2 LatestReleaseDescriptionWebView2;

        private bool _failedToLoadWebView2Content;
        public bool FailedToLoadWebView2Content { get => _failedToLoadWebView2Content; set => SetProperty(ref _failedToLoadWebView2Content, value); }

        private Exception _taskException;
        public Exception TaskException { get => _taskException; set => SetProperty(ref _taskException, value); }

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

                _currentTaskingMethodName = nameof(LoadRepositoryLatestReleaseContent);
                await LoadRepositoryLatestReleaseContent();
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
            var items = await queries.GetAllAsync(login, name);

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
                ViewerSubscriptionState = Repository.ViewerSubscription?.Humanize(),

                SelectedTag = "code",
            };
        }

        private async Task LoadRepositoryLatestReleaseContent()
        {
            if (LatestReleaseDescriptionWebView2 == null)
                return;

            string missingPath = $"https://raw.githubusercontent.com/{Repository.Owner.Login}/{Repository.Name}/{Repository.DefaultBranchRef.Name}/";

            MarkdownApiHandler mdHandler = new();
            var html = await mdHandler.GetHtmlAsync(LatestRelease.DescriptionHTML ?? "<span>No description provided</span>", missingPath, ThemeHelpers.RootTheme.ToString().ToLower());

            // https://github.com/microsoft/microsoft-ui-xaml/issues/3714
            await LatestReleaseDescriptionWebView2.EnsureCoreWebView2Async();

            // https://github.com/microsoft/microsoft-ui-xaml/issues/1967
            // It is no longer the plan for WebView2 to support ms-appx-web:/// and ms-appx-data:///.
            // Instead of using these proprietary protocols the SetVirtualHostNameToFolderMapping API is recommended.
            var CoreWebView2 = LatestReleaseDescriptionWebView2.CoreWebView2;
            if (CoreWebView2 != null)
            {
                CoreWebView2.SetVirtualHostNameToFolderMapping(
                    "fluenthub.app", "Assets/",
                    Microsoft.Web.WebView2.Core.CoreWebView2HostResourceAccessKind.Allow);

                LatestReleaseDescriptionWebView2.NavigateToString(html);
            }
            else
            {
                FailedToLoadWebView2Content = true;
                _logger.Error("Could not initialize WebView2Core in Repository's Releases page.");
            }
        }

        private void SetCurrentTabItem()
        {
            INavigationService navigationService = Ioc.Default.GetRequiredService<INavigationService>();

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = $"Releases · {Login}/{Name}";
            currentItem.Description = $"Releases · {Login}/{Name}";
            currentItem.Icon = new ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Repositories.png"))
            };
        }
    }
}
