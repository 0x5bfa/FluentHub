using FluentHub.Octokit.Queries.Repositories;
using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Services;
using FluentHub.Utils;
using FluentHub.ViewModels.UserControls.Overview;
using FluentHub.ViewModels.UserControls.BlockButtons;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using FluentHub.Octokit.Models.v4;

namespace FluentHub.ViewModels.Repositories.Discussions
{
	public class DiscussionsViewModel : BaseViewModel
	{
		private readonly ObservableCollection<DiscussionBlockButtonViewModel> _items;
		public ReadOnlyObservableCollection<DiscussionBlockButtonViewModel> Items { get; }

		public IAsyncRelayCommand LoadRepositoryDiscussionsPageCommand { get; }
		public IAsyncRelayCommand LoadRepositoryDiscussionsFurtherCommand { get; }

		public DiscussionsViewModel(IFluentHubGitHubClient gitHub) : base(gitHub)
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
			var queries = _gitHub.Repositories.Discussions;

			var result = await queries.GetPageAsync(owner, name, PageRequest.Forward(20));

			_lastPageInfo = result.PageInfo;
			var items = result.Items;

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
				var queries = _gitHub.Repositories.Discussions;

				var result = await queries.GetPageAsync(
					Login,
					Name,
					PageRequest.Forward(20, _lastPageInfo.EndCursor));

				_lastPageInfo = result.PageInfo;
				var items = result.Items;

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
			var queries = _gitHub.Repositories.Repositories;
			Repository = await queries.GetDetailsAsync(owner, name);
		}
	}
}
