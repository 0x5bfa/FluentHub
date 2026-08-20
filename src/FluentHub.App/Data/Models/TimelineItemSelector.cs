// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using FluentHub.Octokit.Models.v4;

namespace FluentHub.App.Models
{
	public partial class TimelineItemSelector : DataTemplateSelector
	{
		public DataTemplate AddedToProjectEventDataTemplate { get; set; } = default!;
		public DataTemplate AssignedEventDataTemplate { get; set; } = default!;
		public DataTemplate ClosedEventDataTemplate { get; set; } = default!;
		public DataTemplate CommentDeletedEventDataTemplate { get; set; } = default!;
		public DataTemplate ConnectedEventDataTemplate { get; set; } = default!;
		public DataTemplate ConvertedToDiscussionEventDataTemplate { get; set; } = default!;
		public DataTemplate ConvertedNoteToIssueEventDataTemplate { get; set; } = default!;
		public DataTemplate CrossReferencedEventDataTemplate { get; set; } = default!;
		public DataTemplate DemilestonedEventDataTemplate { get; set; } = default!;
		public DataTemplate DisconnectedEventDataTemplate { get; set; } = default!;
		public DataTemplate IssueCommentDataTemplate { get; set; } = default!;
		public DataTemplate LabeledEventDataTemplate { get; set; } = default!;
		public DataTemplate LockedEventDataTemplate { get; set; } = default!;
		public DataTemplate MarkedAsDuplicateEventDataTemplate { get; set; } = default!;
		//public DataTemplate MentionedEventDataTemplate { get; set; }
		public DataTemplate MilestonedEventDataTemplate { get; set; } = default!;
		public DataTemplate MovedColumnsInProjectEventDataTemplate { get; set; } = default!;
		public DataTemplate PinnedEventDataTemplate { get; set; } = default!;
		public DataTemplate ReferencedEventDataTemplate { get; set; } = default!;
		public DataTemplate RemovedFromProjectEventDataTemplate { get; set; } = default!;
		public DataTemplate RenamedTitleEventDataTemplate { get; set; } = default!;
		public DataTemplate ReopenedEventDataTemplate { get; set; } = default!;
		//public DataTemplate SubscribedEventDataTemplate { get; set; }
		public DataTemplate TransferredEventDataTemplate { get; set; } = default!;
		public DataTemplate UnassignedEventDataTemplate { get; set; } = default!;
		public DataTemplate UnlabeledEventDataTemplate { get; set; } = default!;
		public DataTemplate UnlockedEventDataTemplate { get; set; } = default!;
		public DataTemplate UnmarkedAsDuplicateEventDataTemplate { get; set; } = default!;
		public DataTemplate UnpinnedEventDataTemplate { get; set; } = default!;
		//public DataTemplate UnsubscribedEventDataTemplate { get; set; }
		public DataTemplate UserBlockedEventDataTemplate { get; set; } = default!;
		public DataTemplate DefaultDataTemplate { get; set; } = default!;

		protected override DataTemplate SelectTemplateCore(object item)
		{
			if (item is null)
				return DefaultDataTemplate;

			var typeName = item.GetType().Name;

			return typeName switch
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
