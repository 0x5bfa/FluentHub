using FluentHub.Octokit.Models.Events;
using FluentHub.ViewModels.UserControls.Labels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.UserControls.Blocks
{
    public class IssueEventBlockViewModel : INotifyPropertyChanged
    {
        private string _eventType;
        public string EventType { get => _eventType; set => SetProperty(ref _eventType, value); }

        //private string actorLogin;
        //public string ActorLogin { get => actorLogin; set => SetProperty(ref actorLogin, value); }

        //private bool actorIsUser;
        //public bool ActorIsUser { get => actorIsUser; set => SetProperty(ref actorIsUser, value); }

        #region AllEvents
        private AddedToProjectEvent addedToProjectEvent;
        public AddedToProjectEvent AddedToProjectEvent { get => addedToProjectEvent; set => SetProperty(ref addedToProjectEvent, value); }

        private AssignedEvent assignedEvent;
        public AssignedEvent AssignedEvent { get => assignedEvent; set => SetProperty(ref assignedEvent, value);}

        private ClosedEvent closedEvent;
        public ClosedEvent ClosedEvent { get => closedEvent; set => SetProperty(ref closedEvent, value); }

        private CommentDeletedEvent commentDeletedEvent;
        public CommentDeletedEvent CommentDeletedEvent { get => commentDeletedEvent; set => SetProperty(ref commentDeletedEvent, value); }

        private ConnectedEvent connectedEvent;
        public ConnectedEvent ConnectedEvent { get => connectedEvent; set => SetProperty(ref connectedEvent, value); }

        private ConvertedNoteToIssueEvent convertedNoteToIssueEvent;
        public ConvertedNoteToIssueEvent ConvertedNoteToIssueEvent { get => convertedNoteToIssueEvent; set => SetProperty(ref convertedNoteToIssueEvent, value); }

        private CrossReferencedEvent crossReferencedEvent;
        public CrossReferencedEvent CrossReferencedEvent { get => crossReferencedEvent; set => SetProperty(ref crossReferencedEvent, value); }

        private DemilestonedEvent demilestonedEvent;
        public DemilestonedEvent DemilestonedEvent { get => demilestonedEvent; set => SetProperty(ref demilestonedEvent, value); }

        private DisconnectedEvent disconnectedEvent;
        public DisconnectedEvent DisconnectedEvent { get => disconnectedEvent; set => SetProperty(ref disconnectedEvent, value); }

        private IssueComment issueComment;
        public IssueComment IssueComment { get => issueComment; set => SetProperty(ref issueComment, value); }

        private LabeledEvent labeledEvent;
        public LabeledEvent LabeledEvent { get => labeledEvent; set => SetProperty(ref labeledEvent, value); }

        private LockedEvent lockedEvent;
        public LockedEvent LockedEvent { get => lockedEvent; set => SetProperty(ref lockedEvent, value); }

        private MarkedAsDuplicateEvent markedAsDuplicateEvent;
        public MarkedAsDuplicateEvent MarkedAsDuplicateEvent { get => markedAsDuplicateEvent; set => SetProperty(ref markedAsDuplicateEvent, value); }

        private MentionedEvent mentionedEvent;
        public MentionedEvent MentionedEvent { get => mentionedEvent; set => SetProperty(ref mentionedEvent, value); }

        private MilestonedEvent milestonedEvent;
        public MilestonedEvent MilestonedEvent { get => milestonedEvent; set => SetProperty(ref milestonedEvent, value); }

        private MovedColumnsInProjectEvent movedColumnsInProjectEvent;
        public MovedColumnsInProjectEvent MovedColumnsInProjectEvent { get => movedColumnsInProjectEvent; set => SetProperty(ref movedColumnsInProjectEvent, value); }

        private PinnedEvent pinnedEvent;
        public PinnedEvent PinnedEvent { get => pinnedEvent; set => SetProperty(ref pinnedEvent, value); }

        private ReferencedEvent referencedEvent;
        public ReferencedEvent ReferencedEvent { get => referencedEvent; set => SetProperty(ref referencedEvent, value); }

        private RemovedFromProjectEvent removedFromProjectEvent;
        public RemovedFromProjectEvent RemovedFromProjectEvent { get => removedFromProjectEvent; set => SetProperty(ref removedFromProjectEvent, value); }

        private RenamedTitleEvent renamedTitleEvent;
        public RenamedTitleEvent RenamedTitleEvent { get => renamedTitleEvent; set => SetProperty(ref renamedTitleEvent, value); }

        private ReopenedEvent reopenedEvent;
        public ReopenedEvent ReopenedEvent { get => reopenedEvent; set => SetProperty(ref reopenedEvent, value); }

        private SubscribedEvent subscribedEvent;
        public SubscribedEvent SubscribedEvent { get => subscribedEvent; set => SetProperty(ref subscribedEvent, value); }

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

        private UnsubscribedEvent unsubscribedEvent;
        public UnsubscribedEvent UnsubscribedEvent { get => unsubscribedEvent; set => SetProperty(ref unsubscribedEvent, value); }

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
