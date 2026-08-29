// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Application;
using FluentHub.Core.Application.Abstractions.Authentication;
using FluentHub.Core.Infrastructure.GitHub.Authorization;
using FluentHub.Models;
using FluentHub.Services;
using FluentHub.Utils;
using System.Windows.Input;
using Windows.System;

namespace FluentHub.ViewModels.SignIn
{
	public enum SignInFlowStage
	{
		Welcome,
		DeviceCode,
		Syncing,
		Error,
		Success,
	}

	internal enum SignInOperation
	{
		RequestDeviceCode,
		OpenBrowser,
		WaitForAuthorization,
		ResolveAccount,
		SaveAccount,
	}

	public class IntroViewModel : ObservableObject
	{
		private readonly IFluentHubGitHubClient _gitHub;
		private readonly IUserSession _sessionManager;
		private readonly AuthorizationService _authorizationService;
		private readonly AccountService _accountService;
		private readonly GitHubTokenStore _tokenStore;
		private readonly ILogger _logger;
		private readonly IMessenger _messenger;
		private CancellationTokenSource? _deviceAuthorizationCancellationTokenSource;

		private SignInFlowStage _stage = SignInFlowStage.Welcome;
		public SignInFlowStage Stage
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

		public bool IsWelcomeStage => Stage == SignInFlowStage.Welcome;
		public bool IsDeviceCodeStage => Stage == SignInFlowStage.DeviceCode;
		public bool IsSyncingStage => Stage == SignInFlowStage.Syncing;
		public bool IsErrorStage => Stage == SignInFlowStage.Error;
		public bool IsSuccessStage => Stage == SignInFlowStage.Success;

		private bool _authorizedSuccessfully;
		public bool AuthorizedSuccessfully
		{
			get => _authorizedSuccessfully;
			private set => SetProperty(ref _authorizedSuccessfully, value);
		}

		private bool _urlWasLaunched;
		public bool UrlWasLaunched
		{
			get => _urlWasLaunched;
			private set => SetProperty(ref _urlWasLaunched, value);
		}

		private Exception _taskException = new();
		public Exception TaskException
		{
			get => _taskException;
			private set => SetProperty(ref _taskException, value);
		}

		private bool _isTaskFaulted;
		public bool IsTaskFaulted
		{
			get => _isTaskFaulted;
			private set => SetProperty(ref _isTaskFaulted, value);
		}

		private bool _isTaskLoading;
		public bool IsTaskLoading
		{
			get => _isTaskLoading;
			private set => SetProperty(ref _isTaskLoading, value);
		}

		private string _deviceUserCode = string.Empty;
		public string DeviceUserCode
		{
			get => _deviceUserCode;
			private set
			{
				SetProperty(ref _deviceUserCode, value);
				OnPropertyChanged(nameof(IsDeviceAuthorizationAvailable));
			}
		}

		private string _deviceVerificationUri = string.Empty;
		public string DeviceVerificationUri
		{
			get => _deviceVerificationUri;
			private set => SetProperty(ref _deviceVerificationUri, value);
		}

		private string _deviceAuthorizationStatus = string.Empty;
		public string DeviceAuthorizationStatus
		{
			get => _deviceAuthorizationStatus;
			private set => SetProperty(ref _deviceAuthorizationStatus, value);
		}

		private string _signedInLogin = string.Empty;
		public string SignedInLogin
		{
			get => _signedInLogin;
			private set => SetProperty(ref _signedInLogin, value);
		}

		private string _errorTitle = string.Empty;
		public string ErrorTitle
		{
			get => _errorTitle;
			private set => SetProperty(ref _errorTitle, value);
		}

		private string _errorMessage = string.Empty;
		public string ErrorMessage
		{
			get => _errorMessage;
			private set => SetProperty(ref _errorMessage, value);
		}

		private string _errorDetails = string.Empty;
		public string ErrorDetails
		{
			get => _errorDetails;
			private set => SetProperty(ref _errorDetails, value);
		}

