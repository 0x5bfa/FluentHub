using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Utils;
using Microsoft.UI.Xaml.Media;
using FluentHub.Octokit.Models.v4;

namespace FluentHub.ViewModels.UserControls
{
	public class TimelineViewModel : ObservableObject
	{
		#region Fields and Properties
		private string _eventType = default!;
		public string EventType { get => _eventType; set => SetProperty(ref _eventType, value); }

		private object _event = default!;
		public object Event { get => _event; set => SetProperty(ref _event, value); }

		private Actor _actor = default!;
		public Actor Actor { get => _actor; set => SetProperty(ref _actor, value); }

		private SolidColorBrush _timelineBadgeBackground = default!;
		public SolidColorBrush TimelineBadgeBackground { get => _timelineBadgeBackground; set => SetProperty(ref _timelineBadgeBackground, value); }

		private string _timelineBadgeGlyph = default!;
		public string TimelineBadgeGlyph { get => _timelineBadgeGlyph; set => SetProperty(ref _timelineBadgeGlyph, value); }
		#endregion

		#region All events
		private AddedToProjectEvent addedToProjectEvent = default!;
		public AddedToProjectEvent AddedToProjectEvent { get => addedToProjectEvent; set => SetProperty(ref addedToProjectEvent, value); }

		private AssignedEvent assignedEvent = default!;
		public AssignedEvent AssignedEvent { get => assignedEvent; set => SetProperty(ref assignedEvent, value); }

		private AutoMergeDisabledEvent autoMergeDisabledEvent = default!;
		public AutoMergeDisabledEvent AutoMergeDisabledEvent { get => autoMergeDisabledEvent; set => SetProperty(ref autoMergeDisabledEvent, value); }

		private AutoMergeEnabledEvent autoMergeEnabledEvent = default!;
		public AutoMergeEnabledEvent AutoMergeEnabledEvent { get => autoMergeEnabledEvent; set => SetProperty(ref autoMergeEnabledEvent, value); }

		private AutoRebaseEnabledEvent autoRebaseEnabledEvent = default!;
		public AutoRebaseEnabledEvent AutoRebaseEnabledEvent { get => autoRebaseEnabledEvent; set => SetProperty(ref autoRebaseEnabledEvent, value); }

		private AutoSquashEnabledEvent autoSquashEnabledEvent = default!;
		public AutoSquashEnabledEvent AutoSquashEnabledEvent { get => autoSquashEnabledEvent; set => SetProperty(ref autoSquashEnabledEvent, value); }

		private AutomaticBaseChangeFailedEvent automaticBaseChangeFailedEvent = default!;
		public AutomaticBaseChangeFailedEvent AutomaticBaseChangeFailedEvent { get => automaticBaseChangeFailedEvent; set => SetProperty(ref automaticBaseChangeFailedEvent, value); }

		private AutomaticBaseChangeSucceededEvent automaticBaseChangeSucceededEvent = default!;
		public AutomaticBaseChangeSucceededEvent AutomaticBaseChangeSucceededEvent { get => automaticBaseChangeSucceededEvent; set => SetProperty(ref automaticBaseChangeSucceededEvent, value); }

		private BaseRefChangedEvent baseRefChangedEvent = default!;
		public BaseRefChangedEvent BaseRefChangedEvent { get => baseRefChangedEvent; set => SetProperty(ref baseRefChangedEvent, value); }

		private BaseRefDeletedEvent baseRefDeletedEvent = default!;
		public BaseRefDeletedEvent BaseRefDeletedEvent { get => baseRefDeletedEvent; set => SetProperty(ref baseRefDeletedEvent, value); }

		private BaseRefForcePushedEvent baseRefForcePushedEvent = default!;
		public BaseRefForcePushedEvent BaseRefForcePushedEvent { get => baseRefForcePushedEvent; set => SetProperty(ref baseRefForcePushedEvent, value); }

		private ClosedEvent closedEvent = default!;
		public ClosedEvent ClosedEvent { get => closedEvent; set => SetProperty(ref closedEvent, value); }

		private CommentDeletedEvent commentDeletedEvent = default!;
		public CommentDeletedEvent CommentDeletedEvent { get => commentDeletedEvent; set => SetProperty(ref commentDeletedEvent, value); }

		private ConnectedEvent connectedEvent = default!;
		public ConnectedEvent ConnectedEvent { get => connectedEvent; set => SetProperty(ref connectedEvent, value); }

