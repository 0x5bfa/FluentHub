// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentHub.Core.Infrastructure.GitHub.Authorization;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Services;
using System.Windows.Input;
using Windows.System;

namespace FluentHub.ViewModels;

public enum LoginStage
{
	Welcome,
	DeviceCode,
	Syncing,
	Error,
	Success,
}

public sealed class LoginViewModel : ObservableObject
{
	private readonly AuthorizationService _authorizationService;
	private readonly GitHubSessionManager _session;
	private readonly IFluentHubGitHubClient _gitHub;
	private readonly JsonSettingsStore _settings;
	private CancellationTokenSource? _authorizationCancellation;
	private LoginStage _stage = LoginStage.Welcome;
	private bool _isTaskLoading;
	private string _deviceUserCode = string.Empty;
	private string _deviceVerificationUri = string.Empty;
	private string _deviceAuthorizationStatus = string.Empty;
	private string _signedInLogin = string.Empty;
	private string _errorTitle = string.Empty;
	private string _errorMessage = string.Empty;
	private string _errorDetails = string.Empty;

	public LoginViewModel(
		AuthorizationService authorizationService,
		GitHubSessionManager session,
		IFluentHubGitHubClient gitHub,
		JsonSettingsStore settings)
	{
		_authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
		_session = session ?? throw new ArgumentNullException(nameof(session));
		_gitHub = gitHub ?? throw new ArgumentNullException(nameof(gitHub));
		_settings = settings ?? throw new ArgumentNullException(nameof(settings));

		AuthorizeWithBrowserCommand = new AsyncRelayCommand(
			AuthorizeWithBrowserAsync,
			() => !IsTaskLoading);
		OpenDeviceVerificationUriCommand = new AsyncRelayCommand(
			OpenDeviceVerificationUriAsync,
			() => IsDeviceAuthorizationAvailable);
		ReturnToWelcomeCommand = new RelayCommand(ReturnToWelcome);
	}

	public LoginStage Stage
	{
		get => _stage;
		private set
		{
			if (!SetProperty(ref _stage, value))
				return;

			OnPropertyChanged(nameof(IsWelcomeStage));
			OnPropertyChanged(nameof(IsDeviceCodeStage));
			OnPropertyChanged(nameof(IsSyncingStage));
			OnPropertyChanged(nameof(IsErrorStage));
			OnPropertyChanged(nameof(IsSuccessStage));
		}
	}

	public bool IsWelcomeStage => Stage == LoginStage.Welcome;

	public bool IsDeviceCodeStage => Stage == LoginStage.DeviceCode;

	public bool IsSyncingStage => Stage == LoginStage.Syncing;

	public bool IsErrorStage => Stage == LoginStage.Error;

	public bool IsSuccessStage => Stage == LoginStage.Success;

	public bool IsTaskLoading
	{
		get => _isTaskLoading;
		private set
		{
			if (!SetProperty(ref _isTaskLoading, value))
				return;

			AuthorizeWithBrowserCommand.NotifyCanExecuteChanged();
		}
	}

	public string DeviceUserCode
	{
		get => _deviceUserCode;
		private set
		{
			if (!SetProperty(ref _deviceUserCode, value))
				return;

			OnPropertyChanged(nameof(IsDeviceAuthorizationAvailable));
			OpenDeviceVerificationUriCommand.NotifyCanExecuteChanged();
		}
	}

	public string DeviceVerificationUri
	{
		get => _deviceVerificationUri;
		private set
		{
			if (!SetProperty(ref _deviceVerificationUri, value))
				return;

			OnPropertyChanged(nameof(IsDeviceAuthorizationAvailable));
			OpenDeviceVerificationUriCommand.NotifyCanExecuteChanged();
		}
	}

	public string DeviceAuthorizationStatus
	{
		get => _deviceAuthorizationStatus;
		private set => SetProperty(ref _deviceAuthorizationStatus, value);
	}

	public string SignedInLogin
	{
		get => _signedInLogin;
		private set => SetProperty(ref _signedInLogin, value);
	}

	public string ErrorTitle
	{
		get => _errorTitle;
		private set => SetProperty(ref _errorTitle, value);
	}

	public string ErrorMessage
	{
		get => _errorMessage;
		private set => SetProperty(ref _errorMessage, value);
	}

	public string ErrorDetails
	{
		get => _errorDetails;
		private set => SetProperty(ref _errorDetails, value);
	}

	public bool IsDeviceAuthorizationAvailable
		=> !string.IsNullOrWhiteSpace(DeviceUserCode) &&
		   !string.IsNullOrWhiteSpace(DeviceVerificationUri);

	public string Version => App.AppVersion;

	public AsyncRelayCommand AuthorizeWithBrowserCommand { get; }

	public AsyncRelayCommand OpenDeviceVerificationUriCommand { get; }

	public RelayCommand ReturnToWelcomeCommand { get; }

	public void CancelAuthorization()
		=> _authorizationCancellation?.Cancel();

