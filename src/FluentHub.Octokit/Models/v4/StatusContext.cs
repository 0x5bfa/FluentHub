// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents an individual commit status context
	/// </summary>
	public class StatusContext
	{
		/// <summary>
		/// The avatar of the OAuth application or the user that created the status
		/// </summary>
		/// <param name="size">The size of the resulting square image.</param>
		public string AvatarUrl { get; set; }

		/// <summary>
		/// This commit this status context is attached to.
		/// </summary>
		public Commit Commit { get; set; }

		/// <summary>
		/// The name of this status context.
		/// </summary>
		public string Context { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was created.
		/// </summary>
		public DateTimeOffset CreatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was created."
		/// <summary>
		public string CreatedAtHumanized { get; set; }

		/// <summary>
		/// The actor who created this status context.
		/// </summary>
		public IActor Creator { get; set; }

		/// <summary>
		/// The description for this status context.
		/// </summary>
		public string Description { get; set; }

		public ID Id { get; set; }

		/// <summary>
		/// Whether this is required to pass before merging for a specific pull request.
		/// </summary>
		/// <param name="pullRequestId">The id of the pull request this is required for</param>
		/// <param name="pullRequestNumber">The number of the pull request this is required for</param>
		public bool IsRequired { get; set; }

		/// <summary>
		/// The state of this status context.
		/// </summary>
		public StatusState State { get; set; }

		/// <summary>
		/// The URL for this status context.
		/// </summary>
		public string TargetUrl { get; set; }
	}
}
