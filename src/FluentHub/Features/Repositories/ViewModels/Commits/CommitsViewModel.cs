using FluentHub.Core.Infrastructure.GitHub.Queries.Repositories;
using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Services;
using FluentHub.Utils;
using FluentHub.Shared.Controls.ViewModels.Overview;
using FluentHub.Shared.Controls.ViewModels.BlockButtons;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using FluentHub.Core.Application.Models;

namespace FluentHub.Features.Repositories.ViewModels.Commits
{
	public class CommitsViewModel : BaseViewModel
	{
		private RepoContextViewModel contextViewModel = default!;
		public RepoContextViewModel ContextViewModel { get => contextViewModel; set => SetProperty(ref contextViewModel, value); }

		private readonly ObservableCollection<CommitBlockButtonViewModel> _items;
		public ReadOnlyObservableCollection<CommitBlockButtonViewModel> Items { get; }

		public IAsyncRelayCommand LoadRepositoryCommitsPageCommand { get; }
		public IAsyncRelayCommand LoadRepositoryCommitsFurtherCommand { get; }

		public CommitsViewModel(IFluentHubGitHubClient gitHub, ScreenViewModelDependencies dependencies) : base(gitHub, dependencies)
		{
			_items = new();
			Items = new(_items);

			LoadRepositoryCommitsPageCommand = new AsyncRelayCommand(LoadRepositoryCommitsPageAsync);
			LoadRepositoryCommitsFurtherCommand = new AsyncRelayCommand(LoadRepositoryCommitsFurtherAsync);
		}

		private async Task LoadRepositoryCommitsPageAsync()
		{
			SetTabInformation("Commits", "Commits", "Commits");
			SetLoadingProgress(true);
			InitializeNodePagingInfo();

			_currentTaskingMethodName = nameof(LoadRepositoryCommitsPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadRepositoryAsync);
				await LoadRepositoryAsync(Login, Name);

				_currentTaskingMethodName = nameof(LoadRepositoryCommitsAsync);
				await LoadRepositoryCommitsAsync(Login, Name);

				SetTabInformation($"Commits \u2022 {Login}/{Name}", $"Commits \u2022 {Login}/{Name}");
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

		private async Task LoadRepositoryCommitsAsync(string owner, string name)
		{
			var queries = _gitHub.Repositories.Commits;

			var result = await queries.GetPageAsync(
				owner,
				name,
				ContextViewModel.BranchName,
				PageRequest.Forward(20),
				path: ContextViewModel.Path);

			_lastPageInfo = result.PageInfo;
			var items = result.Items;

			_items.Clear();
			foreach (var item in items)
			{
				CommitBlockButtonViewModel viewModel = new()
				{
					CommitItem = item,
				};

				_items.Add(viewModel);
			}
		}

		private async Task LoadRepositoryCommitsFurtherAsync()
		{
			if (!_lastPageInfo.HasNextPage)
				return;

			SetLoadingProgress(true);

			try
			{
				var queries = _gitHub.Repositories.Commits;

				var result = await queries.GetPageAsync(
					Login,
					Name,
					ContextViewModel.BranchName,
					PageRequest.Forward(20, _lastPageInfo.EndCursor),
					path: ContextViewModel.Path);

				_lastPageInfo = result.PageInfo;
				var items = result.Items;

				foreach (var item in items)
				{
					CommitBlockButtonViewModel viewModel = new()
					{
						CommitItem = item,
					};

					_items.Add(viewModel);
				}
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

		private async Task LoadRepositoryAsync(string owner, string name)
		{
			var queries = _gitHub.Repositories.Repositories;
			Repository = await queries.GetDetailsAsync(owner, name);
		}
	}
}
