using FluentHub.App.ViewModels.UserControls;
using FluentHub.Octokit.Queries.Repositories;

namespace FluentHub.App.ViewModels.Repositories.Commits
{
	public class CommitViewModel : BaseViewModel
	{
		private Commit _commitItem;
		public Commit CommitItem { get => _commitItem; set => SetProperty(ref _commitItem, value); }

		private readonly ObservableCollection<DiffBlockViewModel> _diffViewModels;
		public ReadOnlyObservableCollection<DiffBlockViewModel> DiffViewModels { get; }

		public IAsyncRelayCommand LoadRepositoryCommitPageCommand { get; }

		public CommitViewModel() : base()
		{
			_diffViewModels = new();
			DiffViewModels = new(_diffViewModels);

			LoadRepositoryCommitPageCommand = new AsyncRelayCommand(LoadRepositoryCommitPageAsync);
		}

		private async Task LoadRepositoryCommitPageAsync()
		{
			SetTabInformation("Commit", "Commit", "Commits");
			SetLoadingProgress(true);
			InitializeNodePagingInfo();

			_currentTaskingMethodName = nameof(LoadRepositoryCommitPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadRepositoryAsync);
				await LoadRepositoryAsync(Login, Name);

				_currentTaskingMethodName = nameof(LoadRepositoryCommitAsync);
				await LoadRepositoryCommitAsync(Login, Name);

				SetTabInformation($"{CommitItem.Message}", $"{CommitItem.Message}");
			}
			catch (Exception ex)
			{
				TaskException = ex;
				IsTaskFaulted = true;
			}
			finally
			{
				SetLoadingProgress(false);
			}
		}

		private async Task LoadRepositoryCommitAsync(string owner, string name)
		{
			DiffQueries queries = new();
			var response = await queries.GetAllAsync(owner, name, CommitItem.Oid);

			_diffViewModels.Clear();
			foreach (var item in response.Files)
			{
				DiffBlockViewModel viewModel = new()
				{
					ChangedFile = item,
				};

				_diffViewModels.Add(viewModel);
			}
		}

		private async Task LoadRepositoryAsync(string owner, string name)
		{
			RepositoryQueries queries = new();
			Repository = await queries.GetDetailsAsync(owner, name);
		}
	}
}
