using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FluentHub.Uwp.Models
{
    public class TimelineItemSelector : DataTemplateSelector
    {
        public DataTemplate AddedToProjectEventDataTemplate { get; set; }
        public DataTemplate AssignedEventDataTemplate { get; set; }
        public DataTemplate ClosedEventDataTemplate { get; set; }
        public DataTemplate CommentDeletedEventDataTemplate { get; set; }
        public DataTemplate ConnectedEventDataTemplate { get; set; }
        public DataTemplate ConvertedNoteToIssueEventDataTemplate { get; set; }
        public DataTemplate CrossReferencedEventDataTemplate { get; set; }
        public DataTemplate DemilestonedEventDataTemplate { get; set; }
        public DataTemplate DisconnectedEventDataTemplate { get; set; }
        public DataTemplate IssueCommentDataTemplate { get; set; }
        public DataTemplate LabeledEventDataTemplate { get; set; }
        public DataTemplate LockedEventDataTemplate { get; set; }
        public DataTemplate MarkedAsDuplicateEventDataTemplate { get; set; }
        public DataTemplate MilestonedEventDataTemplate { get; set; }
        public DataTemplate MovedColumnsInProjectEventDataTemplate { get; set; }
        public DataTemplate PinnedEventDataTemplate { get; set; }
        public DataTemplate ReferencedEventDataTemplate { get; set; }
        public DataTemplate RemovedFromProjectEventDataTemplate { get; set; }
        public DataTemplate RenamedTitleEventDataTemplate { get; set; }
        public DataTemplate ReopenedEventDataTemplate { get; set; }
        public DataTemplate UnassignedEventDataTemplate { get; set; }
        public DataTemplate UnlabeledEventDataTemplate { get; set; }
        public DataTemplate UnlockedEventDataTemplate { get; set; }
        public DataTemplate UnmarkedAsDuplicateEventDataTemplate { get; set; }
        public DataTemplate UnpinnedEventDataTemplate { get; set; }
        public DataTemplate UserBlockedEventDataTemplate { get; set; }
        public DataTemplate DefaultDataTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item)
        {
            if (item is null)
                return DefaultDataTemplate;

            var itemType = item.GetType().ToString().Split(".").ToList().Last();

            switch (itemType)
            {
                case nameof(AddedToProjectEvent):
                    return AddedToProjectEventDataTemplate;
                case nameof(AssignedEvent):
                    return AssignedEventDataTemplate;
                case nameof(ClosedEvent):
                    return ClosedEventDataTemplate;
                case nameof(CommentDeletedEvent):
                    return CommentDeletedEventDataTemplate;
                case nameof(ConnectedEvent):
                    return ConnectedEventDataTemplate;
                case nameof(ConvertedNoteToIssueEvent):
                    return ConvertedNoteToIssueEventDataTemplate;
                case nameof(CrossReferencedEvent):
                    return CrossReferencedEventDataTemplate;
                case nameof(DemilestonedEvent):
                    return DemilestonedEventDataTemplate;
                case nameof(DisconnectedEvent):
                    return DisconnectedEventDataTemplate;
                case nameof(IssueComment):
                    return IssueCommentDataTemplate;
                case nameof(LabeledEvent):
                    return LabeledEventDataTemplate;
                case nameof(LockedEvent):
                    return LockedEventDataTemplate;
                case nameof(MarkedAsDuplicateEvent):
                    return MarkedAsDuplicateEventDataTemplate;
                case nameof(MilestonedEvent):
                    return MilestonedEventDataTemplate;
                case nameof(MovedColumnsInProjectEvent):
                    return MovedColumnsInProjectEventDataTemplate;
                case nameof(PinnedEvent):
                    return PinnedEventDataTemplate;
                case nameof(ReferencedEvent):
                    return ReferencedEventDataTemplate;
                case nameof(RemovedFromProjectEvent):
                    return RemovedFromProjectEventDataTemplate;
                case nameof(RenamedTitleEvent):
                    return RenamedTitleEventDataTemplate;
                case nameof(ReopenedEvent):
                    return ReopenedEventDataTemplate;
                case nameof(UnassignedEvent):
                    return UnassignedEventDataTemplate;
                case nameof(UnlabeledEvent):
                    return UnlabeledEventDataTemplate;
                case nameof(UnlockedEvent):
                    return UnlockedEventDataTemplate;
                case nameof(UnmarkedAsDuplicateEvent):
                    return UnmarkedAsDuplicateEventDataTemplate;
                case nameof(UnpinnedEvent):
                    return UnpinnedEventDataTemplate;
                case nameof(UserBlockedEvent):
                    return UserBlockedEventDataTemplate;
                default:
                    return DefaultDataTemplate;
            }
        }
    }
}
