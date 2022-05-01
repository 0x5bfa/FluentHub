using FluentHub.Helpers;
using FluentHub.Octokit.Authorization;
using FluentHub.Services;
using FluentHub.Services.Navigation;
using FluentHub.ViewModels;
using FluentHub.Views;
using FluentHub.Views.SignIn;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Messaging;
using Octokit;
using Serilog;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
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

namespace FluentHub
{
    sealed partial class App : Windows.UI.Xaml.Application
    {
        Frame rootFrame = Window.Current.Content as Frame;

        public static SettingsViewModel Settings { get; private set; } = new SettingsViewModel();

        public static string AppVersion = $"{Package.Current.Id.Version.Major}.{Package.Current.Id.Version.Minor}.{Package.Current.Id.Version.Build}.{Package.Current.Id.Version.Revision}";

        public App()
        {
            InitializeComponent();

            Suspending += OnSuspending;

            UnhandledException += async (s, e) =>
            {
                Services.GetService<ILogger>()?.Fatal(e.Exception, "Unhandled exception");
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

            Log.Logger = GetSerilogLogger();
            Services = ConfigureServices();
        }

        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public new static App Current => (App)Windows.UI.Xaml.Application.Current;

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
                .AddSingleton<Backend.ILogger>(new Utils.SerilogWrapperLogger(Log.Logger))
                .AddSingleton<Backend.ToastService>()
                .AddSingleton<IMessenger>(StrongReferenceMessenger.Default)
                // ViewModels
                .AddSingleton<MainPageViewModel>()
                .AddTransient<ViewModels.AppSettings.AboutViewModel>()
                .AddTransient<ViewModels.AppSettings.AppearanceViewModel>()
                .AddTransient<ViewModels.Home.NotificationsViewModel>()
                .AddTransient<ViewModels.Home.ActivitiesViewModel>()
                .AddTransient<ViewModels.Organizations.OverviewViewModel>()
                .AddTransient<ViewModels.Organizations.RepositoriesViewModel>()
                .AddTransient<ViewModels.Repositories.Codes.Layouts.DetailsLayoutViewModel>()
                .AddTransient<ViewModels.Repositories.Codes.ReleasesViewModel>()
                .AddTransient<ViewModels.Repositories.Commits.CommitsViewModel>()
                .AddTransient<ViewModels.Repositories.Commits.CommitViewModel>()
                .AddTransient<ViewModels.Repositories.Discussions.DiscussionsViewModel>()
                .AddTransient<ViewModels.Repositories.Discussions.DiscussionViewModel>()
                .AddTransient<ViewModels.Repositories.Issues.IssueViewModel>()
                .AddTransient<ViewModels.Repositories.Issues.IssuesViewModel>()
                .AddTransient<ViewModels.Repositories.Projects.ProjectsViewModel>()
                .AddTransient<ViewModels.Repositories.Projects.ProjectViewModel>()
                .AddTransient<ViewModels.Repositories.PullRequests.ConversationViewModel>()
                .AddTransient<ViewModels.Repositories.PullRequests.CommitsViewModel>()
                .AddTransient<ViewModels.Repositories.PullRequests.FileChangesViewModel>()
                .AddTransient<ViewModels.Repositories.PullRequests.PullRequestViewModel>()
                .AddTransient<ViewModels.Repositories.PullRequests.PullRequestsViewModel>()
                .AddTransient<ViewModels.UserControls.Blocks.FileContentBlockViewModel>()
                .AddTransient<ViewModels.UserControls.Blocks.FileNavigationBlockViewModel>()
                .AddTransient<ViewModels.UserControls.Blocks.ReadmeContentBlockViewModel>()
                .AddTransient<ViewModels.UserControls.Blocks.LatestCommitBlockViewModel>()
                .AddTransient<ViewModels.Users.FollowersViewModel>()
                .AddTransient<ViewModels.Users.FollowingViewModel>()
                .AddTransient<ViewModels.Users.ProfilePageViewModel>()
                .AddTransient<ViewModels.Users.IssuesViewModel>()
                .AddTransient<ViewModels.Users.OverviewViewModel>()
                .AddTransient<ViewModels.Users.PullRequestsViewModel>()
                .AddTransient<ViewModels.Users.DiscussionsViewModel>()
                .AddTransient<ViewModels.Users.RepositoriesViewModel>()
                .AddTransient<ViewModels.Users.OrganizationsViewModel>()
                .AddTransient<ViewModels.Users.StarredReposViewModel>()
                .AddTransient<ViewModels.Users.FollowersViewModel>()
                .AddTransient<ViewModels.Users.FollowingViewModel>()
                .BuildServiceProvider();
        }

        private static ILogger GetSerilogLogger()
        {
            string logFilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "FluentHub.Logs/Log.log");

            var logger = new LoggerConfiguration()
                .MinimumLevel
#if DEBUG
                .Verbose()
#else
                .Error()
#endif
                .WriteTo
                .File(logFilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            return logger;
        }

        private void InitializeAsync()
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

            if (rootFrame.Content == null)
            {
                if (Settings.SetupCompleted == true)
                {
                    rootFrame.Navigate(typeof(MainPage));
                }
                else
                {
                    Settings.SetupProgress = false;
                    Settings.SetupCompleted = false;

                    rootFrame.Navigate(typeof(IntroPage));
                }

                ThemeHelper.Initialize();
                Window.Current.Activate();
            }

            Log.Information("App.InitializeAsync() done");
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            bool openInNewTab = false;

            if (rootFrame is null)
            {
                openInNewTab = true;
            }

            InitializeAsync();

            if (!string.IsNullOrWhiteSpace(args.Arguments) && Uri.TryCreate(args.Arguments, UriKind.RelativeOrAbsolute, out var uri))
            {
                await HandleUriActivationAsync(uri, openInNewTab);
            }
        }

        protected async override void OnActivated(IActivatedEventArgs args)
        {
            InitializeAsync();

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
            var logger = Services.GetService<ILogger>();
            logger?.Debug("App.HandleUriActivationAsync(): {uri}", uri);

            Type page = null;
            object param = null;
            switch (uri.Authority.ToLower())
            {
                case "profile":
                case "notifications":
                case "activities":
                case "issues":
                case "pullrequests":
                case "discussions":
                case "repositories":
                case "organizations":
                case "starred":
                    page = typeof(Views.Home.UserHomePage);
                    param = uri.Authority;
                    break;
                case "settings":
                    page = typeof(Views.AppSettings.MainSettingsPage);
                    if (uri.Query.Contains("page"))
                        param = new WwwFormUrlDecoder(uri.Query).GetFirstValueByName("page");
                    break;
                case "auth" when uri.Query.Contains("code"): // fluenthub://auth?code=[code]
                    var code = new WwwFormUrlDecoder(uri.Query).GetFirstValueByName("code");

                    AuthorizationService authService = new();
                    bool status = await authService.RequestOAuthTokenAsync(code);

                    if (status)
                    {
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
