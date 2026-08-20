using FluentHub.Octokit.Queries.Repositories;
using FluentHub.App.Helpers;
using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.Utils;
using FluentHub.App.ViewModels.UserControls.Overview;
using FluentHub.App.ViewModels.UserControls.BlockButtons;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using FluentHub.Octokit.Models.v4;

namespace FluentHub.App.ViewModels.Repositories.Issues
{
	public class IssuesViewModel : BaseViewModel
	{
		private readonly ObservableCollection<IssueBlockButtonViewModel> _pinnedItems;
		public ReadOnlyObservableCollection<IssueBlockButtonViewModel> PinnedItems { get; }

		private readonly ObservableCollection<IssueBlockButtonViewModel> _issueItems;
		public ReadOnlyObservableCollection<IssueBlockButtonViewModel> IssueItems { get; }

		public IAsyncRelayCommand LoadRepositoryIssuesPageCommand { get; }
		public IAsyncRelayCommand LoadRepositoryIssuesFurtherCommand { get; }

		public IssuesViewModel(IFluentHubGitHubClient gitHub) : base(gitHub)
		{
			_issueItems = new();
			IssueItems = new(_issueItems);

			_pinnedItems = new();
			PinnedItems = new(_pinnedItems);

			LoadRepositoryIssuesPageCommand = new AsyncRelayCommand(LoadRepositoryIssuesPageAsync);
			LoadRepositoryIssuesFurtherCommand = new AsyncRelayCommand(LoadRepositoryIssuesFurtherAsync);
		}

		private async Task LoadRepositoryIssuesPageAsync()
		{
			SetTabInformation("Issues", "Issues", "Issues");
			SetLoadingProgress(true);
			InitializeNodePagingInfo();

			_currentTaskingMethodName = nameof(LoadRepositoryIssuesPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadRepositoryAsync);
				await LoadRepositoryAsync(Login, Name);

				_currentTaskingMethodName = nameof(LoadRepositoryIssuesAsync);
				await LoadRepositoryIssuesAsync(Login, Name);

				SetTabInformation($"Issues \u2022 {Login}/{Name}", $"Issues \u2022 {Login}/{Name}");

				if (IssueItems.Count == 0)
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

		private async Task LoadRepositoryIssuesAsync(string owner, string name)
		{
			var queries = _gitHub.Repositories.Issues;

			var result = await queries.GetPageAsync(owner, name, PageRequest.Forward(20));

			_lastPageInfo = result.PageInfo;
			var items = result.Items;

			_issueItems.Clear();
			foreach (var item in items)
			{
				IssueBlockButtonViewModel viewModel = new()
				{
					IssueItem = item,
				};

				_issueItems.Add(viewModel);
			}

			var pinnedIssues = await queries.GetPinnedAllAsync(owner, name);
			if (pinnedIssues == null)
				return;

			_pinnedItems.Clear();
			foreach (var item in pinnedIssues)
			{
				IssueBlockButtonViewModel viewModel = new()
				{
					IssueItem = item,
				};

				_pinnedItems.Add(viewModel);
			}
		}

		private async Task LoadRepositoryIssuesFurtherAsync()
		{
			if (!_lastPageInfo.HasNextPage)
				return;

			SetLoadingProgress(true);

			try
			{
				var queries = _gitHub.Repositories.Issues;

				var result = await queries.GetPageAsync(
					Login,
					Name,
					PageRequest.Forward(20, _lastPageInfo.EndCursor));

				_lastPageInfo = result.PageInfo;
				var items = result.Items;

				foreach (var item in items)
				{
					IssueBlockButtonViewModel viewModel = new()
					{
						IssueItem = item,
					};

					_issueItems.Add(viewModel);
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
