using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Services;
using FluentHub.Utils;
using Microsoft.UI.Xaml.Controls;
using FluentHub.Core.Application.Models;
using FluentHub.Core.Infrastructure.GitHub.Mutations;

namespace FluentHub.Shared.Controls.ViewModels
{
	public class IssueCommentBlockViewModel : ObservableObject
	{
		private readonly IFluentHubGitHubClient _gitHub;

		private readonly ILogger? _logger;

		private readonly IMessenger? _messenger;

		private IssueComment _issueComment = default!;
		public IssueComment IssueComment
		{
			get => _issueComment;
			set
			{
				if (SetProperty(ref _issueComment, value))
				{
					InitializeReactions();
					OnPropertyChanged(nameof(CanEdit));
					OnPropertyChanged(nameof(CanDelete));
					OnPropertyChanged(nameof(CanReact));
				}
			}
		}

		private bool _isMutationRunning;
		public bool IsMutationRunning
		{
			get => _isMutationRunning;
			private set
			{
				if (SetProperty(ref _isMutationRunning, value))
				{
					OnPropertyChanged(nameof(CanEdit));
					OnPropertyChanged(nameof(CanDelete));
					OnPropertyChanged(nameof(CanReact));
				}
			}
		}

		public bool CanEdit => !IsMutationRunning && _issueComment?.ViewerCanUpdate is true;
		public bool CanDelete => !IsMutationRunning && _issueComment?.ViewerCanDelete is true;
		public bool CanReact => !IsMutationRunning && _issueComment?.ViewerCanReact is true;

		private int _thumbsUpCount;
		public int ThumbsUpCount { get => _thumbsUpCount; set => SetProperty(ref _thumbsUpCount, value); }

		private int _thumbsDownCount;
		public int ThumbsDownCount { get => _thumbsDownCount; set => SetProperty(ref _thumbsDownCount, value); }

		private int _laughCount;
		public int LaughCount { get => _laughCount; set => SetProperty(ref _laughCount, value); }

		private int _hoorayCount;
		public int HoorayCount { get => _hoorayCount; set => SetProperty(ref _hoorayCount, value); }

		private int _confusedCount;
		public int ConfusedCount { get => _confusedCount; set => SetProperty(ref _confusedCount, value); }

		private int _heartCount;
		public int HeartCount { get => _heartCount; set => SetProperty(ref _heartCount, value); }

		private int _rocketCount;
		public int RocketCount { get => _rocketCount; set => SetProperty(ref _rocketCount, value); }

		private int _eyesCount;
		public int EyesCount { get => _eyesCount; set => SetProperty(ref _eyesCount, value); }

		private bool _viewerReactedThumbsUp;
		public bool ViewerReactedThumbsUp { get => _viewerReactedThumbsUp; set => SetProperty(ref _viewerReactedThumbsUp, value); }

		private bool _viewerReactedThumbsDown;
		public bool ViewerReactedThumbsDown { get => _viewerReactedThumbsDown; set => SetProperty(ref _viewerReactedThumbsDown, value); }

		private bool _viewerReactedLaugh;
		public bool ViewerReactedLaugh { get => _viewerReactedLaugh; set => SetProperty(ref _viewerReactedLaugh, value); }

		private bool _viewerReactedHooray;
		public bool ViewerReactedHooray { get => _viewerReactedHooray; set => SetProperty(ref _viewerReactedHooray, value); }

		private bool _viewerReactedConfused;
		public bool ViewerReactedConfused { get => _viewerReactedConfused; set => SetProperty(ref _viewerReactedConfused, value); }

		private bool _viewerReactedHeart;
		public bool ViewerReactedHeart { get => _viewerReactedHeart; set => SetProperty(ref _viewerReactedHeart, value); }

		private bool _viewerReactedRocket;
		public bool ViewerReactedRocket { get => _viewerReactedRocket; set => SetProperty(ref _viewerReactedRocket, value); }

		private bool _viewerReactedEyes;
		public bool ViewerReactedEyes { get => _viewerReactedEyes; set => SetProperty(ref _viewerReactedEyes, value); }

		public IssueCommentBlockViewModel(IFluentHubGitHubClient gitHub, IMessenger? messenger = null, ILogger? logger = null)
		{
			_gitHub = gitHub;
			_messenger = messenger;
			_logger = logger;
		}

		public async Task UpdateCommentAsync(string body)
		{
			if (!CanEdit || string.IsNullOrWhiteSpace(body))
				return;

			IsMutationRunning = true;
			try
			{
				var response = await _gitHub.Mutations.Issues.UpdateIssueCommentAsync(new UpdateIssueCommentRequest
				{
					Id = IssueComment.Id,
					Body = body,
				});
				var updated = response.IssueComment
					?? throw new InvalidOperationException("The update comment mutation did not return a comment.");
				updated.Reactions = IssueComment.Reactions;
				updated.ReactionGroups = IssueComment.ReactionGroups;
				IssueComment = updated;
			}
			catch (Exception ex)
			{
				NotifyMutationFailed(nameof(UpdateCommentAsync), ex);
			}
			finally
			{
				IsMutationRunning = false;
			}
		}

