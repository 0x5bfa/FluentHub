// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Windowing;
using Microsoft.Windows.AppLifecycle;
using Windows.ApplicationModel;

namespace FluentHub.Helpers
{
	internal class AppLifecycleHelper
	{
		internal static void CloseApp()
		{
			if (App.Current.SignInWindow is not null)
				App.Current.SignInWindow.Close();
			else
				MainWindow.Instance.Close();
		}

		internal static AppWindow GetAppWindow(Window w)
		{
			var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(w);
			WindowId windowId = Win32Interop.GetWindowIdFromWindow(hWnd);

			return AppWindow.GetFromWindowId(windowId);
		}

		internal static IHost ConfigureServices()
			=> Host.CreateDefaultBuilder()
				.ConfigureServices(services => services.AddFluentHub())
				.Build();
	}
}
