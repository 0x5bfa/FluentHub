// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Models;
using Microsoft.UI;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using System.IO;
using WinUIEx;

namespace FluentHub.Views
{
	public sealed class MainWindow : WindowEx
	{
		private static MainWindow? _Instance;
		private MainPage? _rootView;
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

		public async void InitializeApplication(object? activatedEventArgs, bool forceReload = false)
		{
			_ = activatedEventArgs;

			if (forceReload && _rootView is not null)
			{
				await _rootView.ShutdownAsync();
				_rootView = null;
			}

			if (_rootView is null)
			{
				_rootView = new MainPage();
				Content = _rootView;
				await _rootView.InitializeAsync();
			}

			if (!_rootView.IsLoaded)
			{
				RoutedEventHandler? loaded = null;
				loaded = (sender, args) =>
				{
					_rootView.Loaded -= loaded;
					DispatcherQueue.TryEnqueue(Activate);
				};
				_rootView.Loaded += loaded;
			}
			else
			{
				Activate();
			}
		}

	}
}