		public async Task DeleteCommentAsync()
		{
			if (!CanDelete)
				return;

			IsMutationRunning = true;
			try
			{
				await _gitHub.Mutations.Issues.DeleteIssueCommentAsync(new DeleteIssueCommentRequest
				{
					Id = IssueComment.Id,
				});
				IssueComment.Body = "Comment deleted.";
				IssueComment.ViewerCanDelete = false;
				IssueComment.ViewerCanUpdate = false;
				IssueComment.ViewerCanReact = false;
				OnPropertyChanged(nameof(IssueComment));
			}
			catch (Exception ex)
			{
				NotifyMutationFailed(nameof(DeleteCommentAsync), ex);
			}
			finally
			{
				IsMutationRunning = false;
			}
		}

		public async Task ToggleReactionAsync(ReactionContent content)
		{
			if (!CanReact)
				return;

			IsMutationRunning = true;
			try
			{
				List<ReactionGroup>? groups;
				if (ViewerHasReacted(content))
				{
					var response = await _gitHub.Mutations.Reactions.RemoveAsync(new RemoveReactionRequest
					{
						SubjectId = IssueComment.Id,
						Content = content,
					});
					groups = response.ReactionGroups;
				}
				else
				{
					var response = await _gitHub.Mutations.Reactions.AddAsync(new AddReactionRequest
					{
						SubjectId = IssueComment.Id,
						Content = content,
					});
					groups = response.ReactionGroups;
				}

				IssueComment.ReactionGroups = groups ?? [];
				InitializeReactions();
			}
			catch (Exception ex)
			{
				NotifyMutationFailed(nameof(ToggleReactionAsync), ex);
			}
			finally
			{
				IsMutationRunning = false;
			}
		}

		public void InitializeReactions()
		{
			ThumbsUpCount = 0;
			ThumbsDownCount = 0;
			LaughCount = 0;
			HoorayCount = 0;
			ConfusedCount = 0;
			HeartCount = 0;
			RocketCount = 0;
			EyesCount = 0;
			ViewerReactedThumbsUp = false;
			ViewerReactedThumbsDown = false;
			ViewerReactedLaugh = false;
			ViewerReactedHooray = false;
			ViewerReactedConfused = false;
			ViewerReactedHeart = false;
			ViewerReactedRocket = false;
			ViewerReactedEyes = false;

			if (_issueComment?.ReactionGroups is { Count: > 0 } groups)
			{
				foreach (var group in groups)
					SetReaction(group.Content, group.Reactors?.TotalCount ?? 0, group.ViewerHasReacted);
				return;
			}

			foreach (var reaction in (_issueComment?.Reactions?.Nodes ?? []).OfType<Reaction>())
				IncrementReaction(reaction.Content);
		}

		private bool ViewerHasReacted(ReactionContent content)
			=> content switch
			{
				ReactionContent.ThumbsUp => ViewerReactedThumbsUp,
				ReactionContent.ThumbsDown => ViewerReactedThumbsDown,
				ReactionContent.Laugh => ViewerReactedLaugh,
				ReactionContent.Hooray => ViewerReactedHooray,
				ReactionContent.Confused => ViewerReactedConfused,
				ReactionContent.Heart => ViewerReactedHeart,
				ReactionContent.Rocket => ViewerReactedRocket,
				ReactionContent.Eyes => ViewerReactedEyes,
				_ => false,
			};

		private void IncrementReaction(ReactionContent content)
			=> SetReaction(content, GetReactionCount(content) + 1, ViewerHasReacted(content));

		private int GetReactionCount(ReactionContent content)
			=> content switch
			{
				ReactionContent.ThumbsUp => ThumbsUpCount,
				ReactionContent.ThumbsDown => ThumbsDownCount,
				ReactionContent.Laugh => LaughCount,
				ReactionContent.Hooray => HoorayCount,
				ReactionContent.Confused => ConfusedCount,
				ReactionContent.Heart => HeartCount,
				ReactionContent.Rocket => RocketCount,
				ReactionContent.Eyes => EyesCount,
				_ => 0,
			};

		private void SetReaction(ReactionContent content, int count, bool viewerHasReacted)
		{
			switch (content)
			{
				case ReactionContent.ThumbsUp: ThumbsUpCount = count; ViewerReactedThumbsUp = viewerHasReacted; break;
				case ReactionContent.ThumbsDown: ThumbsDownCount = count; ViewerReactedThumbsDown = viewerHasReacted; break;
				case ReactionContent.Laugh: LaughCount = count; ViewerReactedLaugh = viewerHasReacted; break;
				case ReactionContent.Hooray: HoorayCount = count; ViewerReactedHooray = viewerHasReacted; break;
				case ReactionContent.Confused: ConfusedCount = count; ViewerReactedConfused = viewerHasReacted; break;
				case ReactionContent.Heart: HeartCount = count; ViewerReactedHeart = viewerHasReacted; break;
				case ReactionContent.Rocket: RocketCount = count; ViewerReactedRocket = viewerHasReacted; break;
				case ReactionContent.Eyes: EyesCount = count; ViewerReactedEyes = viewerHasReacted; break;
			}
		}

		private void NotifyMutationFailed(string operationName, Exception exception)
		{
			_logger?.Error(operationName, exception);
			_messenger?.Send(new UserNotificationMessage("Something went wrong", exception.Message, UserNotificationType.Error));
		}
	}
}
