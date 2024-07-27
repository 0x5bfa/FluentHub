// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A Pinned Discussion is a discussion pinned to a repository's index page.
	/// </summary>
	public class PinnedDiscussion
	{
		/// <summary>
		/// Identifies the date and time when the object was created.
		/// </summary>
		public DateTimeOffset CreatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was created."
		/// <summary>
		public string CreatedAtHumanized { get; set; }

		/// <summary>
		/// Identifies the primary key from the database.
		/// </summary>
		public int? DatabaseId { get; set; }

		/// <summary>
		/// The discussion that was pinned.
		/// </summary>
		public Discussion Discussion { get; set; }

		/// <summary>
		/// Color stops of the chosen gradient
		/// </summary>
		public List<string> GradientStopColors { get; set; }

		/// <summary>
		/// The Node ID of the PinnedDiscussion object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// Background texture pattern
		/// </summary>
		public PinnedDiscussionPattern Pattern { get; set; }

		/// <summary>
		/// The actor that pinned this discussion.
		/// </summary>
		public IActor PinnedBy { get; set; }

		/// <summary>
		/// Preconfigured background gradient option
		/// </summary>
		public PinnedDiscussionGradient? PreconfiguredGradient { get; set; }

		/// <summary>
		/// The repository associated with this node.
		/// </summary>
		public Repository Repository { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was last updated.
		/// </summary>
		public DateTimeOffset UpdatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was last updated."
		/// <summary>
		public string UpdatedAtHumanized { get; set; }
	}
}