		private ConvertedNoteToIssueEvent convertedNoteToIssueEvent = default!;
		public ConvertedNoteToIssueEvent ConvertedNoteToIssueEvent { get => convertedNoteToIssueEvent; set => SetProperty(ref convertedNoteToIssueEvent, value); }

		private ConvertToDraftEvent convertToDraftEvent = default!;
		public ConvertToDraftEvent ConvertToDraftEvent { get => convertToDraftEvent; set => SetProperty(ref convertToDraftEvent, value); }

		private CrossReferencedEvent crossReferencedEvent = default!;
		public CrossReferencedEvent CrossReferencedEvent { get => crossReferencedEvent; set => SetProperty(ref crossReferencedEvent, value); }

		private DemilestonedEvent demilestonedEvent = default!;
		public DemilestonedEvent DemilestonedEvent { get => demilestonedEvent; set => SetProperty(ref demilestonedEvent, value); }

		private DeployedEvent deployedEvent = default!;
		public DeployedEvent DeployedEvent { get => deployedEvent; set => SetProperty(ref deployedEvent, value); }

		private DeploymentEnvironmentChangedEvent deploymentEnvironmentChangedEvent = default!;
		public DeploymentEnvironmentChangedEvent DeploymentEnvironmentChangedEvent { get => deploymentEnvironmentChangedEvent; set => SetProperty(ref deploymentEnvironmentChangedEvent, value); }

		private DisconnectedEvent disconnectedEvent = default!;
		public DisconnectedEvent DisconnectedEvent { get => disconnectedEvent; set => SetProperty(ref disconnectedEvent, value); }

		private HeadRefDeletedEvent headRefDeletedEvent = default!;
		public HeadRefDeletedEvent HeadRefDeletedEvent { get => headRefDeletedEvent; set => SetProperty(ref headRefDeletedEvent, value); }

		private HeadRefForcePushedEvent headRefForcePushedEvent = default!;
		public HeadRefForcePushedEvent HeadRefForcePushedEvent { get => headRefForcePushedEvent; set => SetProperty(ref headRefForcePushedEvent, value); }

		private HeadRefRestoredEvent headRefRestoredEvent = default!;
		public HeadRefRestoredEvent HeadRefRestoredEvent { get => headRefRestoredEvent; set => SetProperty(ref headRefRestoredEvent, value); }

		private IssueComment issueComment = default!;
		public IssueComment IssueComment { get => issueComment; set => SetProperty(ref issueComment, value); }

		private LabeledEvent labeledEvent = default!;
		public LabeledEvent LabeledEvent { get => labeledEvent; set => SetProperty(ref labeledEvent, value); }

		private LockedEvent lockedEvent = default!;
		public LockedEvent LockedEvent { get => lockedEvent; set => SetProperty(ref lockedEvent, value); }

		private MarkedAsDuplicateEvent markedAsDuplicateEvent = default!;
		public MarkedAsDuplicateEvent MarkedAsDuplicateEvent { get => markedAsDuplicateEvent; set => SetProperty(ref markedAsDuplicateEvent, value); }

		//private MentionedEvent  mentionedEvent;
		//public MentionedEvent MentionedEvent { get => mentionedEvent; set => SetProperty(ref mentionedEvent, value); }

		private MergedEvent mergedEvent = default!;
		public MergedEvent MergedEvent { get => mergedEvent; set => SetProperty(ref mergedEvent, value); }

		private MilestonedEvent milestonedEvent = default!;
		public MilestonedEvent MilestonedEvent { get => milestonedEvent; set => SetProperty(ref milestonedEvent, value); }

		private MovedColumnsInProjectEvent movedColumnsInProjectEvent = default!;
		public MovedColumnsInProjectEvent MovedColumnsInProjectEvent { get => movedColumnsInProjectEvent; set => SetProperty(ref movedColumnsInProjectEvent, value); }

		private PinnedEvent pinnedEvent = default!;
		public PinnedEvent PinnedEvent { get => pinnedEvent; set => SetProperty(ref pinnedEvent, value); }

		private PullRequestCommit pullRequestCommit = default!;
		public PullRequestCommit PullRequestCommit { get => pullRequestCommit; set => SetProperty(ref pullRequestCommit, value); }

		private PullRequestCommitCommentThread pullRequestCommitCommentThread = default!;
		public PullRequestCommitCommentThread PullRequestCommitCommentThread { get => pullRequestCommitCommentThread; set => SetProperty(ref pullRequestCommitCommentThread, value); }

