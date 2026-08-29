// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

#nullable enable

namespace FluentHub.Core.Application.Models
{
	// Application contracts projected by FluentHub's GitHub queries.
	// Keep these types limited to fields that the application actually requests or consumes.
	public enum CheckConclusionState
	{
		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("ACTION_REQUIRED")]
		ActionRequired,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("TIMED_OUT")]
		TimedOut,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("CANCELLED")]
		Cancelled,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("FAILURE")]
		Failure,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("SUCCESS")]
		Success,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("NEUTRAL")]
		Neutral,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("SKIPPED")]
		Skipped,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("STARTUP_FAILURE")]
		StartupFailure,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("STALE")]
		Stale
	}
	public enum CheckStatusState
	{
		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("REQUESTED")]
		Requested,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("QUEUED")]
		Queued,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("IN_PROGRESS")]
		InProgress,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("COMPLETED")]
		Completed,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("WAITING")]
		Waiting,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("PENDING")]
		Pending
	}
	public enum CommentAuthorAssociation
	{
		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("MEMBER")]
		Member,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("OWNER")]
		Owner,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("MANNEQUIN")]
		Mannequin,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("COLLABORATOR")]
		Collaborator,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("CONTRIBUTOR")]
		Contributor,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("FIRST_TIME_CONTRIBUTOR")]
		FirstTimeContributor,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("FIRST_TIMER")]
		FirstTimer,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("NONE")]
		None
	}
	public enum ComparisonStatus
	{
		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("DIVERGED")]
		Diverged,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("AHEAD")]
		Ahead,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("BEHIND")]
		Behind,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("IDENTICAL")]
		Identical
	}
	public enum ContributionLevel
	{
		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("NONE")]
		None,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("FIRST_QUARTILE")]
		FirstQuartile,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("SECOND_QUARTILE")]
		SecondQuartile,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("THIRD_QUARTILE")]
		ThirdQuartile,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("FOURTH_QUARTILE")]
		FourthQuartile
	}
	public enum DeploymentState
	{
		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("ABANDONED")]
		Abandoned,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("ACTIVE")]
		Active,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("DESTROYED")]
		Destroyed,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("ERROR")]
		Error,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("FAILURE")]
		Failure,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("INACTIVE")]
		Inactive,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("PENDING")]
		Pending,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("SUCCESS")]
		Success,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("QUEUED")]
		Queued,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("IN_PROGRESS")]
		InProgress,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("WAITING")]
		Waiting
	}
	public enum DeploymentStatusState
	{
		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("PENDING")]
		Pending,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("SUCCESS")]
		Success,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("FAILURE")]
		Failure,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("INACTIVE")]
		Inactive,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("ERROR")]
		Error,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("QUEUED")]
		Queued,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("IN_PROGRESS")]
		InProgress,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("WAITING")]
		Waiting
	}
	public enum DiscussionOrderField
	{
		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("CREATED_AT")]
		CreatedAt,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("UPDATED_AT")]
		UpdatedAt
	}
	public enum DiscussionStateReason
	{
		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("RESOLVED")]
		Resolved,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("OUTDATED")]
		Outdated,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("DUPLICATE")]
		Duplicate,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("REOPENED")]
		Reopened
	}
	public enum GitSignatureState
	{
		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("VALID")]
		Valid,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("INVALID")]
		Invalid,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("MALFORMED_SIG")]
		MalformedSig,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("UNKNOWN_KEY")]
		UnknownKey,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("BAD_EMAIL")]
		BadEmail,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("UNVERIFIED_EMAIL")]
		UnverifiedEmail,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("NO_USER")]
		NoUser,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("UNKNOWN_SIG_TYPE")]
		UnknownSigType,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("UNSIGNED")]
		Unsigned,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("GPGVERIFY_UNAVAILABLE")]
		GpgverifyUnavailable,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("GPGVERIFY_ERROR")]
		GpgverifyError,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("NOT_SIGNING_KEY")]
		NotSigningKey,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("EXPIRED_KEY")]
		ExpiredKey,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("OCSP_PENDING")]
		OcspPending,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("OCSP_ERROR")]
		OcspError,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("BAD_CERT")]
		BadCert,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("OCSP_REVOKED")]
		OcspRevoked
	}
	public enum IssueClosedStateReason
	{
		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("COMPLETED")]
		Completed,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("NOT_PLANNED")]
		NotPlanned,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("DUPLICATE")]
		Duplicate = 2
	}
	public enum IssueOrderField
	{
		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("CREATED_AT")]
		CreatedAt,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("UPDATED_AT")]
		UpdatedAt,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("COMMENTS")]
		Comments
	}
	public enum IssueState
	{
		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("OPEN")]
		Open,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("CLOSED")]
		Closed
	}
	public enum IssueStateReason
	{
		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("REOPENED")]
		Reopened,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("NOT_PLANNED")]
		NotPlanned,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("COMPLETED")]
		Completed,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("DUPLICATE")]
		Duplicate = 3
	}
	public enum IssueTypeColor
	{
		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("GRAY")]
		Gray = 0,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("BLUE")]
		Blue = 1,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("GREEN")]
		Green = 2,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("YELLOW")]
		Yellow = 3,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("ORANGE")]
		Orange = 4,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("RED")]
		Red = 5,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("PINK")]
		Pink = 6,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("PURPLE")]
		Purple = 7
	}
	public enum LockReason
	{
		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("OFF_TOPIC")]
		OffTopic,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("TOO_HEATED")]
		TooHeated,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("RESOLVED")]
		Resolved,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("SPAM")]
		Spam
	}
	public enum MergeableState
	{
		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("MERGEABLE")]
		Mergeable,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("CONFLICTING")]
		Conflicting,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("UNKNOWN")]
		Unknown
	}
	public enum MilestoneState
	{
		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("OPEN")]
		Open,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("CLOSED")]
		Closed
	}
	public enum OrderDirection
	{
		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("ASC")]
		Asc,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("DESC")]
		Desc
	}
	public enum PackageOrderField
	{
		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("CREATED_AT")]
		CreatedAt
	}
	public enum PackageType
	{
		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("NPM")]
		Npm,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("RUBYGEMS")]
		Rubygems,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("MAVEN")]
		Maven,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("DOCKER")]
		Docker,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("DEBIAN")]
		Debian,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("NUGET")]
		Nuget,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("PYPI")]
		Pypi
	}
	public enum ProjectCardState
	{
		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("CONTENT_ONLY")]
		ContentOnly,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("NOTE_ONLY")]
		NoteOnly,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("REDACTED")]
		Redacted
	}
	public enum ProjectState
	{
		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("OPEN")]
		Open,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("CLOSED")]
		Closed
	}
	public enum ProjectV2ItemType
	{
		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("ISSUE")]
		Issue,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("PULL_REQUEST")]
		PullRequest,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("DRAFT_ISSUE")]
		DraftIssue,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("REDACTED")]
		Redacted
	}
	public enum ProjectV2ViewLayout
	{
		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("BOARD_LAYOUT")]
		BoardLayout,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("TABLE_LAYOUT")]
		TableLayout,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("ROADMAP_LAYOUT")]
		RoadmapLayout
	}
	public enum PullRequestMergeMethod
	{
		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("MERGE")]
		Merge,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("SQUASH")]
		Squash,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("REBASE")]
		Rebase
	}
	public enum PullRequestReviewCommentState
	{
		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("PENDING")]
		Pending,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("SUBMITTED")]
		Submitted
	}
	public enum PullRequestReviewEvent
	{
		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("COMMENT")]
		Comment,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("APPROVE")]
		Approve,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("REQUEST_CHANGES")]
		RequestChanges,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("DISMISS")]
		Dismiss
	}
	public enum PullRequestReviewState
	{
		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("PENDING")]
		Pending,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("COMMENTED")]
		Commented,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("APPROVED")]
		Approved,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("CHANGES_REQUESTED")]
		ChangesRequested,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("DISMISSED")]
		Dismissed
	}
	public enum PullRequestState
	{
		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("OPEN")]
		Open,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("CLOSED")]
		Closed,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("MERGED")]
		Merged
	}
	public enum PullRequestUpdateState
	{
		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("OPEN")]
		Open,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("CLOSED")]
		Closed
	}
	public enum ReactionContent
	{
		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("THUMBS_UP")]
		ThumbsUp,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("THUMBS_DOWN")]
		ThumbsDown,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("LAUGH")]
		Laugh,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("HOORAY")]
		Hooray,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("CONFUSED")]
		Confused,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("HEART")]
		Heart,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("ROCKET")]
		Rocket,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("EYES")]
		Eyes
	}
	public enum ReleaseOrderField
	{
		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("CREATED_AT")]
		CreatedAt,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("NAME")]
		Name
	}
	public enum RepositoryAffiliation
	{
		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("OWNER")]
		Owner,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("COLLABORATOR")]
		Collaborator,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("ORGANIZATION_MEMBER")]
		OrganizationMember
	}
	public enum RepositoryLockReason
	{
		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("MOVING")]
		Moving,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("BILLING")]
		Billing,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("RENAME")]
		Rename,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("MIGRATING")]
		Migrating,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("TRADE_RESTRICTION")]
		TradeRestriction,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("TRANSFERRING_OWNERSHIP")]
		TransferringOwnership
	}
	public enum RepositoryOrderField
	{
		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("CREATED_AT")]
		CreatedAt,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("UPDATED_AT")]
		UpdatedAt,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("PUSHED_AT")]
		PushedAt,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("NAME")]
		Name,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("STARGAZERS")]
		Stargazers
	}
	public enum RepositoryPermission
	{
		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("ADMIN")]
		Admin,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("MAINTAIN")]
		Maintain,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("WRITE")]
		Write,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("TRIAGE")]
		Triage,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("READ")]
		Read
	}
	public enum RepositoryPrivacy
	{
		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("PUBLIC")]
		Public,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("PRIVATE")]
		Private
	}
	public enum RepositoryVisibility
	{
		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("PRIVATE")]
		Private,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("PUBLIC")]
		Public,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("INTERNAL")]
		Internal
	}
	public enum StarOrderField
	{
		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("STARRED_AT")]
		StarredAt
	}
	public enum StatusState
	{
		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("EXPECTED")]
		Expected,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("ERROR")]
		Error,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("FAILURE")]
		Failure,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("PENDING")]
		Pending,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("SUCCESS")]
		Success
	}
	public enum SubscriptionState
	{
		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("UNSUBSCRIBED")]
		Unsubscribed,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("SUBSCRIBED")]
		Subscribed,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("IGNORED")]
		Ignored
	}
	public enum UserBlockDuration
	{
		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("ONE_DAY")]
		OneDay,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("THREE_DAYS")]
		ThreeDays,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("ONE_WEEK")]
		OneWeek,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("ONE_MONTH")]
		OneMonth,

		[global::System.Text.Json.Serialization.JsonStringEnumMemberName("PERMANENT")]
		Permanent
	}

	public interface IActor
	{
		public string AvatarUrl { get; set; }

		public string Login { get; set; }

		public string ResourcePath { get; set; }

		public string Url { get; set; }
	}

	public interface IGitObject
	{
		public string AbbreviatedOid { get; set; }

		public string CommitUrl { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public string Oid { get; set; }

		public Repository Repository { get; set; }
	}

	public interface IGitSignature
	{
		public string Email { get; set; }

		public bool IsValid { get; set; }

		public string Payload { get; set; }

		public string Signature { get; set; }

		public User? Signer { get; set; }

		public GitSignatureState State { get; set; }

		public bool WasSignedByGitHub { get; set; }
	}

	public interface INode
	{
		public global::Octokit.GraphQL.ID Id { get; set; }
	}

	public interface IProjectOwner
	{
		public global::Octokit.GraphQL.ID Id { get; set; }

		public ProjectConnection Projects { get; set; }
	}

	public interface IProjectV2Owner
	{
		public global::Octokit.GraphQL.ID Id { get; set; }

		public ProjectV2? ProjectV2 { get; set; }

		public ProjectV2Connection ProjectsV2 { get; set; }
	}

	public interface IReactable
	{
		public global::Octokit.GraphQL.ID Id { get; set; }

		public global::System.Collections.Generic.List<ReactionGroup>? ReactionGroups { get; set; }

		public ReactionConnection Reactions { get; set; }

		public bool ViewerCanReact { get; set; }
	}

	public interface IRepositoryInfo
	{
		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public string? Description { get; set; }

		public string DescriptionHTML { get; set; }

		public int ForkCount { get; set; }

		public bool HasIssuesEnabled { get; set; }

		public bool HasProjectsEnabled { get; set; }

		public bool HasSponsorshipsEnabled { get; set; }

		public string? HomepageUrl { get; set; }

		public bool IsArchived { get; set; }

		public bool IsFork { get; set; }

		public bool IsInOrganization { get; set; }

		public bool IsMirror { get; set; }

		public bool IsPrivate { get; set; }

		public bool IsTemplate { get; set; }

		public License? LicenseInfo { get; set; }

		public RepositoryLockReason? LockReason { get; set; }

		public string Name { get; set; }

		public string NameWithOwner { get; set; }

		public RepositoryOwner Owner { get; set; }

		public string ResourcePath { get; set; }

		public global::System.DateTimeOffset UpdatedAt { get; set; }

		public string? UpdatedAtHumanized { get; set; }

		public string Url { get; set; }

		public RepositoryVisibility Visibility { get; set; }
	}

	public interface IRepositoryOwner
	{
		public string AvatarUrl { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public string Login { get; set; }

		public RepositoryConnection Repositories { get; set; }

		public Repository? Repository { get; set; }

		public string ResourcePath { get; set; }

		public string Url { get; set; }
	}

	public interface ISubscribable
	{
		public global::Octokit.GraphQL.ID Id { get; set; }

		public bool ViewerCanSubscribe { get; set; }

		public SubscriptionState? ViewerSubscription { get; set; }
	}

	public class Actor : IActor
	{
		public string AvatarUrl { get; set; } = default!;

		public string Login { get; set; } = default!;

		public string ResourcePath { get; set; } = default!;

		public string Url { get; set; } = default!;
	}

	public class AddCommentRequest
	{
		public string Body { get; set; } = default!;

		public string? ClientMutationId { get; set; }

		public global::Octokit.GraphQL.ID SubjectId { get; set; }
	}

	public class AddCommentResult
	{
		public string? ClientMutationId { get; set; }

		public IssueCommentEdge? CommentEdge { get; set; }

		public Node? Subject { get; set; }
	}

	public class AddPullRequestReviewRequest
	{
		public string? Body { get; set; }

		public string? ClientMutationId { get; set; }

		public global::System.Collections.Generic.List<DraftPullRequestReviewComment?>? Comments { get; set; }

		public string? CommitOID { get; set; }

		public PullRequestReviewEvent? Event { get; set; }

		public global::Octokit.GraphQL.ID PullRequestId { get; set; }

		public global::System.Collections.Generic.List<DraftPullRequestReviewThread?>? Threads { get; set; }
	}

	public class AddPullRequestReviewResult
	{
		public string? ClientMutationId { get; set; }

		public PullRequestReview? PullRequestReview { get; set; }
	}

	public class AddReactionRequest
	{
		public string? ClientMutationId { get; set; }

		public ReactionContent Content { get; set; }

		public global::Octokit.GraphQL.ID SubjectId { get; set; }
	}

	public class AddReactionResult
	{
		public string? ClientMutationId { get; set; }

		public Reaction? Reaction { get; set; }

		public global::System.Collections.Generic.List<ReactionGroup>? ReactionGroups { get; set; }

		public IReactable? Subject { get; set; }
	}

	public class AddStarResult
	{
		public string? ClientMutationId { get; set; }
	}

	public class AddedToProjectEvent
	{
		public Actor? Actor { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public ProjectCard? ProjectCard { get; set; }
	}

	public class App
	{
		public string? ClientId { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public string? Description { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public string LogoBackgroundColor { get; set; } = default!;

		public string LogoUrl { get; set; } = default!;

		public string Name { get; set; } = default!;

		public global::System.DateTimeOffset UpdatedAt { get; set; }

		public string? UpdatedAtHumanized { get; set; }

		public string Url { get; set; } = default!;
	}

	public class AssignedEvent
	{
		public Actor? Actor { get; set; }

		public Assignee? Assignee { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public User? User { get; set; }
	}

	public class Assignee
	{
		public Bot? Bot { get; set; }

		public Mannequin? Mannequin { get; set; }

		public Organization? Organization { get; set; }

		public User? User { get; set; }
	}

	public class AutoMergeDisabledEvent
	{
		public Actor? Actor { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public PullRequest? PullRequest { get; set; }

		public string? Reason { get; set; }

		public string? ReasonCode { get; set; }
	}

	public class AutoMergeEnabledEvent
	{
		public Actor? Actor { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public PullRequest? PullRequest { get; set; }
	}

	public class AutoRebaseEnabledEvent
	{
		public Actor? Actor { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public PullRequest? PullRequest { get; set; }
	}

	public class AutoSquashEnabledEvent
	{
		public Actor? Actor { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public PullRequest? PullRequest { get; set; }
	}

	public class AutomaticBaseChangeFailedEvent
	{
		public Actor? Actor { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public string NewBase { get; set; } = default!;

		public string OldBase { get; set; } = default!;

		public PullRequest PullRequest { get; set; } = default!;
	}

	public class AutomaticBaseChangeSucceededEvent
	{
		public Actor? Actor { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public string NewBase { get; set; } = default!;

		public string OldBase { get; set; } = default!;

		public PullRequest PullRequest { get; set; } = default!;
	}

	public class BaseRefChangedEvent
	{
		public Actor? Actor { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public string CurrentRefName { get; set; } = default!;

		public global::Octokit.GraphQL.ID Id { get; set; }

		public string PreviousRefName { get; set; } = default!;

		public PullRequest PullRequest { get; set; } = default!;
	}

	public class BaseRefDeletedEvent
	{
		public Actor? Actor { get; set; }

		public string? BaseRefName { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public PullRequest? PullRequest { get; set; }
	}

	public class BaseRefForcePushedEvent
	{
		public Actor? Actor { get; set; }

		public Commit? AfterCommit { get; set; }

		public Commit? BeforeCommit { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public PullRequest PullRequest { get; set; } = default!;

		public Ref? Ref { get; set; }
	}

	public class Blob
	{
		public string AbbreviatedOid { get; set; } = default!;

		public int ByteSize { get; set; }

		public string CommitUrl { get; set; } = default!;

		public global::Octokit.GraphQL.ID Id { get; set; }

		public bool? IsBinary { get; set; }

		public bool IsTruncated { get; set; }

		public string Oid { get; set; } = default!;

		public Repository Repository { get; set; } = default!;

		public string? Text { get; set; }
	}

	public class Bot
	{
		public string AvatarUrl { get; set; } = default!;

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public string Login { get; set; } = default!;

		public string ResourcePath { get; set; } = default!;

		public global::System.DateTimeOffset UpdatedAt { get; set; }

		public string? UpdatedAtHumanized { get; set; }

		public string Url { get; set; } = default!;
	}

	public class CheckRun
	{
		public CheckSuite CheckSuite { get; set; } = default!;

		public global::System.DateTimeOffset? CompletedAt { get; set; }

		public CheckConclusionState? Conclusion { get; set; }

		public Deployment? Deployment { get; set; }

		public string? DetailsUrl { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public string Name { get; set; } = default!;

		public Repository Repository { get; set; } = default!;

		public string ResourcePath { get; set; } = default!;

		public global::System.DateTimeOffset? StartedAt { get; set; }

		public string? StartedAtHumanized { get; set; }

		public CheckStatusState Status { get; set; }

		public CheckStepConnection? Steps { get; set; }

		public string? Text { get; set; }

		public string? Title { get; set; }

		public string Url { get; set; } = default!;
	}

	public class CheckRunConnection
	{
		public global::System.Collections.Generic.List<CheckRunEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<CheckRun?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class CheckRunEdge
	{
		public string Cursor { get; set; } = default!;

		public CheckRun? Node { get; set; }
	}

	public class CheckStep
	{
		public global::System.DateTimeOffset? CompletedAt { get; set; }

		public CheckConclusionState? Conclusion { get; set; }

		public string Name { get; set; } = default!;

		public int Number { get; set; }

		public global::System.DateTimeOffset? StartedAt { get; set; }

		public string? StartedAtHumanized { get; set; }

		public CheckStatusState Status { get; set; }
	}

	public class CheckStepConnection
	{
		public global::System.Collections.Generic.List<CheckStepEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<CheckStep?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class CheckStepEdge
	{
		public string Cursor { get; set; } = default!;

		public CheckStep? Node { get; set; }
	}

	public class CheckSuite
	{
		public App? App { get; set; }

		public Ref? Branch { get; set; }

		public CheckRunConnection? CheckRuns { get; set; }

		public Commit Commit { get; set; } = default!;

		public CheckConclusionState? Conclusion { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public User? Creator { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public Repository Repository { get; set; } = default!;

		public string ResourcePath { get; set; } = default!;

		public CheckStatusState Status { get; set; }

		public global::System.DateTimeOffset UpdatedAt { get; set; }

		public string? UpdatedAtHumanized { get; set; }

		public string Url { get; set; } = default!;

		public WorkflowRun? WorkflowRun { get; set; }
	}

	public class CheckSuiteConnection
	{
		public global::System.Collections.Generic.List<CheckSuiteEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<CheckSuite?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class CheckSuiteEdge
	{
		public string Cursor { get; set; } = default!;

		public CheckSuite? Node { get; set; }
	}

	public class CloseIssueRequest
	{
		public string? ClientMutationId { get; set; }

		public global::Octokit.GraphQL.ID IssueId { get; set; }

		public IssueClosedStateReason? StateReason { get; set; }
	}

	public class CloseIssueResult
	{
		public string? ClientMutationId { get; set; }

		public Issue? Issue { get; set; }
	}

	public class ClosePullRequestRequest
	{
		public string? ClientMutationId { get; set; }

		public global::Octokit.GraphQL.ID PullRequestId { get; set; }
	}

	public class ClosePullRequestResult
	{
		public string? ClientMutationId { get; set; }

		public PullRequest? PullRequest { get; set; }
	}

	public class ClosedEvent
	{
		public Actor? Actor { get; set; }

		public Closer? Closer { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public string ResourcePath { get; set; } = default!;

		public IssueStateReason? StateReason { get; set; }

		public string Url { get; set; } = default!;
	}

	public class Closer
	{
		public Commit? Commit { get; set; }

		public ProjectV2? ProjectV2 { get; set; }

		public PullRequest? PullRequest { get; set; }
	}

	public class Comment
	{
		public Actor? Author { get; set; }

		public CommentAuthorAssociation AuthorAssociation { get; set; }

		public string Body { get; set; } = default!;

		public string BodyHTML { get; set; } = default!;

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public bool IncludesCreatedEdit { get; set; }

		public global::System.DateTimeOffset? LastEditedAt { get; set; }

		public global::System.DateTimeOffset? PublishedAt { get; set; }

		public string? PublishedAtHumanized { get; set; }

		public global::System.DateTimeOffset UpdatedAt { get; set; }

		public string? UpdatedAtHumanized { get; set; }

		public bool ViewerDidAuthor { get; set; }
	}

	public class CommentDeletedEvent
	{
		public Actor? Actor { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public Actor? DeletedCommentAuthor { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }
	}

	public class Commit
	{
		public string AbbreviatedOid { get; set; } = default!;

		public int Additions { get; set; }

		public GitActor? Author { get; set; }

		public int ChangedFiles { get; set; }

		public int? ChangedFilesIfAvailable { get; set; }

		public CheckSuiteConnection? CheckSuites { get; set; }

		public CommitCommentConnection Comments { get; set; } = default!;

		public string CommitUrl { get; set; } = default!;

		public global::System.DateTimeOffset CommittedDate { get; set; }

		public string? CommittedDateHumanized { get; set; }

		public GitActor? Committer { get; set; }

		public int Deletions { get; set; }

		public TreeEntry? File { get; set; }

		public CommitHistoryConnection History { get; set; } = default!;

		public global::Octokit.GraphQL.ID Id { get; set; }

		public string Message { get; set; } = default!;

		public string MessageBody { get; set; } = default!;

		public string MessageHeadline { get; set; } = default!;

		public string Oid { get; set; } = default!;

		public CommitConnection Parents { get; set; } = default!;

		public Repository Repository { get; set; } = default!;

		public string ResourcePath { get; set; } = default!;

		public GitSignature? Signature { get; set; }

		public Status? Status { get; set; }

		public StatusCheckRollup? StatusCheckRollup { get; set; }

		public Tree Tree { get; set; } = default!;

		public string Url { get; set; } = default!;

		public bool ViewerCanSubscribe { get; set; }

		public SubscriptionState? ViewerSubscription { get; set; }
	}

	public class CommitAuthor
	{
		public global::System.Collections.Generic.List<string>? Emails { get; set; }

		public global::Octokit.GraphQL.ID? Id { get; set; }
	}

	public class CommitComment
	{
		public Actor? Author { get; set; }

		public CommentAuthorAssociation AuthorAssociation { get; set; }

		public string Body { get; set; } = default!;

		public string BodyHTML { get; set; } = default!;

		public Commit? Commit { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public bool IncludesCreatedEdit { get; set; }

		public bool IsMinimized { get; set; }

		public global::System.DateTimeOffset? LastEditedAt { get; set; }

		public string? MinimizedReason { get; set; }

		public string? Path { get; set; }

		public global::System.DateTimeOffset? PublishedAt { get; set; }

		public string? PublishedAtHumanized { get; set; }

		public global::System.Collections.Generic.List<ReactionGroup>? ReactionGroups { get; set; }

		public ReactionConnection Reactions { get; set; } = default!;

		public Repository Repository { get; set; } = default!;

		public string ResourcePath { get; set; } = default!;

		public global::System.DateTimeOffset UpdatedAt { get; set; }

		public string? UpdatedAtHumanized { get; set; }

		public string Url { get; set; } = default!;

		public bool ViewerCanDelete { get; set; }

		public bool ViewerCanMinimize { get; set; }

		public bool ViewerCanReact { get; set; }

		public bool ViewerCanUpdate { get; set; }

		public bool ViewerDidAuthor { get; set; }
	}

	public class CommitCommentConnection
	{
		public global::System.Collections.Generic.List<CommitCommentEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<CommitComment?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class CommitCommentEdge
	{
		public string Cursor { get; set; } = default!;

		public CommitComment? Node { get; set; }
	}

	public class CommitConnection
	{
		public global::System.Collections.Generic.List<CommitEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<Commit?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class CommitEdge
	{
		public string Cursor { get; set; } = default!;

		public Commit? Node { get; set; }
	}

	public class CommitHistoryConnection
	{
		public global::System.Collections.Generic.List<CommitEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<Commit?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class Comparison
	{
		public ComparisonCommitConnection Commits { get; set; } = default!;

		public global::Octokit.GraphQL.ID Id { get; set; }

		public ComparisonStatus Status { get; set; }
	}

	public class ComparisonCommitConnection
	{
		public global::System.Collections.Generic.List<CommitEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<Commit?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class ConnectedEvent
	{
		public Actor? Actor { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public bool IsCrossRepository { get; set; }

		public ReferencedSubject Source { get; set; } = default!;

		public ReferencedSubject Subject { get; set; } = default!;
	}

	public class ContributionCalendar
	{
		public global::System.Collections.Generic.List<string> Colors { get; set; } = [];

		public global::System.Collections.Generic.List<ContributionCalendarMonth> Months { get; set; } = [];

		public int TotalContributions { get; set; }

		public global::System.Collections.Generic.List<ContributionCalendarWeek> Weeks { get; set; } = [];
	}

	public class ContributionCalendarDay
	{
		public string Color { get; set; } = default!;

		public int ContributionCount { get; set; }

		public ContributionLevel ContributionLevel { get; set; }

		public string Date { get; set; } = string.Empty;

		public int Weekday { get; set; }
	}

	public class ContributionCalendarMonth
	{
		public string FirstDay { get; set; } = string.Empty;

		public string Name { get; set; } = string.Empty;

		public int TotalWeeks { get; set; }

		public int Year { get; set; }
	}

	public class ContributionCalendarWeek
	{
		public global::System.Collections.Generic.List<ContributionCalendarDay> ContributionDays { get; set; } = [];

		public string FirstDay { get; set; } = string.Empty;
	}

	public class ContributionsCollection
	{
		public ContributionCalendar ContributionCalendar { get; set; } = default!;

		public global::System.DateTimeOffset StartedAt { get; set; }

		public string? StartedAtHumanized { get; set; }

		public User User { get; set; } = default!;
	}

	public class ConvertToDraftEvent
	{
		public Actor? Actor { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public PullRequest PullRequest { get; set; } = default!;

		public string ResourcePath { get; set; } = default!;

		public string Url { get; set; } = default!;
	}

	public class ConvertedNoteToIssueEvent
	{
		public Actor? Actor { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public ProjectCard? ProjectCard { get; set; }
	}

	public class ConvertedToDiscussionEvent
	{
		public Actor? Actor { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public Discussion? Discussion { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }
	}

	public class CreateIssueRequest
	{
		public global::System.Collections.Generic.List<global::Octokit.GraphQL.ID>? AssigneeIds { get; set; }

		public string? Body { get; set; }

		public string? ClientMutationId { get; set; }

		public string? IssueTemplate { get; set; }

		public global::System.Collections.Generic.List<global::Octokit.GraphQL.ID>? LabelIds { get; set; }

		public global::Octokit.GraphQL.ID? MilestoneId { get; set; }

		public global::System.Collections.Generic.List<global::Octokit.GraphQL.ID>? ProjectIds { get; set; }

		public global::Octokit.GraphQL.ID RepositoryId { get; set; }

		public string Title { get; set; } = default!;
	}

	public class CreateIssueResult
	{
		public string? ClientMutationId { get; set; }

		public Issue? Issue { get; set; }
	}

	public class CrossReferencedEvent
	{
		public Actor? Actor { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public bool IsCrossRepository { get; set; }

		public global::System.DateTimeOffset ReferencedAt { get; set; }

		public string ResourcePath { get; set; } = default!;

		public ReferencedSubject Source { get; set; } = default!;

		public ReferencedSubject Target { get; set; } = default!;

		public string Url { get; set; } = default!;

		public bool WillCloseTarget { get; set; }
	}

	public class DeleteIssueCommentRequest
	{
		public string? ClientMutationId { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }
	}

	public class DeleteIssueCommentResult
	{
		public string? ClientMutationId { get; set; }
	}

	public class DemilestonedEvent
	{
		public Actor? Actor { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public string MilestoneTitle { get; set; } = default!;

		public MilestoneItem Subject { get; set; } = default!;
	}

	public class DeployedEvent
	{
		public Actor? Actor { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public Deployment Deployment { get; set; } = default!;

		public global::Octokit.GraphQL.ID Id { get; set; }

		public PullRequest PullRequest { get; set; } = default!;

		public Ref? Ref { get; set; }
	}

	public class Deployment
	{
		public Commit? Commit { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public Actor Creator { get; set; } = default!;

		public string? Description { get; set; }

		public string? Environment { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public string? Payload { get; set; }

		public Ref? Ref { get; set; }

		public Repository Repository { get; set; } = default!;

		public DeploymentState? State { get; set; }

		public string? Task { get; set; }

		public global::System.DateTimeOffset UpdatedAt { get; set; }

		public string? UpdatedAtHumanized { get; set; }
	}

	public class DeploymentEnvironmentChangedEvent
	{
		public Actor? Actor { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public DeploymentStatus DeploymentStatus { get; set; } = default!;

		public global::Octokit.GraphQL.ID Id { get; set; }

		public PullRequest PullRequest { get; set; } = default!;
	}

	public class DeploymentStatus
	{
		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public Actor Creator { get; set; } = default!;

		public Deployment Deployment { get; set; } = default!;

		public string? Description { get; set; }

		public string? Environment { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public DeploymentStatusState State { get; set; }

		public global::System.DateTimeOffset UpdatedAt { get; set; }

		public string? UpdatedAtHumanized { get; set; }
	}

	public class DisconnectedEvent
	{
		public Actor? Actor { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public bool IsCrossRepository { get; set; }

		public ReferencedSubject Source { get; set; } = default!;

		public ReferencedSubject Subject { get; set; } = default!;
	}

	public class Discussion
	{
		public LockReason? ActiveLockReason { get; set; }

		public global::System.DateTimeOffset? AnswerChosenAt { get; set; }

		public Actor? Author { get; set; }

		public CommentAuthorAssociation AuthorAssociation { get; set; }

		public string Body { get; set; } = default!;

		public string BodyHTML { get; set; } = default!;

		public DiscussionCategory Category { get; set; } = default!;

		public bool Closed { get; set; }

		public global::System.DateTimeOffset? ClosedAt { get; set; }

		public DiscussionCommentConnection Comments { get; set; } = default!;

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public bool IncludesCreatedEdit { get; set; }

		public LabelConnection? Labels { get; set; }

		public global::System.DateTimeOffset? LastEditedAt { get; set; }

		public bool Locked { get; set; }

		public int Number { get; set; }

		public global::System.DateTimeOffset? PublishedAt { get; set; }

		public string? PublishedAtHumanized { get; set; }

		public global::System.Collections.Generic.List<ReactionGroup>? ReactionGroups { get; set; }

		public ReactionConnection Reactions { get; set; } = default!;

		public Repository Repository { get; set; } = default!;

		public string ResourcePath { get; set; } = default!;

		public DiscussionStateReason? StateReason { get; set; }

		public string Title { get; set; } = default!;

		public global::System.DateTimeOffset UpdatedAt { get; set; }

		public string? UpdatedAtHumanized { get; set; }

		public int UpvoteCount { get; set; }

		public string Url { get; set; } = default!;

		public bool ViewerCanClose { get; set; }

		public bool ViewerCanDelete { get; set; }

		public bool ViewerCanLabel { get; set; }

		public bool ViewerCanReact { get; set; }

		public bool ViewerCanReopen { get; set; }

		public bool ViewerCanSubscribe { get; set; }

		public bool ViewerCanUpdate { get; set; }

		public bool ViewerCanUpvote { get; set; }

		public bool ViewerDidAuthor { get; set; }

		public bool ViewerHasUpvoted { get; set; }

		public SubscriptionState? ViewerSubscription { get; set; }
	}

	public class DiscussionCategory
	{
		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public string? Description { get; set; }

		public string Emoji { get; set; } = default!;

		public global::Octokit.GraphQL.ID Id { get; set; }

		public string Name { get; set; } = default!;

		public Repository Repository { get; set; } = default!;

		public global::System.DateTimeOffset UpdatedAt { get; set; }

		public string? UpdatedAtHumanized { get; set; }
	}

	public class DiscussionComment
	{
		public Actor? Author { get; set; }

		public CommentAuthorAssociation AuthorAssociation { get; set; }

		public string Body { get; set; } = default!;

		public string BodyHTML { get; set; } = default!;

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public Discussion? Discussion { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public bool IncludesCreatedEdit { get; set; }

		public bool IsMinimized { get; set; }

		public global::System.DateTimeOffset? LastEditedAt { get; set; }

		public string? MinimizedReason { get; set; }

		public global::System.DateTimeOffset? PublishedAt { get; set; }

		public string? PublishedAtHumanized { get; set; }

		public global::System.Collections.Generic.List<ReactionGroup>? ReactionGroups { get; set; }

		public ReactionConnection Reactions { get; set; } = default!;

		public string ResourcePath { get; set; } = default!;

		public global::System.DateTimeOffset UpdatedAt { get; set; }

		public string? UpdatedAtHumanized { get; set; }

		public int UpvoteCount { get; set; }

		public string Url { get; set; } = default!;

		public bool ViewerCanDelete { get; set; }

		public bool ViewerCanMinimize { get; set; }

		public bool ViewerCanReact { get; set; }

		public bool ViewerCanUpdate { get; set; }

		public bool ViewerCanUpvote { get; set; }

		public bool ViewerDidAuthor { get; set; }

		public bool ViewerHasUpvoted { get; set; }
	}

	public class DiscussionCommentConnection
	{
		public global::System.Collections.Generic.List<DiscussionCommentEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<DiscussionComment?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class DiscussionCommentEdge
	{
		public string Cursor { get; set; } = default!;

		public DiscussionComment? Node { get; set; }
	}

	public class DiscussionConnection
	{
		public global::System.Collections.Generic.List<DiscussionEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<Discussion?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class DiscussionEdge
	{
		public string Cursor { get; set; } = default!;

		public Discussion? Node { get; set; }
	}

	public class DiscussionOrder
	{
		public OrderDirection Direction { get; set; }

		public DiscussionOrderField Field { get; set; }
	}

	public class DraftPullRequestReviewComment
	{
		public string Body { get; set; } = default!;

		public string Path { get; set; } = default!;
	}

	public class DraftPullRequestReviewThread
	{
		public string Body { get; set; } = default!;

		public string? Path { get; set; }
	}

	public class Environment
	{
		public global::Octokit.GraphQL.ID Id { get; set; }

		public bool? IsPinned { get; set; }

		public string Name { get; set; } = default!;
	}

	public class FollowerConnection
	{
		public global::System.Collections.Generic.List<UserEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<User?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class FollowingConnection
	{
		public global::System.Collections.Generic.List<UserEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<User?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class GitActor
	{
		public string AvatarUrl { get; set; } = default!;

		public string? Email { get; set; }

		public string? Name { get; set; }

		public User? User { get; set; }
	}

	public class GitSignature : IGitSignature
	{
		public string Email { get; set; } = default!;

		public bool IsValid { get; set; }

		public string Payload { get; set; } = default!;

		public string Signature { get; set; } = default!;

		public User? Signer { get; set; }

		public GitSignatureState State { get; set; }

		public bool WasSignedByGitHub { get; set; }
	}

	public class HeadRefDeletedEvent
	{
		public Actor? Actor { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public Ref? HeadRef { get; set; }

		public string HeadRefName { get; set; } = default!;

		public global::Octokit.GraphQL.ID Id { get; set; }

		public PullRequest PullRequest { get; set; } = default!;
	}

	public class HeadRefForcePushedEvent
	{
		public Actor? Actor { get; set; }

		public Commit? AfterCommit { get; set; }

		public Commit? BeforeCommit { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public PullRequest PullRequest { get; set; } = default!;

		public Ref? Ref { get; set; }
	}

	public class HeadRefRestoredEvent
	{
		public Actor? Actor { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public PullRequest PullRequest { get; set; } = default!;
	}

	public class Issue
	{
		public LockReason? ActiveLockReason { get; set; }

		public UserConnection Assignees { get; set; } = default!;

		public Actor? Author { get; set; }

		public CommentAuthorAssociation AuthorAssociation { get; set; }

		public string Body { get; set; } = default!;

		public string BodyHTML { get; set; } = default!;

		public bool Closed { get; set; }

		public global::System.DateTimeOffset? ClosedAt { get; set; }

		public IssueCommentConnection Comments { get; set; } = default!;

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public bool IncludesCreatedEdit { get; set; }

		public bool? IsPinned { get; set; }

		public LabelConnection? Labels { get; set; }

		public global::System.DateTimeOffset? LastEditedAt { get; set; }

		public bool Locked { get; set; }

		public Milestone? Milestone { get; set; }

		public int Number { get; set; }

		public UserConnection Participants { get; set; } = default!;

		public ProjectCardConnection ProjectCards { get; set; } = default!;

		public ProjectV2? ProjectV2 { get; set; }

		public ProjectV2Connection ProjectsV2 { get; set; } = default!;

		public global::System.DateTimeOffset? PublishedAt { get; set; }

		public string? PublishedAtHumanized { get; set; }

		public global::System.Collections.Generic.List<ReactionGroup>? ReactionGroups { get; set; }

		public ReactionConnection Reactions { get; set; } = default!;

		public Repository Repository { get; set; } = default!;

		public string ResourcePath { get; set; } = default!;

		public IssueState State { get; set; }

		public IssueStateReason? StateReason { get; set; }

		public IssueTimelineConnection Timeline { get; set; } = default!;

		public IssueTimelineItemsConnection TimelineItems { get; set; } = default!;

		public string Title { get; set; } = default!;

		public global::System.DateTimeOffset UpdatedAt { get; set; }

		public string? UpdatedAtHumanized { get; set; }

		public string Url { get; set; } = default!;

		public bool ViewerCanClose { get; set; }

		public bool ViewerCanDelete { get; set; }

		public bool ViewerCanLabel { get; set; }

		public bool ViewerCanReact { get; set; }

		public bool ViewerCanReopen { get; set; }

		public bool ViewerCanSubscribe { get; set; }

		public bool ViewerCanUpdate { get; set; }

		public bool ViewerDidAuthor { get; set; }

		public SubscriptionState? ViewerSubscription { get; set; }
	}

	public class IssueComment
	{
		public Actor? Author { get; set; }

		public CommentAuthorAssociation AuthorAssociation { get; set; }

		public string Body { get; set; } = default!;

		public string BodyHTML { get; set; } = default!;

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public bool IncludesCreatedEdit { get; set; }

		public bool IsMinimized { get; set; }

		public bool? IsPinned { get; set; }

		public Issue Issue { get; set; } = default!;

		public global::System.DateTimeOffset? LastEditedAt { get; set; }

		public string? MinimizedReason { get; set; }

		public global::System.DateTimeOffset? PublishedAt { get; set; }

		public string? PublishedAtHumanized { get; set; }

		public PullRequest? PullRequest { get; set; }

		public global::System.Collections.Generic.List<ReactionGroup>? ReactionGroups { get; set; }

		public ReactionConnection Reactions { get; set; } = default!;

		public Repository Repository { get; set; } = default!;

		public string ResourcePath { get; set; } = default!;

		public global::System.DateTimeOffset UpdatedAt { get; set; }

		public string? UpdatedAtHumanized { get; set; }

		public string Url { get; set; } = default!;

		public bool ViewerCanDelete { get; set; }

		public bool ViewerCanMinimize { get; set; }

		public bool ViewerCanReact { get; set; }

		public bool ViewerCanUpdate { get; set; }

		public bool ViewerDidAuthor { get; set; }
	}

	public class IssueCommentConnection
	{
		public global::System.Collections.Generic.List<IssueCommentEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<IssueComment?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class IssueCommentEdge
	{
		public string Cursor { get; set; } = default!;

		public IssueComment? Node { get; set; }
	}

	public class IssueConnection
	{
		public global::System.Collections.Generic.List<IssueEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<Issue?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class IssueEdge
	{
		public string Cursor { get; set; } = default!;

		public Issue? Node { get; set; }
	}

	public class IssueFilters
	{
		public string? Assignee { get; set; }

		public global::System.Collections.Generic.List<string>? Labels { get; set; }

		public string? Milestone { get; set; }

		public string? Type { get; set; }
	}

	public class IssueOrPullRequest
	{
		public Issue? Issue { get; set; }

		public PullRequest? PullRequest { get; set; }
	}

	public class IssueOrder
	{
		public OrderDirection Direction { get; set; }

		public IssueOrderField Field { get; set; }
	}

	public class IssueTemplate
	{
		public string? About { get; set; }

		public UserConnection Assignees { get; set; } = default!;

		public string? Body { get; set; }

		public string Filename { get; set; } = default!;

		public LabelConnection? Labels { get; set; }

		public string Name { get; set; } = default!;

		public string? Title { get; set; }

		public IssueType? Type { get; set; }
	}

	public class IssueTimelineConnection
	{
		public global::System.Collections.Generic.List<IssueTimelineItemEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<IssueTimelineItem?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class IssueTimelineItem
	{
		public AssignedEvent? AssignedEvent { get; set; }

		public ClosedEvent? ClosedEvent { get; set; }

		public Commit? Commit { get; set; }

		public CrossReferencedEvent? CrossReferencedEvent { get; set; }

		public DemilestonedEvent? DemilestonedEvent { get; set; }

		public IssueComment? IssueComment { get; set; }

		public LabeledEvent? LabeledEvent { get; set; }

		public LockedEvent? LockedEvent { get; set; }

		public MilestonedEvent? MilestonedEvent { get; set; }

		public ReferencedEvent? ReferencedEvent { get; set; }

		public RenamedTitleEvent? RenamedTitleEvent { get; set; }

		public ReopenedEvent? ReopenedEvent { get; set; }

		public SubscribedEvent? SubscribedEvent { get; set; }

		public TransferredEvent? TransferredEvent { get; set; }

		public UnassignedEvent? UnassignedEvent { get; set; }

		public UnlabeledEvent? UnlabeledEvent { get; set; }

		public UnlockedEvent? UnlockedEvent { get; set; }

		public UnsubscribedEvent? UnsubscribedEvent { get; set; }

		public UserBlockedEvent? UserBlockedEvent { get; set; }
	}

	public class IssueTimelineItemEdge
	{
		public string Cursor { get; set; } = default!;

		public IssueTimelineItem? Node { get; set; }
	}

	public class IssueTimelineItems
	{
		public AddedToProjectEvent? AddedToProjectEvent { get; set; }

		public AssignedEvent? AssignedEvent { get; set; }

		public ClosedEvent? ClosedEvent { get; set; }

		public CommentDeletedEvent? CommentDeletedEvent { get; set; }

		public ConnectedEvent? ConnectedEvent { get; set; }

		public ConvertedNoteToIssueEvent? ConvertedNoteToIssueEvent { get; set; }

		public ConvertedToDiscussionEvent? ConvertedToDiscussionEvent { get; set; }

		public CrossReferencedEvent? CrossReferencedEvent { get; set; }

		public DemilestonedEvent? DemilestonedEvent { get; set; }

		public DisconnectedEvent? DisconnectedEvent { get; set; }

		public IssueComment? IssueComment { get; set; }

		public LabeledEvent? LabeledEvent { get; set; }

		public LockedEvent? LockedEvent { get; set; }

		public MarkedAsDuplicateEvent? MarkedAsDuplicateEvent { get; set; }

		public MentionedEvent? MentionedEvent { get; set; }

		public MilestonedEvent? MilestonedEvent { get; set; }

		public MovedColumnsInProjectEvent? MovedColumnsInProjectEvent { get; set; }

		public PinnedEvent? PinnedEvent { get; set; }

		public ReferencedEvent? ReferencedEvent { get; set; }

		public RemovedFromProjectEvent? RemovedFromProjectEvent { get; set; }

		public RenamedTitleEvent? RenamedTitleEvent { get; set; }

		public ReopenedEvent? ReopenedEvent { get; set; }

		public SubscribedEvent? SubscribedEvent { get; set; }

		public TransferredEvent? TransferredEvent { get; set; }

		public UnassignedEvent? UnassignedEvent { get; set; }

		public UnlabeledEvent? UnlabeledEvent { get; set; }

		public UnlockedEvent? UnlockedEvent { get; set; }

		public UnmarkedAsDuplicateEvent? UnmarkedAsDuplicateEvent { get; set; }

		public UnpinnedEvent? UnpinnedEvent { get; set; }

		public UnsubscribedEvent? UnsubscribedEvent { get; set; }

		public UserBlockedEvent? UserBlockedEvent { get; set; }
	}

	public class IssueTimelineItemsConnection
	{
		public global::System.Collections.Generic.List<IssueTimelineItemsEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<IssueTimelineItems?>? Nodes { get; set; }

		public int PageCount { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }

		public global::System.DateTimeOffset UpdatedAt { get; set; }

		public string? UpdatedAtHumanized { get; set; }
	}

	public class IssueTimelineItemsEdge
	{
		public string Cursor { get; set; } = default!;

		public IssueTimelineItems? Node { get; set; }
	}

	public class IssueType
	{
		public IssueTypeColor Color { get; set; }

		public string? Description { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public bool IsEnabled { get; set; }

		public bool IsPrivate { get; set; }

		public IssueConnection Issues { get; set; } = default!;

		public string Name { get; set; } = default!;
	}

	public class Label
	{
		public string Color { get; set; } = default!;

		public global::System.DateTimeOffset? CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public string? Description { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public IssueConnection Issues { get; set; } = default!;

		public string Name { get; set; } = default!;

		public PullRequestConnection PullRequests { get; set; } = default!;

		public Repository Repository { get; set; } = default!;

		public string ResourcePath { get; set; } = default!;

		public global::System.DateTimeOffset? UpdatedAt { get; set; }

		public string? UpdatedAtHumanized { get; set; }

		public string Url { get; set; } = default!;
	}

	public class LabelConnection
	{
		public global::System.Collections.Generic.List<LabelEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<Label?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class LabelEdge
	{
		public string Cursor { get; set; } = default!;

		public Label? Node { get; set; }
	}

	public class LabeledEvent
	{
		public Actor? Actor { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public Label Label { get; set; } = default!;
	}

	public class Language
	{
		public string? Color { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public string Name { get; set; } = default!;
	}

	public class LanguageConnection
	{
		public global::System.Collections.Generic.List<LanguageEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<Language?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class LanguageEdge
	{
		public string Cursor { get; set; } = default!;

		public Language Node { get; set; } = default!;

		public int Size { get; set; }
	}

	public class License
	{
		public string Body { get; set; } = default!;

		public string? Description { get; set; }

		public bool Hidden { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public string Key { get; set; } = default!;

		public string Name { get; set; } = default!;

		public string? Url { get; set; }
	}

	public class LockedEvent
	{
		public Actor? Actor { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public LockReason? LockReason { get; set; }
	}

	public class Mannequin
	{
		public string AvatarUrl { get; set; } = default!;

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public string? Email { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public string Login { get; set; } = default!;

		public string? Name { get; set; }

		public string ResourcePath { get; set; } = default!;

		public global::System.DateTimeOffset UpdatedAt { get; set; }

		public string? UpdatedAtHumanized { get; set; }

		public string Url { get; set; } = default!;
	}

	public class MarkedAsDuplicateEvent
	{
		public Actor? Actor { get; set; }

		public IssueOrPullRequest? Canonical { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public IssueOrPullRequest? Duplicate { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public bool IsCrossRepository { get; set; }
	}

	public class MentionedEvent
	{
		public Actor? Actor { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }
	}

	public class MergePullRequestRequest
	{
		public string? AuthorEmail { get; set; }

		public string? ClientMutationId { get; set; }

		public string? CommitBody { get; set; }

		public string? CommitHeadline { get; set; }

		public string? ExpectedHeadOid { get; set; }

		public PullRequestMergeMethod? MergeMethod { get; set; }

		public global::Octokit.GraphQL.ID PullRequestId { get; set; }
	}

	public class MergePullRequestResult
	{
		public Actor? Actor { get; set; }

		public string? ClientMutationId { get; set; }

		public PullRequest? PullRequest { get; set; }
	}

	public class MergedEvent
	{
		public Actor? Actor { get; set; }

		public Commit? Commit { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public Ref? MergeRef { get; set; }

		public string MergeRefName { get; set; } = default!;

		public PullRequest PullRequest { get; set; } = default!;

		public string ResourcePath { get; set; } = default!;

		public string Url { get; set; } = default!;
	}

	public class Milestone
	{
		public bool Closed { get; set; }

		public global::System.DateTimeOffset? ClosedAt { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public Actor? Creator { get; set; }

		public string? Description { get; set; }

		public string? DescriptionHTML { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public IssueConnection Issues { get; set; } = default!;

		public int Number { get; set; }

		public double ProgressPercentage { get; set; }

		public PullRequestConnection PullRequests { get; set; } = default!;

		public Repository Repository { get; set; } = default!;

		public string ResourcePath { get; set; } = default!;

		public MilestoneState State { get; set; }

		public string Title { get; set; } = default!;

		public global::System.DateTimeOffset UpdatedAt { get; set; }

		public string? UpdatedAtHumanized { get; set; }

		public string Url { get; set; } = default!;

		public bool ViewerCanClose { get; set; }

		public bool ViewerCanReopen { get; set; }
	}

	public class MilestoneConnection
	{
		public global::System.Collections.Generic.List<MilestoneEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<Milestone?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class MilestoneEdge
	{
		public string Cursor { get; set; } = default!;

		public Milestone? Node { get; set; }
	}

	public class MilestoneItem
	{
		public Issue? Issue { get; set; }

		public PullRequest? PullRequest { get; set; }
	}

	public class MilestonedEvent
	{
		public Actor? Actor { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public string MilestoneTitle { get; set; } = default!;

		public MilestoneItem Subject { get; set; } = default!;
	}

	public class MovedColumnsInProjectEvent
	{
		public Actor? Actor { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public ProjectCard? ProjectCard { get; set; }
	}

	public class Node : INode
	{
		public global::Octokit.GraphQL.ID Id { get; set; }
	}

	public class Organization
	{
		public string AvatarUrl { get; set; } = default!;

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public string? Description { get; set; }

		public string? DescriptionHTML { get; set; }

		public string? Email { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public bool IsVerified { get; set; }

		public string? Location { get; set; }

		public string Login { get; set; } = default!;

		public string? Name { get; set; }

		public PackageConnection Packages { get; set; } = default!;

		public PinnableItemConnection PinnableItems { get; set; } = default!;

		public PinnableItemConnection PinnedItems { get; set; } = default!;

		public ProjectV2? ProjectV2 { get; set; }

		public ProjectConnection Projects { get; set; } = default!;

		public ProjectV2Connection ProjectsV2 { get; set; } = default!;

		public RepositoryConnection Repositories { get; set; } = default!;

		public Repository? Repository { get; set; }

		public DiscussionConnection RepositoryDiscussions { get; set; } = default!;

		public string ResourcePath { get; set; } = default!;

		public SponsorConnection Sponsors { get; set; } = default!;

		public string? TwitterUsername { get; set; }

		public global::System.DateTimeOffset UpdatedAt { get; set; }

		public string? UpdatedAtHumanized { get; set; }

		public string Url { get; set; } = default!;

		public bool ViewerCanChangePinnedItems { get; set; }

		public bool ViewerCanSponsor { get; set; }

		public bool ViewerIsAMember { get; set; }

		public bool ViewerIsFollowing { get; set; }

		public bool ViewerIsSponsoring { get; set; }

		public string? WebsiteUrl { get; set; }
	}

	public class OrganizationConnection
	{
		public global::System.Collections.Generic.List<OrganizationEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<Organization?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class OrganizationEdge
	{
		public string Cursor { get; set; } = default!;

		public Organization? Node { get; set; }
	}

	public class Package
	{
		public global::Octokit.GraphQL.ID Id { get; set; }

		public PackageVersion? LatestVersion { get; set; }

		public string Name { get; set; } = default!;

		public PackageType PackageType { get; set; }

		public Repository? Repository { get; set; }

		public PackageStatistics? Statistics { get; set; }

		public PackageVersion? Version { get; set; }
	}

	public class PackageConnection
	{
		public global::System.Collections.Generic.List<PackageEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<Package?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class PackageEdge
	{
		public string Cursor { get; set; } = default!;

		public Package? Node { get; set; }
	}

	public class PackageFile
	{
		public global::Octokit.GraphQL.ID Id { get; set; }

		public string Name { get; set; } = default!;

		public PackageVersion? PackageVersion { get; set; }

		public int? Size { get; set; }

		public global::System.DateTimeOffset UpdatedAt { get; set; }

		public string? UpdatedAtHumanized { get; set; }

		public string? Url { get; set; }
	}

	public class PackageFileConnection
	{
		public global::System.Collections.Generic.List<PackageFileEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<PackageFile?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class PackageFileEdge
	{
		public string Cursor { get; set; } = default!;

		public PackageFile? Node { get; set; }
	}

	public class PackageOrder
	{
		public OrderDirection? Direction { get; set; }

		public PackageOrderField? Field { get; set; }
	}

	public class PackageStatistics
	{
		public int DownloadsTotalCount { get; set; }
	}

	public class PackageVersion
	{
		public PackageFileConnection Files { get; set; } = default!;

		public global::Octokit.GraphQL.ID Id { get; set; }

		public Package? Package { get; set; }

		public string? Readme { get; set; }

		public Release? Release { get; set; }

		public PackageVersionStatistics? Statistics { get; set; }

		public string Version { get; set; } = default!;
	}

	public class PackageVersionStatistics
	{
		public int DownloadsTotalCount { get; set; }
	}

	public class PageInfo
	{
		public string? EndCursor { get; set; }

		public bool HasNextPage { get; set; }

		public bool HasPreviousPage { get; set; }

		public string? StartCursor { get; set; }
	}

	public class PinnableItem
	{
		public Repository? Repository { get; set; }
	}

	public class PinnableItemConnection
	{
		public global::System.Collections.Generic.List<PinnableItemEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<PinnableItem?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class PinnableItemEdge
	{
		public string Cursor { get; set; } = default!;

		public PinnableItem? Node { get; set; }
	}

	public class PinnedEvent
	{
		public Actor? Actor { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public Issue Issue { get; set; } = default!;
	}

	public class PinnedIssue
	{
		public global::Octokit.GraphQL.ID Id { get; set; }

		public Issue Issue { get; set; } = default!;

		public Repository Repository { get; set; } = default!;
	}

	public class PinnedIssueConnection
	{
		public global::System.Collections.Generic.List<PinnedIssueEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<PinnedIssue?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class PinnedIssueEdge
	{
		public string Cursor { get; set; } = default!;

		public PinnedIssue? Node { get; set; }
	}

	public class Project
	{
		public string? Body { get; set; }

		public string BodyHTML { get; set; } = default!;

		public bool Closed { get; set; }

		public global::System.DateTimeOffset? ClosedAt { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public Actor? Creator { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public string Name { get; set; } = default!;

		public int Number { get; set; }

		public IProjectOwner Owner { get; set; } = default!;

		public string ResourcePath { get; set; } = default!;

		public ProjectState State { get; set; }

		public global::System.DateTimeOffset UpdatedAt { get; set; }

		public string? UpdatedAtHumanized { get; set; }

		public string Url { get; set; } = default!;

		public bool ViewerCanClose { get; set; }

		public bool ViewerCanReopen { get; set; }

		public bool ViewerCanUpdate { get; set; }
	}

	public class ProjectCard
	{
		public ProjectColumn? Column { get; set; }

		public ProjectCardItem? Content { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public Actor? Creator { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public bool IsArchived { get; set; }

		public string? Note { get; set; }

		public string ResourcePath { get; set; } = default!;

		public ProjectCardState? State { get; set; }

		public global::System.DateTimeOffset UpdatedAt { get; set; }

		public string? UpdatedAtHumanized { get; set; }

		public string Url { get; set; } = default!;
	}

	public class ProjectCardConnection
	{
		public global::System.Collections.Generic.List<ProjectCardEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<ProjectCard?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class ProjectCardEdge
	{
		public string Cursor { get; set; } = default!;

		public ProjectCard? Node { get; set; }
	}

	public class ProjectCardItem
	{
		public Issue? Issue { get; set; }

		public PullRequest? PullRequest { get; set; }
	}

	public class ProjectColumn
	{
		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public string Name { get; set; } = default!;

		public string ResourcePath { get; set; } = default!;

		public global::System.DateTimeOffset UpdatedAt { get; set; }

		public string? UpdatedAtHumanized { get; set; }

		public string Url { get; set; } = default!;
	}

	public class ProjectConnection
	{
		public global::System.Collections.Generic.List<ProjectEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<Project?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class ProjectEdge
	{
		public string Cursor { get; set; } = default!;

		public Project? Node { get; set; }
	}

	public class ProjectV2
	{
		public bool Closed { get; set; }

		public global::System.DateTimeOffset? ClosedAt { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public Actor? Creator { get; set; }

		public ProjectV2FieldConfiguration? Field { get; set; }

		public ProjectV2FieldConfigurationConnection Fields { get; set; } = default!;

		public global::Octokit.GraphQL.ID Id { get; set; }

		public ProjectV2ItemConnection Items { get; set; } = default!;

		public int Number { get; set; }

		public IProjectV2Owner Owner { get; set; } = default!;

		public bool Public { get; set; }

		public string? Readme { get; set; }

		public RepositoryConnection Repositories { get; set; } = default!;

		public string ResourcePath { get; set; } = default!;

		public string? ShortDescription { get; set; }

		public bool Template { get; set; }

		public string Title { get; set; } = default!;

		public global::System.DateTimeOffset UpdatedAt { get; set; }

		public string? UpdatedAtHumanized { get; set; }

		public string Url { get; set; } = default!;

		public ProjectV2View? View { get; set; }

		public bool ViewerCanClose { get; set; }

		public bool ViewerCanReopen { get; set; }

		public bool ViewerCanUpdate { get; set; }

		public ProjectV2ViewConnection Views { get; set; } = default!;
	}

	public class ProjectV2Connection
	{
		public global::System.Collections.Generic.List<ProjectV2Edge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<ProjectV2?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class ProjectV2Edge
	{
		public string Cursor { get; set; } = default!;

		public ProjectV2? Node { get; set; }
	}

	public class ProjectV2FieldConfiguration
	{
	}

	public class ProjectV2FieldConfigurationConnection
	{
		public global::System.Collections.Generic.List<ProjectV2FieldConfigurationEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<ProjectV2FieldConfiguration?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class ProjectV2FieldConfigurationEdge
	{
		public string Cursor { get; set; } = default!;

		public ProjectV2FieldConfiguration? Node { get; set; }
	}

	public class ProjectV2Item
	{
		public ProjectV2ItemContent? Content { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public Actor? Creator { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public bool IsArchived { get; set; }

		public ProjectV2ItemType Type { get; set; }

		public global::System.DateTimeOffset UpdatedAt { get; set; }

		public string? UpdatedAtHumanized { get; set; }
	}

	public class ProjectV2ItemConnection
	{
		public global::System.Collections.Generic.List<ProjectV2ItemEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<ProjectV2Item?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class ProjectV2ItemContent
	{
		public Issue? Issue { get; set; }

		public PullRequest? PullRequest { get; set; }
	}

	public class ProjectV2ItemEdge
	{
		public string Cursor { get; set; } = default!;

		public ProjectV2Item? Node { get; set; }
	}

	public class ProjectV2View
	{
		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public ProjectV2FieldConfigurationConnection? Fields { get; set; }

		public string? Filter { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public ProjectV2ViewLayout Layout { get; set; }

		public string Name { get; set; } = default!;

		public int Number { get; set; }

		public global::System.DateTimeOffset UpdatedAt { get; set; }

		public string? UpdatedAtHumanized { get; set; }
	}

	public class ProjectV2ViewConnection
	{
		public global::System.Collections.Generic.List<ProjectV2ViewEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<ProjectV2View?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class ProjectV2ViewEdge
	{
		public string Cursor { get; set; } = default!;

		public ProjectV2View? Node { get; set; }
	}

	public class PullRequest
	{
		public LockReason? ActiveLockReason { get; set; }

		public int Additions { get; set; }

		public UserConnection Assignees { get; set; } = default!;

		public Actor? Author { get; set; }

		public CommentAuthorAssociation AuthorAssociation { get; set; }

		public Ref? BaseRef { get; set; }

		public string BaseRefName { get; set; } = default!;

		public string Body { get; set; } = default!;

		public string BodyHTML { get; set; } = default!;

		public int ChangedFiles { get; set; }

		public bool Closed { get; set; }

		public global::System.DateTimeOffset? ClosedAt { get; set; }

		public IssueCommentConnection Comments { get; set; } = default!;

		public PullRequestCommitConnection Commits { get; set; } = default!;

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public int Deletions { get; set; }

		public PullRequestChangedFileConnection? Files { get; set; }

		public Ref? HeadRef { get; set; }

		public string HeadRefName { get; set; } = default!;

		public string HeadRefOid { get; set; } = default!;

		public Repository? HeadRepository { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public bool IncludesCreatedEdit { get; set; }

		public bool IsCrossRepository { get; set; }

		public bool IsDraft { get; set; }

		public LabelConnection? Labels { get; set; }

		public global::System.DateTimeOffset? LastEditedAt { get; set; }

		public PullRequestReviewConnection? LatestReviews { get; set; }

		public bool Locked { get; set; }

		public bool MaintainerCanModify { get; set; }

		public MergeableState Mergeable { get; set; }

		public bool Merged { get; set; }

		public Milestone? Milestone { get; set; }

		public int Number { get; set; }

		public UserConnection Participants { get; set; } = default!;

		public ProjectCardConnection ProjectCards { get; set; } = default!;

		public ProjectV2? ProjectV2 { get; set; }

		public ProjectV2Connection ProjectsV2 { get; set; } = default!;

		public global::System.DateTimeOffset? PublishedAt { get; set; }

		public string? PublishedAtHumanized { get; set; }

		public global::System.Collections.Generic.List<ReactionGroup>? ReactionGroups { get; set; }

		public ReactionConnection Reactions { get; set; } = default!;

		public Repository Repository { get; set; } = default!;

		public string ResourcePath { get; set; } = default!;

		public ReviewRequestConnection? ReviewRequests { get; set; }

		public PullRequestReviewConnection? Reviews { get; set; }

		public PullRequestState State { get; set; }

		public StatusCheckRollup? StatusCheckRollup { get; set; }

		public PullRequestTimelineConnection Timeline { get; set; } = default!;

		public PullRequestTimelineItemsConnection TimelineItems { get; set; } = default!;

		public string Title { get; set; } = default!;

		public global::System.DateTimeOffset UpdatedAt { get; set; }

		public string? UpdatedAtHumanized { get; set; }

		public string Url { get; set; } = default!;

		public bool ViewerCanClose { get; set; }

		public bool ViewerCanLabel { get; set; }

		public bool ViewerCanMergeAsAdmin { get; set; }

		public bool ViewerCanReact { get; set; }

		public bool ViewerCanReopen { get; set; }

		public bool ViewerCanSubscribe { get; set; }

		public bool ViewerCanUpdate { get; set; }

		public bool ViewerDidAuthor { get; set; }

		public SubscriptionState? ViewerSubscription { get; set; }
	}

	public class PullRequestChangedFile
	{
		public int Additions { get; set; }

		public int Deletions { get; set; }

		public string Path { get; set; } = default!;
	}

	public class PullRequestChangedFileConnection
	{
		public global::System.Collections.Generic.List<PullRequestChangedFileEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<PullRequestChangedFile?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class PullRequestChangedFileEdge
	{
		public string Cursor { get; set; } = default!;

		public PullRequestChangedFile? Node { get; set; }
	}

	public class PullRequestCommit
	{
		public Commit Commit { get; set; } = default!;

		public global::Octokit.GraphQL.ID Id { get; set; }

		public PullRequest PullRequest { get; set; } = default!;

		public string ResourcePath { get; set; } = default!;

		public string Url { get; set; } = default!;
	}

	public class PullRequestCommitCommentThread
	{
		public CommitCommentConnection Comments { get; set; } = default!;

		public Commit Commit { get; set; } = default!;

		public global::Octokit.GraphQL.ID Id { get; set; }

		public string? Path { get; set; }

		public PullRequest PullRequest { get; set; } = default!;

		public Repository Repository { get; set; } = default!;
	}

	public class PullRequestCommitConnection
	{
		public global::System.Collections.Generic.List<PullRequestCommitEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<PullRequestCommit?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class PullRequestCommitEdge
	{
		public string Cursor { get; set; } = default!;

		public PullRequestCommit? Node { get; set; }
	}

	public class PullRequestConnection
	{
		public global::System.Collections.Generic.List<PullRequestEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<PullRequest?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class PullRequestEdge
	{
		public string Cursor { get; set; } = default!;

		public PullRequest? Node { get; set; }
	}

	public class PullRequestReview
	{
		public Actor? Author { get; set; }

		public CommentAuthorAssociation AuthorAssociation { get; set; }

		public string Body { get; set; } = default!;

		public string BodyHTML { get; set; } = default!;

		public PullRequestReviewCommentConnection Comments { get; set; } = default!;

		public Commit? Commit { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public bool IncludesCreatedEdit { get; set; }

		public bool IsMinimized { get; set; }

		public global::System.DateTimeOffset? LastEditedAt { get; set; }

		public string? MinimizedReason { get; set; }

		public global::System.DateTimeOffset? PublishedAt { get; set; }

		public string? PublishedAtHumanized { get; set; }

		public PullRequest PullRequest { get; set; } = default!;

		public global::System.Collections.Generic.List<ReactionGroup>? ReactionGroups { get; set; }

		public ReactionConnection Reactions { get; set; } = default!;

		public Repository Repository { get; set; } = default!;

		public string ResourcePath { get; set; } = default!;

		public PullRequestReviewState State { get; set; }

		public global::System.DateTimeOffset? SubmittedAt { get; set; }

		public global::System.DateTimeOffset UpdatedAt { get; set; }

		public string? UpdatedAtHumanized { get; set; }

		public string Url { get; set; } = default!;

		public bool ViewerCanDelete { get; set; }

		public bool ViewerCanMinimize { get; set; }

		public bool ViewerCanReact { get; set; }

		public bool ViewerCanUpdate { get; set; }

		public bool ViewerDidAuthor { get; set; }
	}

	public class PullRequestReviewComment
	{
		public Actor? Author { get; set; }

		public CommentAuthorAssociation AuthorAssociation { get; set; }

		public string Body { get; set; } = default!;

		public string BodyHTML { get; set; } = default!;

		public Commit? Commit { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public bool IncludesCreatedEdit { get; set; }

		public bool IsMinimized { get; set; }

		public global::System.DateTimeOffset? LastEditedAt { get; set; }

		public string? MinimizedReason { get; set; }

		public string Path { get; set; } = default!;

		public global::System.DateTimeOffset? PublishedAt { get; set; }

		public string? PublishedAtHumanized { get; set; }

		public PullRequest PullRequest { get; set; } = default!;

		public PullRequestReview? PullRequestReview { get; set; }

		public global::System.Collections.Generic.List<ReactionGroup>? ReactionGroups { get; set; }

		public ReactionConnection Reactions { get; set; } = default!;

		public Repository Repository { get; set; } = default!;

		public string ResourcePath { get; set; } = default!;

		public PullRequestReviewCommentState State { get; set; }

		public global::System.DateTimeOffset UpdatedAt { get; set; }

		public string? UpdatedAtHumanized { get; set; }

		public string Url { get; set; } = default!;

		public bool ViewerCanDelete { get; set; }

		public bool ViewerCanMinimize { get; set; }

		public bool ViewerCanReact { get; set; }

		public bool ViewerCanUpdate { get; set; }

		public bool ViewerDidAuthor { get; set; }
	}

	public class PullRequestReviewCommentConnection
	{
		public global::System.Collections.Generic.List<PullRequestReviewCommentEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<PullRequestReviewComment?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class PullRequestReviewCommentEdge
	{
		public string Cursor { get; set; } = default!;

		public PullRequestReviewComment? Node { get; set; }
	}

	public class PullRequestReviewConnection
	{
		public global::System.Collections.Generic.List<PullRequestReviewEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<PullRequestReview?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class PullRequestReviewEdge
	{
		public string Cursor { get; set; } = default!;

		public PullRequestReview? Node { get; set; }
	}

	public class PullRequestReviewThread
	{
		public PullRequestReviewCommentConnection Comments { get; set; } = default!;

		public global::Octokit.GraphQL.ID Id { get; set; }

		public string Path { get; set; } = default!;

		public PullRequest PullRequest { get; set; } = default!;

		public Repository Repository { get; set; } = default!;
	}

	public class PullRequestRevisionMarker
	{
		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public PullRequest PullRequest { get; set; } = default!;
	}

	public class PullRequestTimelineConnection
	{
		public global::System.Collections.Generic.List<PullRequestTimelineItemEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<PullRequestTimelineItem?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class PullRequestTimelineItem
	{
		public AssignedEvent? AssignedEvent { get; set; }

		public BaseRefDeletedEvent? BaseRefDeletedEvent { get; set; }

		public BaseRefForcePushedEvent? BaseRefForcePushedEvent { get; set; }

		public ClosedEvent? ClosedEvent { get; set; }

		public Commit? Commit { get; set; }

		public CrossReferencedEvent? CrossReferencedEvent { get; set; }

		public DemilestonedEvent? DemilestonedEvent { get; set; }

		public DeployedEvent? DeployedEvent { get; set; }

		public DeploymentEnvironmentChangedEvent? DeploymentEnvironmentChangedEvent { get; set; }

		public HeadRefDeletedEvent? HeadRefDeletedEvent { get; set; }

		public HeadRefForcePushedEvent? HeadRefForcePushedEvent { get; set; }

		public HeadRefRestoredEvent? HeadRefRestoredEvent { get; set; }

		public IssueComment? IssueComment { get; set; }

		public LabeledEvent? LabeledEvent { get; set; }

		public LockedEvent? LockedEvent { get; set; }

		public MergedEvent? MergedEvent { get; set; }

		public MilestonedEvent? MilestonedEvent { get; set; }

		public PullRequestReview? PullRequestReview { get; set; }

		public PullRequestReviewComment? PullRequestReviewComment { get; set; }

		public PullRequestReviewThread? PullRequestReviewThread { get; set; }

		public ReferencedEvent? ReferencedEvent { get; set; }

		public RenamedTitleEvent? RenamedTitleEvent { get; set; }

		public ReopenedEvent? ReopenedEvent { get; set; }

		public ReviewDismissedEvent? ReviewDismissedEvent { get; set; }

		public ReviewRequestRemovedEvent? ReviewRequestRemovedEvent { get; set; }

		public ReviewRequestedEvent? ReviewRequestedEvent { get; set; }

		public SubscribedEvent? SubscribedEvent { get; set; }

		public UnassignedEvent? UnassignedEvent { get; set; }

		public UnlabeledEvent? UnlabeledEvent { get; set; }

		public UnlockedEvent? UnlockedEvent { get; set; }

		public UnsubscribedEvent? UnsubscribedEvent { get; set; }

		public UserBlockedEvent? UserBlockedEvent { get; set; }
	}

	public class PullRequestTimelineItemEdge
	{
		public string Cursor { get; set; } = default!;

		public PullRequestTimelineItem? Node { get; set; }
	}

	public class PullRequestTimelineItems
	{
		public AddedToProjectEvent? AddedToProjectEvent { get; set; }

		public AssignedEvent? AssignedEvent { get; set; }

		public AutoMergeDisabledEvent? AutoMergeDisabledEvent { get; set; }

		public AutoMergeEnabledEvent? AutoMergeEnabledEvent { get; set; }

		public AutoRebaseEnabledEvent? AutoRebaseEnabledEvent { get; set; }

		public AutoSquashEnabledEvent? AutoSquashEnabledEvent { get; set; }

		public AutomaticBaseChangeFailedEvent? AutomaticBaseChangeFailedEvent { get; set; }

		public AutomaticBaseChangeSucceededEvent? AutomaticBaseChangeSucceededEvent { get; set; }

		public BaseRefChangedEvent? BaseRefChangedEvent { get; set; }

		public BaseRefDeletedEvent? BaseRefDeletedEvent { get; set; }

		public BaseRefForcePushedEvent? BaseRefForcePushedEvent { get; set; }

		public ClosedEvent? ClosedEvent { get; set; }

		public CommentDeletedEvent? CommentDeletedEvent { get; set; }

		public ConnectedEvent? ConnectedEvent { get; set; }

		public ConvertToDraftEvent? ConvertToDraftEvent { get; set; }

		public ConvertedNoteToIssueEvent? ConvertedNoteToIssueEvent { get; set; }

		public ConvertedToDiscussionEvent? ConvertedToDiscussionEvent { get; set; }

		public CrossReferencedEvent? CrossReferencedEvent { get; set; }

		public DemilestonedEvent? DemilestonedEvent { get; set; }

		public DeployedEvent? DeployedEvent { get; set; }

		public DeploymentEnvironmentChangedEvent? DeploymentEnvironmentChangedEvent { get; set; }

		public DisconnectedEvent? DisconnectedEvent { get; set; }

		public HeadRefDeletedEvent? HeadRefDeletedEvent { get; set; }

		public HeadRefForcePushedEvent? HeadRefForcePushedEvent { get; set; }

		public HeadRefRestoredEvent? HeadRefRestoredEvent { get; set; }

		public IssueComment? IssueComment { get; set; }

		public LabeledEvent? LabeledEvent { get; set; }

		public LockedEvent? LockedEvent { get; set; }

		public MarkedAsDuplicateEvent? MarkedAsDuplicateEvent { get; set; }

		public MentionedEvent? MentionedEvent { get; set; }

		public MergedEvent? MergedEvent { get; set; }

		public MilestonedEvent? MilestonedEvent { get; set; }

		public MovedColumnsInProjectEvent? MovedColumnsInProjectEvent { get; set; }

		public PinnedEvent? PinnedEvent { get; set; }

		public PullRequestCommit? PullRequestCommit { get; set; }

		public PullRequestCommitCommentThread? PullRequestCommitCommentThread { get; set; }

		public PullRequestReview? PullRequestReview { get; set; }

		public PullRequestReviewThread? PullRequestReviewThread { get; set; }

		public PullRequestRevisionMarker? PullRequestRevisionMarker { get; set; }

		public ReadyForReviewEvent? ReadyForReviewEvent { get; set; }

		public ReferencedEvent? ReferencedEvent { get; set; }

		public RemovedFromProjectEvent? RemovedFromProjectEvent { get; set; }

		public RenamedTitleEvent? RenamedTitleEvent { get; set; }

		public ReopenedEvent? ReopenedEvent { get; set; }

		public ReviewDismissedEvent? ReviewDismissedEvent { get; set; }

		public ReviewRequestRemovedEvent? ReviewRequestRemovedEvent { get; set; }

		public ReviewRequestedEvent? ReviewRequestedEvent { get; set; }

		public SubscribedEvent? SubscribedEvent { get; set; }

		public TransferredEvent? TransferredEvent { get; set; }

		public UnassignedEvent? UnassignedEvent { get; set; }

		public UnlabeledEvent? UnlabeledEvent { get; set; }

		public UnlockedEvent? UnlockedEvent { get; set; }

		public UnmarkedAsDuplicateEvent? UnmarkedAsDuplicateEvent { get; set; }

		public UnpinnedEvent? UnpinnedEvent { get; set; }

		public UnsubscribedEvent? UnsubscribedEvent { get; set; }

		public UserBlockedEvent? UserBlockedEvent { get; set; }
	}

	public class PullRequestTimelineItemsConnection
	{
		public global::System.Collections.Generic.List<PullRequestTimelineItemsEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<PullRequestTimelineItems?>? Nodes { get; set; }

		public int PageCount { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }

		public global::System.DateTimeOffset UpdatedAt { get; set; }

		public string? UpdatedAtHumanized { get; set; }
	}

	public class PullRequestTimelineItemsEdge
	{
		public string Cursor { get; set; } = default!;

		public PullRequestTimelineItems? Node { get; set; }
	}

	public class ReactingUserConnection
	{
		public global::System.Collections.Generic.List<ReactingUserEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<User?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class ReactingUserEdge
	{
		public string Cursor { get; set; } = default!;

		public User Node { get; set; } = default!;
	}

	public class Reaction
	{
		public ReactionContent Content { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public User? User { get; set; }
	}

	public class ReactionConnection
	{
		public global::System.Collections.Generic.List<ReactionEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<Reaction?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }

		public bool ViewerHasReacted { get; set; }
	}

	public class ReactionEdge
	{
		public string Cursor { get; set; } = default!;

		public Reaction? Node { get; set; }
	}

	public class ReactionGroup
	{
		public ReactionContent Content { get; set; }

		public global::System.DateTimeOffset? CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public ReactorConnection Reactors { get; set; } = default!;

		public IReactable Subject { get; set; } = default!;

		public ReactingUserConnection Users { get; set; } = default!;

		public bool ViewerHasReacted { get; set; }
	}

	public class Reactor
	{
		public Bot? Bot { get; set; }

		public Mannequin? Mannequin { get; set; }

		public Organization? Organization { get; set; }

		public User? User { get; set; }
	}

	public class ReactorConnection
	{
		public global::System.Collections.Generic.List<ReactorEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<Reactor?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class ReactorEdge
	{
		public string Cursor { get; set; } = default!;

		public Reactor Node { get; set; } = default!;
	}

	public class ReadyForReviewEvent
	{
		public Actor? Actor { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public PullRequest PullRequest { get; set; } = default!;

		public string ResourcePath { get; set; } = default!;

		public string Url { get; set; } = default!;
	}

	public class Ref
	{
		public Comparison? Compare { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public string Name { get; set; } = default!;

		public Repository Repository { get; set; } = default!;

		public IGitObject? Target { get; set; }
	}

	public class RefConnection
	{
		public global::System.Collections.Generic.List<RefEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<Ref?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class RefEdge
	{
		public string Cursor { get; set; } = default!;

		public Ref? Node { get; set; }
	}

	public class ReferencedEvent
	{
		public Actor? Actor { get; set; }

		public Commit? Commit { get; set; }

		public Repository CommitRepository { get; set; } = default!;

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public bool IsCrossRepository { get; set; }

		public bool IsDirectReference { get; set; }

		public ReferencedSubject Subject { get; set; } = default!;
	}

	public class ReferencedSubject
	{
		public Issue? Issue { get; set; }

		public PullRequest? PullRequest { get; set; }
	}

	public class Release
	{
		public User? Author { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public string? Description { get; set; }

		public string? DescriptionHTML { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public bool IsDraft { get; set; }

		public bool IsLatest { get; set; }

		public bool IsPrerelease { get; set; }

		public string? Name { get; set; }

		public global::System.DateTimeOffset? PublishedAt { get; set; }

		public string? PublishedAtHumanized { get; set; }

		public global::System.Collections.Generic.List<ReactionGroup>? ReactionGroups { get; set; }

		public ReactionConnection Reactions { get; set; } = default!;

		public ReleaseAssetConnection ReleaseAssets { get; set; } = default!;

		public Repository Repository { get; set; } = default!;

		public string ResourcePath { get; set; } = default!;

		public Ref? Tag { get; set; }

		public Commit? TagCommit { get; set; }

		public string TagName { get; set; } = default!;

		public global::System.DateTimeOffset UpdatedAt { get; set; }

		public string? UpdatedAtHumanized { get; set; }

		public string Url { get; set; } = default!;

		public bool ViewerCanReact { get; set; }
	}

	public class ReleaseAsset
	{
		public string ContentType { get; set; } = default!;

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public int DownloadCount { get; set; }

		public string DownloadUrl { get; set; } = default!;

		public global::Octokit.GraphQL.ID Id { get; set; }

		public string Name { get; set; } = default!;

		public Release? Release { get; set; }

		public int Size { get; set; }

		public global::System.DateTimeOffset UpdatedAt { get; set; }

		public string? UpdatedAtHumanized { get; set; }

		public string Url { get; set; } = default!;
	}

	public class ReleaseAssetConnection
	{
		public global::System.Collections.Generic.List<ReleaseAssetEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<ReleaseAsset?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class ReleaseAssetEdge
	{
		public string Cursor { get; set; } = default!;

		public ReleaseAsset? Node { get; set; }
	}

	public class ReleaseConnection
	{
		public global::System.Collections.Generic.List<ReleaseEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<Release?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class ReleaseEdge
	{
		public string Cursor { get; set; } = default!;

		public Release? Node { get; set; }
	}

	public class ReleaseOrder
	{
		public OrderDirection Direction { get; set; }

		public ReleaseOrderField Field { get; set; }
	}

	public class RemoveReactionRequest
	{
		public string? ClientMutationId { get; set; }

		public ReactionContent Content { get; set; }

		public global::Octokit.GraphQL.ID SubjectId { get; set; }
	}

	public class RemoveReactionResult
	{
		public string? ClientMutationId { get; set; }

		public Reaction? Reaction { get; set; }

		public global::System.Collections.Generic.List<ReactionGroup>? ReactionGroups { get; set; }

		public IReactable? Subject { get; set; }
	}

	public class RemoveStarResult
	{
		public string? ClientMutationId { get; set; }
	}

	public class RemovedFromProjectEvent
	{
		public Actor? Actor { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }
	}

	public class RenamedTitleEvent
	{
		public Actor? Actor { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public string CurrentTitle { get; set; } = default!;

		public global::Octokit.GraphQL.ID Id { get; set; }

		public string PreviousTitle { get; set; } = default!;

		public RenamedTitleSubject Subject { get; set; } = default!;
	}

	public class RenamedTitleSubject
	{
		public Issue? Issue { get; set; }

		public PullRequest? PullRequest { get; set; }
	}

	public class ReopenIssueRequest
	{
		public string? ClientMutationId { get; set; }

		public global::Octokit.GraphQL.ID IssueId { get; set; }
	}

	public class ReopenIssueResult
	{
		public string? ClientMutationId { get; set; }

		public Issue? Issue { get; set; }
	}

	public class ReopenPullRequestRequest
	{
		public string? ClientMutationId { get; set; }

		public global::Octokit.GraphQL.ID PullRequestId { get; set; }
	}

	public class ReopenPullRequestResult
	{
		public string? ClientMutationId { get; set; }

		public PullRequest? PullRequest { get; set; }
	}

	public class ReopenedEvent
	{
		public Actor? Actor { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public IssueStateReason? StateReason { get; set; }
	}

	public class Repository
	{
		public UserConnection AssignableUsers { get; set; } = default!;

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public Ref? DefaultBranchRef { get; set; }

		public string? Description { get; set; }

		public string DescriptionHTML { get; set; } = default!;

		public Discussion? Discussion { get; set; }

		public DiscussionCategory? DiscussionCategory { get; set; }

		public DiscussionConnection Discussions { get; set; } = default!;

		public Environment? Environment { get; set; }

		public int ForkCount { get; set; }

		public bool ForkingAllowed { get; set; }

		public bool HasIssuesEnabled { get; set; }

		public bool HasProjectsEnabled { get; set; }

		public bool HasSponsorshipsEnabled { get; set; }

		public string? HomepageUrl { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public bool IsArchived { get; set; }

		public bool IsEmpty { get; set; }

		public bool IsFork { get; set; }

		public bool IsInOrganization { get; set; }

		public bool IsMirror { get; set; }

		public bool IsPrivate { get; set; }

		public bool IsTemplate { get; set; }

		public Issue? Issue { get; set; }

		public IssueOrPullRequest? IssueOrPullRequest { get; set; }

		public IssueConnection Issues { get; set; } = default!;

		public Label? Label { get; set; }

		public LabelConnection? Labels { get; set; }

		public LanguageConnection? Languages { get; set; }

		public Release? LatestRelease { get; set; }

		public License? LicenseInfo { get; set; }

		public RepositoryLockReason? LockReason { get; set; }

		public Milestone? Milestone { get; set; }

		public MilestoneConnection? Milestones { get; set; }

		public string Name { get; set; } = default!;

		public string NameWithOwner { get; set; } = default!;

		public IGitObject? Object { get; set; }

		public RepositoryOwner Owner { get; set; } = default!;

		public PackageConnection Packages { get; set; } = default!;

		public PinnedIssueConnection? PinnedIssues { get; set; }

		public Language? PrimaryLanguage { get; set; }

		public ProjectV2? ProjectV2 { get; set; }

		public ProjectConnection Projects { get; set; } = default!;

		public ProjectV2Connection ProjectsV2 { get; set; } = default!;

		public PullRequest? PullRequest { get; set; }

		public PullRequestConnection PullRequests { get; set; } = default!;

		public global::System.DateTimeOffset? PushedAt { get; set; }

		public Ref? Ref { get; set; }

		public RefConnection? Refs { get; set; }

		public Release? Release { get; set; }

		public ReleaseConnection Releases { get; set; } = default!;

		public string ResourcePath { get; set; } = default!;

		public int StargazerCount { get; set; }

		public global::System.DateTimeOffset UpdatedAt { get; set; }

		public string? UpdatedAtHumanized { get; set; }

		public string Url { get; set; } = default!;

		public bool ViewerCanSubscribe { get; set; }

		public bool ViewerHasStarred { get; set; }

		public RepositoryPermission? ViewerPermission { get; set; }

		public SubscriptionState? ViewerSubscription { get; set; }

		public RepositoryVisibility Visibility { get; set; }

		public UserConnection Watchers { get; set; } = default!;
	}

	public class RepositoryConnection
	{
		public global::System.Collections.Generic.List<RepositoryEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<Repository?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class RepositoryEdge
	{
		public string Cursor { get; set; } = default!;

		public Repository? Node { get; set; }
	}

	public class RepositoryInvitation
	{
		public string? Email { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public IRepositoryInfo? Repository { get; set; }
	}

	public class RepositoryOrder
	{
		public OrderDirection Direction { get; set; }

		public RepositoryOrderField Field { get; set; }
	}

	public class RepositoryOwner : IRepositoryOwner
	{
		public string AvatarUrl { get; set; } = default!;

		public global::Octokit.GraphQL.ID Id { get; set; }

		public string Login { get; set; } = default!;

		public RepositoryConnection Repositories { get; set; } = default!;

		public Repository? Repository { get; set; }

		public string ResourcePath { get; set; } = default!;

		public string Url { get; set; } = default!;
	}

	public class RequestedReviewer
	{
		public string? AvatarUrl { get; set; }

		public Bot? Bot { get; set; }

		public string? Login { get; set; }

		public Mannequin? Mannequin { get; set; }

		public User? User { get; set; }
	}

	public class ReviewDismissedEvent
	{
		public Actor? Actor { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public string? DismissalMessage { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public PullRequest PullRequest { get; set; } = default!;

		public PullRequestCommit? PullRequestCommit { get; set; }

		public string ResourcePath { get; set; } = default!;

		public PullRequestReview? Review { get; set; }

		public string Url { get; set; } = default!;
	}

	public class ReviewRequest
	{
		public global::Octokit.GraphQL.ID Id { get; set; }

		public PullRequest PullRequest { get; set; } = default!;

		public RequestedReviewer? RequestedReviewer { get; set; }
	}

	public class ReviewRequestConnection
	{
		public global::System.Collections.Generic.List<ReviewRequestEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<ReviewRequest?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class ReviewRequestEdge
	{
		public string Cursor { get; set; } = default!;

		public ReviewRequest? Node { get; set; }
	}

	public class ReviewRequestRemovedEvent
	{
		public Actor? Actor { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public PullRequest PullRequest { get; set; } = default!;

		public RequestedReviewer? RequestedReviewer { get; set; }
	}

	public class ReviewRequestedEvent
	{
		public Actor? Actor { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public PullRequest PullRequest { get; set; } = default!;

		public RequestedReviewer? RequestedReviewer { get; set; }
	}

	public class Sponsor
	{
		public Organization? Organization { get; set; }

		public User? User { get; set; }
	}

	public class SponsorConnection
	{
		public global::System.Collections.Generic.List<SponsorEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<Sponsor?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class SponsorEdge
	{
		public string Cursor { get; set; } = default!;

		public Sponsor? Node { get; set; }
	}

	public class StarOrder
	{
		public OrderDirection Direction { get; set; }

		public StarOrderField Field { get; set; }
	}

	public class StarredRepositoryConnection
	{
		public global::System.Collections.Generic.List<StarredRepositoryEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<Repository?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class StarredRepositoryEdge
	{
		public string Cursor { get; set; } = default!;

		public Repository Node { get; set; } = default!;
	}

	public class Status
	{
		public Commit? Commit { get; set; }

		public StatusContext? Context { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public StatusState State { get; set; }
	}

	public class StatusCheckRollup
	{
		public Commit? Commit { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public StatusState State { get; set; }
	}

	public class StatusContext
	{
		public string? AvatarUrl { get; set; }

		public Commit? Commit { get; set; }

		public string Context { get; set; } = default!;

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public Actor? Creator { get; set; }

		public string? Description { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public StatusState State { get; set; }

		public string? TargetUrl { get; set; }
	}

	public class Subscribable : ISubscribable
	{
		public global::Octokit.GraphQL.ID Id { get; set; }

		public bool ViewerCanSubscribe { get; set; }

		public SubscriptionState? ViewerSubscription { get; set; }
	}

	public class SubscribedEvent
	{
		public Actor? Actor { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public Subscribable Subscribable { get; set; } = default!;
	}

	public class Tag
	{
		public string AbbreviatedOid { get; set; } = default!;

		public string CommitUrl { get; set; } = default!;

		public global::Octokit.GraphQL.ID Id { get; set; }

		public string? Message { get; set; }

		public string Name { get; set; } = default!;

		public string Oid { get; set; } = default!;

		public Repository Repository { get; set; } = default!;

		public IGitObject Target { get; set; } = default!;
	}

	public class Topic
	{
		public global::Octokit.GraphQL.ID Id { get; set; }

		public string Name { get; set; } = default!;

		public RepositoryConnection Repositories { get; set; } = default!;

		public int StargazerCount { get; set; }

		public bool ViewerHasStarred { get; set; }
	}

	public class TransferredEvent
	{
		public Actor? Actor { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public Repository? FromRepository { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public Issue Issue { get; set; } = default!;
	}

	public class Tree
	{
		public string AbbreviatedOid { get; set; } = default!;

		public string CommitUrl { get; set; } = default!;

		public global::System.Collections.Generic.List<TreeEntry>? Entries { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public string Oid { get; set; } = default!;

		public Repository Repository { get; set; } = default!;
	}

	public class TreeEntry
	{
		public Language? Language { get; set; }

		public int Mode { get; set; }

		public string Name { get; set; } = default!;

		public IGitObject? Object { get; set; }

		public string Oid { get; set; } = default!;

		public string? Path { get; set; }

		public Repository Repository { get; set; } = default!;

		public int Size { get; set; }

		public string Type { get; set; } = default!;
	}

	public class UnassignedEvent
	{
		public Actor? Actor { get; set; }

		public Assignee? Assignee { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public User? User { get; set; }
	}

	public class UnlabeledEvent
	{
		public Actor? Actor { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public Label Label { get; set; } = default!;
	}

	public class UnlockedEvent
	{
		public Actor? Actor { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }
	}

	public class UnmarkedAsDuplicateEvent
	{
		public Actor? Actor { get; set; }

		public IssueOrPullRequest? Canonical { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public IssueOrPullRequest? Duplicate { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public bool IsCrossRepository { get; set; }
	}

	public class UnpinnedEvent
	{
		public Actor? Actor { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public Issue Issue { get; set; } = default!;
	}

	public class UnsubscribedEvent
	{
		public Actor? Actor { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public Subscribable Subscribable { get; set; } = default!;
	}

	public class UpdateIssueCommentRequest
	{
		public string Body { get; set; } = default!;

		public string? ClientMutationId { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }
	}

	public class UpdateIssueCommentResult
	{
		public string? ClientMutationId { get; set; }

		public IssueComment? IssueComment { get; set; }
	}

	public class UpdateIssueRequest
	{
		public global::System.Collections.Generic.List<global::Octokit.GraphQL.ID>? AssigneeIds { get; set; }

		public string? Body { get; set; }

		public string? ClientMutationId { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public global::System.Collections.Generic.List<global::Octokit.GraphQL.ID>? LabelIds { get; set; }

		public global::Octokit.GraphQL.ID? MilestoneId { get; set; }

		public global::System.Collections.Generic.List<global::Octokit.GraphQL.ID>? ProjectIds { get; set; }

		public IssueState? State { get; set; }

		public string? Title { get; set; }
	}

	public class UpdateIssueResult
	{
		public Actor? Actor { get; set; }

		public string? ClientMutationId { get; set; }

		public Issue? Issue { get; set; }
	}

	public class UpdatePullRequestRequest
	{
		public global::System.Collections.Generic.List<global::Octokit.GraphQL.ID>? AssigneeIds { get; set; }

		public string? BaseRefName { get; set; }

		public string? Body { get; set; }

		public string? ClientMutationId { get; set; }

		public global::System.Collections.Generic.List<global::Octokit.GraphQL.ID>? LabelIds { get; set; }

		public bool? MaintainerCanModify { get; set; }

		public global::Octokit.GraphQL.ID? MilestoneId { get; set; }

		public global::System.Collections.Generic.List<global::Octokit.GraphQL.ID>? ProjectIds { get; set; }

		public global::Octokit.GraphQL.ID PullRequestId { get; set; }

		public PullRequestUpdateState? State { get; set; }

		public string? Title { get; set; }
	}

	public class UpdatePullRequestResult
	{
		public Actor? Actor { get; set; }

		public string? ClientMutationId { get; set; }

		public PullRequest? PullRequest { get; set; }
	}

	public class UpdateSubscriptionRequest
	{
		public string? ClientMutationId { get; set; }

		public SubscriptionState State { get; set; }

		public global::Octokit.GraphQL.ID SubscribableId { get; set; }
	}

	public class UpdateSubscriptionResult
	{
		public string? ClientMutationId { get; set; }

		public Subscribable? Subscribable { get; set; }
	}

	public class User
	{
		public string AvatarUrl { get; set; } = default!;

		public string? Bio { get; set; }

		public string? Company { get; set; }

		public ContributionsCollection ContributionsCollection { get; set; } = default!;

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public string Email { get; set; } = default!;

		public FollowerConnection Followers { get; set; } = default!;

		public FollowingConnection Following { get; set; } = default!;

		public global::Octokit.GraphQL.ID Id { get; set; }

		public bool IsBountyHunter { get; set; }

		public bool IsCampusExpert { get; set; }

		public bool IsDeveloperProgramMember { get; set; }

		public bool IsEmployee { get; set; }

		public bool IsGitHubStar { get; set; }

		public bool IsViewer { get; set; }

		public IssueConnection Issues { get; set; } = default!;

		public string? Location { get; set; }

		public string Login { get; set; } = default!;

		public string? Name { get; set; }

		public Organization? Organization { get; set; }

		public OrganizationConnection Organizations { get; set; } = default!;

		public PackageConnection Packages { get; set; } = default!;

		public PinnableItemConnection PinnableItems { get; set; } = default!;

		public PinnableItemConnection PinnedItems { get; set; } = default!;

		public ProjectV2? ProjectV2 { get; set; }

		public ProjectConnection Projects { get; set; } = default!;

		public ProjectV2Connection ProjectsV2 { get; set; } = default!;

		public PullRequestConnection PullRequests { get; set; } = default!;

		public RepositoryConnection Repositories { get; set; } = default!;

		public Repository? Repository { get; set; }

		public DiscussionConnection RepositoryDiscussions { get; set; } = default!;

		public string ResourcePath { get; set; } = default!;

		public SponsorConnection Sponsors { get; set; } = default!;

		public StarredRepositoryConnection StarredRepositories { get; set; } = default!;

		public UserStatus? Status { get; set; }

		public RepositoryConnection TopRepositories { get; set; } = default!;

		public string? TwitterUsername { get; set; }

		public global::System.DateTimeOffset UpdatedAt { get; set; }

		public string? UpdatedAtHumanized { get; set; }

		public string Url { get; set; } = default!;

		public bool ViewerCanChangePinnedItems { get; set; }

		public bool ViewerCanSponsor { get; set; }

		public bool ViewerIsFollowing { get; set; }

		public bool ViewerIsSponsoring { get; set; }

		public string? WebsiteUrl { get; set; }
	}

	public class UserBlockedEvent
	{
		public Actor? Actor { get; set; }

		public UserBlockDuration BlockDuration { get; set; }

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public User? Subject { get; set; }
	}

	public class UserConnection
	{
		public global::System.Collections.Generic.List<UserEdge?>? Edges { get; set; }

		public global::System.Collections.Generic.List<User?>? Nodes { get; set; }

		public PageInfo PageInfo { get; set; } = default!;

		public int TotalCount { get; set; }
	}

	public class UserEdge
	{
		public string Cursor { get; set; } = default!;

		public User? Node { get; set; }
	}

	public class UserStatus
	{
		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public string? Emoji { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public bool IndicatesLimitedAvailability { get; set; }

		public string? Message { get; set; }

		public Organization? Organization { get; set; }

		public global::System.DateTimeOffset UpdatedAt { get; set; }

		public string? UpdatedAtHumanized { get; set; }

		public User User { get; set; } = default!;
	}

	public class WorkflowRun
	{
		public CheckSuite CheckSuite { get; set; } = default!;

		public global::System.DateTimeOffset CreatedAt { get; set; }

		public string? CreatedAtHumanized { get; set; }

		public string Event { get; set; } = default!;

		public WorkflowRunFile? File { get; set; }

		public global::Octokit.GraphQL.ID Id { get; set; }

		public string ResourcePath { get; set; } = default!;

		public int RunNumber { get; set; }

		public global::System.DateTimeOffset UpdatedAt { get; set; }

		public string? UpdatedAtHumanized { get; set; }

		public string Url { get; set; } = default!;
	}

	public class WorkflowRunFile
	{
		public global::Octokit.GraphQL.ID Id { get; set; }

		public string Path { get; set; } = default!;

		public string RepositoryName { get; set; } = default!;

		public string ResourcePath { get; set; } = default!;

		public WorkflowRun Run { get; set; } = default!;

		public string Url { get; set; } = default!;
	}
}
