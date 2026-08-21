// Copyright (c) 2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Octokit.Queries.Repositories;
using FluentHub.ViewModels.UserControls.Overview;
using FluentHub.Octokit.Contracts;
using FluentHub.Octokit.Mutations;
using FluentHub.Helpers;
using FluentHub.Models;

namespace FluentHub.ViewModels.Repositories.PullRequests
{
	public class ConversationViewModel : BaseViewModel
	{
		private IssueComment? _bodyComment;

		private PullRequestOverviewViewModel _pullRequestOverviewViewModel = default!;
		public PullRequestOverviewViewModel PullRequestOverviewViewModel { get => _pullRequestOverviewViewModel; set => SetProperty(ref _pullRequestOverviewViewModel, value); }

		private PullRequest pullItem = default!;
		public PullRequest PullItem
		{
			get => pullItem;
			private set
			{
				if (SetProperty(ref pullItem, value))
					NotifyMutationStateChanged();
			}
		}

		private string _commentBody = string.Empty;
		public string CommentBody
		{
			get => _commentBody;
			set
			{
				if (SetProperty(ref _commentBody, value))
					AddCommentCommand?.NotifyCanExecuteChanged();
			}
		}

		private bool _isMutationRunning;
		public bool IsMutationRunning
		{
			get => _isMutationRunning;
			private set
			{
				if (SetProperty(ref _isMutationRunning, value))
					NotifyMutationStateChanged();
			}
		}

		public bool CanEditPullRequest => !IsMutationRunning && pullItem?.ViewerCanUpdate is true;
		public bool CanMergePullRequest
			=> !IsMutationRunning
			&& pullItem is { Closed: false, Merged: false }
			&& pullItem.Mergeable != MergeableState.Conflicting
			&& (pullItem.ViewerCanMergeAsAdmin
				|| Repository?.ViewerPermission is RepositoryPermission.Admin
					or RepositoryPermission.Maintain
					or RepositoryPermission.Write);
		public bool CanSubmitReview => !IsMutationRunning && pullItem is { Closed: false, Merged: false };
		public string PullRequestStateButtonText => pullItem?.Closed is true ? "Reopen pull request" : "Close pull request";
		public string SubscriptionButtonText
			=> pullItem?.ViewerSubscription == SubscriptionState.Subscribed ? "Unsubscribe" : "Subscribe";

		private bool CanAddComment
			=> !IsMutationRunning && pullItem is not null && !string.IsNullOrWhiteSpace(CommentBody);

		private bool CanToggleState
			=> !IsMutationRunning
			&& pullItem is not null
			&& !pullItem.Merged
			&& (pullItem.Closed
				? pullItem.ViewerCanReopen || pullItem.ViewerCanUpdate || ViewerCanManagePullRequests
				: pullItem.ViewerCanClose || pullItem.ViewerCanUpdate || ViewerCanManagePullRequests);

		private bool ViewerCanManagePullRequests
			=> Repository?.ViewerPermission is RepositoryPermission.Admin
				or RepositoryPermission.Maintain
				or RepositoryPermission.Write
				or RepositoryPermission.Triage;

		private bool CanToggleSubscription
			=> !IsMutationRunning && pullItem?.ViewerCanSubscribe is true;

		private readonly ObservableCollection<object> _timelineItems;
		public ReadOnlyObservableCollection<object> TimelineItems { get; set; }

		public IAsyncRelayCommand LoadRepositoryPullRequestConversationPageCommand { get; }
		public IAsyncRelayCommand AddCommentCommand { get; }
		public IAsyncRelayCommand ToggleStateCommand { get; }
		public IAsyncRelayCommand ToggleSubscriptionCommand { get; }

		public ConversationViewModel(IFluentHubGitHubClient gitHub) : base(gitHub)
		{
			var parameter = _navigation.TabView.SelectedItem.NavigationBar.Context;
			Login = parameter.PrimaryText ?? string.Empty;
			Name = parameter.SecondaryText ?? string.Empty;
			Number = parameter.Number;

			_timelineItems = new();
			TimelineItems = new(_timelineItems);

			LoadRepositoryPullRequestConversationPageCommand = new AsyncRelayCommand(LoadRepositoryPullRequestConversationPageAsync);
			AddCommentCommand = new AsyncRelayCommand(AddCommentAsync, () => CanAddComment);
			ToggleStateCommand = new AsyncRelayCommand(ToggleStateAsync, () => CanToggleState);
			ToggleSubscriptionCommand = new AsyncRelayCommand(ToggleSubscriptionAsync, () => CanToggleSubscription);
		}

