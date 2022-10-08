using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.Windows.AppLifecycle;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinRT.Interop;
using WinUIEx;

namespace FluentHub.Uwp
{
    public sealed partial class MainWindow : WindowEx
    {
        public MainWindow()
        {
            InitializeComponent();

            PersistenceId = "FluentHubMainWindow";

            EnsureEarlyWindow();
        }

        private void EnsureEarlyWindow()
        {
            // Set title
            AppWindow.Title = "Files";

            // Set icon
            AppWindow.SetIcon(Path.Combine(Windows.ApplicationModel.Package.Current.InstalledLocation.Path, "Assets/AppTiles/Dev/Logo.ico"));

            // Extend title bar
            AppWindow.TitleBar.ExtendsContentIntoTitleBar = true;

            // Set window buttons background to transparent
            AppWindow.TitleBar.ButtonBackgroundColor = Colors.Transparent;
            AppWindow.TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

            // Set min size
            base.MinHeight = 328;
            base.MinWidth = 516;
        }

        public void InitializeApplication(AppActivationArguments activatedEventArgs)
        {
            var rootFrame = EnsureWindowIsInitialized();

            switch (activatedEventArgs.Data)
            {
                case ILaunchActivatedEventArgs launchArgs:
                    if (rootFrame.Content == null)
                    {
                        if (App.Settings.SetupCompleted == true)
                        {
                            Octokit.Authorization.InitializeOctokit.InitializeApiConnections(App.Settings.AccessToken);

                            rootFrame.Navigate(typeof(Uwp.Views.MainPage));
                        }
                        else
                        {
                            App.Settings.SetupProgress = false;
                            App.Settings.SetupCompleted = false;

                            rootFrame.Navigate(typeof(Uwp.Views.SignIn.IntroPage));
                        }
                    }
                    break;

                case IProtocolActivatedEventArgs eventArgs:
                    break;

                case ICommandLineActivatedEventArgs cmdLineArgs:
                    break;

                case IToastNotificationActivatedEventArgs eventArgsForNotification:
                    break;

                case IStartupTaskActivatedEventArgs:
                    break;

                case IFileActivatedEventArgs fileArgs:
                    break;
            }

            if (rootFrame.Content == null)
            {
                rootFrame.Navigate(typeof(Uwp.Views.MainPage), null, new SuppressNavigationTransitionInfo());
            }

            ((Views.MainPage)rootFrame.Content).Loaded +=
                (s, e) => DispatcherQueue.TryEnqueue(() => Activate());
        }

        private Frame EnsureWindowIsInitialized()
        {
            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (!(App.Window.Content is Frame rootFrame))
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();
                rootFrame.CacheSize = 1;
                rootFrame.NavigationFailed += OnNavigationFailed;

                // Place the frame in the current Window
                App.Window.Content = rootFrame;
            }

            return rootFrame;
        }

        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }
    }
}
