using FluentHub.Octokit.Queries.Repositories;
using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Services;
using FluentHub.ViewModels.UserControls;
using FluentHub.ViewModels.UserControls.Overview;
using FluentHub.Utils;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using FluentHub.Octokit.Models.v4;

namespace FluentHub.ViewModels.Repositories.Commits
{
	public class CommitViewModel : BaseViewModel
	{
		private Commit _commitItem = default!;
		public Commit CommitItem { get => _commitItem; set => SetProperty(ref _commitItem, value); }

		private readonly ObservableCollection<DiffBlockViewModel> _diffViewModels;
		public ReadOnlyObservableCollection<DiffBlockViewModel> DiffViewModels { get; }

		public IAsyncRelayCommand LoadRepositoryCommitPageCommand { get; }

		public CommitViewModel(IFluentHubGitHubClient gitHub) : base(gitHub)
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
			var queries = _gitHub.Repositories.Diffs;
			var response = await queries.GetCommitAsync(owner, name, CommitItem.Oid);

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
			var queries = _gitHub.Repositories.Repositories;
			Repository = await queries.GetDetailsAsync(owner, name);
		}
	}
}
