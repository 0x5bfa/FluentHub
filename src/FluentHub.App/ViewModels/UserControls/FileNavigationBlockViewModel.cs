using FluentHub.App.Models;
using FluentHub.App.Utils;
using FluentHub.App.ViewModels.Repositories;
using FluentHub.Octokit.Queries.Repositories;

namespace FluentHub.App.ViewModels.UserControls
{
	public class FileNavigationBlockViewModel : ObservableObject
	{
		public FileNavigationBlockViewModel(IMessenger messenger = null, ILogger logger = null)
		{
			_messenger = messenger;
			_logger = logger;

			BranchNames = new();

			LoadBranchNameAllCommand = new AsyncRelayCommand(LoadRepositoryBranchsAsync);
		}

		#region Fields and Properties
		private readonly ILogger _logger;
		private readonly IMessenger _messenger;

		public ObservableCollection<string> BranchNames;

		private RepoContextViewModel contextViewModel;
		public RepoContextViewModel ContextViewModel { get => contextViewModel; set => SetProperty(ref contextViewModel, value); }

		public IAsyncRelayCommand LoadBranchNameAllCommand { get; }
		#endregion

		public async Task LoadRepositoryBranchsAsync()
		{
			try
			{
				RepositoryQueries queries = new();

				// temp workaround
				var branchNames = await queries.GetBranchNameAllAsync(contextViewModel.Repository.Owner.Login, contextViewModel.Repository.Name);

				// Reorder
				var alphabetic = new ObservableCollection<string>(branchNames.OrderBy(x => x));
				branchNames.Clear();
				foreach (var orderedItem in alphabetic)
				{
					if (contextViewModel.BranchName == orderedItem)
					{
						branchNames.Insert(0, orderedItem);
					}
					else
					{
						branchNames.Add(orderedItem);
					}
				}

				foreach (var branch in branchNames) BranchNames.Add(branch);
			}
			catch (Exception ex)
			{
				_logger?.Error(nameof(LoadRepositoryBranchsAsync), ex);
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
