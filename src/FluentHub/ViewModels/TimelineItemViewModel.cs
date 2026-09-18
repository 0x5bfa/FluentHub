// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Application.Models;
using System.Collections.ObjectModel;

namespace FluentHub.ViewModels;

public enum TimelineActivityPartKind
{
	Text,
	Label,
	Status,
	Link,
	Strikethrough,
}

public sealed class TimelineActivityPart
{
	private TimelineActivityPart(TimelineActivityPartKind kind, string text, Uri? uri = null, string? color = null)
	{
		Kind = kind;
		Text = text;
		Uri = uri;
		Color = color;
	}

	public TimelineActivityPartKind Kind { get; }

	public string Text { get; }

	public Uri? Uri { get; }

	public string? Color { get; }

	public static TimelineActivityPart FromText(string text)
		=> new(TimelineActivityPartKind.Text, text);

	public static TimelineActivityPart Label(string text, string? color = null)
		=> new(TimelineActivityPartKind.Label, text, color: color);

	public static TimelineActivityPart Status(string text)
		=> new(TimelineActivityPartKind.Status, text);

	public static TimelineActivityPart Link(string text, Uri? uri = null)
		=> new(TimelineActivityPartKind.Link, text, uri);

	public static TimelineActivityPart Strikethrough(string text)
		=> new(TimelineActivityPartKind.Strikethrough, text);
}

public sealed class TimelineActivityContent
{
	private TimelineActivityContent(IEnumerable<TimelineActivityPart> parts)
		=> Parts = new(parts);

	public ObservableCollection<TimelineActivityPart> Parts { get; }

	public bool IsRich => Parts.Any(static part => part.Kind != TimelineActivityPartKind.Text);

	public string Text => string.Concat(Parts.Select(static part => part.Text));

	public static TimelineActivityContent FromText(string text)
		=> new([TimelineActivityPart.FromText(text)]);

	public static TimelineActivityContent FromLocalizedFormat(
		string format,
		params TimelineActivityPart[] values)
	{
		ArgumentNullException.ThrowIfNull(format);
		ArgumentNullException.ThrowIfNull(values);

		if (values.Length == 0)
			return FromText(format);

		var parts = new List<TimelineActivityPart>();
		var literalStart = 0;

		for (var index = 0; index < format.Length; index++)
		{
			if (format[index] != '{')
				continue;

			var closingBrace = format.IndexOf('}', index + 1);
			if (closingBrace < 0)
				break;

			if (!int.TryParse(
				format.AsSpan(index + 1, closingBrace - index - 1),
				NumberStyles.None,
				CultureInfo.InvariantCulture,
				out var valueIndex) ||
				valueIndex < 0 ||
				valueIndex >= values.Length)
			{
				continue;
			}

			AddText(parts, format[literalStart..index]);
			parts.Add(values[valueIndex]);
			index = closingBrace;
			literalStart = closingBrace + 1;
		}

		AddText(parts, format[literalStart..]);
		return parts.Count == 0 ? FromText(format) : new(parts);
	}

	private static void AddText(List<TimelineActivityPart> parts, string text)
	{
		if (text.Length == 0)
			return;

		if (parts.LastOrDefault() is { Kind: TimelineActivityPartKind.Text } previous)
		{
			parts[^1] = TimelineActivityPart.FromText(previous.Text + text);
			return;
		}

		parts.Add(TimelineActivityPart.FromText(text));
	}
}

public sealed class TimelineReference
{
	public TimelineReference(string location, string title, Uri? uri)
	{
		Location = location;
		Title = title;
		Uri = uri;
	}

	public string Location { get; }

	public string Title { get; }

	public Uri? Uri { get; }

	public bool HasUri => Uri is not null;

	public bool HasNoUri => Uri is null;

	public string DisplayText
		=> string.IsNullOrWhiteSpace(Title) ? Location : $"{Location} {Title}";
}

