// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The different kinds of records that can be featured on a GitHub Sponsors profile page.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum SponsorsListingFeaturedItemFeatureableType
	{
		/// <summary>
		/// A repository owned by the user or organization with the GitHub Sponsors profile.
		/// </summary>
		[EnumMember(Value = "REPOSITORY")]
		Repository,

		/// <summary>
		/// A user who belongs to the organization with the GitHub Sponsors profile.
		/// </summary>
		[EnumMember(Value = "USER")]
		User,
	}
}
