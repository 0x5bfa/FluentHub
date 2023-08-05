// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System.IO;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using WinUIEx;

namespace FluentHub.App
{
	public sealed partial class MainWindow : WindowEx
	{
		public MainWindow()
		{
			InitializeComponent();

			EnsureEarlyWindow();
		}

		private void EnsureEarlyWindow()
		{
			// Initialize the Window information
			AppWindow.Title = "FluentHub";
			AppWindow.SetIcon(Path.Combine(Windows.ApplicationModel.Package.Current.InstalledLocation.Path, "Assets/AppTiles/Release/StoreLogo.scale-400.png"));
			AppWindow.TitleBar.ExtendsContentIntoTitleBar = true;
			AppWindow.TitleBar.ButtonBackgroundColor = Colors.Transparent;
			AppWindow.TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
			PersistenceId = "FluentHubMainWindow";
			MinHeight = 516;
			MinWidth = 516;
		}

		public void InitializeApplication(object activatedEventArgs)
		{
			var logger = Ioc.Default.GetService<Utils.ILogger>();
			var messenger = Ioc.Default.GetService<IMessenger>();

			Frame rootFrame = EnsureWindowIsInitialized();
			if (rootFrame is null)
				return;

			Type pageType = typeof(Views.MainPage);

			switch (activatedEventArgs)
			{
				// Launched the app with system activation
				// Usually the users launch from start menu or taskbar
				case ILaunchActivatedEventArgs launchArgs:
					{
						if (rootFrame.Content == null)
						{
							if (App.AppSettings.SetupCompleted == true)
							{
								// Initialize API connection
								Octokit.Authorization.InitializeOctokit.InitializeApiConnections(App.AppSettings.AccessToken);

								rootFrame.Navigate(typeof(Views.MainPage));
								pageType = typeof(Views.MainPage);
							}
							else
							{
								// Reset authorization setup status
								App.AppSettings.SetupProgress = false;
								App.AppSettings.SetupCompleted = false;

								rootFrame.Navigate(typeof(Views.SignIn.IntroPage));
								pageType = typeof(Views.SignIn.IntroPage);
							}
						}

						break;
					}
				// Launched the app with protocol - 'fluenthub://'
				case IProtocolActivatedEventArgs eventArgs:
					{
						switch (eventArgs.Uri.Authority.ToLower())
						{
							// 'fluenthub://auth?code=[code]'
							case "auth" when eventArgs.Uri.Query.Contains("code"):
								{
									// Get GitHub User ID
									var code = new WwwFormUrlDecoder(eventArgs.Uri.Query).GetFirstValueByName("code");

									// Do not add authorize phase to here because this method will be called by non-async method.
									// Even if an authorizing exception occurred, it will not be caught by Try-Catch.
									UserNotificationMessage notification = new("Received GitHub User ID", code);
									messenger?.Send(notification);

									break;
								}
						}

						break;
					}
			}

			if (rootFrame.Content == null)
			{
				rootFrame.Navigate(typeof(Views.MainPage), null, new SuppressNavigationTransitionInfo());
				pageType = typeof(Views.MainPage);
			}

			if (rootFrame.Content is Views.SignIn.IntroPage introPage && pageType == typeof(Views.SignIn.IntroPage))
			{
				introPage.Loaded += (s, e) => DispatcherQueue.TryEnqueue(() => Activate());
			}
			else if (rootFrame.Content is Views.MainPage mainPage && pageType == typeof(Views.MainPage))
			{
				mainPage.Loaded += (s, e) => DispatcherQueue.TryEnqueue(() => Activate());
			}
		}

		private Frame EnsureWindowIsInitialized()
		{
			// Do not repeat app initialization when the Window already has content,
			// just ensure that the window is active
			if (App.WindowInstance.Content is not Frame rootFrame)
			{
				// Create a Frame to act as the navigation context and navigate to the first page
				rootFrame = new() { CacheSize = 1 };
				rootFrame.NavigationFailed += OnNavigationFailed;

				// Place the frame in the current Window
				App.WindowInstance.Content = rootFrame;
			}

			return rootFrame;
		}

		private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
			=> new Exception("Failed to load Page " + e.SourcePageType.FullName);
	}
}