		private async Task LoadRepositoryPullRequestConversationPageAsync()
		{
			SetTabInformation("Pull request", "Pull request", "PullRequests");
			SetLoadingProgress(true);
			InitializeNodePagingInfo();

			_currentTaskingMethodName = nameof(LoadRepositoryPullRequestConversationPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadRepositoryAsync);
				await LoadRepositoryAsync(Login, Name);

				_currentTaskingMethodName = nameof(LoadPullRequestAsync);
				await LoadPullRequestAsync(Login, Name);

				_currentTaskingMethodName = nameof(LoadRepositoryPullRequestCommentsAsync);
				await LoadRepositoryPullRequestCommentsAsync(Login, Name);

				SetTabInformation($"{PullItem.Title}", $"{PullItem.Title}");
			}
			catch (Exception ex)
			{
				TaskException = ex;
				IsTaskFaulted = true;
			}
			finally
			{
				SetLoadingProgress(false);
				PullRequestOverviewViewModel.Loaded = true;
			}
		}

		private async Task LoadRepositoryPullRequestCommentsAsync(string owner, string name)
		{
			var pullRequestQueries = _gitHub.Repositories.PullRequests;
			var queries = _gitHub.Repositories.PullRequestEvents;
			_timelineItems.Clear();

			// Get pull request body comment
			_bodyComment = await pullRequestQueries.GetBodyAsync(owner, name, Number);
			// The pull request body uses the pull request node ID and is edited separately.
			_bodyComment.ViewerCanUpdate = false;
			_bodyComment.ViewerCanDelete = false;
			_timelineItems.Add(_bodyComment);

			// Get all pull request event timeline items
			var pullEvents = await queries.GetAllAsync(owner, name, Number);
			foreach (var item in pullEvents)
				_timelineItems.Add(item);
		}

		private async Task LoadPullRequestAsync(string owner, string name)
		{
			var queries = _gitHub.Repositories.PullRequests;
			PullItem = await queries.GetAsync(owner, name, Number);

			PullRequestOverviewViewModel = new()
			{
				PullRequest = PullItem,
				SelectedTag = "conversation",
			};
		}

		private async Task LoadRepositoryAsync(string owner, string name)
		{
			var queries = _gitHub.Repositories.Repositories;
			Repository = await queries.GetDetailsAsync(owner, name);
		}

		private async Task AddCommentAsync()
		{
			if (!CanAddComment)
				return;

			IsMutationRunning = true;
			try
			{
				var response = await _gitHub.Mutations.PullRequests.AddCommentAsync(new AddCommentRequest
				{
					SubjectId = PullItem.Id,
					Body = CommentBody,
				});
				var comment = response.CommentEdge?.Node
					?? throw new InvalidOperationException("The add comment mutation did not return a comment.");
				comment.Reactions = new ReactionConnection { Nodes = [] };
				comment.ReactionGroups = [];
				_timelineItems.Add(comment);
				CommentBody = string.Empty;
				PullItem.Comments ??= new IssueCommentConnection();
				PullItem.Comments.TotalCount++;
				RefreshOverview();
			}
			catch (Exception ex)
			{
				NotifyMutationFailed(nameof(AddCommentAsync), ex);
			}
			finally
			{
				IsMutationRunning = false;
			}
		}

		private async Task ToggleStateAsync()
		{
			if (!CanToggleState)
				return;

			IsMutationRunning = true;
			try
			{
				PullRequest? pullRequest;
				if (PullItem.Closed)
				{
					var response = await _gitHub.Mutations.PullRequests.ReopenAsync(new ReopenPullRequestRequest
					{
						PullRequestId = PullItem.Id,
					});
					pullRequest = response.PullRequest;
				}
				else
				{
					var response = await _gitHub.Mutations.PullRequests.CloseAsync(new ClosePullRequestRequest
					{
						PullRequestId = PullItem.Id,
					});
					pullRequest = response.PullRequest;
				}

				ApplyPullRequest(pullRequest
					?? throw new InvalidOperationException("The pull request mutation did not return a pull request."));
			}
			catch (Exception ex)
			{
				NotifyMutationFailed(nameof(ToggleStateAsync), ex);
			}
			finally
			{
				IsMutationRunning = false;
			}
		}

		public async Task UpdatePullRequestAsync(string title, string body)
		{
			if (!CanEditPullRequest || string.IsNullOrWhiteSpace(title))
				return;

			IsMutationRunning = true;
			try
			{
				var response = await _gitHub.Mutations.PullRequests.UpdateAsync(new UpdatePullRequestRequest
				{
					PullRequestId = PullItem.Id,
					Title = title.Trim(),
					Body = body,
				});
				ApplyPullRequest(response.PullRequest
					?? throw new InvalidOperationException("The update pull request mutation did not return a pull request."));
				SetTabInformation(PullItem.Title, PullItem.Title);
			}
			catch (Exception ex)
			{
				NotifyMutationFailed(nameof(UpdatePullRequestAsync), ex);
			}
			finally
			{
				IsMutationRunning = false;
			}
		}

