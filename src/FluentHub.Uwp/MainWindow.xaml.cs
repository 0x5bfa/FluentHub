using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FluentHub.Uwp
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
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

        public async Task InitializeApplication(AppActivationArguments activatedEventArgs)
        {
            var rootFrame = EnsureWindowIsInitialized();

            // WINUI3: port activation args from App.xaml.cs.old: OnActivated, OnFileActivated
            switch (activatedEventArgs.Data)
            {
                case ILaunchActivatedEventArgs launchArgs:
                    if (rootFrame.Content == null)
                    {
                        // When the navigation stack isn't restored navigate to the first page,
                        // configuring the new page by passing required information as a navigation
                        // parameter
                        rootFrame.Navigate(typeof(MainPage), launchArgs.Arguments, new SuppressNavigationTransitionInfo());
                    }
                    else
                    {
                        if (!(string.IsNullOrEmpty(launchArgs.Arguments) && MainPageViewModel.AppInstances.Count > 0))
                        {
                            await MainPageViewModel.AddNewTabByPathAsync(typeof(PaneHolderPage), launchArgs.Arguments);
                        }
                    }
                    break;

                case IProtocolActivatedEventArgs eventArgs:
                    if (eventArgs.Uri.AbsoluteUri == "files-uwp:")
                    {
                        rootFrame.Navigate(typeof(MainPage), null, new SuppressNavigationTransitionInfo());
                    }
                    else
                    {
                        var parsedArgs = eventArgs.Uri.Query.TrimStart('?').Split('=');
                        var unescapedValue = Uri.UnescapeDataString(parsedArgs[1]);
                        var folder = (StorageFolder)await FilesystemTasks.Wrap(() => StorageFolder.GetFolderFromPathAsync(unescapedValue).AsTask());
                        if (folder != null && !string.IsNullOrEmpty(folder.Path))
                        {
                            unescapedValue = folder.Path; // Convert short name to long name (#6190)
                        }
                        switch (parsedArgs[0])
                        {
                            case "tab":
                                rootFrame.Navigate(typeof(MainPage), TabItemArguments.Deserialize(unescapedValue), new SuppressNavigationTransitionInfo());
                                break;

                            case "folder":
                                rootFrame.Navigate(typeof(MainPage), unescapedValue, new SuppressNavigationTransitionInfo());
                                break;

                            case "cmd":
                                var ppm = CommandLineParser.ParseUntrustedCommands(unescapedValue);
                                if (ppm.IsEmpty())
                                {
                                    ppm = new ParsedCommands() { new ParsedCommand() { Type = ParsedCommandType.Unknown, Args = new() { "." } } };
                                }
                                await InitializeFromCmdLineArgs(rootFrame, ppm);
                                break;
                        }
                    }
                    break;

                case ICommandLineActivatedEventArgs cmdLineArgs:
                    var operation = cmdLineArgs.Operation;
                    var cmdLineString = operation.Arguments;
                    var activationPath = operation.CurrentDirectoryPath;

                    var parsedCommands = CommandLineParser.ParseUntrustedCommands(cmdLineString);
                    if (parsedCommands != null && parsedCommands.Count > 0)
                    {
                        await InitializeFromCmdLineArgs(rootFrame, parsedCommands, activationPath);
                    }
                    break;

                case IToastNotificationActivatedEventArgs eventArgsForNotification:
                    if (eventArgsForNotification.Argument == "report")
                    {
                        await Windows.System.Launcher.LaunchUriAsync(new Uri(Constants.GitHub.FeedbackUrl));
                    }
                    break;

                case IStartupTaskActivatedEventArgs:
                    break;

                case IFileActivatedEventArgs fileArgs:
                    var index = 0;
                    if (rootFrame.Content == null)
                    {
                        // When the navigation stack isn't restored navigate to the first page,
                        // configuring the new page by passing required information as a navigation
                        // parameter
                        rootFrame.Navigate(typeof(MainPage), fileArgs.Files.First().Path, new SuppressNavigationTransitionInfo());
                        index = 1;
                    }
                    for (; index < fileArgs.Files.Count; index++)
                    {
                        await MainPageViewModel.AddNewTabByPathAsync(typeof(PaneHolderPage), fileArgs.Files[index].Path);
                    }
                    break;
            }

            if (rootFrame.Content == null)
            {
                rootFrame.Navigate(typeof(MainPage), null, new SuppressNavigationTransitionInfo());
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

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }
    }
}
