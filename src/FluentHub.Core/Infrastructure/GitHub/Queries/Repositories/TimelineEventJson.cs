// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace FluentHub.Core.Infrastructure.GitHub.Queries.Repositories;

internal static class TimelineEventJson
{
	public static List<object> Read(JsonElement response, string itemName)
	{
		if (!response.TryGetProperty("result", out var repository) ||
			repository.ValueKind != JsonValueKind.Object ||
			!repository.TryGetProperty(itemName, out var item) ||
			item.ValueKind != JsonValueKind.Object ||
			!item.TryGetProperty("timelineItems", out var timelineItems) ||
			!timelineItems.TryGetProperty("nodes", out var nodes) ||
			nodes.ValueKind != JsonValueKind.Array)
		{
			return [];
		}

		var result = new List<object>();
		foreach (var node in nodes.EnumerateArray())
		{
			var value = Deserialize(node);
			if (value is null)
				continue;

			PopulateUnionMembers(value, node);
			SetHumanizedCreatedAt(value);
			result.Add(value);
		}

		return result;
	}

	private static object? Deserialize(JsonElement node)
	{
		if (!node.TryGetProperty("__typename", out var typeNameElement))
			return null;

		return typeNameElement.GetString() switch
		{
			nameof(AddedToProjectEvent) => Deserialize<AddedToProjectEvent>(node),
			nameof(AssignedEvent) => Deserialize<AssignedEvent>(node),
			nameof(AutoMergeDisabledEvent) => Deserialize<AutoMergeDisabledEvent>(node),
			nameof(AutoMergeEnabledEvent) => Deserialize<AutoMergeEnabledEvent>(node),
			nameof(AutoRebaseEnabledEvent) => Deserialize<AutoRebaseEnabledEvent>(node),
			nameof(AutoSquashEnabledEvent) => Deserialize<AutoSquashEnabledEvent>(node),
			nameof(AutomaticBaseChangeFailedEvent) => Deserialize<AutomaticBaseChangeFailedEvent>(node),
			nameof(AutomaticBaseChangeSucceededEvent) => Deserialize<AutomaticBaseChangeSucceededEvent>(node),
			nameof(BaseRefChangedEvent) => Deserialize<BaseRefChangedEvent>(node),
			nameof(BaseRefDeletedEvent) => Deserialize<BaseRefDeletedEvent>(node),
			nameof(BaseRefForcePushedEvent) => Deserialize<BaseRefForcePushedEvent>(node),
			nameof(ClosedEvent) => Deserialize<ClosedEvent>(node),
			nameof(CommentDeletedEvent) => Deserialize<CommentDeletedEvent>(node),
			nameof(ConnectedEvent) => Deserialize<ConnectedEvent>(node),
			nameof(ConvertToDraftEvent) => Deserialize<ConvertToDraftEvent>(node),
			nameof(ConvertedNoteToIssueEvent) => Deserialize<ConvertedNoteToIssueEvent>(node),
			nameof(ConvertedToDiscussionEvent) => Deserialize<ConvertedToDiscussionEvent>(node),
			nameof(CrossReferencedEvent) => Deserialize<CrossReferencedEvent>(node),
			nameof(DemilestonedEvent) => Deserialize<DemilestonedEvent>(node),
			nameof(DeployedEvent) => Deserialize<DeployedEvent>(node),
			nameof(DeploymentEnvironmentChangedEvent) => Deserialize<DeploymentEnvironmentChangedEvent>(node),
			nameof(DisconnectedEvent) => Deserialize<DisconnectedEvent>(node),
			nameof(HeadRefDeletedEvent) => Deserialize<HeadRefDeletedEvent>(node),
			nameof(HeadRefForcePushedEvent) => Deserialize<HeadRefForcePushedEvent>(node),
			nameof(HeadRefRestoredEvent) => Deserialize<HeadRefRestoredEvent>(node),
			nameof(IssueComment) => Deserialize<IssueComment>(node),
			nameof(LabeledEvent) => Deserialize<LabeledEvent>(node),
			nameof(LockedEvent) => Deserialize<LockedEvent>(node),
			nameof(MarkedAsDuplicateEvent) => Deserialize<MarkedAsDuplicateEvent>(node),
			nameof(MentionedEvent) => Deserialize<MentionedEvent>(node),
			nameof(MergedEvent) => Deserialize<MergedEvent>(node),
			nameof(MilestonedEvent) => Deserialize<MilestonedEvent>(node),
			nameof(MovedColumnsInProjectEvent) => Deserialize<MovedColumnsInProjectEvent>(node),
			nameof(PinnedEvent) => Deserialize<PinnedEvent>(node),
			nameof(PullRequestCommit) => Deserialize<PullRequestCommit>(node),
			nameof(PullRequestCommitCommentThread) => Deserialize<PullRequestCommitCommentThread>(node),
			nameof(PullRequestReview) => Deserialize<PullRequestReview>(node),
			nameof(PullRequestReviewThread) => Deserialize<PullRequestReviewThread>(node),
			nameof(PullRequestRevisionMarker) => Deserialize<PullRequestRevisionMarker>(node),
			nameof(ReadyForReviewEvent) => Deserialize<ReadyForReviewEvent>(node),
			nameof(ReferencedEvent) => Deserialize<ReferencedEvent>(node),
			nameof(RemovedFromProjectEvent) => Deserialize<RemovedFromProjectEvent>(node),
			nameof(RenamedTitleEvent) => Deserialize<RenamedTitleEvent>(node),
			nameof(ReopenedEvent) => Deserialize<ReopenedEvent>(node),
			nameof(ReviewDismissedEvent) => Deserialize<ReviewDismissedEvent>(node),
			nameof(ReviewRequestRemovedEvent) => Deserialize<ReviewRequestRemovedEvent>(node),
			nameof(ReviewRequestedEvent) => Deserialize<ReviewRequestedEvent>(node),
			nameof(SubscribedEvent) => Deserialize<SubscribedEvent>(node),
			nameof(TransferredEvent) => Deserialize<TransferredEvent>(node),
			nameof(UnassignedEvent) => Deserialize<UnassignedEvent>(node),
			nameof(UnlabeledEvent) => Deserialize<UnlabeledEvent>(node),
			nameof(UnlockedEvent) => Deserialize<UnlockedEvent>(node),
			nameof(UnmarkedAsDuplicateEvent) => Deserialize<UnmarkedAsDuplicateEvent>(node),
			nameof(UnpinnedEvent) => Deserialize<UnpinnedEvent>(node),
			nameof(UnsubscribedEvent) => Deserialize<UnsubscribedEvent>(node),
			nameof(UserBlockedEvent) => Deserialize<UserBlockedEvent>(node),
			_ => null,
		};
	}

