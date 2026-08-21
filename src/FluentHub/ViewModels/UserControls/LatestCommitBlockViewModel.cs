using FluentHub.Octokit.Queries.Repositories;
using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Utils;
using FluentHub.ViewModels.Repositories;
using FluentHub.Octokit.Contracts;

namespace FluentHub.ViewModels.UserControls
{
	public class LatestCommitBlockViewModel : ObservableObject
	{
		private readonly IFluentHubGitHubClient _gitHub;

		public LatestCommitBlockViewModel(IFluentHubGitHubClient gitHub, IMessenger? messenger = null, ILogger? logger = null)
		{
			_gitHub = gitHub;
			_messenger = messenger;
			_logger = logger;

			LoadLatestCommitBlockCommand = new AsyncRelayCommand(LoadRepositoryLatestCommitAsync);
		}

		#region Fields and Properties
		private readonly ILogger? _logger;
		private readonly IMessenger? _messenger;

		private RepoContextViewModel _contextViewModel = default!;
		public RepoContextViewModel ContextViewModel { get => _contextViewModel; set => SetProperty(ref _contextViewModel, value); }

		private Commit _latestCommit = default!;
		public Commit LatestCommit { get => _latestCommit; set => SetProperty(ref _latestCommit, value); }

		private int _totalCommitCount;
		public int TotalCommitCount { get => _totalCommitCount; set => SetProperty(ref _totalCommitCount, value); }

		public IAsyncRelayCommand LoadLatestCommitBlockCommand { get; }
		#endregion

		public async Task LoadRepositoryLatestCommitAsync()
		{
			try
			{
				var queries = _gitHub.Repositories.Commits;
				var response = await queries.GetLatestAsync(
					ContextViewModel.Repository.Name,
					ContextViewModel.Repository.Owner.Login,
					ContextViewModel.BranchName,
					ContextViewModel.Path);

				TotalCommitCount = response.History?.TotalCount ?? 0;
				LatestCommit = response.History?.Nodes?.FirstOrDefault() ?? default!;
			}
			catch (Exception ex)
			{
				_logger?.Error(nameof(LoadRepositoryLatestCommitAsync), ex);
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
