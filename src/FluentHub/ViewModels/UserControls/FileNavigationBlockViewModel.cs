using FluentHub.Core.Queries.Repositories;
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
			TagNames = new();
		}

		#region Fields and Properties
		private readonly ILogger? _logger;
		private readonly IMessenger? _messenger;

		public ObservableCollection<string> BranchNames { get; }
		public ObservableCollection<string> TagNames { get; }

		private RepoContextViewModel contextViewModel = default!;
		public RepoContextViewModel ContextViewModel
		{
			get => contextViewModel;
			set
			{
				if (!SetProperty(ref contextViewModel, value))
					return;

				_loadReferencesTask = null;
				BranchNames.Clear();
				TagNames.Clear();
			}
		}

		private Task? _loadReferencesTask;
		#endregion

		public Task EnsureReferencesLoadedAsync()
		{
			if (_loadReferencesTask is null || _loadReferencesTask.IsFaulted || _loadReferencesTask.IsCanceled)
				_loadReferencesTask = LoadReferencesAsync();

			return _loadReferencesTask;
		}

		private async Task LoadReferencesAsync()
		{
			try
			{
				var queries = _gitHub.Repositories.Repositories;
				var references = await queries.GetBranchAndTagNamesAsync(
					contextViewModel.Repository.Owner.Login,
					contextViewModel.Repository.Name);

				BranchNames.Clear();
				foreach (var branch in references.Branches
					.OrderBy(branch => branch.Equals(contextViewModel.BranchName, StringComparison.Ordinal) ? 0 : 1)
					.ThenBy(branch => branch, StringComparer.OrdinalIgnoreCase))
				{
					BranchNames.Add(branch);
				}

				TagNames.Clear();
				foreach (var tag in references.Tags)
					TagNames.Add(tag);
			}
			catch (Exception ex)
			{
				_logger?.Error(nameof(LoadReferencesAsync), ex);
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
