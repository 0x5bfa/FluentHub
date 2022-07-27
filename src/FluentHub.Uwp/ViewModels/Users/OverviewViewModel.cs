using FluentHub.Octokit.Queries.Users;
using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.ViewModels.Repositories;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;
using FluentHub.Uwp.Utils;

namespace FluentHub.Uwp.ViewModels.Users
{
    public class OverviewViewModel : ObservableObject
    {
        public OverviewViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _logger = logger;
            _messenger = messenger;
            _repositoryItems = new();
            RepositoryItems = new(_repositoryItems);

            RefreshRepositoryCommand = new AsyncRelayCommand<string>(LoadUserOverviewAsync);
        }

        #region Fields and Properties
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;

        private readonly ObservableCollection<RepoButtonBlockViewModel> _repositoryItems;
        public ReadOnlyObservableCollection<RepoButtonBlockViewModel> RepositoryItems { get; }

        private string _loginName;
        public string LoginName { get => _loginName; set => SetProperty(ref _loginName, value); }

        private RepoContextViewModel _contextViewModel;
        public RepoContextViewModel ContextViewModel { get => _contextViewModel; set => SetProperty(ref _contextViewModel, value); }

        public IAsyncRelayCommand RefreshRepositoryCommand { get; }
        #endregion

        private async Task LoadUserOverviewAsync(string login)
        {
            try
            {
                // For user readme
                ContextViewModel = new RepoContextViewModel()
                {
                    Repository = new()
                    {
                        Owner = new RepositoryOwner()
                        {
                            Login = LoginName,
                        },
                        Name = LoginName,
                    }
                };

                PinnedItemQueries queries = new();
                var items = await queries.GetAllAsync(login);
                if (items == null) return;

                _repositoryItems.Clear();
                foreach (var item in items)
                {
                    RepoButtonBlockViewModel viewModel = new()
                    {
                        Repository = item,
                        DisplayDetails = false,
                        DisplayStarButton = false,
                    };

                    _repositoryItems.Add(viewModel);
                }
            }
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadUserOverviewAsync), ex);
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
