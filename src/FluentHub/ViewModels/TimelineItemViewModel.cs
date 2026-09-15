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
				"reviewed"),
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
			AddedToProjectEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, "added this to a project"),
			AssignedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, "assigned an item"),
			ClosedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, "closed this"),
			CommentDeletedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, "deleted a comment"),
			ConnectedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, "connected this with another item"),
			ConvertedNoteToIssueEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, "converted a note to an issue"),
			ConvertedToDiscussionEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, "converted this to a discussion"),
			CrossReferencedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, "cross-referenced this"),
			DemilestonedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, "removed the milestone"),
			DisconnectedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, "disconnected this from another item"),
			LabeledEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, "added a label"),
			LockedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, "locked this conversation"),
			MarkedAsDuplicateEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, "marked this as a duplicate"),
			MergedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, "merged this"),
			MilestonedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, "added a milestone"),
			MovedColumnsInProjectEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, "moved this in a project"),
			PinnedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, "pinned this"),
			ReferencedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, "referenced this"),
			ReadyForReviewEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, "marked this ready for review"),
			RemovedFromProjectEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, "removed this from a project"),
			RenamedTitleEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, "renamed the title"),
			ReopenedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, "reopened this"),
			ReviewDismissedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, "dismissed a review"),
			ReviewRequestRemovedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, "removed a review request"),
			ReviewRequestedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, "requested a review"),
			SubscribedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, "subscribed"),
			TransferredEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, "transferred this"),
			UnassignedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, "unassigned an item"),
			UnlabeledEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, "removed a label"),
			UnlockedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, "unlocked this conversation"),
			UnmarkedAsDuplicateEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, "unmarked this as a duplicate"),
			UnpinnedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, "unpinned this"),
			UnsubscribedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, "unsubscribed"),
			UserBlockedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, "blocked a user"),
			PullRequestCommit value => CommitActivity(value.Commit?.Author, value.Commit?.CommittedDate, null, "committed changes"),
			PullRequestRevisionMarker value => Activity(null, value.CreatedAt, value.CreatedAtHumanized, "updated the pull request"),
			PullRequestReviewThread => Activity(null, null, null, "updated a review thread"),
			PullRequestCommitCommentThread => Activity(null, null, null, "updated a commit discussion"),
			_ => Activity(null, null, null, GetFallbackMessage(item)),
		};
	}

	private static TimelineItemViewModel Comment(
		Actor? author,
		string? body,
		DateTimeOffset createdAt,
		string? createdAtHumanized,
		bool isEdited,
		string actionText = "commented")
		=> new(
			TimelineItemKind.Comment,
			GetAuthorName(author?.Login),
			author?.AvatarUrl,
			FormatDate(createdAt, createdAtHumanized),
			actionText,
			string.IsNullOrWhiteSpace(body) ? "No description provided." : body,
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
		=> string.IsNullOrWhiteSpace(login) ? "GitHub" : login;

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
			? name[..^"Event".Length].ToLowerInvariant() + " event"
			: "updated this";
	}
}
