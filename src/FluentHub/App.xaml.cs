using FluentHub.Helpers;
using FluentHub.Octokit.Queries.Users;
using FluentHub.Services;
using FluentHub.Services.Auth;
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

        public static string DefaultHost { get; private set; } = "https://github.com";

        public static string RestApiEndPoint { get; private set; } = "https://api.github.com";

        public static string GraphQlApiEndPoint { get; private set; } = "https://api.github.com/graphql";

        public static string SignedInUserName { get; private set; }

        private static InternetConnectionHelpers internetConnection = new InternetConnectionHelpers();

        public static string AppVersion = $"{Package.Current.Id.Version.Major}.{Package.Current.Id.Version.Minor}.{Package.Current.Id.Version.Build}.{Package.Current.Id.Version.Revision}";

        public App()
        {
            InitializeComponent();

            Suspending += OnSuspending;

            Services = ConfigureServices();

            if (Settings.SetupCompleted == true)
            {
                if (Settings.Get("AccessToken", "") != "")
                {
                    Client.Credentials = new Credentials(Settings.Get("AccessToken", ""));

                    _ = GetViewerLoginName();
                }
                else
                {
                    Settings.SetupProgress = false;
                    Settings.SetupCompleted = false;

                    rootFrame.Navigate(typeof(IntroPage));
                }
            }

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
            string logFilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Logs/Log.txt");

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo
                .File(logFilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Debug("Initialized logger in FluentHub.");
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            ApplicationView.GetForCurrentView().TitleBar.ButtonBackgroundColor = Colors.Transparent;
            ApplicationView.GetForCurrentView().TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

            if (rootFrame == null)
            {
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    if (Settings.SetupCompleted == true)
                    {
                        Settings.AccountsNamesJoinedSlashes += ("/" + SignedInUserName);
                        rootFrame.Navigate(typeof(MainPage), e.Arguments);
                    }
                    else
                    {
                        rootFrame.Navigate(typeof(IntroPage), e.Arguments);
                    }
                }

                ThemeHelper.Initialize();
                Window.Current.Activate();
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

        private async Task HandleProtocolActivationArguments(IActivatedEventArgs args)
        {
            ProtocolActivatedEventArgs eventArgs = args as ProtocolActivatedEventArgs;

            if (string.IsNullOrEmpty(eventArgs.Uri.Query)) return;

            string code = new WwwFormUrlDecoder(eventArgs.Uri.Query).GetFirstValueByName("code");

            if (code != null)
            {
                RequestAuthorization auth = new RequestAuthorization();

                // Request token with code
                bool status = await auth.RequestOAuthToken(code);

                if (status)
                {
                    User user = await Client.User.Current();
                    SignedInUserName = user.Login;
                    Settings.AccountsNamesJoinedSlashes += ("/" + user.Login);

                    rootFrame.Navigate(typeof(MainPage));
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