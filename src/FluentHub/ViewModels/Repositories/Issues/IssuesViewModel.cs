using FluentHub.Core.Queries.Repositories;
using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Services;
using FluentHub.Utils;
using FluentHub.ViewModels.UserControls.Overview;
using FluentHub.ViewModels.UserControls.BlockButtons;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using FluentHub.Core.Contracts;
using FluentHub.Core.Mutations;

namespace FluentHub.ViewModels.Repositories.Issues
{
	public class IssuesViewModel : BaseViewModel
	{
		private readonly ObservableCollection<IssueBlockButtonViewModel> _pinnedItems;
		public ReadOnlyObservableCollection<IssueBlockButtonViewModel> PinnedItems { get; }

		private readonly ObservableCollection<IssueBlockButtonViewModel> _issueItems;
		public ReadOnlyObservableCollection<IssueBlockButtonViewModel> IssueItems { get; }

		public IAsyncRelayCommand LoadRepositoryIssuesPageCommand { get; }
		public IAsyncRelayCommand LoadRepositoryIssuesFurtherCommand { get; }

		private bool _isIssueMutationRunning;
		public bool IsIssueMutationRunning
		{
			get => _isIssueMutationRunning;
			private set
			{
				if (SetProperty(ref _isIssueMutationRunning, value))
					OnPropertyChanged(nameof(CanCreateIssue));
			}
		}

		public bool CanCreateIssue
			=> !IsIssueMutationRunning
			&& Repository is not null
			&& Repository.HasIssuesEnabled
			&& !Repository.IsArchived;

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
			OnPropertyChanged(nameof(CanCreateIssue));
		}

		public async Task CreateIssueAsync(string title, string body)
		{
			if (!CanCreateIssue || string.IsNullOrWhiteSpace(title))
				return;

			IsIssueMutationRunning = true;

			try
			{
				var response = await _gitHub.Mutations.Issues.CreateIssueAsync(new CreateIssueRequest
				{
					RepositoryId = Repository.Id,
					Title = title.Trim(),
					Body = body,
				});

				var issue = response.Issue
					?? throw new InvalidOperationException("The create issue mutation did not return an issue.");

				issue.Repository = Repository;
				issue.Comments = new IssueCommentConnection();
				issue.Labels = new LabelConnection { Nodes = [] };

				_issueItems.Insert(0, new IssueBlockButtonViewModel { IssueItem = issue });
				IsEmpty = false;
			}
			catch (Exception ex)
			{
				_logger?.Error(nameof(CreateIssueAsync), ex);
				_messenger?.Send(new UserNotificationMessage("Something went wrong", ex.Message, UserNotificationType.Error));
			}
			finally
			{
				IsIssueMutationRunning = false;
			}
		}
	}
}
