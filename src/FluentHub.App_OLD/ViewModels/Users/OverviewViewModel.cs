// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.ViewModels.Repositories;
using FluentHub.App.ViewModels.UserControls.BlockButtons;
using FluentHub.Octokit.Queries.Users;

namespace FluentHub.App.ViewModels.Users
{
	public class OverviewViewModel : BaseViewModel
	{
		private readonly ObservableCollection<RepoBlockButtonViewModel> _pinnedRepositories;
		public ReadOnlyObservableCollection<RepoBlockButtonViewModel> PinnedRepositories { get; }

		private readonly ObservableCollection<RepoBlockButtonViewModel> _pinnableRepositories;
		public ReadOnlyObservableCollection<RepoBlockButtonViewModel> PinnableRepositories { get; }

		private RepoContextViewModel _contextViewModel;
		public RepoContextViewModel ContextViewModel { get => _contextViewModel; set => SetProperty(ref _contextViewModel, value); }

		public IAsyncRelayCommand LoadUserOverviewCommand { get; }
		public IAsyncRelayCommand ShowPinnedRepositoriesEditorDialogCommand { get; }

		public OverviewViewModel() : base()
		{
			_pinnedRepositories = new();
			PinnedRepositories = new(_pinnedRepositories);

			_pinnableRepositories = new();
			PinnableRepositories = new(_pinnableRepositories);

			LoadUserOverviewCommand = new AsyncRelayCommand(LoadUserOverviewAsync);
			ShowPinnedRepositoriesEditorDialogCommand = new AsyncRelayCommand(ShowPinnedRepositoriesEditorDialogAsync);
		}

		private async Task LoadUserOverviewAsync()
		{
			SetTabInformation("Overview", "Overview", "Profile");
			SetLoadingProgress(true);
			InitializeNodePagingInfo();

			_currentTaskingMethodName = nameof(LoadUserOverviewAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadUserAsync);
				await LoadUserAsync(Login);

				_currentTaskingMethodName = nameof(LoadUserPinnableAndPinnedRepositoriesAsync);
				await LoadUserPinnableAndPinnedRepositoriesAsync(Login);

				SetTabInformation("Overview", "Overview");
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

		private async Task LoadUserPinnableAndPinnedRepositoriesAsync(string login)
		{
			_pinnableRepositories.Clear();
			_pinnedRepositories.Clear();

			PinnedItemQueries queries = new();
			var pinnedItemsRes = await queries.GetAllAsync(login);
			if (pinnedItemsRes == null) return;

			if (pinnedItemsRes.Count == 0)
			{
				var pinnableItemsRes = await queries.GetAllPinnableItems(login);
				if (pinnableItemsRes == null) return;

				foreach (var item in pinnableItemsRes)
				{
					RepoBlockButtonViewModel viewModel = new()
					{
						Repository = item,
						DisplayDetails = false,
						DisplayStarButton = false,
					};

					_pinnableRepositories.Add(viewModel);
				}
			}
			else
			{
				foreach (var item in pinnedItemsRes)
				{
					RepoBlockButtonViewModel viewModel = new()
					{
						Repository = item,
						DisplayDetails = false,
						DisplayStarButton = false,
					};

					_pinnedRepositories.Add(viewModel);
				}
			}
		}

		private async Task ShowPinnedRepositoriesEditorDialogAsync()
		{
			var dialogs = new global::FluentHub.App.Dialogs.EditPinnedRepositoriesDialog(Login);
			_ = await dialogs.ShowAsync();
		}
	}
}
