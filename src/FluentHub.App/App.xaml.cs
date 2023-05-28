// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.Utils;
using FluentHub.App.Services;
using FluentHub.App.Services.Navigation;
using FluentHub.App.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Windowing;
using Microsoft.Windows.AppLifecycle;
using Windows.ApplicationModel;
using Windows.Storage;
using CommunityToolkit.WinUI;

namespace FluentHub.App
{
    public partial class App : Application
    {
        public static MainWindow Window { get; private set; } = null!;

        public static IntPtr WindowHandle { get; private set; }

        public new static App Current
            => (App)Application.Current;

        public IServiceProvider Services { get; }

        public static SettingsViewModel AppSettings { get; set; }

        public static string AppVersion =
            $"{Windows.ApplicationModel.Package.Current.Id.Version.Major}." +
            $"{Windows.ApplicationModel.Package.Current.Id.Version.Minor}." +
            $"{Windows.ApplicationModel.Package.Current.Id.Version.Build}." +
            $"{Windows.ApplicationModel.Package.Current.Id.Version.Revision}";

        public App()
        {
            InitializeComponent();

            UnhandledException += OnUnhandledException;
            TaskScheduler.UnobservedTaskException += OnUnobservedException;

            Services = ConfigureServices();
        }

        private static IServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<INavigationService, NavigationService>()
                .AddSingleton<ILogger>(new SerilogWrapperLogger(Serilog.Log.Logger))
                .AddSingleton<ToastService>()
                .AddSingleton<IMessenger>(StrongReferenceMessenger.Default)
                // ViewModels
                .AddSingleton<MainPageViewModel>()
                .AddSingleton<ViewModels.SignIn.IntroViewModel>()
                .AddTransient<ViewModels.AppSettings.AboutViewModel>()
                .AddTransient<ViewModels.AppSettings.Accounts.AccountViewModel>()
                .AddTransient<ViewModels.AppSettings.Accounts.OtherUsersViewModel>()
                .AddTransient<ViewModels.AppSettings.AppearanceViewModel>()
                .AddTransient<ViewModels.Dialogs.AccountSwitchingDialogViewModel>()
                .AddTransient<ViewModels.Dialogs.EditPinnedRepositoriesDialogViewModel>()
                .AddTransient<ViewModels.Dialogs.EditUserProfileViewModel>()
                .AddTransient<ViewModels.Viewers.FeedsViewModel>()
                .AddTransient<ViewModels.Viewers.NotificationsViewModel>()
                .AddTransient<ViewModels.Viewers.UserHomeViewModel>()
                .AddTransient<ViewModels.Organizations.OverviewViewModel>()
                .AddTransient<ViewModels.Organizations.RepositoriesViewModel>()
                .AddTransient<ViewModels.Repositories.Code.Layouts.DetailsLayoutViewModel>()
                .AddTransient<ViewModels.Repositories.Code.Layouts.TreeLayoutViewModel>()
                .AddTransient<ViewModels.Repositories.Commits.CommitsViewModel>()
                .AddTransient<ViewModels.Repositories.Commits.CommitViewModel>()
                .AddTransient<ViewModels.Repositories.Discussions.DiscussionsViewModel>()
                .AddTransient<ViewModels.Repositories.Discussions.DiscussionViewModel>()
                .AddTransient<ViewModels.Repositories.Issues.IssueViewModel>()
                .AddTransient<ViewModels.Repositories.Issues.IssuesViewModel>()
                .AddTransient<ViewModels.Repositories.Projects.ProjectsViewModel>()
                .AddTransient<ViewModels.Repositories.Projects.ProjectViewModel>()
                .AddTransient<ViewModels.Repositories.PullRequests.ChecksViewModel>()
                .AddTransient<ViewModels.Repositories.PullRequests.ConversationViewModel>()
                .AddTransient<ViewModels.Repositories.PullRequests.CommitViewModel>()
                .AddTransient<ViewModels.Repositories.PullRequests.CommitsViewModel>()
                .AddTransient<ViewModels.Repositories.PullRequests.FileChangesViewModel>()
                .AddTransient<ViewModels.Repositories.PullRequests.PullRequestsViewModel>()
                .AddTransient<ViewModels.Repositories.Releases.ReleasesViewModel>()
                .AddTransient<ViewModels.Repositories.Releases.ReleaseViewModel>()
                .AddTransient<ViewModels.Searches.CodeViewModel>()
                .AddTransient<ViewModels.Searches.IssuesViewModel>()
                .AddTransient<ViewModels.Searches.RepositoriesViewModel>()
                .AddTransient<ViewModels.Searches.UsersViewModel>()
                .AddTransient<ViewModels.UserControls.FileContentBlockViewModel>()
                .AddTransient<ViewModels.UserControls.FileNavigationBlockViewModel>()
                .AddTransient<ViewModels.UserControls.IssueCommentBlockViewModel>()
                .AddTransient<ViewModels.UserControls.ReadmeContentBlockViewModel>()
                .AddTransient<ViewModels.UserControls.LatestCommitBlockViewModel>()
                .AddTransient<ViewModels.Users.ContributionsViewModel>()
                .AddTransient<ViewModels.Users.DiscussionsViewModel>()
                .AddTransient<ViewModels.Users.FollowersViewModel>()
                .AddTransient<ViewModels.Users.FollowingViewModel>()
                .AddTransient<ViewModels.Users.IssuesViewModel>()
                .AddTransient<ViewModels.Users.OrganizationsViewModel>()
                .AddTransient<ViewModels.Users.OverviewViewModel>()
                .AddTransient<ViewModels.Users.PackagesViewModel>()
                .AddTransient<ViewModels.Users.ProjectsViewModel>()
                .AddTransient<ViewModels.Users.PullRequestsViewModel>()
                .AddTransient<ViewModels.Users.RepositoriesViewModel>()
                .AddTransient<ViewModels.Users.StarredReposViewModel>()
                .BuildServiceProvider();
        }

