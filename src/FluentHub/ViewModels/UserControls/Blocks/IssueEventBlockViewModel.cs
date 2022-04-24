using FluentHub.Octokit.Models.Events;
using FluentHub.ViewModels.UserControls.Labels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Windows.UI.Xaml.Media;

namespace FluentHub.ViewModels.UserControls.Blocks
{
    public class IssueEventBlockViewModel : INotifyPropertyChanged
    {
        private string _eventType;
        public string EventType { get => _eventType; set => SetProperty(ref _eventType, value); }

        private object _event;
        public object Event { get => _event; set => SetProperty(ref _event, value); }

        private Actor _actor;
        public Actor Actor { get => _actor; set => SetProperty(ref _actor, value); }

        private SolidColorBrush _timelineBadgeBackground;
        public SolidColorBrush TimelineBadgeBackground { get => _timelineBadgeBackground; set => SetProperty(ref _timelineBadgeBackground, value); }

        private string _timelineBadgeGlyph;
        public string TimelineBadgeGlyph { get => _timelineBadgeGlyph; set => SetProperty(ref _timelineBadgeGlyph, value); }

        #region AllEvents
        private AddedToProjectEvent addedToProjectEvent;
        public AddedToProjectEvent AddedToProjectEvent { get => addedToProjectEvent; set => SetProperty(ref addedToProjectEvent, value); }

        private AssignedEvent assignedEvent;
        public AssignedEvent AssignedEvent { get => assignedEvent; set => SetProperty(ref assignedEvent, value); }

        private AutoMergeDisabledEvent autoMergeDisabledEvent;
        public AutoMergeDisabledEvent AutoMergeDisabledEvent { get => autoMergeDisabledEvent; set => SetProperty(ref autoMergeDisabledEvent, value); }

        private AutoMergeEnabledEvent autoMergeEnabledEvent;
        public AutoMergeEnabledEvent AutoMergeEnabledEvent { get => autoMergeEnabledEvent; set => SetProperty(ref autoMergeEnabledEvent, value); }

        private AutoRebaseEnabledEvent autoRebaseEnabledEvent;
        public AutoRebaseEnabledEvent AutoRebaseEnabledEvent { get => autoRebaseEnabledEvent; set => SetProperty(ref autoRebaseEnabledEvent, value); }

        private AutoSquashEnabledEvent autoSquashEnabledEvent;
        public AutoSquashEnabledEvent AutoSquashEnabledEvent { get => autoSquashEnabledEvent; set => SetProperty(ref autoSquashEnabledEvent, value); }

        private AutomaticBaseChangeFailedEvent automaticBaseChangeFailedEvent;
        public AutomaticBaseChangeFailedEvent AutomaticBaseChangeFailedEvent { get => automaticBaseChangeFailedEvent; set => SetProperty(ref automaticBaseChangeFailedEvent, value); }

        private AutomaticBaseChangeSucceededEvent automaticBaseChangeSucceededEvent;
        public AutomaticBaseChangeSucceededEvent AutomaticBaseChangeSucceededEvent { get => automaticBaseChangeSucceededEvent; set => SetProperty(ref automaticBaseChangeSucceededEvent, value); }

        private BaseRefChangedEvent baseRefChangedEvent;
        public BaseRefChangedEvent BaseRefChangedEvent { get => baseRefChangedEvent; set => SetProperty(ref baseRefChangedEvent, value); }

        private BaseRefDeletedEvent baseRefDeletedEvent;
        public BaseRefDeletedEvent BaseRefDeletedEvent { get => baseRefDeletedEvent; set => SetProperty(ref baseRefDeletedEvent, value); }

        private BaseRefForcePushedEvent baseRefForcePushedEvent;
        public BaseRefForcePushedEvent BaseRefForcePushedEvent { get => baseRefForcePushedEvent; set => SetProperty(ref baseRefForcePushedEvent, value); }

        private ClosedEvent closedEvent;
        public ClosedEvent ClosedEvent { get => closedEvent; set => SetProperty(ref closedEvent, value); }

        private CommentDeletedEvent commentDeletedEvent;
        public CommentDeletedEvent CommentDeletedEvent { get => commentDeletedEvent; set => SetProperty(ref commentDeletedEvent, value); }

        private ConnectedEvent connectedEvent;
        public ConnectedEvent ConnectedEvent { get => connectedEvent; set => SetProperty(ref connectedEvent, value); }

