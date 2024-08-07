// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Autogenerated input type of AddDiscussionComment
	/// </summary>
	public class AddDiscussionCommentInput
	{
		/// <summary>
		/// A unique identifier for the client performing the mutation.
		/// </summary>
		public string ClientMutationId { get; set; }

		/// <summary>
		/// The Node ID of the discussion to comment on.
		/// </summary>
		public ID DiscussionId { get; set; }

		/// <summary>
		/// The Node ID of the discussion comment within this discussion to reply to.
		/// </summary>
		public ID? ReplyToId { get; set; }

		/// <summary>
		/// The contents of the comment.
		/// </summary>
		public string Body { get; set; }
	}
}
