// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Location information for an actor
	/// </summary>
	public class ActorLocation
	{
		/// <summary>
		/// City
		/// </summary>
		public string City { get; set; }

		/// <summary>
		/// Country name
		/// </summary>
		public string Country { get; set; }

		/// <summary>
		/// Country code
		/// </summary>
		public string CountryCode { get; set; }

		/// <summary>
		/// Region name
		/// </summary>
		public string Region { get; set; }

		/// <summary>
		/// Region or state code
		/// </summary>
		public string RegionCode { get; set; }
	}
}