        private ConvertedNoteToIssueEvent convertedNoteToIssueEvent;
        public ConvertedNoteToIssueEvent ConvertedNoteToIssueEvent { get => convertedNoteToIssueEvent; set => SetProperty(ref convertedNoteToIssueEvent, value); }

        private ConvertToDraftEvent convertToDraftEvent;
        public ConvertToDraftEvent ConvertToDraftEvent { get => convertToDraftEvent; set => SetProperty(ref convertToDraftEvent, value); }

        private CrossReferencedEvent crossReferencedEvent;
        public CrossReferencedEvent CrossReferencedEvent { get => crossReferencedEvent; set => SetProperty(ref crossReferencedEvent, value); }

        private DemilestonedEvent demilestonedEvent;
        public DemilestonedEvent DemilestonedEvent { get => demilestonedEvent; set => SetProperty(ref demilestonedEvent, value); }

        private DeployedEvent deployedEvent;
        public DeployedEvent DeployedEvent { get => deployedEvent; set => SetProperty(ref deployedEvent, value); }

        private DeploymentEnvironmentChangedEvent deploymentEnvironmentChangedEvent;
        public DeploymentEnvironmentChangedEvent DeploymentEnvironmentChangedEvent { get => deploymentEnvironmentChangedEvent; set => SetProperty(ref deploymentEnvironmentChangedEvent, value); }

        private DisconnectedEvent disconnectedEvent;
        public DisconnectedEvent DisconnectedEvent { get => disconnectedEvent; set => SetProperty(ref disconnectedEvent, value); }

        private HeadRefDeletedEvent headRefDeletedEvent;
        public HeadRefDeletedEvent HeadRefDeletedEvent { get => headRefDeletedEvent; set => SetProperty(ref headRefDeletedEvent, value); }

        private HeadRefForcePushedEvent headRefForcePushedEvent;
        public HeadRefForcePushedEvent HeadRefForcePushedEvent { get => headRefForcePushedEvent; set => SetProperty(ref headRefForcePushedEvent, value); }

        private HeadRefRestoredEvent headRefRestoredEvent;
        public HeadRefRestoredEvent HeadRefRestoredEvent { get => headRefRestoredEvent; set => SetProperty(ref headRefRestoredEvent, value); }

        private IssueComment issueComment;
        public IssueComment IssueComment { get => issueComment; set => SetProperty(ref issueComment, value); }

        private LabeledEvent labeledEvent;
        public LabeledEvent LabeledEvent { get => labeledEvent; set => SetProperty(ref labeledEvent, value); }

        private LockedEvent lockedEvent;
        public LockedEvent LockedEvent { get => lockedEvent; set => SetProperty(ref lockedEvent, value); }

        private MarkedAsDuplicateEvent markedAsDuplicateEvent;
        public MarkedAsDuplicateEvent MarkedAsDuplicateEvent { get => markedAsDuplicateEvent; set => SetProperty(ref markedAsDuplicateEvent, value); }

        //private MentionedEvent  mentionedEvent;
        //public MentionedEvent MentionedEvent { get => mentionedEvent; set => SetProperty(ref mentionedEvent, value); }

        private MergedEvent mergedEvent;
        public MergedEvent MergedEvent { get => mergedEvent; set => SetProperty(ref mergedEvent, value); }

        private MilestonedEvent milestonedEvent;
        public MilestonedEvent MilestonedEvent { get => milestonedEvent; set => SetProperty(ref milestonedEvent, value); }

        private MovedColumnsInProjectEvent movedColumnsInProjectEvent;
        public MovedColumnsInProjectEvent MovedColumnsInProjectEvent { get => movedColumnsInProjectEvent; set => SetProperty(ref movedColumnsInProjectEvent, value); }

        private PinnedEvent pinnedEvent;
        public PinnedEvent PinnedEvent { get => pinnedEvent; set => SetProperty(ref pinnedEvent, value); }

        private PullRequestCommit pullRequestCommit;
        public PullRequestCommit PullRequestCommit { get => pullRequestCommit; set => SetProperty(ref pullRequestCommit, value); }

        private PullRequestCommitCommentThread pullRequestCommitCommentThread;
        public PullRequestCommitCommentThread PullRequestCommitCommentThread { get => pullRequestCommitCommentThread; set => SetProperty(ref pullRequestCommitCommentThread, value); }

        private PullRequestReview pullRequestReview;
        public PullRequestReview PullRequestReview { get => pullRequestReview; set => SetProperty(ref pullRequestReview, value); }

