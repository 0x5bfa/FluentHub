// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A Saved Reply is text a user can use to reply quickly.
	/// </summary>
	public class SavedReply
	{
		/// <summary>
		/// The body of the saved reply.
		/// </summary>
		public string Body { get; set; }

		/// <summary>
		/// The saved reply body rendered to HTML.
		/// </summary>
		public string BodyHTML { get; set; }

		/// <summary>
		/// Identifies the primary key from the database.
		/// </summary>
		public int? DatabaseId { get; set; }

		/// <summary>
		/// The Node ID of the SavedReply object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// The title of the saved reply.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// The user that saved this reply.
		/// </summary>
		public IActor User { get; set; }
	}
}