        private static void EnsureSettingsAndConfigurationAreBootstrapped()
        {
            AppSettings ??= new SettingsViewModel();
        }

        private void EnsureWindowIsInitialized()
        {
            Window = new MainWindow();
            Window.Activated += Window_Activated;
            //Window.Closed += Window_Closed;
            WindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(Window);
        }

        #region Event Methods
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            var activatedEventArgs = Microsoft.Windows.AppLifecycle.AppInstance.GetCurrent().GetActivatedEventArgs();

            // Initialize MainWindow here
            EnsureWindowIsInitialized();

            // Initialize static services
            EnsureSettingsAndConfigurationAreBootstrapped();

            // Initialize Window
            Window.InitializeApplication(activatedEventArgs.Data);
        }

        public void OnActivated(AppActivationArguments activatedEventArgs)
        {
            // Called from Program class

            // Initialize Window
            Window.DispatcherQueue.EnqueueAsync(() => Window.InitializeApplication(activatedEventArgs.Data));
        }

        private void Window_Activated(object sender, WindowActivatedEventArgs args)
        {
            if (args.WindowActivationState == WindowActivationState.CodeActivated ||
                args.WindowActivationState == WindowActivationState.PointerActivated)
            {
                ApplicationData.Current.LocalSettings.Values["INSTANCE_ACTIVE"] = -System.Diagnostics.Process.GetCurrentProcess().Id;
            }
        }

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
            => throw new Exception("Failed to load Page " + e.SourcePageType.FullName);

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }

        private async void OnUnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
            => await AppUnhandledException(e.Exception);

        private async void OnUnobservedException(object sender, UnobservedTaskExceptionEventArgs e)
            => await AppUnhandledException(e.Exception);

        private async Task AppUnhandledException(Exception ex)
        {
            Services.GetService<Utils.ILogger>()?.Fatal("Unhandled exception", ex);

            try
            {
                await new Microsoft.UI.Xaml.Controls.ContentDialog
                {
                    Title = "Unhandled exception",
                    Content = ex.Message,
                    CloseButtonText = "Close"
                }
                .ShowAsync();
            }
            catch (Exception ex2)
            {
                Services.GetService<Utils.ILogger>()?.Error("Failed to display unhandled exception", ex2);
            }
        }
        #endregion

        public static void CloseApp()
            => Window.Close();

        public static AppWindow GetAppWindow(Window w)
        {
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(w);
            WindowId windowId = Win32Interop.GetWindowIdFromWindow(hWnd);

            return AppWindow.GetFromWindowId(windowId);
        }
    }
}
