// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Autogenerated input type of UpdatePatreonSponsorability
	/// </summary>
	public class UpdatePatreonSponsorabilityInput
	{
		/// <summary>
		/// A unique identifier for the client performing the mutation.
		/// </summary>
		public string ClientMutationId { get; set; }

		/// <summary>
		/// The username of the organization with the GitHub Sponsors profile, if any. Defaults to the GitHub Sponsors profile for the authenticated user if omitted.
		/// </summary>
		public string SponsorableLogin { get; set; }

		/// <summary>
		/// Whether Patreon tiers should be shown on the GitHub Sponsors profile page, allowing potential sponsors to make their payment through Patreon instead of GitHub.
		/// </summary>
		public bool EnablePatreonSponsorships { get; set; }
	}
}