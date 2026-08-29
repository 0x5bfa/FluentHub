// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Core.Infrastructure.GitHub.Queries.Repositories;

internal static class TimelineQueries
{
	public const string Issue = """
		query($owner: String!, $name: String!, $number: Int!) {
		  result: repository(owner: $owner, name: $name) {
		    issue(number: $number) {
		      timelineItems(first: 40) {
		        nodes {
		          __typename
		          ... on AddedToProjectEvent {
		            createdAt id actor { avatarUrl(size: 500) login }
		          }
		          ... on AssignedEvent {
		            createdAt id actor { avatarUrl(size: 500) login }
		            assignee {
		              __typename
		              ... on Bot { login }
		              ... on Mannequin { login }
		              ... on Organization { login }
		              ... on User { login }
		            }
		          }
		          ... on ClosedEvent {
		            createdAt id stateReason actor { avatarUrl(size: 500) login }
		            closer {
		              __typename
		              ... on Commit { message }
		              ... on PullRequest { title }
		            }
		          }
		          ... on CommentDeletedEvent {
		            createdAt id actor { avatarUrl(size: 500) login }
		            deletedCommentAuthor { avatarUrl(size: 500) login }
		          }
		          ... on ConnectedEvent {
		            createdAt id isCrossRepository actor { avatarUrl(size: 500) login }
		            source {
		              __typename
		              ... on Issue { title repository { name owner { avatarUrl(size: 500) login } } }
		              ... on PullRequest { title repository { name owner { avatarUrl(size: 500) login } } }
		            }
		            subject {
		              __typename
		              ... on Issue { title repository { name owner { avatarUrl(size: 500) login } } }
		              ... on PullRequest { title repository { name owner { avatarUrl(size: 500) login } } }
		            }
		          }
		          ... on ConvertedNoteToIssueEvent {
		            createdAt id actor { avatarUrl(size: 500) login }
		          }
		          ... on ConvertedToDiscussionEvent {
		            createdAt id actor { avatarUrl(size: 500) login }
		            discussion { number title }
		          }
		          ... on CrossReferencedEvent {
		            createdAt id isCrossRepository referencedAt willCloseTarget
		            actor { avatarUrl(size: 500) login }
		            source {
		              __typename
		              ... on Issue { number title repository { name owner { avatarUrl(size: 500) login } } }
		              ... on PullRequest { number title repository { name owner { avatarUrl(size: 500) login } } }
		            }
		            target {
		              __typename
		              ... on Issue { number title repository { name owner { avatarUrl(size: 500) login } } }
		              ... on PullRequest { number title repository { name owner { avatarUrl(size: 500) login } } }
		            }
		          }
		          ... on DemilestonedEvent {
		            createdAt id milestoneTitle actor { avatarUrl(size: 500) login }
		          }
		          ... on DisconnectedEvent {
		            createdAt id isCrossRepository actor { avatarUrl(size: 500) login }
		            source {
		              __typename
		              ... on Issue { title repository { name owner { avatarUrl(size: 500) login } } }
		              ... on PullRequest { title repository { name owner { avatarUrl(size: 500) login } } }
		            }
		            subject {
		              __typename
		              ... on Issue { title repository { name owner { avatarUrl(size: 500) login } } }
		              ... on PullRequest { title repository { name owner { avatarUrl(size: 500) login } } }
		            }
		          }
		          ... on IssueComment {
		            authorAssociation body createdAt id isMinimized lastEditedAt minimizedReason
		            updatedAt url viewerCanDelete viewerCanMinimize viewerCanReact viewerCanUpdate viewerDidAuthor
		            author { avatarUrl(size: 500) login }
		            reactionGroups { content viewerHasReacted reactors { totalCount } }
		          }
		          ... on LabeledEvent {
		            createdAt id actor { avatarUrl(size: 500) login }
		            label { color description name }
		          }
		          ... on LockedEvent {
		            createdAt id lockReason actor { avatarUrl(size: 500) login }
		          }
		          ... on MarkedAsDuplicateEvent {
		            createdAt id actor { avatarUrl(size: 500) login }
		            canonical {
		              __typename
		              ... on Issue { title }
		              ... on PullRequest { title }
		            }
		            duplicate {
		              __typename
		              ... on Issue { title }
		              ... on PullRequest { title }
		            }
		          }
		          ... on MentionedEvent {
		            createdAt id actor { avatarUrl(size: 500) login }
		          }
		          ... on MilestonedEvent {
		            createdAt id milestoneTitle actor { avatarUrl(size: 500) login }
		          }
		          ... on MovedColumnsInProjectEvent {
		            createdAt id actor { avatarUrl(size: 500) login }
		          }
		          ... on PinnedEvent {
		            createdAt id actor { avatarUrl(size: 500) login }
		          }
		          ... on ReferencedEvent {
		            createdAt id isCrossRepository isDirectReference actor { avatarUrl(size: 500) login }
		            commit { message }
		            commitRepository { name owner { avatarUrl(size: 500) login } }
		          }
		          ... on RemovedFromProjectEvent {
		            createdAt id actor { avatarUrl(size: 500) login }
		          }
		          ... on RenamedTitleEvent {
		            createdAt currentTitle id previousTitle actor { avatarUrl(size: 500) login }
		          }
		          ... on ReopenedEvent {
		            createdAt id stateReason actor { avatarUrl(size: 500) login }
		          }
		          ... on SubscribedEvent {
		            createdAt id actor { avatarUrl(size: 500) login }
		          }
		          ... on TransferredEvent {
		            createdAt id actor { avatarUrl(size: 500) login }
		            fromRepository { name owner { avatarUrl(size: 500) login } }
		          }
		          ... on UnassignedEvent {
		            createdAt id actor { avatarUrl(size: 500) login }
		            assignee {
		              __typename
		              ... on Bot { login }
		              ... on Mannequin { login }
		              ... on Organization { login }
		              ... on User { login }
		            }
		          }
		          ... on UnlabeledEvent {
		            createdAt id actor { avatarUrl(size: 500) login }
		            label { color description name }
		          }
		          ... on UnlockedEvent {
		            createdAt id actor { avatarUrl(size: 500) login }
		          }
		          ... on UnmarkedAsDuplicateEvent {
		            createdAt id actor { avatarUrl(size: 500) login }
		            canonical {
		              __typename
		              ... on Issue { title }
		              ... on PullRequest { title }
		            }
		            duplicate {
		              __typename
		              ... on Issue { title }
		              ... on PullRequest { title }
		            }
		          }
		          ... on UnpinnedEvent {
		            createdAt id actor { avatarUrl(size: 500) login }
		          }
		          ... on UnsubscribedEvent {
		            createdAt id actor { avatarUrl(size: 500) login }
		          }
		          ... on UserBlockedEvent {
		            blockDuration createdAt id actor { avatarUrl(size: 500) login }
		            subject { login }
		          }
		        }
		      }
		    }
		  }
		}
		""";