        private PullRequestReviewThread pullRequestReviewThread;
        public PullRequestReviewThread PullRequestReviewThread { get => pullRequestReviewThread; set => SetProperty(ref pullRequestReviewThread, value); }

        private PullRequestRevisionMarker pullRequestRevisionMarker;
        public PullRequestRevisionMarker PullRequestRevisionMarker { get => pullRequestRevisionMarker; set => SetProperty(ref pullRequestRevisionMarker, value); }

        private ReadyForReviewEvent readyForReviewEvent;
        public ReadyForReviewEvent ReadyForReviewEvent { get => readyForReviewEvent; set => SetProperty(ref readyForReviewEvent, value); }

        private ReferencedEvent referencedEvent;
        public ReferencedEvent ReferencedEvent { get => referencedEvent; set => SetProperty(ref referencedEvent, value); }

        private RemovedFromProjectEvent removedFromProjectEvent;
        public RemovedFromProjectEvent RemovedFromProjectEvent { get => removedFromProjectEvent; set => SetProperty(ref removedFromProjectEvent, value); }

        private RenamedTitleEvent renamedTitleEvent;
        public RenamedTitleEvent RenamedTitleEvent { get => renamedTitleEvent; set => SetProperty(ref renamedTitleEvent, value); }

        private ReopenedEvent reopenedEvent;
        public ReopenedEvent ReopenedEvent { get => reopenedEvent; set => SetProperty(ref reopenedEvent, value); }

        private ReviewDismissedEvent reviewDismissedEvent;
        public ReviewDismissedEvent ReviewDismissedEvent { get => reviewDismissedEvent; set => SetProperty(ref reviewDismissedEvent, value); }

        private ReviewRequestedEvent reviewRequestedEvent;
        public ReviewRequestedEvent ReviewRequestedEvent { get => reviewRequestedEvent; set => SetProperty(ref reviewRequestedEvent, value); }

        private ReviewRequestRemovedEvent reviewRequestRemovedEvent;
        public ReviewRequestRemovedEvent ReviewRequestRemovedEvent { get => reviewRequestRemovedEvent; set => SetProperty(ref reviewRequestRemovedEvent, value); }

        //private SubscribedEvent  subscribedEvent;
        //public SubscribedEvent SubscribedEvent { get => subscribedEvent; set => SetProperty(ref subscribedEvent, value); }

        private TransferredEvent transferredEvent;
        public TransferredEvent TransferredEvent { get => transferredEvent; set => SetProperty(ref transferredEvent, value); }

        private UnassignedEvent unassignedEvent;
        public UnassignedEvent UnassignedEvent { get => unassignedEvent; set => SetProperty(ref unassignedEvent, value); }

        private UnlabeledEvent unlabeledEvent;
        public UnlabeledEvent UnlabeledEvent { get => unlabeledEvent; set => SetProperty(ref unlabeledEvent, value); }

        private UnlockedEvent unlockedEvent;
        public UnlockedEvent UnlockedEvent { get => unlockedEvent; set => SetProperty(ref unlockedEvent, value); }

        private UnmarkedAsDuplicateEvent unmarkedAsDuplicateEvent;
        public UnmarkedAsDuplicateEvent UnmarkedAsDuplicateEvent { get => unmarkedAsDuplicateEvent; set => SetProperty(ref unmarkedAsDuplicateEvent, value); }

        private UnpinnedEvent unpinnedEvent;
        public UnpinnedEvent UnpinnedEvent { get => unpinnedEvent; set => SetProperty(ref unpinnedEvent, value); }

        //private UnsubscribedEvent  unsubscribedEvent;
        //public UnsubscribedEvent UnsubscribedEvent { get => unsubscribedEvent; set => SetProperty(ref unsubscribedEvent, value); }

        private UserBlockedEvent userBlockedEvent;
        public UserBlockedEvent UserBlockedEvent { get => userBlockedEvent; set => SetProperty(ref userBlockedEvent, value); }
        #endregion

        #region ViewModels
        private IssueCommentBlockViewModel _commentBlockViewModel;
        public IssueCommentBlockViewModel CommentBlockViewModel { get => _commentBlockViewModel; set => SetProperty(ref _commentBlockViewModel, value); }

        private LabelControlViewModel _labelControlViewModel;
        public LabelControlViewModel LabelControlViewModel { get => _labelControlViewModel; set => SetProperty(ref _labelControlViewModel, value); }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }
    }
}
