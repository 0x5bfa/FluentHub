// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using CommunityToolkit.WinUI;
using FluentHub.App.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.Windows.AppLifecycle;
using Serilog;
using Windows.ApplicationModel;
using Windows.Storage;

namespace FluentHub.App
{
	public partial class App : Application
	{
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

			Log.Logger = GetSerilogLogger();
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
			MainWindow.Instance.InitializeApplication(activatedEventArgs.Data);
		}

		public void OnActivated(AppActivationArguments activatedEventArgs)
		{
			// Called from Program class

			// Initialize Window
			_ = MainWindow.Instance.DispatcherQueue.EnqueueAsync(() => MainWindow.Instance.InitializeApplication(activatedEventArgs.Data));
		}

		private static Serilog.ILogger GetSerilogLogger()
		{
			string logFilePath = System.IO.Path.Combine(ApplicationData.Current.LocalFolder.Path, "FluentHub.Logs/Log.log");

			var logger = new LoggerConfiguration()
				.MinimumLevel
#if DEBUG
				.Verbose()
#else
				.Error()
#endif
				.WriteTo
				.File(logFilePath, rollingInterval: RollingInterval.Day)
				.CreateLogger();

			return logger;
		}

		private void EnsureWindowIsInitialized()
		{
			MainWindow.Instance.Activated += Window_Activated;
			//Window.Closed += Window_Closed;
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
			Ioc.Default.GetService<Utils.ILogger>()?.Fatal("Unhandled exception", ex);

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
				Ioc.Default.GetService<Utils.ILogger>()?.Error("Failed to display unhandled exception", ex2);
			}
		}
	}
}
