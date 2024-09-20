using FluentHub.App.Models;
using FluentHub.App.Utils;
using FluentHub.App.ViewModels.Repositories;
using FluentHub.Octokit.Queries.Repositories;

namespace FluentHub.App.ViewModels.UserControls
{
	public class LatestCommitBlockViewModel : ObservableObject
	{
		public LatestCommitBlockViewModel(IMessenger messenger = null, ILogger logger = null)
		{
			_messenger = messenger;
			_logger = logger;

			LoadLatestCommitBlockCommand = new AsyncRelayCommand(LoadRepositoryLatestCommitAsync);
		}

		#region Fields and Properties
		private readonly ILogger _logger;
		private readonly IMessenger _messenger;

		private RepoContextViewModel _contextViewModel;
		public RepoContextViewModel ContextViewModel { get => _contextViewModel; set => SetProperty(ref _contextViewModel, value); }

		private Commit _latestCommit;
		public Commit LatestCommit { get => _latestCommit; set => SetProperty(ref _latestCommit, value); }

		private int _totalCommitCount;
		public int TotalCommitCount { get => _totalCommitCount; set => SetProperty(ref _totalCommitCount, value); }

		public IAsyncRelayCommand LoadLatestCommitBlockCommand { get; }
		#endregion

		public async Task LoadRepositoryLatestCommitAsync()
		{
			try
			{
				CommitQueries queries = new();
				var response = await queries.GetLatestAsync(
					ContextViewModel.Repository.Name,
					ContextViewModel.Repository.Owner.Login,
					ContextViewModel.BranchName,
					ContextViewModel.Path);

				TotalCommitCount = response.History.TotalCount;
				LatestCommit = response.History.Nodes.FirstOrDefault();
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
