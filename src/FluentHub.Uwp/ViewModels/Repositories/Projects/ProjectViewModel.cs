using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using FluentHub.Octokit.Queries.Repositories;
using FluentHub.Uwp.UserControls.Blocks;
using FluentHub.Uwp.ViewModels.UserControls.Blocks;

namespace FluentHub.Uwp.ViewModels.Repositories.Projects
{
    public class ProjectViewModel : ObservableObject
    {
        public ProjectViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;

            LoadProjectPageCommand = new AsyncRelayCommand<string>(LoadProjectPageAsync);
        }

        #region Fields and Properties
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;

        private Project _project;
        public Project Project { get => _project; set => SetProperty(ref _project, value); }

        public IAsyncRelayCommand LoadProjectPageCommand { get; }
        #endregion

        private async Task LoadProjectPageAsync(string url)
        {
            try
            {
                var pathSegments = url.Split("/");

                ProjectQueries queries = new();
                var response = await queries.GetAsync(pathSegments[3], pathSegments[4], Convert.ToInt32(pathSegments[6]));

                if (response == null) return;

                Project = response;
            }
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadProjectPageAsync), ex);
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
