using FluentHub.Helpers;
using FluentHub.Octokit.Queries.Users;
using FluentHub.Services;
using FluentHub.Octokit.Authorization;
using FluentHub.Services.Navigation;
using FluentHub.ViewModels;
using FluentHub.Views;
using FluentHub.Views.SignIn;
using Microsoft.Extensions.DependencyInjection;
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

        public static GitHubClient Client { get; private set; } = new GitHubClient(new ProductHeaderValue("FluentHub"));

        public static SettingsViewModel Settings { get; private set; } = new SettingsViewModel();

        public static string SignedInUserName { get; private set; }

        public static string AppVersion = $"{Package.Current.Id.Version.Major}.{Package.Current.Id.Version.Minor}.{Package.Current.Id.Version.Build}.{Package.Current.Id.Version.Revision}";

        public App()
        {
            InitializeComponent();

            Suspending += OnSuspending;
#if DEBUG
            UnhandledException += async (s, e) =>
            {
                e.Handled = true;
                try
                {
                    await new ContentDialog
                    {
                        Title = "Unhandled exception occured",
                        Content = e.Exception,
                        CloseButtonText = "Close"
                    }.ShowAsync();
                }
                catch { }
            };
#endif
            Services = ConfigureServices();

            IntializeLogger();
            Log.Information("FluentHub has been launched.");
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
                .BuildServiceProvider();
        }

        private async Task GetViewerLoginName()
        {
            ViewerQueries queries = new();
            SignedInUserName = await queries.GetLoginName();
        }

        private void IntializeLogger()
        {
            string logFilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "FluentHub.Logs/Log.txt");

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo
                .File(logFilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Debug("Initialized logger in FluentHub.");
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            ApplicationView.GetForCurrentView().TitleBar.ButtonBackgroundColor = Colors.Transparent;
            ApplicationView.GetForCurrentView().TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

            bool openInNewTab = true;

            if (rootFrame == null)
            {
                openInNewTab = false;
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                Window.Current.Content = rootFrame;
            }

            if (args.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    if (Settings.SetupCompleted == true)
                    {
                        // temp: copy credentials to main thread app (will be removed)
                        Client.Credentials = new Credentials(Settings.AccessToken);
                        await GetViewerLoginName();

                        rootFrame.Navigate(typeof(MainPage), args.Arguments);
                    }
                    else
                    {
                        Settings.SetupProgress = false;
                        Settings.SetupCompleted = false;

                        rootFrame.Navigate(typeof(IntroPage), args.Arguments);
                    }
                }

                ThemeHelper.Initialize();
                Window.Current.Activate();
            }
            if (!string.IsNullOrWhiteSpace(args.Arguments)
                && Uri.TryCreate(args.Arguments, UriKind.RelativeOrAbsolute, out var uri))
            {
                HandleUriActivation(uri, openInNewTab);
            }
        }

        protected async override void OnActivated(IActivatedEventArgs args)
        {
            if (args.Kind == ActivationKind.Protocol)
            {
                if (args.PreviousExecutionState == ApplicationExecutionState.Running)
                {
                    await HandleProtocolActivationArguments(args);
                }
            }
        }

        private void HandleUriActivation(Uri uri!!, bool openInTab)
        {
            var ns = Services.GetRequiredService<INavigationService>();

            if (ns.IsConfigured)
            {
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
                }

                if (page != null)
                {
                    if (openInTab)
                        ns.OpenTab(page, param);
                    else
                        ns.Navigate(page, param);
                }
            }
        }

        private async Task HandleProtocolActivationArguments(IActivatedEventArgs args)
        {
            ProtocolActivatedEventArgs eventArgs = args as ProtocolActivatedEventArgs;

            // fluenthub://?code=[code]
            if (eventArgs.Uri.Query.Contains("code"))
            {
                string code = new WwwFormUrlDecoder(eventArgs.Uri.Query).GetFirstValueByName("code");

                if (code != null)
                {
                    AuthorizationService authService = new();
                    bool status = await authService.RequestOAuthTokenAsync(code);

                    // temp: copy credentials to main thread app (will be removed)
                    App.Client.Credentials = new global::Octokit.Credentials(Settings.AccessToken);

                    if (status)
                    {
                        App.Settings.SetupCompleted = true;
                        await GetViewerLoginName();

                        rootFrame.Navigate(typeof(MainPage));
                    }
                }
            }
            else
            {
                HandleUriActivation(eventArgs.Uri, true);
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