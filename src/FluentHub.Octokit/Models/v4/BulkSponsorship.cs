// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Information about a sponsorship to make for a user or organization with a GitHub Sponsors profile, as part of sponsoring many users or organizations at once.
	/// </summary>
	public class BulkSponsorship
	{
		/// <summary>
		/// The ID of the user or organization who is receiving the sponsorship. Required if sponsorableLogin is not given.
		/// </summary>
		public ID? SponsorableId { get; set; }

		/// <summary>
		/// The username of the user or organization who is receiving the sponsorship. Required if sponsorableId is not given.
		/// </summary>
		public string SponsorableLogin { get; set; }

		/// <summary>
		/// The amount to pay to the sponsorable in US dollars. Valid values: 1-12000.
		/// </summary>
		public int Amount { get; set; }
	}
}
