// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.Utils;
using FluentHub.Octokit.Authorization;
using Windows.System;

namespace FluentHub.App.ViewModels.SignIn
{
	public class IntroViewModel : ObservableObject
	{
		public IntroViewModel(ILogger logger = null, IMessenger messenger = null)
		{
			_logger = logger;
			_messenger = messenger;

			_messenger?.Register<UserNotificationMessage>(this, OnNewNotificationReceived);

			AuthorizeOAuthCommand = new AsyncRelayCommand<string>(AuthorizeOAuthAsync);
			AuthorizeWithBrowserCommand = new AsyncRelayCommand<string>(AuthorizeWithBrowserAsync);
		}

		#region Fields and Properties
		private readonly ILogger _logger;
		private readonly IMessenger _messenger;

		private Exception _exception;
		public Exception Exception { get => _exception; set => SetProperty(ref _exception, value); }

		private bool _authorizedSuccessfully;
		public bool AuthorizedSuccessfully { get => _authorizedSuccessfully; set => SetProperty(ref _authorizedSuccessfully, value); }

		private bool _authorizing;
		public bool Authorizing { get => _authorizing; set => SetProperty(ref _authorizing, value); }

		public IAsyncRelayCommand AuthorizeOAuthCommand { get; set; }
		public IAsyncRelayCommand AuthorizeWithBrowserCommand { get; set; }
		#endregion

		private async Task AuthorizeWithBrowserAsync(string login, CancellationToken token)
		{
			try
			{
				var secrets = await OctokitSecretsService.LoadOctokitSecretsAsync();

				AuthorizationService request = new();
				var url = request.RequestGitHubIdentityAsync(secrets);
				await Launcher.LaunchUriAsync(new Uri(url));

				App.AppSettings.SetupProgress = true;
			}
			catch (Exception ex)
			{
				Exception = ex;
				App.AppSettings.SetupProgress = false;

				_logger?.Error(nameof(AuthorizeWithBrowserAsync), ex);
				throw;
			}
		}

		private async Task AuthorizeOAuthAsync(string code, CancellationToken token)
		{
			try
			{
				Authorizing = true;

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
				Exception = ex;
				_logger?.Info("FluentHub authorization failed.");

				AuthorizedSuccessfully = false;
			}
			finally
			{
				Authorizing = false;
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
				Exception = ex;
				_logger?.Info("FluentHub authorization failed in getting authorized account info.");

				AuthorizedSuccessfully = false;
			}
		}

		private void OnNewNotificationReceived(object recipient, UserNotificationMessage message)
		{
			if (message.Title != "Received GitHub User ID")
				return;

			var code = message.Message;

			var command = AuthorizeOAuthCommand;
			if (command.CanExecute(code))
				command.Execute(code);
		}
	}
}
