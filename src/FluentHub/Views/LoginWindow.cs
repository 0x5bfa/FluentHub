// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml.Media;
using System.IO;
using WinUIEx;

namespace FluentHub.Views
{
	public sealed class LoginWindow : WindowEx
	{
		private readonly SignIn.IntroPage _rootView;

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

			_rootView = new SignIn.IntroPage();
			Content = _rootView;
		}

		public async void Initialize()
		{
			await _rootView.ActivateAsync(new SignInRoute(), CancellationToken.None);

			Activate();
			this.CenterOnScreen();
		}
	}
}