	public const string PullRequest = """
		query($owner: String!, $name: String!, $number: Int!) {
		  result: repository(owner: $owner, name: $name) {
		    pullRequest(number: $number) {
		      timelineItems(first: 40) {
		        nodes {
		          __typename
		          ... on AddedToProjectEvent { actor { avatarUrl(size: 500) login } createdAt }
		          ... on AssignedEvent {
		            actor { avatarUrl(size: 500) login }
		            assignee { __typename ... on User { avatarUrl(size: 500) login } }
		            createdAt
		          }
		          ... on AutoMergeDisabledEvent {
		            createdAt id reason reasonCode actor { avatarUrl(size: 500) login }
		          }
		          ... on AutoMergeEnabledEvent { createdAt actor { avatarUrl(size: 500) login } }
		          ... on AutoRebaseEnabledEvent { createdAt actor { avatarUrl(size: 500) login } }
		          ... on AutoSquashEnabledEvent { createdAt actor { avatarUrl(size: 500) login } }
		          ... on AutomaticBaseChangeFailedEvent {
		            createdAt id newBase oldBase actor { avatarUrl(size: 500) login }
		          }
		          ... on AutomaticBaseChangeSucceededEvent {
		            createdAt id newBase oldBase actor { avatarUrl(size: 500) login }
		          }
		          ... on BaseRefChangedEvent {
		            createdAt currentRefName id previousRefName actor { avatarUrl(size: 500) login }
		          }
		          ... on BaseRefDeletedEvent {
		            baseRefName createdAt id actor { avatarUrl(size: 500) login }
		          }
		          ... on BaseRefForcePushedEvent {
		            createdAt id actor { avatarUrl(size: 500) login }
		            afterCommit { message }
		            beforeCommit { message }
		          }
		          ... on ClosedEvent { actor { avatarUrl(size: 500) login } createdAt id }
		          ... on CommentDeletedEvent {
		            actor { avatarUrl(size: 500) login }
		            deletedCommentAuthor { avatarUrl(size: 500) login }
		            createdAt
		          }
		          ... on ConnectedEvent {
		            actor { avatarUrl(size: 500) login } createdAt id isCrossRepository
		            source {
		              __typename
		              ... on Issue { title }
		              ... on PullRequest { title }
		            }
		            subject {
		              __typename
		              ... on Issue { title }
		              ... on PullRequest { title }
		            }
		          }
		          ... on ConvertToDraftEvent { actor { avatarUrl(size: 500) login } createdAt id }
		          ... on ConvertedNoteToIssueEvent { actor { avatarUrl(size: 500) login } createdAt }
		          ... on ConvertedToDiscussionEvent { actor { avatarUrl(size: 500) login } createdAt id }
		          ... on CrossReferencedEvent {
		            actor { avatarUrl(size: 500) login } createdAt id isCrossRepository referencedAt url willCloseTarget
		            source {
		              __typename
		              ... on Issue { title }
		              ... on PullRequest { title }
		            }
		            target {
		              __typename
		              ... on Issue { title }
		              ... on PullRequest { title }
		            }
		          }
		          ... on DemilestonedEvent {
		            actor { avatarUrl(size: 500) login } milestoneTitle createdAt
		          }
		          ... on DeployedEvent {
		            actor { avatarUrl(size: 500) login } createdAt deployment { description } id ref { name }
		          }
		          ... on DeploymentEnvironmentChangedEvent {
		            actor { avatarUrl(size: 500) login } deploymentStatus { description } createdAt
		          }
		          ... on DisconnectedEvent {
		            actor { avatarUrl(size: 500) login } createdAt id isCrossRepository
		            source {
		              __typename
		              ... on Issue { title }
		              ... on PullRequest { title }
		            }
		            subject {
		              __typename
		              ... on Issue { title }
		              ... on PullRequest { title }
		            }
		          }
		          ... on HeadRefDeletedEvent {
		            actor { avatarUrl(size: 500) login } createdAt headRef { name } headRefName
		          }
		          ... on HeadRefForcePushedEvent {
		            actor { avatarUrl(size: 500) login } afterCommit { message } beforeCommit { message } createdAt id
		          }
		          ... on HeadRefRestoredEvent { actor { avatarUrl(size: 500) login } createdAt }
		          ... on IssueComment {
		            authorAssociation body createdAt id isMinimized lastEditedAt minimizedReason updatedAt url
		            viewerCanDelete viewerCanMinimize viewerCanReact viewerCanUpdate viewerDidAuthor
		            author { avatarUrl(size: 500) login }
		            reactionGroups { content viewerHasReacted reactors { totalCount } }
		          }
		          ... on LabeledEvent {
		            actor { avatarUrl(size: 500) login } label { color description name } createdAt
		          }
		          ... on LockedEvent { actor { avatarUrl(size: 500) login } createdAt lockReason }
		          ... on MarkedAsDuplicateEvent {
		            actor { avatarUrl(size: 500) login }
		            duplicate {
		              __typename
		              ... on Issue { title }
		              ... on PullRequest { title }
		            }
		            createdAt
		          }
		          ... on MentionedEvent { actor { avatarUrl(size: 500) login } createdAt id }
		          ... on MergedEvent { actor { avatarUrl(size: 500) login } mergeRef { name } mergeRefName createdAt }
		          ... on MilestonedEvent { actor { avatarUrl(size: 500) login } createdAt milestoneTitle }
		          ... on MovedColumnsInProjectEvent { actor { avatarUrl(size: 500) login } createdAt }
		          ... on PinnedEvent { actor { avatarUrl(size: 500) login } createdAt }
		          ... on PullRequestCommit {
		            id
		            commit {
		              author { avatarUrl(size: 500) user { login } }
		              message
		            }
		          }
		          ... on PullRequestCommitCommentThread { id }
		          ... on PullRequestReview {
		            id
		            commit { author { avatarUrl(size: 500) user { login } } }
		            authorAssociation body bodyHTML createdAt lastEditedAt updatedAt url
		            viewerCanDelete viewerCanReact viewerCanUpdate viewerDidAuthor
		          }
		          ... on PullRequestReviewThread { id }
		          ... on PullRequestRevisionMarker { createdAt }
		          ... on ReadyForReviewEvent { actor { avatarUrl(size: 500) login } createdAt }
		          ... on ReferencedEvent {
		            actor { avatarUrl(size: 500) login }
		            commit { message }
		            commitRepository { owner { avatarUrl(size: 500) login } name }
		            createdAt id isCrossRepository isDirectReference
		          }
		          ... on RemovedFromProjectEvent { actor { avatarUrl(size: 500) login } id createdAt }
		          ... on RenamedTitleEvent { actor { avatarUrl(size: 500) login } currentTitle previousTitle createdAt }
		          ... on ReopenedEvent { actor { avatarUrl(size: 500) login } stateReason createdAt }
		          ... on ReviewDismissedEvent { actor { avatarUrl(size: 500) login } createdAt dismissalMessage }
		          ... on ReviewRequestRemovedEvent {
		            actor { avatarUrl(size: 500) login }
		            requestedReviewer { __typename ... on User { avatarUrl(size: 500) login } }
		            createdAt
		          }
		          ... on ReviewRequestedEvent {
		            actor { avatarUrl(size: 500) login }
		            requestedReviewer { __typename ... on User { avatarUrl(size: 500) login } }
		            createdAt
		          }
		          ... on SubscribedEvent { actor { avatarUrl(size: 500) login } createdAt id }
		          ... on TransferredEvent {
		            actor { avatarUrl(size: 500) login }
		            fromRepository { owner { avatarUrl(size: 500) login } name }
		            createdAt
		          }
		          ... on UnassignedEvent {
		            actor { avatarUrl(size: 500) login }
		            assignee { __typename ... on User { avatarUrl(size: 500) login } }
		            createdAt
		          }
		          ... on UnlabeledEvent {
		            actor { avatarUrl(size: 500) login } label { color description name } createdAt
		          }
		          ... on UnlockedEvent { actor { avatarUrl(size: 500) login } createdAt }
		          ... on UnmarkedAsDuplicateEvent {
		            actor { avatarUrl(size: 500) login }
		            duplicate {
		              __typename
		              ... on Issue { title }
		              ... on PullRequest { title }
		            }
		            createdAt
		          }
		          ... on UnpinnedEvent { actor { avatarUrl(size: 500) login } createdAt }
		          ... on UnsubscribedEvent { actor { avatarUrl(size: 500) login } createdAt id }
		          ... on UserBlockedEvent { actor { avatarUrl(size: 500) login } blockDuration id createdAt }
		        }
		      }
		    }
		  }
		}
		""";
}
