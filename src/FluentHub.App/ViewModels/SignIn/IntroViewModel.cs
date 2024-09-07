// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

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

		public string ReceivedUserIdentity
		{
			set
			{
				var command = AuthorizeOAuthCommand;
				if (command.CanExecute(value))
					command.Execute(value);
			}
		}

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

		public ICommand AuthorizeOAuthCommand { get; set; }
		public ICommand AuthorizeWithBrowserCommand { get; set; }

		public IntroViewModel()
		{
			_logger = Ioc.Default.GetRequiredService<ILogger>();
			_messenger = Ioc.Default.GetRequiredService<IMessenger>();

			AuthorizeWithBrowserCommand = new AsyncRelayCommand(AuthorizeWithBrowserAsync);
			AuthorizeOAuthCommand = new AsyncRelayCommand<string>(AuthorizeOAuthAsync);
		}

		private async Task AuthorizeWithBrowserAsync()
		{
			try
			{
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

				// Get authorization URL
				AuthorizationService request = new();
				var url = request.RequestGitHubIdentityAsync(secrets);

				// Load the URL in user's browser
				await Launcher.LaunchUriAsync(new Uri(url));

				App.AppSettings.SetupProgress = true;
				UrlWasLaunched = true;
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

			}
		}

		private async Task AuthorizeOAuthAsync(string? code)
		{
			try
			{
				IsTaskLoading = true;

				var secrets = await OctokitSecretsService.LoadOctokitSecretsAsync();

				AuthorizationService authService = new();
				var accessToken = await authService.RequestOAuthTokenAsync(code, secrets);

				_logger?.Info("FluentHub is authorized successfully.");

				// Set token and login to App Settings Container
				await SetAccountInfo(accessToken);

				AuthorizedSuccessfully = true;

				// Setup was completed successfully
				App.AppSettings.SetupProgress = true;
				App.AppSettings.SetupCompleted = true;
			}
			catch (Exception ex)
			{
				TaskException = ex;
				_logger?.Info("FluentHub authorization failed.");

				AuthorizedSuccessfully = false;
				IsTaskFaulted = true;
			}
			finally
			{
				IsTaskLoading = false;
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
