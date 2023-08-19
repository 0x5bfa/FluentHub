using FluentHub.Octokit.Queries.Repositories;
using FluentHub.App.Helpers;
using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.Utils;
using FluentHub.App.ViewModels.UserControls.Overview;
using FluentHub.App.ViewModels.UserControls.BlockButtons;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.App.ViewModels.Repositories.Commits
{
	public class CommitsViewModel : BaseViewModel
	{
		private RepoContextViewModel contextViewModel;
		public RepoContextViewModel ContextViewModel { get => contextViewModel; set => SetProperty(ref contextViewModel, value); }

		private readonly ObservableCollection<CommitBlockButtonViewModel> _items;
		public ReadOnlyObservableCollection<CommitBlockButtonViewModel> Items { get; }

		public IAsyncRelayCommand LoadRepositoryCommitsPageCommand { get; }

		public CommitsViewModel() : base()
		{
			_items = new();
			Items = new(_items);

			LoadRepositoryCommitsPageCommand = new AsyncRelayCommand(LoadRepositoryCommitsPageAsync);
		}

		private async Task LoadRepositoryCommitsPageAsync()
		{
			SetTabInformation("Commits", "Commits", "Commits");
			SetLoadingProgress(true);

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
			CommitQueries queries = new();
			var response = await queries.GetAllAsync(name, owner, ContextViewModel.BranchName, ContextViewModel.Path);

			_items.Clear();
			foreach (var item in response)
			{
				CommitBlockButtonViewModel viewModel = new()
				{
					CommitItem = item,
				};

				_items.Add(viewModel);
			}
		}

		private async Task LoadRepositoryAsync(string owner, string name)
		{
			RepositoryQueries queries = new();
			Repository = await queries.GetDetailsAsync(owner, name);
		}
	}
}
