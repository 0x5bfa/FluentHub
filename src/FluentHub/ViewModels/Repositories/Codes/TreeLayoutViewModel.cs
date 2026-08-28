using FluentHub.Core.Infrastructure.GitHub.Queries.Repositories;
using FluentHub.Models;
using FluentHub.Utils;
using FluentHub.ViewModels.Controls;
using FluentHub.ViewModels.Controls.Overview;

namespace FluentHub.ViewModels.Repositories.Codes
{
	public class TreeLayoutViewModel : BaseViewModel
	{
		private bool _blobSelected;
		public bool BlobSelected { get => _blobSelected; set => SetProperty(ref _blobSelected, value); }

		private RepoContextViewModel _contextViewModel = default!;
		public RepoContextViewModel ContextViewModel { get => _contextViewModel; set => SetProperty(ref _contextViewModel, value); }

		private RepoContextViewModel _selectedContextViewModel = default!;
		public RepoContextViewModel SelectedContextViewModel { get => _selectedContextViewModel; set => SetProperty(ref _selectedContextViewModel, value); }

		private readonly ObservableCollection<TreeLayoutPageModel> _items;
		public ReadOnlyObservableCollection<TreeLayoutPageModel> Items { get; }

		public IAsyncRelayCommand LoadTreeViewContentsCommand { get; }
		public IAsyncRelayCommand LoadRepositoryCommand { get; }

		public TreeLayoutViewModel(IFluentHubGitHubClient gitHub, ScreenViewModelDependencies dependencies) : base(gitHub, dependencies)
		{
			_items = new();
			Items = new(_items);

			LoadTreeViewContentsCommand = new AsyncRelayCommand(LoadRepositoryContentsAsync);
			LoadRepositoryCommand = new AsyncRelayCommand<string>(LoadRepositoryAsync);
		}

		public override async Task ActivateAsync(AppRoute route, CancellationToken cancellationToken)
		{
			await base.ActivateAsync(route, cancellationToken);
			if (route is not RepositoryCodeRoute code)
				return;

			var queries = _gitHub.Repositories.Repositories;
			Repository = await queries.GetDetailsAsync(
				code.Repository.Owner,
				code.Repository.Name,
				cancellationToken);
			ContextViewModel = new RepoContextViewModel
			{
				Repository = Repository,
				BranchName = code.GitRef ?? Repository.DefaultBranchRef?.Name ?? string.Empty,
				Path = code.Path ?? string.Empty,
				IsFile = code.Target == RepositoryCodeTarget.File,
				IsRootDir = string.IsNullOrEmpty(code.Path),
				IsSubDir = !string.IsNullOrEmpty(code.Path) && code.Target == RepositoryCodeTarget.Directory,
			};
		}

		private async Task LoadRepositoryContentsAsync(CancellationToken token)
		{
			SetTabInformation("Repositories", "Repositories", "Repositories");
			SetLoadingProgress(true);

			try
			{
				if (string.IsNullOrEmpty(ContextViewModel.Repository.DefaultBranchRef?.Name))
					return;

				var queries = _gitHub.Repositories.Trees;
				var response = await queries.GetAllAsync(
					ContextViewModel.Repository.Name,
					ContextViewModel.Repository.Owner.Login,
					ContextViewModel.BranchName,
					ContextViewModel.Path);

				foreach (var item in response)
				{
					TreeLayoutPageModel model = new()
					{
						Name = item.Name,
						Path = item.Path ?? string.Empty,
						Tag = item.Type,
						IsBolb = false,
					};

					if (item.Type == "tree")
					{
						model.Glyph = "\uE9A0";
					}
					else
					{
						model.Glyph = "\uE996";
						model.IsBolb = true;
					}

					_items.Add(model);
				}

				var orderedItems =
					new ObservableCollection<TreeLayoutPageModel>
					(Items.OrderByDescending(x => x.Glyph));

				_items.Clear();
				foreach (var item in orderedItems)
					_items.Add(item);

				SetTabInformation("Repositories", "Repositories", "Repositories");
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

		public async Task<List<TreeLayoutPageModel>> LoadSubItemsAsync(string path)
		{
			try
			{
				var pathItems = path.Split("/");
				List<TreeLayoutPageModel> subItems = new();

				if (string.IsNullOrEmpty(ContextViewModel.Repository.DefaultBranchRef?.Name))
					return [];

				var queries = _gitHub.Repositories.Trees;
				var objects = await queries.GetAllAsync(
					ContextViewModel.Repository.Name,
					ContextViewModel.Repository.Owner.Login,
					ContextViewModel.BranchName,
					path);

				foreach (var obj in objects)
				{
					TreeLayoutPageModel model = new()
					{
						Name = obj.Name,
						Path = obj.Path ?? string.Empty,
						Tag = obj.Type,
						IsBolb = false,
					};

					if (obj.Type == "tree")
					{
						model.Glyph = "\uE9A0";
					}
					else
					{
						model.Glyph = "\uE996";
						model.IsBolb = true;
					}

					subItems.Add(model);
				}

				var orderedItems =
					new List<TreeLayoutPageModel>
					(subItems.OrderByDescending(x => x.Glyph));

				subItems.Clear();
				foreach (var item in orderedItems) subItems.Add(item);

				return subItems;
			}
			catch (OperationCanceledException)
			{
				return [];
			}
			catch (Exception ex)
			{
				_logger?.Error(nameof(LoadSubItemsAsync), ex);
				if (_messenger != null)
				{
					UserNotificationMessage notification = new("Something went wrong", ex.Message, UserNotificationType.Error);
					_messenger.Send(notification);
				}
				throw;
			}
		}

		private async Task LoadRepositoryAsync(string? url, CancellationToken token)
		{
			if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
				return;

			var pathSegments = uri.AbsolutePath.Split("/").ToList();
			pathSegments.RemoveAt(0);
			if (pathSegments.Count < 2)
				return;

			var queries = _gitHub.Repositories.Repositories;
			Repository = await queries.GetDetailsAsync(pathSegments[0], pathSegments[1]);
		}
	}
}
