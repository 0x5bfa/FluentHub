using FluentHub.App.Models;
using FluentHub.Octokit.Queries.Repositories;

namespace FluentHub.App.ViewModels.Repositories.Codes
{
	public class DetailsLayoutViewModel : BaseViewModel
	{
		private RepoContextViewModel contextViewModel;
		public RepoContextViewModel ContextViewModel { get => contextViewModel; set => SetProperty(ref contextViewModel, value); }

		private string _currentPath;
		public string CurrentPath { get => _currentPath; set => SetProperty(ref _currentPath, value); }

		private int _branchesTotalCount;
		public int BranchesTotalCount { get => _branchesTotalCount; set => SetProperty(ref _branchesTotalCount, value); }

		private int _tagsTotalCount;
		public int TagsTotalCount { get => _tagsTotalCount; set => SetProperty(ref _tagsTotalCount, value); }

		public static int StaticBranchesTotalCount;
		public static int StaticTagsTotalCount;

		private readonly ObservableCollection<DetailsLayoutListViewModel> _items;
		public ReadOnlyObservableCollection<DetailsLayoutListViewModel> Items { get; }

		public IAsyncRelayCommand LoadDetailsViewPageCommand { get; }

		public DetailsLayoutViewModel() : base()
		{
			var parameter = _navigation.TabView.SelectedItem.NavigationBar.Context;
			Login = parameter.PrimaryText;
			Name = parameter.SecondaryText;

			if (parameter.Parameters is not null)
				CurrentPath = parameter.Parameters as string;

			_items = new();
			Items = new(_items);

			LoadDetailsViewPageCommand = new AsyncRelayCommand(LoadDetailsViewPageAsync);
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

			TreeQueries queries = new();
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
					LatestCommitMessage = item.Commit.Message.Split('\n', 2).FirstOrDefault(),
					UpdatedAt = item.Commit.CommittedDate,
					UpdatedAtHumanized = item.Commit.CommittedDateHumanized,
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
			RepositoryQueries queries = new();
			Repository = await queries.GetDetailsAsync(owner, name);
		}

		private void InitializeRepositoryContext(string owner, string name, string path)
		{
			bool isRootDir = false;
			bool isFile = false;
			bool isSubDir = false;
			bool isDir = false;
			string actualPath = path;
			var pathItems = path?.Split("/").ToList();
			string branchName = "";

			// owner/name
			if (pathItems == null || pathItems.Count == 0)
			{
				isDir = isRootDir = true;
				branchName = Repository.DefaultBranchRef?.Name;
			}
			// owner/name/tree/main
			else if (pathItems.Count == 2)
			{
				isDir = isRootDir = true;
				branchName = pathItems.ElementAt(1);

				pathItems.RemoveRange(0, 2);
				actualPath = string.Join("/", pathItems);
			}
			// owner/name/(tree|blob)/main/path
			else if (pathItems.Count > 2)
			{
				isRootDir = false;
				branchName = pathItems.ElementAt(1);

				isFile = pathItems.ElementAt(0).ToLower() == "blob" ? true : false;
				isSubDir = isDir = pathItems.ElementAt(0).ToLower() == "tree";

				pathItems.RemoveRange(0, 2);
				actualPath = string.Join("/", pathItems);
			}

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
