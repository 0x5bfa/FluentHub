using FluentHub.Octokit.Queries.Users;
using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;
using FluentHub.Uwp.Utils;

namespace FluentHub.Uwp.ViewModels.Users
{
    public class OrganizationsViewModel : ObservableObject
    {
        public OrganizationsViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;
            _messenger = messenger;

            _organizations = new();
            Organizations = new(_organizations);

            RefreshOrganizationsCommand = new AsyncRelayCommand<string>(LoadUserOrganizationsAsync);
        }

        #region Fields and Properties
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;

        private bool _displayTitle;
        public bool DisplayTitle { get => _displayTitle; set => SetProperty(ref _displayTitle, value); }

        private readonly ObservableCollection<OrgButtonBlockViewModel> _organizations;
        public ReadOnlyObservableCollection<OrgButtonBlockViewModel> Organizations { get; }

        public IAsyncRelayCommand RefreshOrganizationsCommand { get; }
        #endregion

        private async Task LoadUserOrganizationsAsync(string login, CancellationToken token)
        {
            try
            {
                OrganizationQueries queries = new();
                var items = await queries.GetAllAsync(login);
                if (items == null) return;

                _organizations.Clear();
                foreach (var item in items)
                {
                    OrgButtonBlockViewModel viewModel = new()
                    {
                        OrgItem = item
                    };

                    _organizations.Add(viewModel);
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadUserOrganizationsAsync), ex);
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
