using FluentHub.Core.Infrastructure.GitHub.Queries.Repositories;
using FluentHub.Models;
using FluentHub.Utils;
using FluentHub.Shared.Controls.ViewModels.Overview;

namespace FluentHub.Features.Repositories.ViewModels.Codes
{
	public class DetailsLayoutViewModel : BaseViewModel
	{
		private RepoContextViewModel contextViewModel = default!;
		public RepoContextViewModel ContextViewModel { get => contextViewModel; set => SetProperty(ref contextViewModel, value); }

		private string _currentPath = default!;
		public string CurrentPath { get => _currentPath; set => SetProperty(ref _currentPath, value); }

		private int _branchesTotalCount;
		public int BranchesTotalCount { get => _branchesTotalCount; set => SetProperty(ref _branchesTotalCount, value); }

		private int _tagsTotalCount;
		public int TagsTotalCount { get => _tagsTotalCount; set => SetProperty(ref _tagsTotalCount, value); }

		private readonly ObservableCollection<DetailsLayoutListViewModel> _items;
		public ReadOnlyObservableCollection<DetailsLayoutListViewModel> Items { get; }
		private bool _isForking;

		public IAsyncRelayCommand LoadDetailsViewPageCommand { get; }
		public IAsyncRelayCommand ToggleStarCommand { get; }

		public bool ViewerHasStarred
			=> Repository?.ViewerHasStarred ?? false;

		public int StargazerCount
			=> Repository?.StargazerCount ?? 0;

		public int ForkCount
			=> Repository?.ForkCount ?? 0;

		public bool CanFork
			=> !_isForking && (Repository?.ForkingAllowed ?? false);

		public DetailsLayoutViewModel(IFluentHubGitHubClient gitHub, ScreenViewModelDependencies dependencies) : base(gitHub, dependencies)
		{
			CurrentPath = CurrentRoute is RepositoryCodeRoute code
				? code.Path ?? string.Empty
				: string.Empty;

			_items = new();
			Items = new(_items);

			LoadDetailsViewPageCommand = new AsyncRelayCommand(LoadDetailsViewPageAsync);
			ToggleStarCommand = new AsyncRelayCommand(ToggleStarAsync, () => Repository is not null);
		}

