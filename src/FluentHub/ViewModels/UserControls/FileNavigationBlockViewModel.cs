using FluentHub.Octokit.Queries.Repositories;
using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Utils;
using FluentHub.ViewModels.Repositories;

namespace FluentHub.ViewModels.UserControls
{
	public class FileNavigationBlockViewModel : ObservableObject
	{
		private readonly IFluentHubGitHubClient _gitHub;

		public FileNavigationBlockViewModel(IFluentHubGitHubClient gitHub, IMessenger? messenger = null, ILogger? logger = null)
		{
			_gitHub = gitHub;
			_messenger = messenger;
			_logger = logger;

			BranchNames = new();

			LoadBranchNameAllCommand = new AsyncRelayCommand(LoadRepositoryBranchsAsync);
		}

		#region Fields and Properties
		private readonly ILogger? _logger;
		private readonly IMessenger? _messenger;

		public ObservableCollection<string> BranchNames;

		private RepoContextViewModel contextViewModel = default!;
		public RepoContextViewModel ContextViewModel { get => contextViewModel; set => SetProperty(ref contextViewModel, value); }

		public IAsyncRelayCommand LoadBranchNameAllCommand { get; }
		#endregion

		public async Task LoadRepositoryBranchsAsync()
		{
			try
			{
				var queries = _gitHub.Repositories.Repositories;

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
