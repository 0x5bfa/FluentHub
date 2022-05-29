using FluentHub.Core;
using FluentHub.Models;
using FluentHub.Octokit.Authorization;
using FluentHub.ViewModels.UserControls.ButtonBlocks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using Octokit;
using System;
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

            AuthorizeCommand = new AsyncRelayCommand(AuthorizeAsync);
        }
        #endregion

        #region properties
        private readonly ILogger _logger;

        private bool _isActive;
        public bool IsActive { get => _isActive; set => SetProperty(ref _isActive, value); }

        public IAsyncRelayCommand AuthorizeCommand { get; }
        #endregion

        #region methods
        private async Task AuthorizeAsync(CancellationToken token)
        {
            try
            {
                var secrets = await Services.OctokitActivationService.GetOctokitSecrets();

                OctokitAuthenticator request = new();
                string url = request.RequestGitHubIdentityAsync(secrets);

                await Windows.System.Launcher.LaunchUriAsync(new Uri(url));

                App.Settings.SetupProgress = true;
                IsActive = true;
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                _logger?.Error(nameof(AuthorizeAsync), ex);
                throw;
            }
        }
        #endregion
    }
}
