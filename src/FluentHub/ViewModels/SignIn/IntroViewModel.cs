// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Models;
using FluentHub.Services;
using FluentHub.Utils;
using FluentHub.Core.Application;
using FluentHub.Core.Authorization;
using System.Windows.Input;
using Windows.System;

namespace FluentHub.ViewModels.SignIn
{
	public class IntroViewModel : ObservableObject
	{
		private readonly IFluentHubGitHubClient _gitHub;
		private readonly IGitHubSessionManager _sessionManager;
		private readonly AuthorizationService _authorizationService;
		private readonly AccountService _accountService;
		private readonly GitHubTokenStore _tokenStore;
		private readonly ILogger _logger;
		private readonly IMessenger _messenger;
		private CancellationTokenSource _deviceAuthorizationCancellationTokenSource = default!;

		private bool _authorizedSuccessfully;
		public bool AuthorizedSuccessfully
		{
			get => _authorizedSuccessfully;
			set => SetProperty(ref _authorizedSuccessfully, value);
		}

		private bool _UrlWasLaunched;
		public bool UrlWasLaunched
		{
			get => _UrlWasLaunched;
			set => SetProperty(ref _UrlWasLaunched, value);
		}

		private Exception _taskException = default!;
		public Exception TaskException { get => _taskException; set => SetProperty(ref _taskException, value); }

		protected bool _IsTaskFaulted;
		public bool IsTaskFaulted { get => _IsTaskFaulted; set => SetProperty(ref _IsTaskFaulted, value); }

		protected bool _IsTaskLoading;
		public bool IsTaskLoading { get => _IsTaskLoading; set => SetProperty(ref _IsTaskLoading, value); }

		private string _deviceUserCode = default!;
		public string DeviceUserCode
		{
			get => _deviceUserCode;
			set
			{
				SetProperty(ref _deviceUserCode, value);
				OnPropertyChanged(nameof(IsDeviceAuthorizationAvailable));
			}
		}

		private string _deviceVerificationUri = default!;
		public string DeviceVerificationUri
		{
			get => _deviceVerificationUri;
			set => SetProperty(ref _deviceVerificationUri, value);
		}

		private string _deviceAuthorizationStatus = default!;
		public string DeviceAuthorizationStatus
		{
			get => _deviceAuthorizationStatus;
			set => SetProperty(ref _deviceAuthorizationStatus, value);
		}

		public bool IsDeviceAuthorizationAvailable
			=> string.IsNullOrEmpty(DeviceUserCode) is false;

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

		public ICommand AuthorizeWithBrowserCommand { get; set; }
		public ICommand OpenDeviceVerificationUriCommand { get; set; }

		public IntroViewModel(
			IFluentHubGitHubClient gitHub,
			IGitHubSessionManager sessionManager,
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
		}

		private async Task AuthorizeWithBrowserAsync()
		{
			_deviceAuthorizationCancellationTokenSource?.Cancel();
			_deviceAuthorizationCancellationTokenSource = new CancellationTokenSource();
			var cancellationToken = _deviceAuthorizationCancellationTokenSource.Token;

			try
			{
				IsTaskLoading = true;
				IsTaskFaulted = false;
				AuthorizedSuccessfully = false;
				DeviceUserCode = string.Empty;
				DeviceVerificationUri = string.Empty;
				DeviceAuthorizationStatus = "Requesting a GitHub device code...";

				var deviceAuthorization = await _authorizationService.RequestDeviceAuthorizationAsync(
					cancellationToken);

				DeviceUserCode = deviceAuthorization.UserCode;
				DeviceVerificationUri = deviceAuthorization.VerificationUri;
				DeviceAuthorizationStatus = "Waiting for GitHub authorization...";

				// Load the URL in user's browser
				await OpenDeviceVerificationUriAsync();

				App.AppSettings.SetupProgress = true;
				UrlWasLaunched = true;

				var progress = new Progress<DeviceAuthorizationPollingStatus>(status =>
					DeviceAuthorizationStatus = status switch
					{
						DeviceAuthorizationPollingStatus.Pending => "Waiting for GitHub authorization...",
						DeviceAuthorizationPollingStatus.SlowedDown => "GitHub asked us to slow down. Still waiting...",
						_ => DeviceAuthorizationStatus,
					});
				var accessToken = await _authorizationService.WaitForDeviceAccessTokenAsync(
					deviceAuthorization,
					progress,
					cancellationToken);

				_logger?.Info("FluentHub is authorized successfully.");

				// Set token and login to App Settings Container
				await SetAccountInfoAsync(accessToken, cancellationToken);

				AuthorizedSuccessfully = true;
				DeviceAuthorizationStatus = "FluentHub is authorized successfully.";

				// Setup was completed successfully
				App.AppSettings.SetupProgress = true;
				App.AppSettings.SetupCompleted = true;
			}
			catch (OperationCanceledException)
			{
				DeviceAuthorizationStatus = string.Empty;
			}
			catch (Exception ex)
			{
				TaskException = ex;
				IsTaskFaulted = true;
				App.AppSettings.SetupProgress = false;

				_logger?.Error(nameof(AuthorizeWithBrowserAsync), ex);
			}
			finally
			{
				IsTaskLoading = false;
			}
		}

		private async Task OpenDeviceVerificationUriAsync()
		{
			if (Uri.TryCreate(DeviceVerificationUri, UriKind.Absolute, out var uri))
			{
				await Launcher.LaunchUriAsync(uri);
			}
		}

		private async Task SetAccountInfoAsync(
			string accessToken,
			CancellationToken cancellationToken)
		{
			_sessionManager.SwitchAccount(accessToken);

			var queries = _gitHub.Users.Users;
			string login = await queries.GetViewerLoginAsync(cancellationToken);

			_tokenStore.SaveToken(login, accessToken);
			App.AppSettings.SignedInUserName = login;
			_accountService.AddAccount(login);
		}
	}
}
