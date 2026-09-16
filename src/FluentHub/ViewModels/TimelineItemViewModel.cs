// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Application.Models;

namespace FluentHub.ViewModels;

public enum TimelineItemKind
{
	Activity,
	Comment,
}

/// <summary>
/// Presentation data for one Issue or pull request timeline item.
/// </summary>
public sealed class TimelineItemViewModel
{
	private TimelineItemViewModel(
		TimelineItemKind kind,
		string authorName,
		string? avatarUrl,
		string dateText,
		string actionText,
		string body,
		string message,
		bool isEdited)
	{
		Kind = kind;
		AuthorName = authorName;
		AvatarUrl = avatarUrl;
		DateText = dateText;
		ActionText = actionText;
		Body = body;
		Message = message;
		IsEdited = isEdited;
	}

	public TimelineItemKind Kind { get; }

	public bool IsComment => Kind == TimelineItemKind.Comment;

	public bool IsActivity => Kind == TimelineItemKind.Activity;

	public string AuthorName { get; }

	public string? AvatarUrl { get; }

	public string DateText { get; }

	public string ActionText { get; }

	public string Body { get; }

	public string Message { get; }

	public bool IsEdited { get; }

	public static IReadOnlyList<TimelineItemViewModel> Create(IEnumerable<object>? items)
	{
		if (items is null)
			return [];

		return items.Select(Create).ToList();
	}

	private static TimelineItemViewModel Create(object item)
	{
		return item switch
		{
			IssueComment comment => Comment(
				comment.Author,
				comment.Body,
				comment.CreatedAt,
				comment.CreatedAtHumanized,
				comment.LastEditedAt is not null),
			PullRequestReview review => Comment(
				review.Author,
				review.Body,
				review.CreatedAt,
				review.CreatedAtHumanized,
				review.LastEditedAt is not null,
				Strings.Timeline_Reviewed.GetLocalized()),
			PullRequestReviewComment reviewComment => Comment(
				reviewComment.Author,
				reviewComment.Body,
				reviewComment.CreatedAt,
				reviewComment.CreatedAtHumanized,
				reviewComment.LastEditedAt is not null),
			Comment comment => Comment(
				comment.Author,
				comment.Body,
				comment.CreatedAt,
				comment.CreatedAtHumanized,
				comment.LastEditedAt is not null),
			AddedToProjectEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_AddedToProject.GetLocalized()),
			AssignedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_AssignedItem.GetLocalized()),
			ClosedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_ClosedThis.GetLocalized()),
			CommentDeletedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_CommentDeleted.GetLocalized()),
			ConnectedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_Connected.GetLocalized()),
			ConvertedNoteToIssueEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_ConvertedNoteToIssue.GetLocalized()),
			ConvertedToDiscussionEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_ConvertedToDiscussion.GetLocalized()),
			CrossReferencedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_CrossReferenced.GetLocalized()),
			DemilestonedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_RemovedMilestone.GetLocalized()),
			DisconnectedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_Disconnected.GetLocalized()),
			LabeledEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_AddedLabel.GetLocalized()),
			LockedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_LockedConversation.GetLocalized()),
			MarkedAsDuplicateEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_MarkedAsDuplicate.GetLocalized()),
			MergedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_MergedThis.GetLocalized()),
			MilestonedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_AddedMilestone.GetLocalized()),
			MovedColumnsInProjectEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_MovedInProject.GetLocalized()),
			PinnedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_PinnedThis.GetLocalized()),
			ReferencedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_ReferencedThis.GetLocalized()),
			ReadyForReviewEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_ReadyForReview.GetLocalized()),
			RemovedFromProjectEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_RemovedFromProject.GetLocalized()),
			RenamedTitleEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_RenamedTitle.GetLocalized()),
			ReopenedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_ReopenedThis.GetLocalized()),
			ReviewDismissedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_DismissedReview.GetLocalized()),
			ReviewRequestRemovedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_RemovedReviewRequest.GetLocalized()),
			ReviewRequestedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_RequestedReview.GetLocalized()),
			SubscribedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_Subscribed.GetLocalized()),
			TransferredEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_TransferredThis.GetLocalized()),
			UnassignedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_UnassignedItem.GetLocalized()),
			UnlabeledEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_RemovedLabel.GetLocalized()),
			UnlockedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_UnlockedConversation.GetLocalized()),
			UnmarkedAsDuplicateEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_UnmarkedAsDuplicate.GetLocalized()),
			UnpinnedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_UnpinnedThis.GetLocalized()),
			UnsubscribedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_Unsubscribed.GetLocalized()),
			UserBlockedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_BlockedUser.GetLocalized()),
			PullRequestCommit value => CommitActivity(value.Commit?.Author, value.Commit?.CommittedDate, null, Strings.Timeline_CommittedChanges.GetLocalized()),
			PullRequestRevisionMarker value => Activity(null, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_UpdatedPullRequest.GetLocalized()),
			PullRequestReviewThread => Activity(null, null, null, Strings.Timeline_UpdatedReviewThread.GetLocalized()),
			PullRequestCommitCommentThread => Activity(null, null, null, Strings.Timeline_UpdatedCommitDiscussion.GetLocalized()),
			_ => Activity(null, null, null, GetFallbackMessage(item)),
		};
	}

	private static TimelineItemViewModel Comment(
		Actor? author,
		string? body,
		DateTimeOffset createdAt,
		string? createdAtHumanized,
		bool isEdited,
		string? actionText = null)
		=> new(
			TimelineItemKind.Comment,
			GetAuthorName(author?.Login),
			author?.AvatarUrl,
			FormatDate(createdAt, createdAtHumanized),
			actionText ?? Strings.Common_Commented.GetLocalized(),
			string.IsNullOrWhiteSpace(body) ? Strings.Common_NoDescriptionProvided.GetLocalized() : body,
			string.Empty,
			isEdited);

	private static TimelineItemViewModel Activity(
		Actor? actor,
		DateTimeOffset? createdAt,
		string? createdAtHumanized,
		string message)
		=> new(
			TimelineItemKind.Activity,
			GetAuthorName(actor?.Login),
			actor?.AvatarUrl,
			FormatDate(createdAt, createdAtHumanized),
			string.Empty,
			string.Empty,
			message,
			false);

	private static TimelineItemViewModel CommitActivity(
		GitActor? actor,
		DateTimeOffset? createdAt,
		string? createdAtHumanized,
		string message)
		=> new(
			TimelineItemKind.Activity,
			GetAuthorName(actor?.User?.Login ?? actor?.Name),
			actor?.AvatarUrl,
			FormatDate(createdAt, createdAtHumanized),
			string.Empty,
			string.Empty,
			message,
			false);

	private static string GetAuthorName(string? login)
		=> string.IsNullOrWhiteSpace(login) ? Strings.Common_GitHub.GetLocalized() : login;

	private static string FormatDate(DateTimeOffset? value, string? humanized)
	{
		if (!string.IsNullOrWhiteSpace(humanized))
			return humanized;

		return value is { } date && date != default
			? date.ToLocalTime().ToString("g")
			: string.Empty;
	}

	private static string GetFallbackMessage(object item)
	{
		var name = item.GetType().Name;
		return name.EndsWith("Event", StringComparison.Ordinal)
			? string.Format(
				CultureInfo.CurrentCulture,
				Strings.Timeline_EventFallback.GetLocalized(),
				name[..^"Event".Length].ToLowerInvariant())
			: Strings.Timeline_UpdatedThis.GetLocalized();
	}
}