		public bool IsDeviceAuthorizationAvailable
			=> !string.IsNullOrEmpty(DeviceUserCode);

		public string Version
		{
			get
			{
				string architecture = Windows.ApplicationModel.Package.Current.Id.Architecture.ToString();

#if DEBUG
				string buildConfiguration = "DEBUG";
#else
				string buildConfiguration = "RELEASE";
#endif

				return $"{App.AppVersion} | {architecture} | {buildConfiguration}";
			}
		}

		public ICommand AuthorizeWithBrowserCommand { get; }
		public ICommand OpenDeviceVerificationUriCommand { get; }
		public ICommand ReturnToWelcomeCommand { get; }

		public IntroViewModel(
			IFluentHubGitHubClient gitHub,
			IUserSession sessionManager,
			AuthorizationService authorizationService,
			AccountService accountService,
			GitHubTokenStore tokenStore,
			ILogger logger,
			IMessenger messenger)
		{
			_gitHub = gitHub;
			_sessionManager = sessionManager;
			_authorizationService = authorizationService;
			_accountService = accountService;
			_tokenStore = tokenStore;
			_logger = logger;
			_messenger = messenger;

			AuthorizeWithBrowserCommand = new AsyncRelayCommand(AuthorizeWithBrowserAsync);
			OpenDeviceVerificationUriCommand = new AsyncRelayCommand(OpenDeviceVerificationUriAsync);
			ReturnToWelcomeCommand = new RelayCommand(ReturnToWelcome);
		}

		public void CancelAuthorization()
			=> _deviceAuthorizationCancellationTokenSource?.Cancel();

		private async Task AuthorizeWithBrowserAsync()
		{
			CancelAuthorization();
			_deviceAuthorizationCancellationTokenSource = new CancellationTokenSource();
			var cancellationToken = _deviceAuthorizationCancellationTokenSource.Token;
			var operation = SignInOperation.RequestDeviceCode;

			try
			{
				IsTaskLoading = true;
				IsTaskFaulted = false;
				AuthorizedSuccessfully = false;
				DeviceUserCode = string.Empty;
				DeviceVerificationUri = string.Empty;
				DeviceAuthorizationStatus = "Requesting a GitHub device code...";
				ErrorTitle = string.Empty;
				ErrorMessage = string.Empty;
				ErrorDetails = string.Empty;
				Stage = SignInFlowStage.DeviceCode;

				var deviceAuthorization = await _authorizationService.RequestDeviceAuthorizationAsync(
					cancellationToken);

				DeviceUserCode = deviceAuthorization.UserCode;
				DeviceVerificationUri = deviceAuthorization.VerificationUri;
				DeviceAuthorizationStatus = "Waiting for authorization in your browser...";

				operation = SignInOperation.OpenBrowser;
				await OpenDeviceVerificationUriAsync();

				App.AppSettings.SetupProgress = true;
				UrlWasLaunched = true;

				var progress = new Progress<DeviceAuthorizationPollingStatus>(status =>
					DeviceAuthorizationStatus = status switch
					{
						DeviceAuthorizationPollingStatus.Pending => "Waiting for authorization in your browser...",
						DeviceAuthorizationPollingStatus.SlowedDown => "GitHub asked us to slow down. Still waiting...",
						_ => DeviceAuthorizationStatus,
					});
				operation = SignInOperation.WaitForAuthorization;
				var accessToken = await _authorizationService.WaitForDeviceAccessTokenAsync(
					deviceAuthorization,
					progress,
					cancellationToken);

				Stage = SignInFlowStage.Syncing;
				DeviceAuthorizationStatus = "Securing your token and loading your account...";
				operation = SignInOperation.ResolveAccount;
				_sessionManager.SwitchAccount(accessToken);
				string login = await _gitHub.Users.Users.GetViewerLoginAsync(cancellationToken);

				operation = SignInOperation.SaveAccount;
				SaveAccountInfo(login, accessToken);

				_logger.Info("FluentHub is authorized successfully.");
				AuthorizedSuccessfully = true;
				DeviceAuthorizationStatus = "Your GitHub account is ready to use.";
				Stage = SignInFlowStage.Success;

				App.AppSettings.SetupProgress = true;
				App.AppSettings.SetupCompleted = true;
			}
			catch (OperationCanceledException)
			{
				DeviceAuthorizationStatus = string.Empty;
			}
			catch (Exception ex)
			{
				SetSignInError(operation, ex);
				App.AppSettings.SetupProgress = false;

				_logger.Error($"{nameof(AuthorizeWithBrowserAsync)} failed during {operation}.", ex);
			}
			finally
			{
				IsTaskLoading = false;
			}
		}