		private async Task LoadDetailsViewPageAsync()
		{
			SetTabInformation("Repository", "Repository", "Repositories");
			SetLoadingProgress(true);
			InitializeNodePagingInfo();

			_currentTaskingMethodName = nameof(LoadDetailsViewPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadRepositoryAsync);
				await LoadRepositoryAsync(Login, Name);

				_currentTaskingMethodName = nameof(InitializeRepositoryContext);
				InitializeRepositoryContext(Login, Name, CurrentPath);

				_currentTaskingMethodName = nameof(LoadRepositoryContentsAsync);
				await LoadRepositoryContentsAsync(Login, Name, ContextViewModel.BranchName, ContextViewModel.Path);

				SetTabInformationPrimitive();
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

		private async Task LoadRepositoryContentsAsync(string login, string name, string branch, string path)
		{
			if (Repository.IsEmpty || ContextViewModel.IsFile)
				return;

			ContextViewModel.IsDir = true;

			var queries = _gitHub.Repositories.Trees;
			var response = await queries.GetWithObjectNameAsync(name, login, branch, path);

			if (string.IsNullOrEmpty(path))
				ContextViewModel.IsRootDir = true;
			else
				ContextViewModel.IsSubDir = true;

			var zippedResponse = response.Files.Zip(response.Commits, (file, commit) => new { File = file, Commit = commit });

			foreach (var item in zippedResponse)
			{
				DetailsLayoutListViewModel listItem = new()
				{
					Type = item.File.Type,
					Name = item.File.Name,
					LatestCommitMessage = item.Commit.Message.Split('\n', 2).FirstOrDefault() ?? string.Empty,
					UpdatedAt = item.Commit.CommittedDate,
					UpdatedAtHumanized = item.Commit.CommittedDateHumanized ?? string.Empty,
				};

				if (item.File.Type == "tree")
					listItem.IconGlyph = "\uE9A0";
				else
					listItem.IconGlyph = "\uE996";

				_items.Add(listItem);
			}

			var orderedByItemType =
				new ObservableCollection<DetailsLayoutListViewModel>(Items.OrderByDescending(x => x.IconGlyph));

			_items.Clear();
			foreach (var orderedItem in orderedByItemType)
				_items.Add(orderedItem);
		}

		private async Task LoadRepositoryAsync(string owner, string name)
		{
			var queries = _gitHub.Repositories.Repositories;
			var repositoryTask = queries.GetDetailsAsync(owner, name);
			var referenceCountsTask = queries.GetBranchAndTagCountAsync(owner, name);
			await Task.WhenAll(repositoryTask, referenceCountsTask);

			Repository = repositoryTask.Result;
			(BranchesTotalCount, TagsTotalCount) = referenceCountsTask.Result;
			NotifyRepositoryActionsChanged();
		}

		public async Task<IReadOnlyList<ForkOwner>> GetAvailableForkOwnersAsync()
		{
			try
			{
				return await _gitHub.Mutations.ForkRepository.GetAvailableOwnersAsync();
			}
			catch (Exception ex)
			{
				_logger.Error(nameof(GetAvailableForkOwnersAsync), ex);
				_messenger.Send(new UserNotificationMessage(
					"Could not load fork destinations",
					ex.Message,
					UserNotificationType.Error));
				return [];
			}
		}

		public async Task<CreateForkResult?> ForkRepositoryAsync(
			ForkOwner destinationOwner,
			string repositoryName,
			string? description,
			bool defaultBranchOnly)
		{
			_isForking = true;
			NotifyRepositoryActionsChanged();

			try
			{
				var fork = await _gitHub.Mutations.ForkRepository.ExecuteAsync(
					new CreateForkRequest
					{
						DefaultBranchOnly = defaultBranchOnly,
						Description = description,
						DestinationOwner = destinationOwner,
						RepositoryName = repositoryName,
						SourceName = Repository.Name,
						SourceOwner = Repository.Owner.Login,
					});

				Repository.ForkCount++;
				NotifyRepositoryActionsChanged();
				await InvalidateRepositoryCacheAsync();

				_messenger.Send(new UserNotificationMessage(
					"Repository forked",
					$"Created {fork.FullName}.",
					UserNotificationType.Success));
				return fork;
			}
			catch (Exception ex)
			{
				_logger.Error(nameof(ForkRepositoryAsync), ex);
				_messenger.Send(new UserNotificationMessage(
					"Could not fork repository",
					ex.Message,
					UserNotificationType.Error));
				return null;
			}
			finally
			{
				_isForking = false;
				NotifyRepositoryActionsChanged();
			}
		}

		private async Task ToggleStarAsync()
		{
			try
			{
				var wasStarred = Repository.ViewerHasStarred;
				if (wasStarred)
					await _gitHub.Mutations.RemoveStar.ExecuteAsync(Repository.Id);
				else
					await _gitHub.Mutations.AddStar.ExecuteAsync(Repository.Id);

				Repository.ViewerHasStarred = !wasStarred;
				Repository.StargazerCount = Math.Max(0, Repository.StargazerCount + (wasStarred ? -1 : 1));
				NotifyRepositoryActionsChanged();
				await InvalidateRepositoryCacheAsync();
			}
			catch (Exception ex)
			{
				_logger.Error(nameof(ToggleStarAsync), ex);
				_messenger.Send(new UserNotificationMessage(
					"Could not update star",
					ex.Message,
					UserNotificationType.Error));
			}
		}

		private async Task InvalidateRepositoryCacheAsync()
		{
			try
			{
				await _gitHub.Repositories.Repositories.InvalidateAsync(
					Repository.Owner.Login,
					Repository.Name);
			}
			catch (Exception ex)
			{
				_logger.Warn("Failed to invalidate repository cache: {0}", ex.Message);
			}
		}

		private void NotifyRepositoryActionsChanged()
		{
			OnPropertyChanged(nameof(ViewerHasStarred));
			OnPropertyChanged(nameof(StargazerCount));
			OnPropertyChanged(nameof(ForkCount));
			OnPropertyChanged(nameof(CanFork));
			ToggleStarCommand.NotifyCanExecuteChanged();
		}

		private void InitializeRepositoryContext(string owner, string name, string path)
		{
			var codeRoute = CurrentRoute as RepositoryCodeRoute;
			var actualPath = codeRoute?.Path ?? path;
			var branchName = codeRoute?.GitRef ?? Repository.DefaultBranchRef?.Name ?? string.Empty;
			var isRootDir = string.IsNullOrEmpty(actualPath);
			var isFile = codeRoute?.Target == RepositoryCodeTarget.File;
			var isDir = !isFile;
			var isSubDir = isDir && !isRootDir;

			ContextViewModel = new()
			{
				Repository = Repository,

				BranchName = branchName,
				IsDir = isDir,
				IsFile = isFile,
				IsSubDir = isSubDir,
				IsRootDir = isRootDir,
				Path = actualPath,
			};
		}

		private void SetTabInformationPrimitive()
		{
			string header;
			string description;

			if (ContextViewModel.IsRootDir)
			{
				if (string.IsNullOrEmpty(Repository.Description))
					header = $"{Repository.Owner.Login}/{Repository.Name}";
				else
					header = $"{Repository.Owner.Login}/{Repository.Name}: {Repository.Description}";
			}
			else
			{
				header = $"{Repository.Name}/{ContextViewModel.Path} at {ContextViewModel.BranchName} \u2022 {Repository.Owner.Login}/{Repository.Name}";
			}

			description = header;

			SetTabInformation(header, description);
		}
	}
}
