// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// An IP address or range of addresses that is allowed to access an owner's resources.
	/// </summary>
	public class IpAllowListEntry
	{
		/// <summary>
		/// A single IP address or range of IP addresses in CIDR notation.
		/// </summary>
		public string AllowListValue { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was created.
		/// </summary>
		public DateTimeOffset CreatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was created."
		/// <summary>
		public string CreatedAtHumanized { get; set; }

		public ID Id { get; set; }

		/// <summary>
		/// Whether the entry is currently active.
		/// </summary>
		public bool IsActive { get; set; }

		/// <summary>
		/// The name of the IP allow list entry.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The owner of the IP allow list entry.
		/// </summary>
		public IpAllowListOwner Owner { get; set; }

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