		private void ReturnToWelcome()
		{
			CancelAuthorization();
			IsTaskFaulted = false;
			AuthorizedSuccessfully = false;
			UrlWasLaunched = false;
			DeviceUserCode = string.Empty;
			DeviceVerificationUri = string.Empty;
			DeviceAuthorizationStatus = string.Empty;
			SignedInLogin = string.Empty;
			ErrorTitle = string.Empty;
			ErrorMessage = string.Empty;
			ErrorDetails = string.Empty;
			Stage = SignInFlowStage.Welcome;
		}

		private async Task OpenDeviceVerificationUriAsync()
		{
			if (!Uri.TryCreate(DeviceVerificationUri, UriKind.Absolute, out var uri))
				throw new InvalidOperationException("GitHub returned an invalid authorization URL.");

			if (!await Launcher.LaunchUriAsync(uri))
				throw new InvalidOperationException("Windows could not open the GitHub authorization page.");
		}

		private void SaveAccountInfo(string login, string accessToken)
		{
			_tokenStore.SaveToken(login, accessToken);
			App.AppSettings.SignedInUserName = login;
			_accountService.AddAccount(login);
			SignedInLogin = login;
		}

		private void SetSignInError(SignInOperation operation, Exception exception)
		{
			TaskException = exception;
			IsTaskFaulted = true;
			DeviceAuthorizationStatus = string.Empty;

			(ErrorTitle, ErrorMessage) = exception switch
			{
				TimeoutException => (
					"The GitHub sign-in code expired",
					"The one-time code was not approved in time. Start the sign-in process again to get a new code."),
				UnauthorizedAccessException => (
					"GitHub authorization was not completed",
					"The authorization request was canceled or denied. Try again and approve FluentHub on GitHub."),
				global::System.Net.Http.HttpRequestException => (
					"Couldn't connect to GitHub",
					"Check your internet connection and GitHub's availability, then try again."),
				_ => operation switch
				{
					SignInOperation.RequestDeviceCode => (
						"Couldn't start GitHub sign-in",
						"FluentHub could not request a one-time sign-in code from GitHub. Try again in a moment."),
					SignInOperation.OpenBrowser => (
						"Couldn't open GitHub in your browser",
						"Windows could not open the GitHub authorization page. Check your default browser and try again."),
					SignInOperation.WaitForAuthorization => (
						"GitHub couldn't complete the authorization",
						"FluentHub did not receive a usable access token from GitHub. Start the sign-in process again."),
					SignInOperation.ResolveAccount => (
						"Signed in, but couldn't load your GitHub account",
						"GitHub authorized FluentHub, but the signed-in user could not be retrieved. Check your connection and try again."),
					SignInOperation.SaveAccount => (
						"Signed in, but couldn't save your account",
						"GitHub authorized FluentHub, but Windows could not store the account securely. Try again or check Windows Credential Manager."),
					_ => (
						"Couldn't complete GitHub sign-in",
						"An unexpected error interrupted the sign-in process. Try again."),
				},
			};

			var details = string.IsNullOrWhiteSpace(exception.Message)
				? exception.GetType().Name
				: $"{exception.GetType().Name}: {exception.Message}";
			ErrorDetails = $"Step: {operation}\r\n{details}";
			Stage = SignInFlowStage.Error;
		}
	}
}
