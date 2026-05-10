// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.Utils;
using FluentHub.Octokit.Authorization;
using System.Windows.Input;
using Windows.System;

namespace FluentHub.App.ViewModels.SignIn
{
	public class IntroViewModel : ObservableObject
	{
		private readonly ILogger _logger;
		private readonly IMessenger _messenger;
		private CancellationTokenSource _deviceAuthorizationCancellationTokenSource;

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

		private Exception _taskException;
		public Exception TaskException { get => _taskException; set => SetProperty(ref _taskException, value); }

		protected bool _IsTaskFaulted;
		public bool IsTaskFaulted { get => _IsTaskFaulted; set => SetProperty(ref _IsTaskFaulted, value); }

		protected bool _IsTaskLoading;
		public bool IsTaskLoading { get => _IsTaskLoading; set => SetProperty(ref _IsTaskLoading, value); }

		private string _deviceUserCode;
		public string DeviceUserCode
		{
			get => _deviceUserCode;
			set
			{
				SetProperty(ref _deviceUserCode, value);
				OnPropertyChanged(nameof(IsDeviceAuthorizationAvailable));
			}
		}

		private string _deviceVerificationUri;
		public string DeviceVerificationUri
		{
			get => _deviceVerificationUri;
			set => SetProperty(ref _deviceVerificationUri, value);
		}

		private string _deviceAuthorizationStatus;
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

		public IntroViewModel()
		{
			_logger = Ioc.Default.GetRequiredService<ILogger>();
			_messenger = Ioc.Default.GetRequiredService<IMessenger>();

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

				var secrets = await OctokitSecretsService.LoadOctokitSecretsAsync();

				if (secrets is null)
				{
					// Show error
					TaskException = new NullReferenceException(
						$"Please set values in AppCredentials.config\r\n" +
						$"For more information, visit our GitHub link below.");

					App.AppSettings.SetupProgress = false;
					_logger?.Error(nameof(AuthorizeWithBrowserAsync), TaskException);
					return;
				}

				AuthorizationService authService = new();
				var deviceAuthorization = await authService.RequestDeviceAuthorizationAsync(secrets);

				DeviceUserCode = deviceAuthorization.UserCode;
				DeviceVerificationUri = deviceAuthorization.VerificationUri;
				DeviceAuthorizationStatus = "Waiting for GitHub authorization...";

				// Load the URL in user's browser
				await OpenDeviceVerificationUriAsync();

				App.AppSettings.SetupProgress = true;
				UrlWasLaunched = true;

				var accessToken = await WaitForDeviceAccessTokenAsync(authService, secrets, deviceAuthorization, cancellationToken);

				_logger?.Info("FluentHub is authorized successfully.");

				// Set token and login to App Settings Container
				await SetAccountInfo(accessToken);

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

		private async Task<string> WaitForDeviceAccessTokenAsync(
			AuthorizationService authService,
			OctokitSecrets secrets,
			DeviceAuthorizationResponse deviceAuthorization,
			CancellationToken cancellationToken)
		{
			var interval = Math.Max(deviceAuthorization.Interval ?? 5, 5);
			var expiresAt = DateTimeOffset.UtcNow.AddSeconds(deviceAuthorization.ExpiresIn);

			while (DateTimeOffset.UtcNow < expiresAt)
			{
				await Task.Delay(TimeSpan.FromSeconds(interval), cancellationToken);

				try
				{
					return await authService.RequestDeviceAccessTokenAsync(deviceAuthorization.DeviceCode, secrets);
				}
				catch (DeviceAuthorizationPendingException)
				{
					DeviceAuthorizationStatus = "Waiting for GitHub authorization...";
				}
				catch (DeviceAuthorizationSlowDownException ex)
				{
					interval = Math.Max(ex.Interval ?? interval + 5, interval + 5);
					DeviceAuthorizationStatus = "GitHub asked us to slow down. Still waiting...";
				}
			}

			throw new TimeoutException("The GitHub device authorization code has expired.");
		}

		private async Task OpenDeviceVerificationUriAsync()
		{
			if (Uri.TryCreate(DeviceVerificationUri, UriKind.Absolute, out var uri))
			{
				await Launcher.LaunchUriAsync(uri);
			}
		}

		private async Task SetAccountInfo(string accessToken)
		{
			App.AppSettings.AccessToken = accessToken;

			try
			{
				Octokit.Queries.Users.UserQueries queries = new();
				string login = await queries.GetViewerLogin();
				App.AppSettings.SignedInUserName = login;

				AccountService.AddAccount(login);
			}
			catch (Exception ex)
			{
				TaskException = ex;
				_logger?.Info("FluentHub authorization failed in getting authorized account info.");

				AuthorizedSuccessfully = false;
			}
		}
	}
}
