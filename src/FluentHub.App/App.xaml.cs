// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.Utils;
using FluentHub.App.Services;
using FluentHub.App.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Windowing;
using Microsoft.Windows.AppLifecycle;
using Windows.ApplicationModel;
using Windows.Storage;
using CommunityToolkit.WinUI;
using FluentHub.App.ViewModels.Repositories.Codes;

namespace FluentHub.App
{
	public partial class App : Application
	{
		public static MainWindow WindowInstance { get; private set; } = null!;

		public static IntPtr WindowHandle { get; private set; }

		public new static App Current
			=> (App)Application.Current;

		public static SettingsViewModel AppSettings { get; set; }

		public static string AppVersion =
			$"{Windows.ApplicationModel.Package.Current.Id.Version.Major}." +
			$"{Windows.ApplicationModel.Package.Current.Id.Version.Minor}." +
			$"{Windows.ApplicationModel.Package.Current.Id.Version.Build}." +
			$"{Windows.ApplicationModel.Package.Current.Id.Version.Revision}";

		public App()
		{
			InitializeComponent();

			UnhandledException += OnUnhandledException;
			TaskScheduler.UnobservedTaskException += OnUnobservedException;

			var _host = AppLifecycleHelper.ConfigureServices();
			Ioc.Default.ConfigureServices(_host.Services);

			AppSettings ??= new SettingsViewModel();
		}

		protected override void OnLaunched(LaunchActivatedEventArgs e)
		{
			var activatedEventArgs = Microsoft.Windows.AppLifecycle.AppInstance.GetCurrent().GetActivatedEventArgs();

			// Initialize MainWindow here
			EnsureWindowIsInitialized();

			// Initialize Window
			WindowInstance.InitializeApplication(activatedEventArgs.Data);
		}

		public void OnActivated(AppActivationArguments activatedEventArgs)
		{
			// Called from Program class

			// Initialize Window
			WindowInstance.DispatcherQueue.EnqueueAsync(() => WindowInstance.InitializeApplication(activatedEventArgs.Data));
		}

		private void EnsureWindowIsInitialized()
		{
			WindowInstance = new MainWindow();
			WindowInstance.Activated += Window_Activated;
			//Window.Closed += Window_Closed;
			WindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(WindowInstance);
		}

		private void Window_Activated(object sender, WindowActivatedEventArgs args)
		{
			if (args.WindowActivationState == WindowActivationState.CodeActivated ||
				args.WindowActivationState == WindowActivationState.PointerActivated)
			{
				ApplicationData.Current.LocalSettings.Values["INSTANCE_ACTIVE"] = -System.Diagnostics.Process.GetCurrentProcess().Id;
			}
		}

		void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
			=> throw new Exception("Failed to load Page " + e.SourcePageType.FullName);

		private void OnSuspending(object sender, SuspendingEventArgs e)
		{
			var deferral = e.SuspendingOperation.GetDeferral();
			//TODO: Save application state and stop any background activity
			deferral.Complete();
		}

		private async void OnUnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
			=> await AppUnhandledException(e.Exception);

		private async void OnUnobservedException(object sender, UnobservedTaskExceptionEventArgs e)
			=> await AppUnhandledException(e.Exception);

		private async Task AppUnhandledException(Exception ex)
		{
			Ioc.Default.GetService<ILogger>()?.Fatal("Unhandled exception", ex);

			try
			{
				await new Microsoft.UI.Xaml.Controls.ContentDialog
				{
					Title = "Unhandled exception",
					Content = ex.Message,
					CloseButtonText = "Close"
				}
				.ShowAsync();
			}
			catch (Exception ex2)
			{
				Ioc.Default.GetService<ILogger>()?.Error("Failed to display unhandled exception", ex2);
			}
		}
	}
}
