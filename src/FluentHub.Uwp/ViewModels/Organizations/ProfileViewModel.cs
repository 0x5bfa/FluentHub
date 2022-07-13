using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using FluentHub.Octokit.Queries.Organizations;

namespace FluentHub.Uwp.ViewModels.Organizations
{
    public class ProfileViewModel : ObservableObject
    {
        public ProfileViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;

            LoadOrganizationAsyncCommand = new AsyncRelayCommand<string>(GetOrganizationAsync);
        }

        #region Fields and Properties
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;

        private Organization organization;
        public Organization Organization { get => organization; private set => SetProperty(ref organization, value); }

        public IAsyncRelayCommand LoadOrganizationAsyncCommand { get; }
        #endregion

        public async Task GetOrganizationAsync(string org)
        {
            try
            {
                OrganizationQueries queries = new();
                var organization = await queries.GetOverview(org);
                if (organization == null) return;

                Organization = organization;
            }
            catch (Exception ex)
            {
                _logger?.Error("LoadOrganizationOverviewAsync", ex);
                if (_messenger != null)
                {
                    UserNotificationMessage notification = new("Something went wrong", ex.Message, UserNotificationType.Error);
                    _messenger.Send(notification);
                }
                throw;
            }
        }
    }
}
