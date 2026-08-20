using FluentHub.Octokit.Queries.Repositories;
using FluentHub.App.Helpers;
using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.Utils;
using FluentHub.App.ViewModels.UserControls.Overview;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using FluentHub.Octokit.Models.v4;
using FluentHub.Octokit.Mutations;

namespace FluentHub.App.ViewModels.Repositories.Issues
{
	public class IssueViewModel : BaseViewModel
	{
		private Issue _issueItem = default!;
		public Issue IssueItem
		{
			get => _issueItem;
			private set
			{
				if (SetProperty(ref _issueItem, value))
				{
					OnPropertyChanged(nameof(IssueStateButtonText));
					NotifyMutationCommandsCanExecuteChanged();
				}
			}
		}

		private string _commentBody = string.Empty;
		public string CommentBody
		{
			get => _commentBody;
			set
			{
				if (SetProperty(ref _commentBody, value))
					NotifyMutationCommandsCanExecuteChanged();
			}
		}

		private bool _isIssueMutationRunning;
		public bool IsIssueMutationRunning
		{
			get => _isIssueMutationRunning;
			private set
			{
				if (SetProperty(ref _isIssueMutationRunning, value))
					NotifyMutationCommandsCanExecuteChanged();
			}
		}

		public string IssueStateButtonText
			=> _issueItem?.Closed is true ? "Reopen issue" : "Close issue";

		private bool CanAddIssueComment
			=> !IsIssueMutationRunning
			&& _issueItem is not null
			&& !string.IsNullOrWhiteSpace(CommentBody);

		private bool CanToggleIssueState
			=> !IsIssueMutationRunning
			&& _issueItem is not null
			&& (_issueItem.Closed
				? _issueItem.ViewerCanReopen || _issueItem.ViewerCanUpdate
				: _issueItem.ViewerCanClose || _issueItem.ViewerCanUpdate);

		private readonly ObservableCollection<object> _timelineItems;
		public ReadOnlyObservableCollection<object> TimelineItems { get; set; }

		public IAsyncRelayCommand LoadRepositoryIssuePageCommand { get; }
		public IAsyncRelayCommand AddIssueCommentCommand { get; }
		public IAsyncRelayCommand ToggleIssueStateCommand { get; }

		public IssueViewModel(IFluentHubGitHubClient gitHub) : base(gitHub)
		{
			_timelineItems = new();
			TimelineItems = new(_timelineItems);

			LoadRepositoryIssuePageCommand = new AsyncRelayCommand(LoadRepositoryIssuePageAsync);
			AddIssueCommentCommand = new AsyncRelayCommand(AddIssueCommentAsync, () => CanAddIssueComment);
			ToggleIssueStateCommand = new AsyncRelayCommand(ToggleIssueStateAsync, () => CanToggleIssueState);
		}

		private async Task LoadRepositoryIssuePageAsync()
		{
			SetTabInformation("Issue", "Issue", "Issues");
			SetLoadingProgress(true);
			InitializeNodePagingInfo();

			_currentTaskingMethodName = nameof(LoadRepositoryIssuePageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadRepositoryAsync);
				await LoadRepositoryAsync(Login, Name);

				_currentTaskingMethodName = nameof(LoadRepositoryOneIssueAsync);
				await LoadRepositoryOneIssueAsync(Login, Name);

				SetTabInformation($"{IssueItem.Title}", $"{IssueItem.Title}");
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

		private async Task LoadRepositoryOneIssueAsync(string owner, string name)
		{
			var issueQueries = _gitHub.Repositories.Issues;
			var queries = _gitHub.Repositories.IssueEvents;
			_timelineItems.Clear();

			IssueItem = await issueQueries.GetAsync(owner, name, Number);

			var bodyComment = await issueQueries.GetBodyAsync(owner, name, Number);
			_timelineItems.Add(bodyComment);

			var issueEvents = await queries.GetAllAsync(owner, name, Number);
			foreach (var item in issueEvents)
				_timelineItems.Add(item);
		}

		private async Task LoadRepositoryAsync(string owner, string name)
		{
			var queries = _gitHub.Repositories.Repositories;
			Repository = await queries.GetDetailsAsync(owner, name);
		}

		private async Task AddIssueCommentAsync()
		{
			if (!CanAddIssueComment)
				return;

			IsIssueMutationRunning = true;

			try
			{
				var mutations = _gitHub.Mutations.Issues;
				var response = await mutations.AddCommentAsync(new AddCommentInput
				{
					SubjectId = IssueItem.Id,
					Body = CommentBody,
				});

				var issueComment = response.CommentEdge?.Node
					?? throw new InvalidOperationException("The add comment mutation did not return a comment.");

				issueComment.Reactions ??= new ReactionConnection
				{
					Nodes = [],
				};

				_timelineItems.Add(issueComment);
				CommentBody = string.Empty;

				IssueItem.Comments ??= new IssueCommentConnection();
				IssueItem.Comments.TotalCount++;
				OnPropertyChanged(nameof(IssueItem));
			}
			catch (Exception ex)
			{
				NotifyMutationFailed(nameof(AddIssueCommentAsync), ex);
			}
			finally
			{
				IsIssueMutationRunning = false;
			}
		}

		private async Task ToggleIssueStateAsync()
		{
			if (!CanToggleIssueState)
				return;

			IsIssueMutationRunning = true;

			try
			{
				var mutations = _gitHub.Mutations.Issues;
				Issue? issue;

				if (IssueItem.Closed)
				{
					var response = await mutations.ReopenIssueAsync(new ReopenIssueInput
					{
						IssueId = IssueItem.Id,
					});

					issue = response.Issue;
				}
				else
				{
					var response = await mutations.CloseIssueAsync(new CloseIssueInput
					{
						IssueId = IssueItem.Id,
						StateReason = IssueClosedStateReason.Completed,
					});

					issue = response.Issue;
				}

				ApplyIssueState(issue ?? throw new InvalidOperationException("The issue mutation did not return an issue."));
			}
			catch (Exception ex)
			{
				NotifyMutationFailed(nameof(ToggleIssueStateAsync), ex);
			}
			finally
			{
				IsIssueMutationRunning = false;
			}
		}

		private void ApplyIssueState(Issue issue)
		{
			IssueItem.Closed = issue.Closed;
			IssueItem.State = issue.State;
			IssueItem.StateReason = issue.StateReason;
			IssueItem.UpdatedAt = issue.UpdatedAt;
			IssueItem.UpdatedAtHumanized = issue.UpdatedAtHumanized;
			IssueItem.ViewerCanClose = issue.ViewerCanClose;
			IssueItem.ViewerCanReopen = issue.ViewerCanReopen;
			IssueItem.ViewerCanUpdate = issue.ViewerCanUpdate;

			OnPropertyChanged(nameof(IssueItem));
			OnPropertyChanged(nameof(IssueStateButtonText));
			NotifyMutationCommandsCanExecuteChanged();
		}

		private void NotifyMutationCommandsCanExecuteChanged()
		{
			AddIssueCommentCommand?.NotifyCanExecuteChanged();
			ToggleIssueStateCommand?.NotifyCanExecuteChanged();
			OnPropertyChanged(nameof(IssueStateButtonText));
		}

		private void NotifyMutationFailed(string operationName, Exception exception)
		{
			_logger?.Error(operationName, exception);
			_messenger?.Send(new UserNotificationMessage("Something went wrong", exception.Message, UserNotificationType.Error));
		}
	}
}
