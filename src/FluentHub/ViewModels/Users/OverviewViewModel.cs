// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Application;
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

		private Uri? _profileReadmeEditUri;
		public Uri? ProfileReadmeEditUri { get => _profileReadmeEditUri; set => SetProperty(ref _profileReadmeEditUri, value); }

		private ContributionCalendar? _contributionCalendar;
		public ContributionCalendar? ContributionCalendar { get => _contributionCalendar; set => SetProperty(ref _contributionCalendar, value); }

		private ObservableCollection<object> _contributionCalendarItems = [];
		public ObservableCollection<object> ContributionCalendarItems { get => _contributionCalendarItems; set => SetProperty(ref _contributionCalendarItems, value); }

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
			ProfileReadmeEditUri = null;

			try
			{
				var profileReadmeTask = LoadProfileReadmeAsync(Login);
				await Task.WhenAll(
					LoadUserAsync(Login),
					LoadUserPinnableAndPinnedRepositoriesAsync(Login),
					profileReadmeTask,
					LoadContributionCalendarAsync(Login));

				var profileReadme = await profileReadmeTask;
				if (User.IsViewer && !string.IsNullOrWhiteSpace(profileReadme.DefaultBranchName))
				{
					var owner = Uri.EscapeDataString(profileReadme.OwnerLogin);
					var repository = Uri.EscapeDataString(profileReadme.RepositoryName);
					var branch = Uri.EscapeDataString(profileReadme.DefaultBranchName);
					ProfileReadmeEditUri = new Uri(
						$"https://github.com/{owner}/{repository}/edit/{branch}/README.md");
				}

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

		private async Task<ProfileReadme> LoadProfileReadmeAsync(string login)
		{
			ProfileReadmeBaseUrl = null;
			ProfileReadmeMarkdown = string.Empty;

			var profileReadme = await _gitHub.Users.Users.GetProfileReadmeAsync(login);
			if (string.IsNullOrWhiteSpace(profileReadme.Markdown))
				return profileReadme;

			var escapedOwner = Uri.EscapeDataString(profileReadme.OwnerLogin);
			var escapedRepository = Uri.EscapeDataString(profileReadme.RepositoryName);
			var escapedBranch = Uri.EscapeDataString(profileReadme.DefaultBranchName);
			ProfileReadmeBaseUrl =
				$"https://raw.githubusercontent.com/{escapedOwner}/{escapedRepository}/{escapedBranch}/";
			ProfileReadmeMarkdown = profileReadme.Markdown;

			return profileReadme;
		}

		private async Task LoadContributionCalendarAsync(string login)
		{
			ContributionCalendar = null;
			ContributionCalendarItems = [];

			try
			{
				var calendar = await _gitHub.Users.Activities.GetContributionCalendarAsync(login);
				var items = ContributionCalendarService.CreateItems(calendar);
				if (items.Count == 0)
					return;

				ContributionCalendarItems = new(items);
				ContributionCalendar = calendar;
			}
			catch (Exception ex)
			{
				_logger?.Error(nameof(LoadContributionCalendarAsync), ex);
			}
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
