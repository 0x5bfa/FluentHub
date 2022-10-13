using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.WinUI;
using CommunityToolkit.WinUI.Helpers;
using CommunityToolkit.WinUI.Notifications;
using FluentHub.App;
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
using Serilog;
using System;
using System.IO;
using System.Linq;
using Windows.ApplicationModel;
using Windows.Storage;

namespace FluentHub.App
{
    public partial class App : Application
    {
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

        private IServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<INavigationService, NavigationService>()
                .AddSingleton<Utils.ILogger>(new SerilogWrapperLogger(Serilog.Log.Logger))
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
                .AddTransient<ViewModels.Dialogs.EditUserProfileViewModel>()
                .AddTransient<ViewModels.Home.FeedsViewModel>()
                .AddTransient<ViewModels.Home.NotificationsViewModel>()
                .AddTransient<ViewModels.Home.UserHomeViewModel>()
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
                .AddTransient<ViewModels.Repositories.PullRequests.ConversationViewModel>()
                .AddTransient<ViewModels.Repositories.PullRequests.CommitViewModel>()
                .AddTransient<ViewModels.Repositories.PullRequests.CommitsViewModel>()
                .AddTransient<ViewModels.Repositories.PullRequests.FileChangesViewModel>()
                .AddTransient<ViewModels.Repositories.PullRequests.PullRequestsViewModel>()
                .AddTransient<ViewModels.Repositories.Releases.ReleasesViewModel>()
                .AddTransient<ViewModels.Searches.CodeViewModel>()
                .AddTransient<ViewModels.Searches.IssuesViewModel>()
                .AddTransient<ViewModels.Searches.RepositoriesViewModel>()
                .AddTransient<ViewModels.Searches.UsersViewModel>()
                .AddTransient<ViewModels.SignIn.IntroViewModel>()
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

        private static void EnsureSettingsAndConfigurationAreBootstrapped()
        {
            AppSettings ??= new SettingsViewModel();
        }

        private static async Task StartAppCenter()
        {
            //try
            //{
            //    if (!AppCenter.Configured)
            //    {
            //        var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///Resources/AppCenterKey.txt"));
            //        var lines = await FileIO.ReadTextAsync(file);
            //        using var document = System.Text.Json.JsonDocument.Parse(lines);
            //        var obj = document.RootElement;
            //        AppCenter.Start(obj.GetProperty("key").GetString(), typeof(Analytics), typeof(Crashes));
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Logger.Warn(ex, "AppCenter could not be started.");
            //}
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            var mainInstance = Microsoft.Windows.AppLifecycle.AppInstance.FindOrRegisterForKey("main");
            var activatedEventArgs = Microsoft.Windows.AppLifecycle.AppInstance.GetCurrent().GetActivatedEventArgs();

            //if (!mainInstance.IsCurrent)
            //{
            //    // Redirect the activation (and args) to the "main" instance, and exit.
            //    await mainInstance.RedirectActivationToAsync(activatedEventArgs);
            //    System.Diagnostics.Process.GetCurrentProcess().Kill();
            //    return;
            //}

            EnsureSettingsAndConfigurationAreBootstrapped();

            // Initialize MainWindow here
            EnsureWindowIsInitialized();

            await Window.InitializeApplication(activatedEventArgs);
        }

        public async Task OnActivated(AppActivationArguments activatedEventArgs)
        {
            await Window.InitializeApplication(activatedEventArgs);
        }

        private void EnsureWindowIsInitialized()
        {
            Window = new MainWindow();
            //Window.Activated += Window_Activated;
            //Window.Closed += Window_Closed;
            WindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(Window);
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

        public static TEnum GetEnum<TEnum>(string text) where TEnum : struct
        {
            if (!typeof(TEnum).GetType().IsEnum)
            {
                throw new InvalidOperationException("Generic parameter 'TEnum' must be an enum.");
            }
            return (TEnum)Enum.Parse(typeof(TEnum), text);
        }

        public static void CloseApp()
            => Window.Close();

        public static AppWindow GetAppWindow(Window w)
        {
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(w);

            Microsoft.UI.WindowId windowId = Win32Interop.GetWindowIdFromWindow(hWnd);

            return AppWindow.GetFromWindowId(windowId);
        }

        public static MainWindow Window { get; private set; } = null!;

        public static IntPtr WindowHandle { get; private set; }
    }
}
