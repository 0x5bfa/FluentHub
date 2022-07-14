using FluentHub.Octokit.Queries.Repositories;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;

namespace FluentHub.Uwp.ViewModels.Repositories.Codes
{
    public class ReleasesViewModel : ObservableObject
    {
        public ReleasesViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;
            _messenger = messenger;

            _items = new();
            Items = new(_items);

            LoadReleasesPageCommand = new AsyncRelayCommand(LoadRepositoryReleasesAsync);
        }

        #region Fields and Properties
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;

        private RepoContextViewModel contextViewModel;
        public RepoContextViewModel ContextViewModel { get => contextViewModel; set => SetProperty(ref contextViewModel, value); }

        private readonly ObservableCollection<Release> _items;
        public ReadOnlyObservableCollection<Release> Items { get; }

        public IAsyncRelayCommand LoadReleasesPageCommand { get; }
        #endregion

        private async Task LoadRepositoryReleasesAsync(CancellationToken token)
        {
            try
            {
                ReleaseQueries queries = new();
                var items = await queries.GetAllAsync(
                    ContextViewModel.Repository.Owner.Login,
                    ContextViewModel.Repository.Name
                    );

                _items.Clear();
                foreach (var item in items) _items.Add(item);
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadRepositoryReleasesAsync), ex);
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
