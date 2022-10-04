using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.Models
{
    public class TimelineItemSelector : DataTemplateSelector
    {
        public DataTemplate AddedToProjectEventDataTemplate { get; set; }
        public DataTemplate AssignedEventDataTemplate { get; set; }
        public DataTemplate ClosedEventDataTemplate { get; set; }
        public DataTemplate CommentDeletedEventDataTemplate { get; set; }
        public DataTemplate ConnectedEventDataTemplate { get; set; }
        public DataTemplate ConvertedToDiscussionEventDataTemplate { get; set; }
        public DataTemplate ConvertedNoteToIssueEventDataTemplate { get; set; }
        public DataTemplate CrossReferencedEventDataTemplate { get; set; }
        public DataTemplate DemilestonedEventDataTemplate { get; set; }
        public DataTemplate DisconnectedEventDataTemplate { get; set; }
        public DataTemplate IssueCommentDataTemplate { get; set; }
        public DataTemplate LabeledEventDataTemplate { get; set; }
        public DataTemplate LockedEventDataTemplate { get; set; }
        public DataTemplate MarkedAsDuplicateEventDataTemplate { get; set; }
        public DataTemplate MentionedEventDataTemplate { get; set; }
        public DataTemplate MilestonedEventDataTemplate { get; set; }
        public DataTemplate MovedColumnsInProjectEventDataTemplate { get; set; }
        public DataTemplate PinnedEventDataTemplate { get; set; }
        public DataTemplate ReferencedEventDataTemplate { get; set; }
        public DataTemplate RemovedFromProjectEventDataTemplate { get; set; }
        public DataTemplate RenamedTitleEventDataTemplate { get; set; }
        public DataTemplate ReopenedEventDataTemplate { get; set; }
        public DataTemplate SubscribedEventDataTemplate { get; set; }
        public DataTemplate TransferredEventDataTemplate { get; set; }
        public DataTemplate UnassignedEventDataTemplate { get; set; }
        public DataTemplate UnlabeledEventDataTemplate { get; set; }
        public DataTemplate UnlockedEventDataTemplate { get; set; }
        public DataTemplate UnmarkedAsDuplicateEventDataTemplate { get; set; }
        public DataTemplate UnpinnedEventDataTemplate { get; set; }
        public DataTemplate UnsubscribedEventDataTemplate { get; set; }
        public DataTemplate UserBlockedEventDataTemplate { get; set; }
        public DataTemplate DefaultDataTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item)
        {
            if (item is null)
                return DefaultDataTemplate;

            var itemType = item.GetType().ToString().Split(".").ToList().Last();

            return itemType switch
            {
                nameof(AddedToProjectEvent) => AddedToProjectEventDataTemplate,
                nameof(AssignedEvent) => AssignedEventDataTemplate,
                nameof(ClosedEvent) => ClosedEventDataTemplate,
                nameof(CommentDeletedEvent) => CommentDeletedEventDataTemplate,
                nameof(ConnectedEvent) => ConnectedEventDataTemplate,
                nameof(ConvertedToDiscussionEvent) => ConvertedToDiscussionEventDataTemplate,
                nameof(ConvertedNoteToIssueEvent) => ConvertedNoteToIssueEventDataTemplate,
                nameof(CrossReferencedEvent) => CrossReferencedEventDataTemplate,
                nameof(DemilestonedEvent) => DemilestonedEventDataTemplate,
                nameof(DisconnectedEvent) => DisconnectedEventDataTemplate,
                nameof(IssueComment) => IssueCommentDataTemplate,
                nameof(LabeledEvent) => LabeledEventDataTemplate,
                nameof(LockedEvent) => LockedEventDataTemplate,
                nameof(MarkedAsDuplicateEvent) => MarkedAsDuplicateEventDataTemplate,
                //nameof(MentionedEvent) => MentionedEventDataTemplate;
                nameof(MilestonedEvent) => MilestonedEventDataTemplate,
                nameof(MovedColumnsInProjectEvent) => MovedColumnsInProjectEventDataTemplate,
                nameof(PinnedEvent) => PinnedEventDataTemplate,
                nameof(ReferencedEvent) => ReferencedEventDataTemplate,
                nameof(RemovedFromProjectEvent) => RemovedFromProjectEventDataTemplate,
                nameof(RenamedTitleEvent) => RenamedTitleEventDataTemplate,
                nameof(ReopenedEvent) => ReopenedEventDataTemplate,
                //nameof(SubscribedEvent) => SubscribedEventDataTemplate;
                nameof(TransferredEvent) => TransferredEventDataTemplate,
                nameof(UnassignedEvent) => UnassignedEventDataTemplate,
                nameof(UnlabeledEvent) => UnlabeledEventDataTemplate,
                nameof(UnlockedEvent) => UnlockedEventDataTemplate,
                nameof(UnmarkedAsDuplicateEvent) => UnmarkedAsDuplicateEventDataTemplate,
                nameof(UnpinnedEvent) => UnpinnedEventDataTemplate,
                //nameof(UnsubscribedEvent) => UnsubscribedEventDataTemplate;
                nameof(UserBlockedEvent) => UserBlockedEventDataTemplate,
                _ => DefaultDataTemplate,
            };
        }
    }
}
