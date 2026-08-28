// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Infrastructure.GitHub.Queries.Users;
using FluentHub.ViewModels.Repositories;
using FluentHub.ViewModels.Controls.BlockButtons;

namespace FluentHub.ViewModels.Users
{
	public class OverviewViewModel : BaseViewModel
	{
		private readonly ObservableCollection<RepoBlockButtonViewModel> _pinnedRepositories;
		public ReadOnlyObservableCollection<RepoBlockButtonViewModel> PinnedRepositories { get; }

		private readonly ObservableCollection<RepoBlockButtonViewModel> _pinnableRepositories;
		public ReadOnlyObservableCollection<RepoBlockButtonViewModel> PinnableRepositories { get; }

		private RepoContextViewModel _contextViewModel = default!;
		public RepoContextViewModel ContextViewModel { get => _contextViewModel; set => SetProperty(ref _contextViewModel, value); }

		private string? _profileReadmeBaseUrl;
		public string? ProfileReadmeBaseUrl { get => _profileReadmeBaseUrl; set => SetProperty(ref _profileReadmeBaseUrl, value); }

		private string _profileReadmeMarkdown = string.Empty;
		public string ProfileReadmeMarkdown { get => _profileReadmeMarkdown; set => SetProperty(ref _profileReadmeMarkdown, value); }

		public IAsyncRelayCommand LoadUserOverviewCommand { get; }
		public IAsyncRelayCommand ShowPinnedRepositoriesEditorDialogCommand { get; }

		public OverviewViewModel(IFluentHubGitHubClient gitHub, ScreenViewModelDependencies dependencies) : base(gitHub, dependencies)
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
				await Task.WhenAll(
					LoadUserAsync(Login),
					LoadUserPinnableAndPinnedRepositoriesAsync(Login),
					LoadProfileReadmeAsync(Login));

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

		private async Task LoadProfileReadmeAsync(string login)
		{
			ProfileReadmeBaseUrl = null;
			ProfileReadmeMarkdown = string.Empty;

			var markdown = await _gitHub.Users.Users.GetProfileReadmeMarkdownAsync(login);
			if (string.IsNullOrWhiteSpace(markdown))
				return;

			ProfileReadmeBaseUrl = $"https://raw.githubusercontent.com/{login}/{login}/HEAD/";
			ProfileReadmeMarkdown = markdown;
		}

		private async Task LoadUserPinnableAndPinnedRepositoriesAsync(string login)
		{
			_pinnableRepositories.Clear();
			_pinnedRepositories.Clear();

			var queries = _gitHub.Users.PinnedItems;
			var pinnedItemsRes = await queries.GetAllAsync(login);
			if (pinnedItemsRes == null) return;

			if (pinnedItemsRes.Count == 0)
			{
				var pinnableItemsRes = await queries.GetAllPinnableItemsAsync(login);
				if (pinnableItemsRes == null) return;

				foreach (var item in pinnableItemsRes)
				{
					RepoBlockButtonViewModel viewModel = new(_gitHub)
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
					RepoBlockButtonViewModel viewModel = new(_gitHub)
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
			var dialogs = new global::FluentHub.Views.Dialogs.EditPinnedRepositoriesDialog(Login);
			_ = await dialogs.ShowAsync();
		}
	}
}
