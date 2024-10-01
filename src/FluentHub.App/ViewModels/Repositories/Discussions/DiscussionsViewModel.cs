using FluentHub.App.ViewModels.UserControls.BlockButtons;
using FluentHub.Octokit.Queries.Repositories;

namespace FluentHub.App.ViewModels.Repositories.Discussions
{
	public class DiscussionsViewModel : BaseViewModel
	{
		private readonly ObservableCollection<DiscussionBlockButtonViewModel> _items;
		public ReadOnlyObservableCollection<DiscussionBlockButtonViewModel> Items { get; }

		public IAsyncRelayCommand LoadRepositoryDiscussionsPageCommand { get; }
		public IAsyncRelayCommand LoadRepositoryDiscussionsFurtherCommand { get; }

		public DiscussionsViewModel() : base()
		{
			_items = new();
			Items = new(_items);

			LoadRepositoryDiscussionsPageCommand = new AsyncRelayCommand(LoadRepositoryDiscussionsPageAsync);
			LoadRepositoryDiscussionsFurtherCommand = new AsyncRelayCommand(LoadRepositoryDiscussionsFurtherAsync);
		}

		private async Task LoadRepositoryDiscussionsPageAsync()
		{
			SetTabInformation("Discussions", "Discussions", "Discussions");
			SetLoadingProgress(true);
			InitializeNodePagingInfo();

			_currentTaskingMethodName = nameof(LoadRepositoryDiscussionsPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadRepositoryAsync);
				await LoadRepositoryAsync(Login, Name);

				_currentTaskingMethodName = nameof(LoadRepositoryDiscussionsAsync);
				await LoadRepositoryDiscussionsAsync(Login, Name);

				SetTabInformation($"Discussions \u2022 {Login}/{Name}", $"Discussions \u2022 {Login}/{Name}");

				if (Items.Count == 0)
					IsEmpty = true;
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

		private async Task LoadRepositoryDiscussionsAsync(string owner, string name)
		{
			DiscussionQueries queries = new();

			var result = await queries.GetAllAsync(
				owner: owner,
				name: name,
				first: 20);
			if (result.Response is null || result.PageInfo is null)
				return;

			_lastPageInfo = result.PageInfo;
			var items = (List<Discussion>)result.Response;

			_items.Clear();
			foreach (var item in items)
			{
				DiscussionBlockButtonViewModel viewModel = new()
				{
					Item = item,
				};

				_items.Add(viewModel);
			}
		}

		private async Task LoadRepositoryDiscussionsFurtherAsync()
		{
			if (!_lastPageInfo.HasNextPage)
				return;

			SetLoadingProgress(true);

			try
			{
				DiscussionQueries queries = new();

				var result = await queries.GetAllAsync(
					owner: Login,
					name: Name,
					first: 20,
					after: _lastPageInfo.EndCursor);
				if (result.Response is null || result.PageInfo is null)
					return;

				_lastPageInfo = result.PageInfo;
				var items = (List<Discussion>)result.Response;

				foreach (var item in items)
				{
					DiscussionBlockButtonViewModel viewModel = new()
					{
						Item = item,
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
			RepositoryQueries queries = new();
			Repository = await queries.GetDetailsAsync(owner, name);
		}
	}
}
