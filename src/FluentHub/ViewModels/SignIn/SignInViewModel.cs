using FluentHub.Core;
using FluentHub.Octokit.Authorization;
using FluentHub.Octokit.Models;
using FluentHub.Models;
using FluentHub.Octokit.Queries.Users;
using FluentHub.ViewModels.UserControls.ButtonBlocks;
using Humanizer;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.SignIn
{
    public class SignInViewModel : ObservableObject
    {
        #region constructor
        public SignInViewModel(ILogger logger = null)
        {
            _logger = logger;

            AuthorizeWithBrowserCommand = new AsyncRelayCommand<string>(AuthorizeWithBrowser);
        }
        #endregion

        #region fields
        private readonly ILogger _logger;

        private bool isLoading;
        public bool IsLoading { get => isLoading; set => SetProperty(ref isLoading, value); }

        private string authErrorMessage;
        public string AuthErrorMessage { get => authErrorMessage; set => SetProperty(ref authErrorMessage, value); }

        public IAsyncRelayCommand AuthorizeWithBrowserCommand { get; }
        #endregion

        #region methods
        private async Task AuthorizeWithBrowser(string login, CancellationToken token)
        {
            try
            {
                var secrets = await Services.OctokitSecretsService.LoadOctokitSecretsAsync();

                AuthorizationService request = new();
                await request.RequestGitHubIdentityAsync(secrets);

                App.Settings.SetupProgress = true;
                IsLoading = true;
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                _logger?.Error("AuthorizeWithBrowser", ex);
                AuthErrorMessage = ex.Message;
                App.Settings.SetupProgress = false;
                IsLoading = false;
                throw;
            }
        }
        #endregion
    }
}
