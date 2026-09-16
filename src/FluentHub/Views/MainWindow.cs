// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml.Media;
using Windows.UI.WindowManagement;
using WinUIEx;

namespace FluentHub.Views
{
	public sealed class MainWindow : WindowEx
	{
		private static MainWindow? _Instance;
		private RootView _rootView;

		public static MainWindow Instance => _Instance ??= new();

		public MainWindow()
		{
			SystemBackdrop = new MicaBackdrop();

			AppWindow.Title = Strings.MainWindow_Title.GetLocalized();
			AppWindow.SetIcon(Path.Combine(Windows.ApplicationModel.Package.Current.InstalledLocation.Path, "Assets/Branding.ico"));
			AppWindow.TitleBar.ExtendsContentIntoTitleBar = true;
			AppWindow.TitleBar.PreferredHeightOption = TitleBarHeightOption.Tall;
			AppWindow.TitleBar.ButtonBackgroundColor = Colors.Transparent;
			AppWindow.TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
			MinHeight = 516;
			MinWidth = 516;

			_rootView = new RootView();
			Content = _rootView;
		}

		public void InitializeApplication(object? activatedEventArgs, bool forceReload = false)
		{
			_ = activatedEventArgs;

			if (forceReload)
			{
				_rootView = new RootView();
				Content = _rootView;
			}

			Activate();
		}
	}
}
