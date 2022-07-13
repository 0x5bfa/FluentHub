using FluentHub.Octokit.Authorization;
using FluentHub.Octokit.Queries.Users;
using FluentHub.Uwp.Utils;
using FluentHub.Uwp.Models;
using Windows.System;

namespace FluentHub.Uwp.ViewModels.SignIn
{
    public class IntroViewModel : ObservableObject
    {
        public IntroViewModel(ILogger logger = null)
        {
            _logger = logger;

            AuthorizeWithBrowserCommand = new AsyncRelayCommand<string>(AuthorizeWithBrowser);
        }

        #region Fields and Properties
        private readonly ILogger _logger;

        private bool isLoading;
        public bool IsLoading { get => isLoading; set => SetProperty(ref isLoading, value); }

        private string authErrorMessage;
        public string AuthErrorMessage { get => authErrorMessage; set => SetProperty(ref authErrorMessage, value); }

        public IAsyncRelayCommand AuthorizeWithBrowserCommand { get; set; }
        #endregion

        private async Task AuthorizeWithBrowser(string login, CancellationToken token)
        {
            try
            {
                var secrets = await Services.OctokitSecretsService.LoadOctokitSecretsAsync();

                AuthorizationService request = new();
                var url = await request.RequestGitHubIdentityAsync(secrets);
                await Launcher.LaunchUriAsync(new Uri(url));

                App.Settings.SetupProgress = true;
                IsLoading = true;
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                _logger?.Error(nameof(AuthorizeWithBrowser), ex);
                AuthErrorMessage = ex.Message;
                App.Settings.SetupProgress = false;
                IsLoading = false;
                throw;
            }
        }
    }
}
