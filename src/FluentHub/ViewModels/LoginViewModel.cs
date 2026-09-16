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
			DeviceAuthorizationStatus = Strings.Login_RequestingDeviceCodeStatus.GetLocalized();
			Stage = LoginStage.DeviceCode;

			var deviceAuthorization = await _authorizationService
				.RequestDeviceAuthorizationAsync(cancellationToken);
			DeviceUserCode = deviceAuthorization.UserCode;
			DeviceVerificationUri = deviceAuthorization.VerificationUri;

			operation = LoginOperation.OpenBrowser;
			DeviceAuthorizationStatus = Strings.Login_OpeningGitHubStatus.GetLocalized();
			await OpenDeviceVerificationUriAsync();
			DeviceAuthorizationStatus = Strings.Login_WaitingForAuthorizationStatus.GetLocalized();

			var progress = new Progress<DeviceAuthorizationPollingStatus>(status =>
				DeviceAuthorizationStatus = status switch
				{
					DeviceAuthorizationPollingStatus.Pending => Strings.Login_WaitingForAuthorizationStatus.GetLocalized(),
					DeviceAuthorizationPollingStatus.SlowedDown => Strings.Login_GitHubSlowedDownStatus.GetLocalized(),
					_ => DeviceAuthorizationStatus,
				});

			operation = LoginOperation.WaitForAuthorization;
			var accessToken = await _authorizationService.WaitForDeviceAccessTokenAsync(
				deviceAuthorization,
				progress,
				cancellationToken);

			Stage = LoginStage.Syncing;
			DeviceAuthorizationStatus = Strings.Login_GitHubAccountLoadingStatus.GetLocalized();
			operation = LoginOperation.ResolveAccount;
			_session.SwitchAccount(accessToken);
			var login = await _gitHub.Users.Users.GetViewerLoginAsync(cancellationToken);

			operation = LoginOperation.SaveSettings;
			await _settings.SaveSessionAsync(login, accessToken, cancellationToken);

			SignedInLogin = login;
			DeviceAuthorizationStatus = Strings.Login_AccountReadyStatus.GetLocalized();
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
			throw new InvalidOperationException(Strings.Login_GitHubAuthorizationUrlInvalid.GetLocalized());

		if (!await Launcher.LaunchUriAsync(uri))
			throw new InvalidOperationException(Strings.Login_CouldNotOpenGitHubPage.GetLocalized());
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
				Strings.Login_SignInCodeExpiredTitle.GetLocalized(),
				Strings.Login_SignInCodeExpiredMessage.GetLocalized()),
			UnauthorizedAccessException => (
				Strings.Login_AuthorizationNotCompletedTitle.GetLocalized(),
				Strings.Login_AuthorizationNotCompletedMessage.GetLocalized()),
			HttpRequestException => (
				Strings.Login_CouldNotConnectTitle.GetLocalized(),
				Strings.Login_CouldNotConnectMessage.GetLocalized()),
			_ => operation switch
			{
				LoginOperation.RequestDeviceCode => (
					Strings.Login_CouldNotStartSignInTitle.GetLocalized(),
					Strings.Login_CouldNotStartSignInMessage.GetLocalized()),
				LoginOperation.OpenBrowser => (
					Strings.Login_CouldNotOpenGitHubTitle.GetLocalized(),
					Strings.Login_CouldNotOpenGitHubMessage.GetLocalized()),
				LoginOperation.WaitForAuthorization => (
					Strings.Login_CouldNotCompleteAuthorizationTitle.GetLocalized(),
					Strings.Login_CouldNotCompleteAuthorizationMessage.GetLocalized()),
				LoginOperation.ResolveAccount => (
					Strings.Login_CouldNotLoadGitHubAccountTitle.GetLocalized(),
					Strings.Login_CouldNotLoadGitHubAccountMessage.GetLocalized()),
				LoginOperation.SaveSettings => (
					Strings.Login_CouldNotSaveSettingsTitle.GetLocalized(),
					Strings.Login_CouldNotSaveSettingsMessage.GetLocalized()),
				_ => (
					Strings.Login_CouldNotCompleteSignInTitle.GetLocalized(),
					Strings.Login_CouldNotCompleteSignInMessage.GetLocalized()),
			},
		};

		ErrorDetails = string.IsNullOrWhiteSpace(exception.Message)
			? string.Format(
				CultureInfo.CurrentCulture,
				Strings.Login_ErrorDetailsWithoutMessage.GetLocalized(),
				operation,
				exception.GetType().Name)
			: string.Format(
				CultureInfo.CurrentCulture,
				Strings.Login_ErrorDetailsWithMessage.GetLocalized(),
				operation,
				exception.GetType().Name,
				exception.Message);
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
