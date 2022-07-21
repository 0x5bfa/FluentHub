using FluentHub.Octokit.Queries.Repositories;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;

namespace FluentHub.Uwp.ViewModels.Repositories.Projects
{
    public class ProjectsViewModel : ObservableObject
    {
        public ProjectsViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;

            _items = new();
            Items = new(_items);

            LoadProjectsPageCommand = new AsyncRelayCommand<string>(LoadProjectsPageAsync);
        }

        #region Fields and Properties
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;

        private readonly ObservableCollection<ProjectButtonBlockViewModel> _items;
        public ReadOnlyObservableCollection<ProjectButtonBlockViewModel> Items { get; }

        public IAsyncRelayCommand LoadProjectsPageCommand { get; }
        #endregion

        private async Task LoadProjectsPageAsync(string nameWithOwner)
        {
            try
            {
                _messenger?.Send(new LoadingMessaging(true));

                ProjectQueries queries = new();
                var items = await queries.GetAllAsync(nameWithOwner.Split("/")[0], nameWithOwner.Split("/")[1]);

                _items.Clear();
                foreach (var item in items)
                {
                    ProjectButtonBlockViewModel viewmodel = new()
                    {
                        Item = item,
                    };

                    _items.Add(viewmodel);
                }
            }
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadProjectsPageAsync), ex);
                if (_messenger != null)
                {
                    UserNotificationMessage notification = new("Something went wrong", ex.Message, UserNotificationType.Error);
                    _messenger.Send(notification);
                }
                throw;
            }
            finally
            {
                _messenger?.Send(new LoadingMessaging(false));
            }
        }
    }
}