	private static T? Deserialize<T>(JsonElement node)
	{
		var typeInfo = (JsonTypeInfo<T>)(TimelineEventJsonContext.Default.GetTypeInfo(typeof(T))
			?? throw new InvalidOperationException($"No timeline JSON metadata is registered for {typeof(T)}."));
		return System.Text.Json.JsonSerializer.Deserialize(node, typeInfo);
	}

	private static void PopulateUnionMembers(object value, JsonElement node)
	{
		switch (value)
		{
			case AssignedEvent assigned:
				assigned.Assignee = ReadAssignee(node, "assignee");
				break;
			case UnassignedEvent unassigned:
				unassigned.Assignee = ReadAssignee(node, "assignee");
				break;
			case ClosedEvent closed:
				closed.Closer = ReadCloser(node, "closer");
				break;
			case ConnectedEvent connected:
				connected.Source = ReadReferencedSubject(node, "source") ?? new();
				connected.Subject = ReadReferencedSubject(node, "subject") ?? new();
				break;
			case DisconnectedEvent disconnected:
				disconnected.Source = ReadReferencedSubject(node, "source") ?? new();
				disconnected.Subject = ReadReferencedSubject(node, "subject") ?? new();
				break;
			case CrossReferencedEvent crossReferenced:
				crossReferenced.Source = ReadReferencedSubject(node, "source") ?? new();
				crossReferenced.Target = ReadReferencedSubject(node, "target") ?? new();
				break;
			case MarkedAsDuplicateEvent marked:
				marked.Canonical = ReadIssueOrPullRequest(node, "canonical");
				marked.Duplicate = ReadIssueOrPullRequest(node, "duplicate");
				break;
			case UnmarkedAsDuplicateEvent unmarked:
				unmarked.Canonical = ReadIssueOrPullRequest(node, "canonical");
				unmarked.Duplicate = ReadIssueOrPullRequest(node, "duplicate");
				break;
			case ReviewRequestRemovedEvent removed:
				removed.RequestedReviewer = ReadRequestedReviewer(node, "requestedReviewer");
				break;
			case ReviewRequestedEvent requested:
				requested.RequestedReviewer = ReadRequestedReviewer(node, "requestedReviewer");
				break;
		}
	}

