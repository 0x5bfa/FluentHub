// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Models;
using Microsoft.UI;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System.IO;
using WinUIEx;

namespace FluentHub
{
	public sealed class MainWindow : WindowEx
	{
		private static MainWindow? _Instance;
		public static MainWindow Instance => _Instance ??= new();

		public IntPtr WindowHandle { get; }

		public MainWindow()
		{
			WindowHandle = this.GetWindowHandle();
			SystemBackdrop = new MicaBackdrop { Kind = MicaKind.BaseAlt };

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

			// Workaround for full screen window messing up the taskbar
			// https://github.com/microsoft/microsoft-ui-xaml/issues/8431
			//InteropHelpers.SetPropW(WindowHandle, "NonRudeHWND", new IntPtr(1));
		}

		public void InitializeApplication(object? activatedEventArgs, bool forceReload = false)
		{
			Frame rootFrame = EnsureWindowIsInitialized();
			_ = activatedEventArgs;

			if (forceReload || rootFrame.Content is null)
			{
				rootFrame.Navigate(typeof(Views.MainPage), null, new SuppressNavigationTransitionInfo());
				rootFrame.BackStack.Clear();
			}

			if (rootFrame.Content is FrameworkElement content && !content.IsLoaded)
			{
				RoutedEventHandler? loaded = null;
				loaded = (sender, args) =>
				{
					content.Loaded -= loaded;
					DispatcherQueue.TryEnqueue(Activate);
				};
				content.Loaded += loaded;
			}
			else
			{
				Activate();
			}
		}

		private Frame EnsureWindowIsInitialized()
		{
			// Do not repeat app initialization when the Window already has content,
			// just ensure that the window is active
			if (MainWindow.Instance.Content is not Frame rootFrame)
			{
				// Create a Frame to act as the navigation context and navigate to the first page
				rootFrame = new() { CacheSize = 1 };
				rootFrame.NavigationFailed += OnNavigationFailed;

				// Place the frame in the current Window
				MainWindow.Instance.Content = rootFrame;
			}

			return rootFrame;
		}

		private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
			=> throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
	}
}