		public async Task MergeAsync(PullRequestMergeMethod method, string headline, string body)
		{
			if (!CanMergePullRequest)
				return;

			IsMutationRunning = true;
			try
			{
				var response = await _gitHub.Mutations.PullRequests.MergeAsync(new MergePullRequestRequest
				{
					PullRequestId = PullItem.Id,
					ExpectedHeadOid = PullItem.HeadRefOid,
					MergeMethod = method,
					CommitHeadline = string.IsNullOrWhiteSpace(headline) ? null : headline.Trim(),
					CommitBody = body,
				});
				ApplyPullRequest(response.PullRequest
					?? throw new InvalidOperationException("The merge pull request mutation did not return a pull request."));
			}
			catch (Exception ex)
			{
				NotifyMutationFailed(nameof(MergeAsync), ex);
			}
			finally
			{
				IsMutationRunning = false;
			}
		}

		public async Task SubmitReviewAsync(PullRequestReviewEvent reviewEvent, string body)
		{
			if (!CanSubmitReview)
				return;

			IsMutationRunning = true;
			try
			{
				await _gitHub.Mutations.PullRequests.AddReviewAsync(new AddPullRequestReviewRequest
				{
					PullRequestId = PullItem.Id,
					CommitOID = PullItem.HeadRefOid,
					Body = body,
					Event = reviewEvent,
				});
				_messenger?.Send(new UserNotificationMessage("Review submitted", "Your pull request review was submitted.", UserNotificationType.Success));
			}
			catch (Exception ex)
			{
				NotifyMutationFailed(nameof(SubmitReviewAsync), ex);
			}
			finally
			{
				IsMutationRunning = false;
			}
		}

		private async Task ToggleSubscriptionAsync()
		{
			if (!CanToggleSubscription)
				return;

			IsMutationRunning = true;
			try
			{
				var nextState = PullItem.ViewerSubscription == SubscriptionState.Subscribed
					? SubscriptionState.Unsubscribed
					: SubscriptionState.Subscribed;
				var response = await _gitHub.Mutations.Subscriptions.UpdateAsync(new UpdateSubscriptionRequest
				{
					SubscribableId = PullItem.Id,
					State = nextState,
				});
				PullItem.ViewerSubscription = response.Subscribable?.ViewerSubscription ?? nextState;
				RefreshOverview();
			}
			catch (Exception ex)
			{
				NotifyMutationFailed(nameof(ToggleSubscriptionAsync), ex);
			}
			finally
			{
				IsMutationRunning = false;
			}
		}

		private void ApplyPullRequest(PullRequest pullRequest)
		{
			PullItem.Body = pullRequest.Body;
			PullItem.Closed = pullRequest.Closed;
			PullItem.HeadRefOid = pullRequest.HeadRefOid;
			PullItem.Mergeable = pullRequest.Mergeable;
			PullItem.Merged = pullRequest.Merged;
			PullItem.State = pullRequest.State;
			PullItem.Title = pullRequest.Title;
			PullItem.UpdatedAt = pullRequest.UpdatedAt;
			PullItem.UpdatedAtHumanized = pullRequest.UpdatedAtHumanized;
			PullItem.ViewerCanClose = pullRequest.ViewerCanClose;
			PullItem.ViewerCanMergeAsAdmin = pullRequest.ViewerCanMergeAsAdmin;
			PullItem.ViewerCanReopen = pullRequest.ViewerCanReopen;
			PullItem.ViewerCanSubscribe = pullRequest.ViewerCanSubscribe;
			PullItem.ViewerCanUpdate = pullRequest.ViewerCanUpdate;
			PullItem.ViewerSubscription = pullRequest.ViewerSubscription;

			if (_bodyComment is not null && _bodyComment.Body != pullRequest.Body)
			{
				_bodyComment.Body = pullRequest.Body;
				var index = _timelineItems.IndexOf(_bodyComment);
				if (index >= 0)
				{
					_timelineItems.RemoveAt(index);
					_timelineItems.Insert(index, _bodyComment);
				}
			}

			RefreshOverview();
		}

		private void RefreshOverview()
		{
			PullRequestOverviewViewModel = new PullRequestOverviewViewModel
			{
				PullRequest = PullItem,
				SelectedTag = "conversation",
				Loaded = true,
			};
			NotifyMutationStateChanged();
		}

		private void NotifyMutationStateChanged()
		{
			AddCommentCommand?.NotifyCanExecuteChanged();
			ToggleStateCommand?.NotifyCanExecuteChanged();
			ToggleSubscriptionCommand?.NotifyCanExecuteChanged();
			OnPropertyChanged(nameof(CanEditPullRequest));
			OnPropertyChanged(nameof(CanMergePullRequest));
			OnPropertyChanged(nameof(CanSubmitReview));
			OnPropertyChanged(nameof(PullRequestStateButtonText));
			OnPropertyChanged(nameof(SubscriptionButtonText));
		}

		private void NotifyMutationFailed(string operationName, Exception exception)
		{
			_logger?.Error(operationName, exception);
			_messenger?.Send(new UserNotificationMessage("Something went wrong", exception.Message, UserNotificationType.Error));
		}
	}
}
