using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Utils;
using FluentHub.Octokit.Authorization;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.Services.Navigation;
using FluentHub.Uwp.ViewModels;
using FluentHub.Uwp.Views;
using FluentHub.Uwp.Views.SignIn;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.IO;
using System.Reflection;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Uwp
{
    sealed partial class App : Application
    {
        Frame rootFrame = Window.Current.Content as Frame;

        public static SettingsViewModel Settings { get; set; } = new SettingsViewModel();

        public static string AppVersion =
            $"{Windows.ApplicationModel.Package.Current.Id.Version.Major}." +
            $"{Windows.ApplicationModel.Package.Current.Id.Version.Minor}." +
            $"{Windows.ApplicationModel.Package.Current.Id.Version.Build}." +
            $"{Windows.ApplicationModel.Package.Current.Id.Version.Revision}";

        public readonly static string DefaultGitHubDomain = "https://github.com";

        public App()
        {
            InitializeComponent();

            Suspending += OnSuspending;

            UnhandledException += async (s, e) =>
            {
                Services.GetService<Serilog.ILogger>()?.Fatal(e.Exception, "Unhandled exception");
#if DEBUG
                e.Handled = true;
                try
                {
                    await new ContentDialog
                    {
                        Title = "Unhandled exception",
                        Content = e.Message,
                        CloseButtonText = "Close"
                    }.ShowAsync();
                }
                catch { }
#endif
            };

            Serilog.Log.Logger = GetSerilogLogger();
            Services = ConfigureServices();
        }

        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public new static App Current => (App)Application.Current;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>
        public IServiceProvider Services { get; }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private static IServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<INavigationService, NavigationService>()
                .AddSingleton<Utils.ILogger>(new SerilogWrapperLogger(Log.Logger))
                .AddSingleton<ToastService>()
                .AddSingleton<IMessenger>(StrongReferenceMessenger.Default)
                // ViewModels
                .AddSingleton<MainPageViewModel>()
                .AddTransient<ViewModels.AppSettings.AboutViewModel>()
                .AddTransient<ViewModels.AppSettings.Accounts.AccountViewModel>()
                .AddTransient<ViewModels.AppSettings.Accounts.OtherUsersViewModel>()
                .AddTransient<ViewModels.AppSettings.AppearanceViewModel>()
                .AddTransient<ViewModels.Dialogs.AccountSwitchingDialogViewModel>()
                .AddTransient<ViewModels.Dialogs.EditPinnedRepositoriesDialogViewModel>()
                .AddTransient<ViewModels.Home.FeedsViewModel>()
                .AddTransient<ViewModels.Home.NotificationsViewModel>()
                .AddTransient<ViewModels.Home.UserHomeViewModel>()
                .AddTransient<ViewModels.Organizations.OverviewViewModel>()
                .AddTransient<ViewModels.Organizations.RepositoriesViewModel>()
                .AddTransient<ViewModels.Repositories.Code.Layouts.DetailsLayoutViewModel>()
                .AddTransient<ViewModels.Repositories.Code.Layouts.TreeLayoutViewModel>()
                .AddTransient<ViewModels.Repositories.Code.ReleasesViewModel>()
                .AddTransient<ViewModels.Repositories.Commits.CommitsViewModel>()
                .AddTransient<ViewModels.Repositories.Commits.CommitViewModel>()
                .AddTransient<ViewModels.Repositories.Discussions.DiscussionsViewModel>()
                .AddTransient<ViewModels.Repositories.Discussions.DiscussionViewModel>()
                .AddTransient<ViewModels.Repositories.Issues.IssueViewModel>()
                .AddTransient<ViewModels.Repositories.Issues.IssuesViewModel>()
                .AddTransient<ViewModels.Repositories.Projects.ProjectsViewModel>()
                .AddTransient<ViewModels.Repositories.Projects.ProjectViewModel>()
                .AddTransient<ViewModels.Repositories.PullRequests.ConversationViewModel>()
                .AddTransient<ViewModels.Repositories.PullRequests.CommitViewModel>()
                .AddTransient<ViewModels.Repositories.PullRequests.CommitsViewModel>()
                .AddTransient<ViewModels.Repositories.PullRequests.FileChangesViewModel>()
                .AddTransient<ViewModels.Repositories.PullRequests.PullRequestsViewModel>()
                .AddTransient<ViewModels.Search.CodeViewModel>()
                .AddTransient<ViewModels.Search.MainSearchViewModel>()
                .AddTransient<ViewModels.Search.RepoViewModel>()
                .AddTransient<ViewModels.SignIn.IntroViewModel>()
                .AddTransient<ViewModels.UserControls.Blocks.FileContentBlockViewModel>()
                .AddTransient<ViewModels.UserControls.Blocks.FileNavigationBlockViewModel>()
                .AddTransient<ViewModels.UserControls.Blocks.IssueCommentBlockViewModel>()
                .AddTransient<ViewModels.UserControls.Blocks.ReadmeContentBlockViewModel>()
                .AddTransient<ViewModels.UserControls.Blocks.LatestCommitBlockViewModel>()
                .AddTransient<ViewModels.Users.FollowersViewModel>()
                .AddTransient<ViewModels.Users.FollowingViewModel>()
                .AddTransient<ViewModels.Users.IssuesViewModel>()
                .AddTransient<ViewModels.Users.OverviewViewModel>()
                .AddTransient<ViewModels.Users.PullRequestsViewModel>()
                .AddTransient<ViewModels.Users.DiscussionsViewModel>()
                .AddTransient<ViewModels.Users.RepositoriesViewModel>()
                .AddTransient<ViewModels.Users.OrganizationsViewModel>()
                .AddTransient<ViewModels.Users.StarredReposViewModel>()
                .BuildServiceProvider();
        }

        private static Serilog.ILogger GetSerilogLogger()
        {
            string logFilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "FluentHub.Logs/Log.log");

            var logger = new Serilog.LoggerConfiguration()
                .MinimumLevel
#if DEBUG
                .Verbose()
#else
                .Error()
#endif
                .WriteTo
                .File(logFilePath, rollingInterval: Serilog.RollingInterval.Day)
                .CreateLogger();

            return logger;
        }

        private void Initialize(LaunchActivatedEventArgs args = null)
        {
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            ApplicationView.GetForCurrentView().TitleBar.ButtonBackgroundColor = Colors.Transparent;
            ApplicationView.GetForCurrentView().TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

            if (rootFrame == null)
            {
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                Window.Current.Content = rootFrame;
            }

            if (args != null)
            {
                if (!args.PrelaunchActivated)
                {
                    CoreApplication.EnablePrelaunch(true);

                    if (rootFrame.Content == null)
                    {
                        if (Settings.SetupCompleted == true)
                        {
                            InitializeOctokit.InitializeApiConnections(Settings.AccessToken);

                            rootFrame.Navigate(typeof(MainPage));
                        }
                        else
                        {
                            Settings.SetupProgress = false;
                            Settings.SetupCompleted = false;

                            rootFrame.Navigate(typeof(IntroPage));
                        }
                    }
                }
            }
            else if (rootFrame.Content == null)
            {
                if (Settings.SetupCompleted == true)
                {
                    InitializeOctokit.InitializeApiConnections(Settings.AccessToken);

                    rootFrame.Navigate(typeof(MainPage));
                }
                else
                {
                    Settings.SetupProgress = false;
                    Settings.SetupCompleted = false;

                    rootFrame.Navigate(typeof(IntroPage));
                }
            }

            ThemeHelper.Initialize();
            Window.Current.Activate();

            var logger = Services.GetService<Utils.ILogger>();
            logger?.Debug("App.Initialize() done");
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            bool openInNewTab = false;

            if (rootFrame is null)
            {
                openInNewTab = true;
            }

            Initialize(args);

            if (!string.IsNullOrWhiteSpace(args.Arguments) && Uri.TryCreate(args.Arguments, UriKind.RelativeOrAbsolute, out var uri))
            {
                await HandleUriActivationAsync(uri, openInNewTab);
            }
        }

        protected async override void OnActivated(IActivatedEventArgs args)
        {
            Initialize();

            switch (args.Kind)
            {
                case ActivationKind.Protocol:
                    var protocolArgs = (ProtocolActivatedEventArgs)args;
                    await HandleUriActivationAsync(protocolArgs.Uri, true);
                    break;
            }
        }

        private async Task HandleUriActivationAsync(Uri uri, bool openInNewTab)
        {
            var logger = Services.GetService<Utils.ILogger>();
            logger?.Debug("App.HandleUriActivationAsync(): {uri}", uri);

            Type page = null;
            object param = null;
            switch (uri.Authority.ToLower())
            {
                case "profile":
                    page = typeof(Views.Users.OverviewPage);
                    break;
                case "notifications":
                    page = typeof(Views.Home.NotificationsPage);
                    break;
                case "activities":
                    page = typeof(Views.Home.ActivitiesPage);
                    break;
                case "issues":
                    page = typeof(Views.Repositories.Issues.IssuesPage);
                    break;
                case "pullrequests":
                    page = typeof(Views.Repositories.PullRequests.PullRequestsPage);
                    break;
                case "discussions":
                    page = typeof(Views.Repositories.Discussions.DiscussionsPage);
                    break;
                case "repositories":
                    page = App.Settings.UseDetailsView ?
                        typeof(Views.Repositories.Code.Layouts.DetailsLayoutView) :
                        typeof(Views.Repositories.Code.Layouts.TreeLayoutView);
                    break;
                case "organizations":
                    page = typeof(Views.Organizations.OverviewPage);
                    break;
                case "stars":
                    page = typeof(Views.Users.StarredReposPage);
                    param = uri.AbsoluteUri;
                    break;
                case "settings":
                    page = typeof(Views.AppSettings.MainSettingsPage);
                    param = uri.AbsoluteUri;
                    break;
                case "reset":
                    page = typeof(Views.SignIn.IntroPage);
                    //  This code should work, but throws a 'Use of unassigned variable 'rootFrame'' error.
                    //  Frame rootFrame = (Frame)Window.Current.Content;
                    //  rootFrame.Navigate(typeof(SignIn.IntroPage));
                    param = uri.AbsoluteUri;
                    break;
                    // The UI of this feature is slightly broken, therefore
                    // TODO: Fix logout protocol
                case "auth" when uri.Query.Contains("code"): // fluenthub://auth?code=[code]
                    var code = new WwwFormUrlDecoder(uri.Query).GetFirstValueByName("code");
                    bool status;

                    try
                    {
                        AuthorizationService authService = new();
                        var accessToken = await authService.RequestOAuthTokenAsync(code);
                        logger?.Info("FluentHub is authorized successfully.");

                        // Set token and login to App Settings Container
                        await SetAccountInfo(accessToken);

                        status = true;
                    }
                    catch (Exception ex)
                    {
                        status = false;
                        logger?.Info("FluentHub failed to authorize.", ex);
                    }

                    if (status)
                    {
                        Settings.SetupProgress = true;
                        Settings.SetupCompleted = true;
                        rootFrame.Navigate(typeof(MainPage));
                    }

                    return;
            }

            var ns = Services.GetRequiredService<INavigationService>();
            if (ns.IsConfigured)
            {
                if (page != null)
                {
                    if (openInNewTab)
                        ns.OpenTab(page, param);
                    else
                        ns.Navigate(page, param);
                }
            }
        }

        private async Task SetAccountInfo(string accessToken)
        {
            Settings.AccessToken = accessToken;

            Octokit.Queries.Users.UserQueries queries = new();
            string login = await queries.GetViewerLogin();
            Settings.SignedInUserName = login;

            AccountService.AddAccount(login);
        }

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }

        public static TEnum GetEnum<TEnum>(string text) where TEnum : struct
        {
            if (!typeof(TEnum).GetTypeInfo().IsEnum)
            {
                throw new InvalidOperationException("Generic parameter 'TEnum' must be an enum.");
            }
            return (TEnum)Enum.Parse(typeof(TEnum), text);
        }

        public static async void CloseApp()
        {
            if (!await ApplicationView.GetForCurrentView().TryConsolidateAsync())
            {
                Current.Exit();
            }
        }
    }
}
