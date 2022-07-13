using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using FluentHub.Octokit.Queries.Repositories;
using FluentHub.Uwp.UserControls.Blocks;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;

namespace FluentHub.Uwp.ViewModels.Repositories.Discussions
{
    public class DiscussionsViewModel : ObservableObject
    {
        public DiscussionsViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;

            _items = new();
            Items = new(_items);

            LoadDiscussionsPageCommand = new AsyncRelayCommand<string>(LoadDiscussionsPageAsync);
        }

        #region Fields and Properties
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;

        private readonly ObservableCollection<DiscussionButtonBlockViewModel> _items;
        public ReadOnlyObservableCollection<DiscussionButtonBlockViewModel> Items { get; }

        public IAsyncRelayCommand LoadDiscussionsPageCommand { get; }
        #endregion

        private async Task LoadDiscussionsPageAsync(string nameWithOwner)
        {
            try
            {
                DiscussionQueries queries = new();
                var items = await queries.GetAllAsync(nameWithOwner.Split("/")[0], nameWithOwner.Split("/")[1]);

                _items.Clear();
                foreach (var item in items)
                {
                    DiscussionButtonBlockViewModel viewmodel = new()
                    {
                        Item = item,
                    };

                    _items.Add(viewmodel);
                }
            }
            catch (Exception ex)
            {
                _logger?.Error("LoadDiscussionsPageAsync", ex);
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
