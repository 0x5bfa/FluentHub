using FluentHub.Services.Auth;
using FluentHub.ViewModels;
using FluentHub.Views;
using Octokit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace FluentHub
{
    sealed partial class App : Windows.UI.Xaml.Application
    {
        Frame rootFrame = Window.Current.Content as Frame;

        public static GitHubClient Client { get; private set; } = new GitHubClient(new ProductHeaderValue("FluentHub"));

        public static MainViewModel MainViewModel { get; private set; } = new MainViewModel();

        public static SettingsViewModel settings { get; private set; } = new SettingsViewModel();

        public static string Host { get; private set; } = "https://github.com";

        public static string SignedInUserName { get; private set; }

        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;

            // restore token or password
            if (settings.SetupCompleted == true)
            {
                if (settings.Get("accessToken", "") != "")
                {
                    Client.Credentials = new Credentials(settings.Get("accessToken", ""));
                }
                else if (settings.Get("username", "") != "" && settings.Get("password", "") != "")
                {
                    Client.Credentials = new Credentials(settings.Get("username", ""), settings.Get("password", ""));
                }
                else
                {
                    return; // or throw exception
                }
            }
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            // Customize title bar
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            ApplicationView.GetForCurrentView().TitleBar.ButtonBackgroundColor = Colors.Transparent;
            ApplicationView.GetForCurrentView().TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    if (settings.SetupCompleted == true)
                    {
                        User user = await Client.User.Current();
                        SignedInUserName = user.Login;
                    }

                    _ = !settings.SetupCompleted ?
                        rootFrame.Navigate(typeof(WelcomePage), e.Arguments) :
                        rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }

                // Ensure the current window is active
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

        public static async void CloseApp()
        {
            if (!await ApplicationView.GetForCurrentView().TryConsolidateAsync())
            {
                Windows.UI.Xaml.Application.Current.Exit();
            }
        }
    }
}
