// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI;
using Microsoft.UI.Xaml.Media;
using System.IO;
using WinUIEx;

namespace FluentHub.Views
{
	public sealed class LoginWindow : WindowEx
	{
		private readonly LoginView _rootView;

		public LoginWindow()
		{
			SystemBackdrop = new MicaBackdrop();

			AppWindow.Title = "FluentHub";
			AppWindow.SetIcon(Path.Combine(Windows.ApplicationModel.Package.Current.InstalledLocation.Path, "Assets/Branding.ico"));
			AppWindow.TitleBar.ExtendsContentIntoTitleBar = true;
			AppWindow.TitleBar.ButtonBackgroundColor = Colors.Transparent;
			AppWindow.TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
			Width = 480;
			Height = 720;
			IsMaximizable = false;
			IsResizable = false;

			_rootView = new LoginView();
			Content = _rootView;
		}

		public void Initialize()
		{
			Activate();
			this.CenterOnScreen();
		}
	}
}
