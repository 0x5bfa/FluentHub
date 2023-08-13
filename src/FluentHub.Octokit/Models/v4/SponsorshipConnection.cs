// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The connection type for Sponsorship.
	/// </summary>
	public class SponsorshipConnection
	{
		/// <summary>
		/// A list of edges.
		/// </summary>
		public List<SponsorshipEdge> Edges { get; set; }

		/// <summary>
		/// A list of nodes.
		/// </summary>
		public List<Sponsorship> Nodes { get; set; }

		/// <summary>
		/// Information to aid in pagination.
		/// </summary>
		public PageInfo PageInfo { get; set; }

		/// <summary>
		/// Identifies the total count of items in the connection.
		/// </summary>
		public int TotalCount { get; set; }

		/// <summary>
		/// The total amount in cents of all recurring sponsorships in the connection whose amount you can view. Does not include one-time sponsorships.
		/// </summary>
		public int TotalRecurringMonthlyPriceInCents { get; set; }

		/// <summary>
		/// The total amount in USD of all recurring sponsorships in the connection whose amount you can view. Does not include one-time sponsorships.
		/// </summary>
		public int TotalRecurringMonthlyPriceInDollars { get; set; }
	}
}
