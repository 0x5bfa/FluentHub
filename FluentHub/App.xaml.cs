using FluentHub.Views;
using FluentHub.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
using Octokit;
using System.Threading.Tasks;
using FluentHub.Services.Auth;
using System.Net.NetworkInformation;
using FluentHub.ViewModels;

namespace FluentHub
{
    sealed partial class App : Windows.UI.Xaml.Application
    {
        public static GitHubClient Client { get; set; } = new GitHubClient(new ProductHeaderValue("FluentHub"));

        private Frame rootFrame = Window.Current.Content as Frame;

        public static SettingsViewModel settings { get; private set; }

        public static string AuthedUserName { get; private set; }

        public static string DefaultDomain { get; private set; } = "https://github.com";

        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;

            settings = new SettingsViewModel();

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

            if (rootFrame == null)
            {
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                }

                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    User user = await Client.User.Current();
                    AuthedUserName = user.Login;

                    _ = !settings.SetupCompleted ?
                        rootFrame.Navigate(typeof(WelcomePage), e.Arguments) :
                        rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }

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
                    AuthedUserName = user.Login;

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
            deferral.Complete();
        }
    }
}