	private static Assignee? ReadAssignee(JsonElement parent, string propertyName)
	{
		if (!TryReadUnion(parent, propertyName, out var value, out var typeName))
			return null;

		return typeName switch
		{
			nameof(Bot) => new Assignee { Bot = Deserialize<Bot>(value) },
			nameof(Mannequin) => new Assignee { Mannequin = Deserialize<Mannequin>(value) },
			nameof(Organization) => new Assignee { Organization = Deserialize<Organization>(value) },
			nameof(User) => new Assignee { User = Deserialize<User>(value) },
			_ => null,
		};
	}

	private static Closer? ReadCloser(JsonElement parent, string propertyName)
	{
		if (!TryReadUnion(parent, propertyName, out var value, out var typeName))
			return null;

		return typeName switch
		{
			nameof(Commit) => new Closer { Commit = Deserialize<Commit>(value) },
			nameof(PullRequest) => new Closer { PullRequest = Deserialize<PullRequest>(value) },
			_ => null,
		};
	}

	private static ReferencedSubject? ReadReferencedSubject(JsonElement parent, string propertyName)
	{
		if (!TryReadUnion(parent, propertyName, out var value, out var typeName))
			return null;

		return typeName switch
		{
			nameof(Issue) => new ReferencedSubject { Issue = Deserialize<Issue>(value) },
			nameof(PullRequest) => new ReferencedSubject { PullRequest = Deserialize<PullRequest>(value) },
			_ => null,
		};
	}

	private static IssueOrPullRequest? ReadIssueOrPullRequest(JsonElement parent, string propertyName)
	{
		if (!TryReadUnion(parent, propertyName, out var value, out var typeName))
			return null;

		return typeName switch
		{
			nameof(Issue) => new IssueOrPullRequest { Issue = Deserialize<Issue>(value) },
			nameof(PullRequest) => new IssueOrPullRequest { PullRequest = Deserialize<PullRequest>(value) },
			_ => null,
		};
	}

	private static RequestedReviewer? ReadRequestedReviewer(JsonElement parent, string propertyName)
	{
		if (!TryReadUnion(parent, propertyName, out var value, out var typeName))
			return null;

		return typeName switch
		{
			nameof(Bot) => new RequestedReviewer { Bot = Deserialize<Bot>(value) },
			nameof(Mannequin) => new RequestedReviewer { Mannequin = Deserialize<Mannequin>(value) },
			nameof(User) => new RequestedReviewer { User = Deserialize<User>(value) },
			_ => null,
		};
	}

	private static bool TryReadUnion(
		JsonElement parent,
		string propertyName,
		out JsonElement value,
		out string? typeName)
	{
		if (parent.TryGetProperty(propertyName, out value) &&
			value.ValueKind == JsonValueKind.Object &&
			value.TryGetProperty("__typename", out var typeNameElement))
		{
			typeName = typeNameElement.GetString();
			return true;
		}

		typeName = null;
		return false;
	}

	private static void SetHumanizedCreatedAt(object value)
	{
		switch (value)
		{
			case AddedToProjectEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case AssignedEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case AutoMergeDisabledEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case AutoMergeEnabledEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case AutoRebaseEnabledEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case AutoSquashEnabledEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case AutomaticBaseChangeFailedEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case AutomaticBaseChangeSucceededEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case BaseRefChangedEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case BaseRefDeletedEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case BaseRefForcePushedEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case ClosedEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case CommentDeletedEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case ConnectedEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case ConvertToDraftEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case ConvertedNoteToIssueEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case ConvertedToDiscussionEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case CrossReferencedEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case DemilestonedEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case DeployedEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case DeploymentEnvironmentChangedEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case DisconnectedEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case HeadRefDeletedEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case HeadRefForcePushedEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case HeadRefRestoredEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case IssueComment item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case LabeledEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case LockedEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case MarkedAsDuplicateEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case MentionedEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case MergedEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case MilestonedEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case MovedColumnsInProjectEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case PinnedEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case PullRequestReview item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case PullRequestRevisionMarker item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case ReadyForReviewEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case ReferencedEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case RemovedFromProjectEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case RenamedTitleEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case ReopenedEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case ReviewDismissedEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case ReviewRequestRemovedEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case ReviewRequestedEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case SubscribedEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case TransferredEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case UnassignedEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case UnlabeledEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case UnlockedEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case UnmarkedAsDuplicateEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case UnpinnedEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case UnsubscribedEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
			case UserBlockedEvent item: item.CreatedAtHumanized = item.CreatedAt.ToRelativeTime(); break;
		}
	}
}

