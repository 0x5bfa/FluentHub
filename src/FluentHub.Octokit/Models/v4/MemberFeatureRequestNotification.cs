// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents a member feature request notification
	/// </summary>
	public class MemberFeatureRequestNotification
	{
		/// <summary>
		/// Represents member feature request body containing entity name and the number of feature requests
		/// </summary>
		public string Body { get; set; }

		/// <summary>
		/// The Node ID of the MemberFeatureRequestNotification object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// Represents member feature request notification title
		/// </summary>
		public string Title { get; set; }

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
