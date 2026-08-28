// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using FluentHub.Core.Application.Models;

namespace FluentHub.Models
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

			return item switch
			{
				AddedToProjectEvent => AddedToProjectEventDataTemplate,
				AssignedEvent => AssignedEventDataTemplate,
				ClosedEvent => ClosedEventDataTemplate,
				CommentDeletedEvent => CommentDeletedEventDataTemplate,
				ConnectedEvent => ConnectedEventDataTemplate,
				ConvertedToDiscussionEvent => ConvertedToDiscussionEventDataTemplate,
				ConvertedNoteToIssueEvent => ConvertedNoteToIssueEventDataTemplate,
				CrossReferencedEvent => CrossReferencedEventDataTemplate,
				DemilestonedEvent => DemilestonedEventDataTemplate,
				DisconnectedEvent => DisconnectedEventDataTemplate,
				IssueComment => IssueCommentDataTemplate,
				LabeledEvent => LabeledEventDataTemplate,
				LockedEvent => LockedEventDataTemplate,
				MarkedAsDuplicateEvent => MarkedAsDuplicateEventDataTemplate,
				MilestonedEvent => MilestonedEventDataTemplate,
				MovedColumnsInProjectEvent => MovedColumnsInProjectEventDataTemplate,
				PinnedEvent => PinnedEventDataTemplate,
				ReferencedEvent => ReferencedEventDataTemplate,
				RemovedFromProjectEvent => RemovedFromProjectEventDataTemplate,
				RenamedTitleEvent => RenamedTitleEventDataTemplate,
				ReopenedEvent => ReopenedEventDataTemplate,
				TransferredEvent => TransferredEventDataTemplate,
				UnassignedEvent => UnassignedEventDataTemplate,
				UnlabeledEvent => UnlabeledEventDataTemplate,
				UnlockedEvent => UnlockedEventDataTemplate,
				UnmarkedAsDuplicateEvent => UnmarkedAsDuplicateEventDataTemplate,
				UnpinnedEvent => UnpinnedEventDataTemplate,
				UserBlockedEvent => UserBlockedEventDataTemplate,
				_ => DefaultDataTemplate,
			};
		}
	}
}
