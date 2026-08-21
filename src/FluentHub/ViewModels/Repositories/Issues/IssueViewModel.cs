using FluentHub.Octokit.Queries.Repositories;
using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Services;
using FluentHub.Utils;
using FluentHub.ViewModels.UserControls.Overview;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using FluentHub.Octokit.Contracts;
using FluentHub.Octokit.Mutations;

namespace FluentHub.ViewModels.Repositories.Issues
{
	public class IssueViewModel : BaseViewModel
	{
		private IssueComment? _bodyComment;

		private Issue _issueItem = default!;
		public Issue IssueItem
		{
			get => _issueItem;
			private set
			{
				if (SetProperty(ref _issueItem, value))
				{
					OnPropertyChanged(nameof(IssueStateButtonText));
					OnPropertyChanged(nameof(SubscriptionButtonText));
					OnPropertyChanged(nameof(CanEditIssue));
					OnPropertyChanged(nameof(CanEditMetadata));
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

		public string SubscriptionButtonText
			=> _issueItem?.ViewerSubscription == SubscriptionState.Subscribed ? "Unsubscribe" : "Subscribe";

		public bool CanEditIssue
			=> !IsIssueMutationRunning && _issueItem?.ViewerCanUpdate is true;

		public bool CanEditMetadata
			=> !IsIssueMutationRunning
			&& (_issueItem?.ViewerCanLabel is true || ViewerCanManageIssues);

		public bool CanCloseIssue
			=> _issueItem?.Closed is false && CanToggleIssueState;

		private bool ViewerCanManageIssues
			=> Repository?.ViewerPermission is RepositoryPermission.Admin
				or RepositoryPermission.Maintain
				or RepositoryPermission.Write
				or RepositoryPermission.Triage;

		private bool CanAddIssueComment
			=> !IsIssueMutationRunning
			&& _issueItem is not null
			&& !string.IsNullOrWhiteSpace(CommentBody);

		private bool CanToggleIssueState
			=> !IsIssueMutationRunning
			&& _issueItem is not null
			&& (_issueItem.Closed
				? _issueItem.ViewerCanReopen || _issueItem.ViewerCanUpdate || ViewerCanManageIssues
				: _issueItem.ViewerCanClose || _issueItem.ViewerCanUpdate || ViewerCanManageIssues);

		private bool CanToggleSubscription
			=> !IsIssueMutationRunning
			&& _issueItem?.ViewerCanSubscribe is true;

		private readonly ObservableCollection<object> _timelineItems;
		public ReadOnlyObservableCollection<object> TimelineItems { get; set; }

		public IAsyncRelayCommand LoadRepositoryIssuePageCommand { get; }
		public IAsyncRelayCommand AddIssueCommentCommand { get; }
		public IAsyncRelayCommand ToggleIssueStateCommand { get; }
		public IAsyncRelayCommand ToggleSubscriptionCommand { get; }

		public IssueViewModel(IFluentHubGitHubClient gitHub) : base(gitHub)
		{
			_timelineItems = new();
			TimelineItems = new(_timelineItems);

			LoadRepositoryIssuePageCommand = new AsyncRelayCommand(LoadRepositoryIssuePageAsync);
			AddIssueCommentCommand = new AsyncRelayCommand(AddIssueCommentAsync, () => CanAddIssueComment);
			ToggleIssueStateCommand = new AsyncRelayCommand(ToggleIssueStateAsync, () => CanToggleIssueState);
			ToggleSubscriptionCommand = new AsyncRelayCommand(ToggleSubscriptionAsync, () => CanToggleSubscription);
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

			_bodyComment = await issueQueries.GetBodyAsync(owner, name, Number);
			// The issue body uses the issue node ID, so it must be edited through UpdateIssue.
			_bodyComment.ViewerCanUpdate = false;
			_bodyComment.ViewerCanDelete = false;
			_timelineItems.Add(_bodyComment);

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
				var response = await mutations.AddCommentAsync(new AddCommentRequest
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
					var response = await mutations.ReopenIssueAsync(new ReopenIssueRequest
					{
						IssueId = IssueItem.Id,
					});

					issue = response.Issue;
				}
				else
				{
					var response = await mutations.CloseIssueAsync(new CloseIssueRequest
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

		public async Task CloseIssueAsync(IssueClosedStateReason reason)
		{
			if (!CanToggleIssueState || IssueItem.Closed)
				return;

			IsIssueMutationRunning = true;

			try
			{
				var response = await _gitHub.Mutations.Issues.CloseIssueAsync(new CloseIssueRequest
				{
					IssueId = IssueItem.Id,
					StateReason = reason,
				});

				ApplyIssueState(response.Issue
					?? throw new InvalidOperationException("The close issue mutation did not return an issue."));
			}
			catch (Exception ex)
			{
				NotifyMutationFailed(nameof(CloseIssueAsync), ex);
			}
			finally
			{
				IsIssueMutationRunning = false;
			}
		}

		public async Task UpdateIssueAsync(string title, string body)
		{
			if (!CanEditIssue || string.IsNullOrWhiteSpace(title))
				return;

			IsIssueMutationRunning = true;

			try
			{
				var response = await _gitHub.Mutations.Issues.UpdateIssueAsync(new UpdateIssueRequest
				{
					Id = IssueItem.Id,
					Title = title.Trim(),
					Body = body,
				});

				ApplyIssueState(response.Issue
					?? throw new InvalidOperationException("The update issue mutation did not return an issue."));
				SetTabInformation(IssueItem.Title, IssueItem.Title);
			}
			catch (Exception ex)
			{
				NotifyMutationFailed(nameof(UpdateIssueAsync), ex);
			}
			finally
			{
				IsIssueMutationRunning = false;
			}
		}

		public Task<Repository> GetIssueOptionsAsync()
			=> _gitHub.Repositories.Repositories.GetIssueOptionsAsync(Login, Name);

		public async Task UpdateMetadataAsync(
			IReadOnlyCollection<User> assignees,
			IReadOnlyCollection<Label> labels,
			Milestone? milestone)
		{
			if (!CanEditMetadata)
				return;

			IsIssueMutationRunning = true;

			try
			{
				var response = await _gitHub.Mutations.Issues.UpdateIssueAsync(new UpdateIssueRequest
				{
					Id = IssueItem.Id,
					AssigneeIds = assignees.Select(x => x.Id).ToList(),
					LabelIds = labels.Select(x => x.Id).ToList(),
					MilestoneId = milestone?.Id,
				});

				ApplyIssueState(response.Issue
					?? throw new InvalidOperationException("The update issue mutation did not return an issue."));
				IssueItem.Assignees = new UserConnection { Nodes = assignees.Cast<User?>().ToList() };
				IssueItem.Labels = new LabelConnection { Nodes = labels.Cast<Label?>().ToList() };
				IssueItem.Milestone = milestone;
				OnPropertyChanged(nameof(IssueItem));
			}
			catch (Exception ex)
			{
				NotifyMutationFailed(nameof(UpdateMetadataAsync), ex);
			}
			finally
			{
				IsIssueMutationRunning = false;
			}
		}

		private async Task ToggleSubscriptionAsync()
		{
			if (!CanToggleSubscription)
				return;

			IsIssueMutationRunning = true;

			try
			{
				var nextState = IssueItem.ViewerSubscription == SubscriptionState.Subscribed
					? SubscriptionState.Unsubscribed
					: SubscriptionState.Subscribed;
				var response = await _gitHub.Mutations.Subscriptions.UpdateAsync(new UpdateSubscriptionRequest
				{
					SubscribableId = IssueItem.Id,
					State = nextState,
				});

				IssueItem.ViewerSubscription = response.Subscribable?.ViewerSubscription ?? nextState;
				OnPropertyChanged(nameof(IssueItem));
				OnPropertyChanged(nameof(SubscriptionButtonText));
			}
			catch (Exception ex)
			{
				NotifyMutationFailed(nameof(ToggleSubscriptionAsync), ex);
			}
			finally
			{
				IsIssueMutationRunning = false;
			}
		}

		private void ApplyIssueState(Issue issue)
		{
			IssueItem.Body = issue.Body;
			IssueItem.Closed = issue.Closed;
			IssueItem.Title = issue.Title;
			IssueItem.State = issue.State;
			IssueItem.StateReason = issue.StateReason;
			IssueItem.UpdatedAt = issue.UpdatedAt;
			IssueItem.UpdatedAtHumanized = issue.UpdatedAtHumanized;
			IssueItem.ViewerCanClose = issue.ViewerCanClose;
			IssueItem.ViewerCanReopen = issue.ViewerCanReopen;
			IssueItem.ViewerCanLabel = issue.ViewerCanLabel;
			IssueItem.ViewerCanSubscribe = issue.ViewerCanSubscribe;
			IssueItem.ViewerCanUpdate = issue.ViewerCanUpdate;
			IssueItem.ViewerSubscription = issue.ViewerSubscription;

			if (_bodyComment is not null && _bodyComment.Body != issue.Body)
			{
				_bodyComment.Body = issue.Body;
				var index = _timelineItems.IndexOf(_bodyComment);
				if (index >= 0)
				{
					_timelineItems.RemoveAt(index);
					_timelineItems.Insert(index, _bodyComment);
				}
			}

			OnPropertyChanged(nameof(IssueItem));
			OnPropertyChanged(nameof(IssueStateButtonText));
			OnPropertyChanged(nameof(SubscriptionButtonText));
			OnPropertyChanged(nameof(CanEditIssue));
			OnPropertyChanged(nameof(CanEditMetadata));
			OnPropertyChanged(nameof(CanCloseIssue));
			NotifyMutationCommandsCanExecuteChanged();
		}

		private void NotifyMutationCommandsCanExecuteChanged()
		{
			AddIssueCommentCommand?.NotifyCanExecuteChanged();
			ToggleIssueStateCommand?.NotifyCanExecuteChanged();
			ToggleSubscriptionCommand?.NotifyCanExecuteChanged();
			OnPropertyChanged(nameof(IssueStateButtonText));
			OnPropertyChanged(nameof(SubscriptionButtonText));
			OnPropertyChanged(nameof(CanEditIssue));
			OnPropertyChanged(nameof(CanEditMetadata));
			OnPropertyChanged(nameof(CanCloseIssue));
		}

		private void NotifyMutationFailed(string operationName, Exception exception)
		{
			_logger?.Error(operationName, exception);
			_messenger?.Send(new UserNotificationMessage("Something went wrong", exception.Message, UserNotificationType.Error));
		}
	}
}
