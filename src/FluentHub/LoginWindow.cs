// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.IO;
using WinUIEx;

namespace FluentHub
{
	public sealed class LoginWindow : WindowEx
	{
		private readonly Frame _rootFrame;

		public LoginWindow()
		{
			SystemBackdrop = new MicaBackdrop { Kind = MicaKind.BaseAlt };

			AppWindow.Title = "FluentHub";
			AppWindow.SetIcon(Path.Combine(
				Windows.ApplicationModel.Package.Current.InstalledLocation.Path,
				"Assets/AppTiles/Release/StoreLogo.scale-400.png"));
			AppWindow.TitleBar.ExtendsContentIntoTitleBar = true;
			AppWindow.TitleBar.ButtonBackgroundColor = Colors.Transparent;
			AppWindow.TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

			Width = 480;
			Height = 720;
			MinWidth = 420;
			MinHeight = 620;
			MaxWidth = 720;
			MaxHeight = 960;
			IsMaximizable = false;

			_rootFrame = new Frame { CacheSize = 1 };
			_rootFrame.NavigationFailed += OnNavigationFailed;
			Content = _rootFrame;
		}

		public void Initialize()
		{
			if (_rootFrame.Content is null)
				_rootFrame.Navigate(typeof(Views.SignIn.IntroPage));

			Activate();
			this.CenterOnScreen();
		}

		private static void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
			=> throw new Exception("Failed to load page " + e.SourcePageType.FullName);
	}
}
