using FluentHub.Octokit.Authorization;
using FluentHub.Uwp;
using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Utils;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.Services.Navigation;
using FluentHub.Uwp.ViewModels;
using FluentHub.Uwp.Views;
using FluentHub.Uwp.Views.SignIn;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.Windows.AppLifecycle;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.ViewManagement;
using Microsoft.UI;
using Windows.ApplicationModel.Core;
using Microsoft.UI.Windowing;

namespace FluentHub
{
    public partial class App : Application
    {
        public static SettingsViewModel Settings { get; set; }

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

        public new static App Current
            => (App)Application.Current;

        public IServiceProvider Services { get; }

        private IServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<INavigationService, NavigationService>()
                .AddSingleton<Uwp.Utils.ILogger>(new SerilogWrapperLogger(Serilog.Log.Logger))
                .AddSingleton<ToastService>()
                .AddSingleton<IMessenger>(StrongReferenceMessenger.Default)
                // ViewModels
                .AddSingleton<MainPageViewModel>()
                .AddTransient<Uwp.ViewModels.AppSettings.AboutViewModel>()
                .AddTransient<Uwp.ViewModels.AppSettings.Accounts.AccountViewModel>()
                .AddTransient<Uwp.ViewModels.AppSettings.Accounts.OtherUsersViewModel>()
                .AddTransient<Uwp.ViewModels.AppSettings.AppearanceViewModel>()
                .AddTransient<Uwp.ViewModels.Dialogs.AccountSwitchingDialogViewModel>()
                .AddTransient<Uwp.ViewModels.Dialogs.EditPinnedRepositoriesDialogViewModel>()
                .AddTransient<Uwp.ViewModels.Home.FeedsViewModel>()
                .AddTransient<Uwp.ViewModels.Home.NotificationsViewModel>()
                .AddTransient<Uwp.ViewModels.Home.UserHomeViewModel>()
                .AddTransient<Uwp.ViewModels.Organizations.OverviewViewModel>()
                .AddTransient<Uwp.ViewModels.Organizations.RepositoriesViewModel>()
                .AddTransient<Uwp.ViewModels.Repositories.Code.Layouts.DetailsLayoutViewModel>()
                .AddTransient<Uwp.ViewModels.Repositories.Code.Layouts.TreeLayoutViewModel>()
                .AddTransient<Uwp.ViewModels.Repositories.Commits.CommitsViewModel>()
                .AddTransient<Uwp.ViewModels.Repositories.Commits.CommitViewModel>()
                .AddTransient<Uwp.ViewModels.Repositories.Discussions.DiscussionsViewModel>()
                .AddTransient<Uwp.ViewModels.Repositories.Discussions.DiscussionViewModel>()
                .AddTransient<Uwp.ViewModels.Repositories.Issues.IssueViewModel>()
                .AddTransient<Uwp.ViewModels.Repositories.Issues.IssuesViewModel>()
                .AddTransient<Uwp.ViewModels.Repositories.Projects.ProjectsViewModel>()
                .AddTransient<Uwp.ViewModels.Repositories.Projects.ProjectViewModel>()
                .AddTransient<Uwp.ViewModels.Repositories.PullRequests.ConversationViewModel>()
                .AddTransient<Uwp.ViewModels.Repositories.PullRequests.CommitViewModel>()
                .AddTransient<Uwp.ViewModels.Repositories.PullRequests.CommitsViewModel>()
                .AddTransient<Uwp.ViewModels.Repositories.PullRequests.FileChangesViewModel>()
                .AddTransient<Uwp.ViewModels.Repositories.PullRequests.PullRequestsViewModel>()
                .AddTransient<Uwp.ViewModels.Repositories.Releases.ReleasesViewModel>()
                .AddTransient<Uwp.ViewModels.Searches.CodeViewModel>()
                .AddTransient<Uwp.ViewModels.Searches.IssuesViewModel>()
                .AddTransient<Uwp.ViewModels.Searches.RepositoriesViewModel>()
                .AddTransient<Uwp.ViewModels.Searches.UsersViewModel>()
                .AddTransient<Uwp.ViewModels.SignIn.IntroViewModel>()
                .AddTransient<Uwp.ViewModels.UserControls.FileContentBlockViewModel>()
                .AddTransient<Uwp.ViewModels.UserControls.FileNavigationBlockViewModel>()
                .AddTransient<Uwp.ViewModels.UserControls.IssueCommentBlockViewModel>()
                .AddTransient<Uwp.ViewModels.UserControls.ReadmeContentBlockViewModel>()
                .AddTransient<Uwp.ViewModels.UserControls.LatestCommitBlockViewModel>()
                .AddTransient<Uwp.ViewModels.Users.ContributionsViewModel>()
                .AddTransient<Uwp.ViewModels.Users.DiscussionsViewModel>()
                .AddTransient<Uwp.ViewModels.Users.FollowersViewModel>()
                .AddTransient<Uwp.ViewModels.Users.FollowingViewModel>()
                .AddTransient<Uwp.ViewModels.Users.IssuesViewModel>()
                .AddTransient<Uwp.ViewModels.Users.OrganizationsViewModel>()
                .AddTransient<Uwp.ViewModels.Users.OverviewViewModel>()
                .AddTransient<Uwp.ViewModels.Users.PackagesViewModel>()
                .AddTransient<Uwp.ViewModels.Users.ProjectsViewModel>()
                .AddTransient<Uwp.ViewModels.Users.PullRequestsViewModel>()
                .AddTransient<Uwp.ViewModels.Users.RepositoriesViewModel>()
                .AddTransient<Uwp.ViewModels.Users.StarredReposViewModel>()
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

        private static async Task EnsureSettingsAndConfigurationAreBootstrapped()
        {
            Settings ??= new SettingsViewModel();
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

            if (!mainInstance.IsCurrent)
            {
                // Redirect the activation (and args) to the "main" instance, and exit.
                await mainInstance.RedirectActivationToAsync(activatedEventArgs);
                System.Diagnostics.Process.GetCurrentProcess().Kill();
                return;
            }

            await EnsureSettingsAndConfigurationAreBootstrapped();

            // Initialize MainWindow here
            EnsureWindowIsInitialized();
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

        private static void OnUnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
            => AppUnhandledException(e.Exception);

        private static void OnUnobservedException(object sender, UnobservedTaskExceptionEventArgs e)
            => AppUnhandledException(e.Exception);

        private static void AppUnhandledException(Exception ex)
        {
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

            Microsoft.UI.WindowId windowId =
                Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);

            return AppWindow.GetFromWindowId(windowId);
        }

        public static MainWindow Window { get; private set; }

        public static IntPtr WindowHandle { get; private set; }
    }
}
