// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{

	/// <summary>
	/// Represents a contribution a user made on GitHub, such as opening an issue.
	/// </summary>
	public interface IContribution
	{
		/// <summary>
		/// Whether this contribution is associated with a record you do not have access to. For
		/// example, your own 'first issue' contribution may have been made on a repository you can no
		/// longer access.
		/// </summary>
		bool IsRestricted { get; set; }

		/// <summary>
		/// When this contribution was made.
		/// </summary>
		DateTimeOffset OccurredAt { get; set; }

		/// <summary>
		/// Humanized string of "When this contribution was made."
		/// <summary>
		string OccurredAtHumanized { get; set; }

		/// <summary>
		/// The HTTP path for this contribution.
		/// </summary>
		string ResourcePath { get; set; }

		/// <summary>
		/// The HTTP URL for this contribution.
		/// </summary>
		string Url { get; set; }

		/// <summary>
		/// The user who made this contribution.
		/// </summary>
		User User { get; set; }
	}
}

namespace FluentHub.Octokit.Models.v4
{
	public class Contribution : IContribution
	{
		public bool IsRestricted { get; set; }

		public DateTimeOffset OccurredAt { get; set; }

		public string OccurredAtHumanized { get; set; }

		public string ResourcePath { get; set; }

		public string Url { get; set; }

		public User User { get; set; }
	}
}

