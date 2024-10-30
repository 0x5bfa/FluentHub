using FluentHub.Octokit.Queries.Repositories;

namespace FluentHub.App.ViewModels.Repositories.Discussions
{
	public class DiscussionViewModel : BaseViewModel
	{
		private Discussion _discussion;
		public Discussion Discussion { get => _discussion; set => SetProperty(ref _discussion, value); }

		public IAsyncRelayCommand LoadRepositoryDiscussionPageCommand { get; }

		public DiscussionViewModel() : base()
		{
			LoadRepositoryDiscussionPageCommand = new AsyncRelayCommand(LoadRepositoryDiscussionPageAsync);
		}

		private async Task LoadRepositoryDiscussionPageAsync()
		{
			SetTabInformation("Discussion", "Discussion", "Discussions");
			SetLoadingProgress(true);
			InitializeNodePagingInfo();

			_currentTaskingMethodName = nameof(LoadRepositoryDiscussionPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadRepositoryAsync);
				await LoadRepositoryAsync(Login, Name);

				_currentTaskingMethodName = nameof(LoadRepositoryOneDiscussionAsync);
				await LoadRepositoryOneDiscussionAsync(Login, Name);

				SetTabInformation($"{Discussion.Title}", $"{Discussion.Title}");
			}
			catch (Exception ex)
			{
				TaskException = ex;
				IsTaskFaulted = true;

				_logger?.Error(_currentTaskingMethodName, ex);
			}
			finally
			{
				SetLoadingProgress(false);
			}
		}

		private async Task LoadRepositoryOneDiscussionAsync(string owner, string name)
		{
			DiscussionQueries queries = new();
			var response = await queries.GetAsync(owner, name, Number);

			if (response == null)
				return;

			Discussion = response;
		}

		private async Task LoadRepositoryAsync(string owner, string name)
		{
			RepositoryQueries queries = new();
			Repository = await queries.GetDetailsAsync(owner, name);
		}
	}
}