[JsonSourceGenerationOptions(
	PropertyNameCaseInsensitive = true,
	UseStringEnumConverter = true)]
[JsonSerializable(typeof(AddedToProjectEvent))]
[JsonSerializable(typeof(AssignedEvent))]
[JsonSerializable(typeof(AutoMergeDisabledEvent))]
[JsonSerializable(typeof(AutoMergeEnabledEvent))]
[JsonSerializable(typeof(AutoRebaseEnabledEvent))]
[JsonSerializable(typeof(AutoSquashEnabledEvent))]
[JsonSerializable(typeof(AutomaticBaseChangeFailedEvent))]
[JsonSerializable(typeof(AutomaticBaseChangeSucceededEvent))]
[JsonSerializable(typeof(BaseRefChangedEvent))]
[JsonSerializable(typeof(BaseRefDeletedEvent))]
[JsonSerializable(typeof(BaseRefForcePushedEvent))]
[JsonSerializable(typeof(ClosedEvent))]
[JsonSerializable(typeof(CommentDeletedEvent))]
[JsonSerializable(typeof(ConnectedEvent))]
[JsonSerializable(typeof(ConvertToDraftEvent))]
[JsonSerializable(typeof(ConvertedNoteToIssueEvent))]
[JsonSerializable(typeof(ConvertedToDiscussionEvent))]
[JsonSerializable(typeof(CrossReferencedEvent))]
[JsonSerializable(typeof(DemilestonedEvent))]
[JsonSerializable(typeof(DeployedEvent))]
[JsonSerializable(typeof(DeploymentEnvironmentChangedEvent))]
[JsonSerializable(typeof(DisconnectedEvent))]
[JsonSerializable(typeof(HeadRefDeletedEvent))]
[JsonSerializable(typeof(HeadRefForcePushedEvent))]
[JsonSerializable(typeof(HeadRefRestoredEvent))]
[JsonSerializable(typeof(IssueComment))]
[JsonSerializable(typeof(LabeledEvent))]
[JsonSerializable(typeof(LockedEvent))]
[JsonSerializable(typeof(MarkedAsDuplicateEvent))]
[JsonSerializable(typeof(MentionedEvent))]
[JsonSerializable(typeof(MergedEvent))]
[JsonSerializable(typeof(MilestonedEvent))]
[JsonSerializable(typeof(MovedColumnsInProjectEvent))]
[JsonSerializable(typeof(PinnedEvent))]
[JsonSerializable(typeof(PullRequestCommit))]
[JsonSerializable(typeof(PullRequestCommitCommentThread))]
[JsonSerializable(typeof(PullRequestReview))]
[JsonSerializable(typeof(PullRequestReviewThread))]
[JsonSerializable(typeof(PullRequestRevisionMarker))]
[JsonSerializable(typeof(ReadyForReviewEvent))]
[JsonSerializable(typeof(ReferencedEvent))]
[JsonSerializable(typeof(RemovedFromProjectEvent))]
[JsonSerializable(typeof(RenamedTitleEvent))]
[JsonSerializable(typeof(ReopenedEvent))]
[JsonSerializable(typeof(ReviewDismissedEvent))]
[JsonSerializable(typeof(ReviewRequestRemovedEvent))]
[JsonSerializable(typeof(ReviewRequestedEvent))]
[JsonSerializable(typeof(SubscribedEvent))]
[JsonSerializable(typeof(TransferredEvent))]
[JsonSerializable(typeof(UnassignedEvent))]
[JsonSerializable(typeof(UnlabeledEvent))]
[JsonSerializable(typeof(UnlockedEvent))]
[JsonSerializable(typeof(UnmarkedAsDuplicateEvent))]
[JsonSerializable(typeof(UnpinnedEvent))]
[JsonSerializable(typeof(UnsubscribedEvent))]
[JsonSerializable(typeof(UserBlockedEvent))]
[JsonSerializable(typeof(Bot))]
[JsonSerializable(typeof(Mannequin))]
[JsonSerializable(typeof(Organization))]
[JsonSerializable(typeof(User))]
[JsonSerializable(typeof(Commit))]
[JsonSerializable(typeof(Issue))]
[JsonSerializable(typeof(PullRequest))]
internal sealed partial class TimelineEventJsonContext : JsonSerializerContext;
