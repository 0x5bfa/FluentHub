// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Infrastructure.GitHub.Authorization;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Services;
using FluentHub.Views;
using Microsoft.UI.Xaml;
using Windows.ApplicationModel;

namespace FluentHub;

public partial class App : Application
{
	private MainWindow? _mainWindow;

	public App()
	{
		InitializeComponent();

		Settings = new JsonSettingsStore();
		Authorization = new AuthorizationService();
		Session = new GitHubSessionManager();
		GitHub = new FluentHubGitHubClient(new GitHubApiClient(Session));
	}

	public new static App Current => (App)Application.Current;

	public static string AppVersion
		=> $"{Package.Current.Id.Version.Major}." +
		$"{Package.Current.Id.Version.Minor}." +
		$"{Package.Current.Id.Version.Build}." +
		$"{Package.Current.Id.Version.Revision}";

	public JsonSettingsStore Settings { get; }

	public AuthorizationService Authorization { get; }

	public GitHubSessionManager Session { get; }

	public IFluentHubGitHubClient GitHub { get; }

	protected override async void OnLaunched(LaunchActivatedEventArgs args)
	{
		try
		{
			await Settings.LoadAsync();
		}
		catch
		{
			Settings.ClearInMemory();
		}

		if (Settings.HasSession)
			TryRestoreSession();

		ShowMainWindow();
	}

	private bool TryRestoreSession()
	{
		try
		{
			Session.SwitchAccount(Settings.AccessToken!);
			return true;
		}
		catch
		{
			Settings.ClearInMemory();
			return false;
		}
	}

	private void ShowMainWindow()
	{
		_mainWindow ??= MainWindow.Instance;
		_mainWindow.InitializeApplication(null);
	}
}
