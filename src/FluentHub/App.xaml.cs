// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Utils;
using FluentHub.Services;
using FluentHub.ViewModels.AppSettings;
using FluentHub.Core.Application.Abstractions.Authentication;
using Microsoft.Extensions.Hosting;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Windowing;
using Microsoft.Windows.AppLifecycle;
using Windows.ApplicationModel;
using Windows.Storage;
using CommunityToolkit.WinUI;
using FluentHub.ViewModels.Repositories.Codes;
using WinUIEx;

namespace FluentHub
{
	public partial class App : Application
	{
		private readonly IHost _host;
		private object? _pendingActivationData;
		private bool _mainWindowInitialized;
		private bool _mainWindowVisible;

		public new static App Current
			=> (App)Application.Current;

		public static SettingsViewModel AppSettings { get; set; } = default!;
		public LoginWindow? SignInWindow { get; private set; }

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

			_host = AppLifecycleHelper.ConfigureServices();
			Ioc.Default.ConfigureServices(_host.Services);

			AppSettings ??= new SettingsViewModel();
		}

		protected override void OnLaunched(LaunchActivatedEventArgs e)
		{
			var activatedEventArgs = Microsoft.Windows.AppLifecycle.AppInstance.GetCurrent().GetActivatedEventArgs();
			_pendingActivationData = activatedEventArgs.Data;

			if (AppSettings.SetupCompleted && TryRestoreGitHubSession())
			{
				ShowMainWindow(activatedEventArgs.Data);
			}
			else
			{
				AppSettings.SetupProgress = false;
				AppSettings.SetupCompleted = false;
				ShowSignInWindow();
			}
		}

		public void OnActivated(AppActivationArguments activatedEventArgs)
		{
			_pendingActivationData = activatedEventArgs.Data;

			if (SignInWindow is not null)
			{
				_ = SignInWindow.DispatcherQueue.EnqueueAsync(SignInWindow.Activate);
			}
			else if (_mainWindowInitialized)
			{
				_ = MainWindow.Instance.DispatcherQueue.EnqueueAsync(
					() => MainWindow.Instance.InitializeApplication(activatedEventArgs.Data));
			}
		}

		public void CompleteSignIn()
		{
			if (!AppSettings.SetupCompleted)
				return;

			var signInWindow = SignInWindow;
			ShowMainWindow(_pendingActivationData, forceReload: _mainWindowInitialized);
			signInWindow?.Close();
		}

		public void SignOut()
		{
			Ioc.Default.GetRequiredService<GitHubTokenStore>()
				.RemoveToken(AppSettings.SignedInUserName);

			AppSettings.SetupProgress = false;
			AppSettings.SetupCompleted = false;
			_mainWindowVisible = false;
			MainWindow.Instance.Hide();
			ShowSignInWindow();
		}

		private void ShowMainWindow(object? activatedEventArgs, bool forceReload = false)
		{
			EnsureMainWindowIsInitialized();
			MainWindow.Instance.InitializeApplication(activatedEventArgs, forceReload);
			_mainWindowVisible = true;
		}

		private void ShowSignInWindow()
		{
			if (SignInWindow is null)
			{
				SignInWindow = new LoginWindow();
				SignInWindow.Activated += Window_Activated;
				SignInWindow.Closed += SignInWindow_Closed;
			}

			SignInWindow.Initialize();
		}

		private void EnsureMainWindowIsInitialized()
		{
			if (_mainWindowInitialized)
				return;

			MainWindow.Instance.Activated += Window_Activated;
			MainWindow.Instance.Closed += (_, _) => _host.Dispose();
			_mainWindowInitialized = true;
		}

		private void SignInWindow_Closed(object sender, WindowEventArgs args)
		{
			SignInWindow = null;

			if (_mainWindowVisible)
				return;

			if (_mainWindowInitialized)
				MainWindow.Instance.Close();
			else
				_host.Dispose();
		}

		private static bool TryRestoreGitHubSession()
		{
			var logger = Ioc.Default.GetService<Utils.ILogger>();

			try
			{
				var accessToken = Ioc.Default.GetRequiredService<GitHubTokenStore>()
					.GetToken(AppSettings.SignedInUserName);

				if (string.IsNullOrWhiteSpace(accessToken))
				{
					logger?.Warn("No secured GitHub access token was found for the signed-in account.");
					return false;
				}

				Ioc.Default.GetRequiredService<IUserSession>().SwitchAccount(accessToken);
				return true;
			}
			catch (Exception ex)
			{
				logger?.Error("Failed to restore the secured GitHub access token.", ex);
				return false;
			}
		}

		private void Window_Activated(object sender, WindowActivatedEventArgs args)
		{
			if (args.WindowActivationState == WindowActivationState.CodeActivated ||
				args.WindowActivationState == WindowActivationState.PointerActivated)
			{
				ApplicationData.Current.LocalSettings.Values["INSTANCE_ACTIVE"] = -System.Diagnostics.Process.GetCurrentProcess().Id;
			}
		}

		private void OnSuspending(object sender, SuspendingEventArgs e)
		{
			var deferral = e.SuspendingOperation.GetDeferral();
			//TODO: Save application state and stop any background activity
			deferral.Complete();
		}

		private async void OnUnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
			=> await AppUnhandledException(e.Exception);

		private async void OnUnobservedException(object? sender, UnobservedTaskExceptionEventArgs e)
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