	private async Task AuthorizeWithBrowserAsync()
	{
		CancelAuthorization();
		using var cancellation = new CancellationTokenSource();
		_authorizationCancellation = cancellation;
		var cancellationToken = cancellation.Token;
		var operation = LoginOperation.RequestDeviceCode;

		try
		{
			IsTaskLoading = true;
			ResetError();
			DeviceUserCode = string.Empty;
			DeviceVerificationUri = string.Empty;
			DeviceAuthorizationStatus = "Requesting a GitHub device code...";
			Stage = LoginStage.DeviceCode;

			var deviceAuthorization = await _authorizationService
				.RequestDeviceAuthorizationAsync(cancellationToken);
			DeviceUserCode = deviceAuthorization.UserCode;
			DeviceVerificationUri = deviceAuthorization.VerificationUri;

			operation = LoginOperation.OpenBrowser;
			DeviceAuthorizationStatus = "Opening GitHub in your browser...";
			await OpenDeviceVerificationUriAsync();
			DeviceAuthorizationStatus = "Waiting for authorization in your browser...";

			var progress = new Progress<DeviceAuthorizationPollingStatus>(status =>
				DeviceAuthorizationStatus = status switch
				{
					DeviceAuthorizationPollingStatus.Pending => "Waiting for authorization in your browser...",
					DeviceAuthorizationPollingStatus.SlowedDown => "GitHub asked us to slow down. Still waiting...",
					_ => DeviceAuthorizationStatus,
				});

			operation = LoginOperation.WaitForAuthorization;
			var accessToken = await _authorizationService.WaitForDeviceAccessTokenAsync(
				deviceAuthorization,
				progress,
				cancellationToken);

			Stage = LoginStage.Syncing;
			DeviceAuthorizationStatus = "Loading your GitHub account...";
			operation = LoginOperation.ResolveAccount;
			_session.SwitchAccount(accessToken);
			var login = await _gitHub.Users.Users.GetViewerLoginAsync(cancellationToken);

			operation = LoginOperation.SaveSettings;
			await _settings.SaveSessionAsync(login, accessToken, cancellationToken);

			SignedInLogin = login;
			DeviceAuthorizationStatus = "Your GitHub account is ready to use.";
			Stage = LoginStage.Success;
		}
		catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
		{
			DeviceAuthorizationStatus = string.Empty;
			Stage = LoginStage.Welcome;
		}
		catch (Exception exception)
		{
			SetSignInError(operation, exception);
		}
		finally
		{
			if (ReferenceEquals(_authorizationCancellation, cancellation))
				_authorizationCancellation = null;

			IsTaskLoading = false;
		}
	}

	private async Task OpenDeviceVerificationUriAsync()
	{
		if (!Uri.TryCreate(DeviceVerificationUri, UriKind.Absolute, out var uri))
			throw new InvalidOperationException("GitHub returned an invalid authorization URL.");

		if (!await Launcher.LaunchUriAsync(uri))
			throw new InvalidOperationException("Windows could not open the GitHub authorization page.");
	}

	private void ReturnToWelcome()
	{
		CancelAuthorization();
		DeviceUserCode = string.Empty;
		DeviceVerificationUri = string.Empty;
		DeviceAuthorizationStatus = string.Empty;
		SignedInLogin = string.Empty;
		ResetError();
		Stage = LoginStage.Welcome;
	}

	private void ResetError()
	{
		ErrorTitle = string.Empty;
		ErrorMessage = string.Empty;
		ErrorDetails = string.Empty;
	}

	private void SetSignInError(LoginOperation operation, Exception exception)
	{
		(ErrorTitle, ErrorMessage) = exception switch
		{
			TimeoutException => (
				"The GitHub sign-in code expired",
				"The one-time code was not approved in time. Start the sign-in process again."),
			UnauthorizedAccessException => (
				"GitHub authorization was not completed",
				"The authorization request was canceled or denied. Try again and approve FluentHub on GitHub."),
			HttpRequestException => (
				"Couldn't connect to GitHub",
				"Check your internet connection and GitHub's availability, then try again."),
			_ => operation switch
			{
				LoginOperation.RequestDeviceCode => (
					"Couldn't start GitHub sign-in",
					"FluentHub could not request a one-time sign-in code from GitHub. Try again in a moment."),
				LoginOperation.OpenBrowser => (
					"Couldn't open GitHub in your browser",
					"Windows could not open the GitHub authorization page. Check your default browser and try again."),
				LoginOperation.WaitForAuthorization => (
					"GitHub couldn't complete the authorization",
					"FluentHub did not receive a usable access token from GitHub. Start the sign-in process again."),
				LoginOperation.ResolveAccount => (
					"Signed in, but couldn't load your GitHub account",
					"GitHub authorized FluentHub, but the signed-in user could not be retrieved. Check your connection and try again."),
				LoginOperation.SaveSettings => (
					"Signed in, but couldn't save your settings",
					"GitHub authorized FluentHub, but the local JSON settings file could not be written. Check your access to the local app data folder."),
				_ => (
					"Couldn't complete GitHub sign-in",
					"An unexpected error interrupted the sign-in process. Try again."),
			},
		};

		ErrorDetails = string.IsNullOrWhiteSpace(exception.Message)
			? $"Step: {operation}\r\n{exception.GetType().Name}"
			: $"Step: {operation}\r\n{exception.GetType().Name}: {exception.Message}";
		DeviceAuthorizationStatus = string.Empty;
		Stage = LoginStage.Error;
	}

	private enum LoginOperation
	{
		RequestDeviceCode,
		OpenBrowser,
		WaitForAuthorization,
		ResolveAccount,
		SaveSettings,
	}
}
