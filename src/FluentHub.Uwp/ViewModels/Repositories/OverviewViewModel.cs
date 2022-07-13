using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using FluentHub.Octokit.Queries.Repositories;
using FluentHub.Uwp.ViewModels.UserControls.Labels;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml;

namespace FluentHub.Uwp.ViewModels.Repositories
{
    public class OverviewViewModel : ObservableObject
    {
        public OverviewViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;

            RepositoryVisibilityLabel = new()
            {
                Color = "#64000000"
            };

            LoadOverviewPageCommand = new AsyncRelayCommand<string>(LoadOverviewPageAsync);
        }

        #region Fields and Properties
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;

        private Repository _repository;
        public Repository Repository { get => _repository; set => SetProperty(ref _repository, value); }

        private LabelControlViewModel _repositoryVisibilityLabel;
        public LabelControlViewModel RepositoryVisibilityLabel { get => _repositoryVisibilityLabel; set => SetProperty(ref _repositoryVisibilityLabel, value); }

        private string _viewerSubscriptionState;
        public string ViewerSubscriptionState { get => _viewerSubscriptionState; set => SetProperty(ref _viewerSubscriptionState, value); }

        public IAsyncRelayCommand LoadOverviewPageCommand { get; }
        #endregion

        private async Task LoadOverviewPageAsync(string url, CancellationToken token)
        {
            try
            {
                var uri = new Uri(url);
                var pathSegments = uri.AbsolutePath.Split("/").ToList();
                pathSegments.RemoveAt(0);
                string owner = pathSegments[0];
                string name = pathSegments[1];

                RepositoryQueries queries = new();
                Repository = await queries.GetDetailsAsync(pathSegments[0], pathSegments[1]);

                ViewerSubscriptionState = Repository.ViewerSubscription?.Humanize();
                RepositoryVisibilityLabel.Name = Repository.IsPrivate ? "Private" : "Public";
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadOverviewPageAsync), ex);
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