public enum TimelineItemKind
{
	Activity,
	Comment,
	TitleChanged,
	CrossReferenced,
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
		bool isEdited,
		string? previousTitle = null,
		string? currentTitle = null,
		string? referenceLocation = null,
		string? referenceTitle = null,
		TimelineActivityContent? activityContent = null,
		TimelineReference? crossReference = null)
	{
		Kind = kind;
		AuthorName = authorName;
		AvatarUrl = avatarUrl;
		DateText = dateText;
		ActionText = actionText;
		Body = body;
		Message = message;
		IsEdited = isEdited;
		PreviousTitle = previousTitle;
		CurrentTitle = currentTitle;
		CrossReference = crossReference;
		ReferenceLocation = referenceLocation ?? crossReference?.Location;
		ReferenceTitle = referenceTitle ?? crossReference?.Title;
		ActivityContent = activityContent ?? TimelineActivityContent.FromText(message);
	}

	public TimelineItemKind Kind { get; }

	public bool IsComment => Kind == TimelineItemKind.Comment;

	public bool IsActivity => !IsComment;

	public string AuthorName { get; }

	public string? AvatarUrl { get; }

	public string DateText { get; }

	public string ActionText { get; }

	public string Body { get; }

	public string Message { get; }

	public TimelineActivityContent ActivityContent { get; }

	public bool IsEdited { get; }

	public string? PreviousTitle { get; }

	public string? CurrentTitle { get; }

	public string? ReferenceLocation { get; }

	public string? ReferenceTitle { get; }

	public TimelineReference? CrossReference { get; }

	public bool HasReferenceLocation => !string.IsNullOrWhiteSpace(ReferenceLocation);

	public bool HasReferenceTitle => !string.IsNullOrWhiteSpace(ReferenceTitle);

	public bool HasCrossReference => CrossReference is not null;

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
			CrossReferencedEvent value => CrossReferenceActivity(value),
			DemilestonedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_RemovedMilestone.GetLocalized()),
			DisconnectedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_Disconnected.GetLocalized()),
			LabeledEvent value => LabelActivity(
				value.Actor,
				value.CreatedAt,
				value.CreatedAtHumanized,
				Strings.Timeline_AddedLabel,
				Strings.Timeline_AddedLabelWithName,
				value.Label),
			LockedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_LockedConversation.GetLocalized()),
			MarkedAsDuplicateEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_MarkedAsDuplicate.GetLocalized()),
			MentionedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_MentionedThis.GetLocalized()),
			MergedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_MergedThis.GetLocalized()),
			MilestonedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_AddedMilestone.GetLocalized()),
			MovedColumnsInProjectEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_MovedInProject.GetLocalized()),
			PinnedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_PinnedThis.GetLocalized()),
			ReferencedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_ReferencedThis.GetLocalized()),
			ReadyForReviewEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_ReadyForReview.GetLocalized()),
			RemovedFromProjectEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_RemovedFromProject.GetLocalized()),
			RenamedTitleEvent value => TitleChangedActivity(value),
			ReopenedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_ReopenedThis.GetLocalized()),
			ReviewDismissedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_DismissedReview.GetLocalized()),
			ReviewRequestRemovedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_RemovedReviewRequest.GetLocalized()),
			ReviewRequestedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_RequestedReview.GetLocalized()),
			SubscribedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_Subscribed.GetLocalized()),
			TransferredEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_TransferredThis.GetLocalized()),
			UnassignedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_UnassignedItem.GetLocalized()),
			UnlabeledEvent value => LabelActivity(
				value.Actor,
				value.CreatedAt,
				value.CreatedAtHumanized,
				Strings.Timeline_RemovedLabel,
				Strings.Timeline_RemovedLabelWithName,
				value.Label),
			UnlockedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_UnlockedConversation.GetLocalized()),
			UnmarkedAsDuplicateEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_UnmarkedAsDuplicate.GetLocalized()),
			UnpinnedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_UnpinnedThis.GetLocalized()),
			UnsubscribedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_Unsubscribed.GetLocalized()),
			UserBlockedEvent value => Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_BlockedUser.GetLocalized()),
			AutoMergeDisabledEvent value => FallbackActivity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, value),
			AutoMergeEnabledEvent value => FallbackActivity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, value),
			AutoRebaseEnabledEvent value => FallbackActivity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, value),
			AutoSquashEnabledEvent value => FallbackActivity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, value),
			AutomaticBaseChangeFailedEvent value => FallbackActivity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, value),
			AutomaticBaseChangeSucceededEvent value => FallbackActivity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, value),
			BaseRefChangedEvent value => FallbackActivity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, value),
			BaseRefDeletedEvent value => FallbackActivity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, value),
			BaseRefForcePushedEvent value => FallbackActivity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, value),
			ConvertToDraftEvent value => FallbackActivity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, value),
			DeployedEvent value => FallbackActivity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, value),
			DeploymentEnvironmentChangedEvent value => FallbackActivity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, value),
			HeadRefDeletedEvent value => FallbackActivity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, value),
			HeadRefForcePushedEvent value => FallbackActivity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, value),
			HeadRefRestoredEvent value => FallbackActivity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, value),
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
		string message,
		TimelineItemKind kind = TimelineItemKind.Activity,
		string? previousTitle = null,
		string? currentTitle = null,
		string? referenceLocation = null,
		string? referenceTitle = null,
		TimelineActivityContent? activityContent = null,
		TimelineReference? crossReference = null)
		=> new(
			kind,
			GetAuthorName(actor?.Login),
			actor?.AvatarUrl,
			FormatDate(createdAt, createdAtHumanized),
			string.Empty,
			string.Empty,
			message,
			false,
			previousTitle,
			currentTitle,
			referenceLocation,
			referenceTitle,
			activityContent,
			crossReference);

	private static TimelineItemViewModel FallbackActivity(
		Actor? actor,
		DateTimeOffset createdAt,
		string? createdAtHumanized,
		object item)
		=> Activity(actor, createdAt, createdAtHumanized, GetFallbackMessage(item));

	private static TimelineItemViewModel LabelActivity(
		Actor? actor,
		DateTimeOffset createdAt,
		string? createdAtHumanized,
		string messageResourceKey,
		string richMessageResourceKey,
		Label? label)
	{
		var message = messageResourceKey.GetLocalized();
		var labelName = label?.Name;
		if (string.IsNullOrWhiteSpace(labelName))
			return Activity(
				actor,
				createdAt,
				createdAtHumanized,
				message);

		var richMessage = richMessageResourceKey.GetLocalized();
		if (richMessage == richMessageResourceKey)
			return Activity(actor, createdAt, createdAtHumanized, message);

		var content = TimelineActivityContent.FromLocalizedFormat(
			richMessage,
			TimelineActivityPart.Label(labelName, label?.Color));

		return Activity(
			actor,
			createdAt,
			createdAtHumanized,
			message,
			activityContent: content);
	}

	private static TimelineItemViewModel TitleChangedActivity(RenamedTitleEvent value)
	{
		if (string.IsNullOrWhiteSpace(value.PreviousTitle) || string.IsNullOrWhiteSpace(value.CurrentTitle))
			return Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, Strings.Timeline_RenamedTitle.GetLocalized());

		var message = Strings.Timeline_RenamedTitle.GetLocalized();
		var richMessage = Strings.Timeline_RenamedTitleWithValues.GetLocalized();
		if (richMessage == Strings.Timeline_RenamedTitleWithValues)
			return Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, message);

		var content = TimelineActivityContent.FromLocalizedFormat(
			richMessage,
			TimelineActivityPart.Strikethrough(value.PreviousTitle),
			TimelineActivityPart.FromText(value.CurrentTitle));

		return Activity(
			value.Actor,
			value.CreatedAt,
			value.CreatedAtHumanized,
			message,
			TimelineItemKind.TitleChanged,
			previousTitle: value.PreviousTitle,
			currentTitle: value.CurrentTitle,
			activityContent: content);
	}

	private static TimelineItemViewModel CrossReferenceActivity(CrossReferencedEvent value)
	{
		var details = GetReferenceDetails(value.Source);
		var message = Strings.Timeline_CrossReferenced.GetLocalized();
		if (details is null)
			return Activity(value.Actor, value.CreatedAt, value.CreatedAtHumanized, message);

		return Activity(
			value.Actor,
			value.CreatedAt,
			value.CreatedAtHumanized,
			message,
			TimelineItemKind.CrossReferenced,
			referenceLocation: details.Location,
			referenceTitle: details.Title,
			crossReference: details);
	}

	private static TimelineReference? GetReferenceDetails(ReferencedSubject? source)
	{
		if (source?.Issue is { } issue)
			return CreateReference(issue.Number, issue.Repository, issue.Title, issue.Url);

		if (source?.PullRequest is { } pullRequest)
			return CreateReference(pullRequest.Number, pullRequest.Repository, pullRequest.Title, pullRequest.Url);

		return null;
	}

	private static TimelineReference CreateReference(
		int number,
		Repository? repository,
		string title,
		string? url)
	{
		var location = FormatReference(number, repository) ?? title;
		var uri = Uri.TryCreate(url, UriKind.Absolute, out var parsedUri) ? parsedUri : null;
		return new TimelineReference(location, title, uri);
	}

	private static string? FormatReference(int number, Repository? repository)
	{
		var repositoryName = repository is null
			? null
			: !string.IsNullOrWhiteSpace(repository.NameWithOwner)
				? repository.NameWithOwner
				: !string.IsNullOrWhiteSpace(repository.Owner?.Login) && !string.IsNullOrWhiteSpace(repository.Name)
					? $"{repository.Owner.Login}/{repository.Name}"
					: repository.Name;

		if (string.IsNullOrWhiteSpace(repositoryName))
			return number > 0 ? $"#{number}" : null;

		return number > 0 ? $"{repositoryName}#{number}" : repositoryName;
	}

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