		private PullRequestReview pullRequestReview = default!;
		public PullRequestReview PullRequestReview { get => pullRequestReview; set => SetProperty(ref pullRequestReview, value); }

		private PullRequestReviewThread pullRequestReviewThread = default!;
		public PullRequestReviewThread PullRequestReviewThread { get => pullRequestReviewThread; set => SetProperty(ref pullRequestReviewThread, value); }

		private PullRequestRevisionMarker pullRequestRevisionMarker = default!;
		public PullRequestRevisionMarker PullRequestRevisionMarker { get => pullRequestRevisionMarker; set => SetProperty(ref pullRequestRevisionMarker, value); }

		private ReadyForReviewEvent readyForReviewEvent = default!;
		public ReadyForReviewEvent ReadyForReviewEvent { get => readyForReviewEvent; set => SetProperty(ref readyForReviewEvent, value); }

		private ReferencedEvent referencedEvent = default!;
		public ReferencedEvent ReferencedEvent { get => referencedEvent; set => SetProperty(ref referencedEvent, value); }

		private RemovedFromProjectEvent removedFromProjectEvent = default!;
		public RemovedFromProjectEvent RemovedFromProjectEvent { get => removedFromProjectEvent; set => SetProperty(ref removedFromProjectEvent, value); }

		private RenamedTitleEvent renamedTitleEvent = default!;
		public RenamedTitleEvent RenamedTitleEvent { get => renamedTitleEvent; set => SetProperty(ref renamedTitleEvent, value); }

		private ReopenedEvent reopenedEvent = default!;
		public ReopenedEvent ReopenedEvent { get => reopenedEvent; set => SetProperty(ref reopenedEvent, value); }

		private ReviewDismissedEvent reviewDismissedEvent = default!;
		public ReviewDismissedEvent ReviewDismissedEvent { get => reviewDismissedEvent; set => SetProperty(ref reviewDismissedEvent, value); }

		private ReviewRequestedEvent reviewRequestedEvent = default!;
		public ReviewRequestedEvent ReviewRequestedEvent { get => reviewRequestedEvent; set => SetProperty(ref reviewRequestedEvent, value); }

		private ReviewRequestRemovedEvent reviewRequestRemovedEvent = default!;
		public ReviewRequestRemovedEvent ReviewRequestRemovedEvent { get => reviewRequestRemovedEvent; set => SetProperty(ref reviewRequestRemovedEvent, value); }

		//private SubscribedEvent  subscribedEvent;
		//public SubscribedEvent SubscribedEvent { get => subscribedEvent; set => SetProperty(ref subscribedEvent, value); }

		private TransferredEvent transferredEvent = default!;
		public TransferredEvent TransferredEvent { get => transferredEvent; set => SetProperty(ref transferredEvent, value); }

		private UnassignedEvent unassignedEvent = default!;
		public UnassignedEvent UnassignedEvent { get => unassignedEvent; set => SetProperty(ref unassignedEvent, value); }

		private UnlabeledEvent unlabeledEvent = default!;
		public UnlabeledEvent UnlabeledEvent { get => unlabeledEvent; set => SetProperty(ref unlabeledEvent, value); }

		private UnlockedEvent unlockedEvent = default!;
		public UnlockedEvent UnlockedEvent { get => unlockedEvent; set => SetProperty(ref unlockedEvent, value); }

		private UnmarkedAsDuplicateEvent unmarkedAsDuplicateEvent = default!;
		public UnmarkedAsDuplicateEvent UnmarkedAsDuplicateEvent { get => unmarkedAsDuplicateEvent; set => SetProperty(ref unmarkedAsDuplicateEvent, value); }

		private UnpinnedEvent unpinnedEvent = default!;
		public UnpinnedEvent UnpinnedEvent { get => unpinnedEvent; set => SetProperty(ref unpinnedEvent, value); }

		//private UnsubscribedEvent  unsubscribedEvent;
		//public UnsubscribedEvent UnsubscribedEvent { get => unsubscribedEvent; set => SetProperty(ref unsubscribedEvent, value); }

		private UserBlockedEvent userBlockedEvent = default!;
		public UserBlockedEvent UserBlockedEvent { get => userBlockedEvent; set => SetProperty(ref userBlockedEvent, value); }
		#endregion

		#region Viewmodels
		private IssueCommentBlockViewModel _commentBlockViewModel = default!;
		public IssueCommentBlockViewModel CommentBlockViewModel { get => _commentBlockViewModel; set => SetProperty(ref _commentBlockViewModel, value); }
		#endregion
	}
}
