// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Indicates where an interaction limit is configured.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum RepositoryInteractionLimitOrigin
	{
		/// <summary>
		/// A limit that is configured at the repository level.
		/// </summary>
		[EnumMember(Value = "REPOSITORY")]
		Repository,

		/// <summary>
		/// A limit that is configured at the organization level.
		/// </summary>
		[EnumMember(Value = "ORGANIZATION")]
		Organization,

		/// <summary>
		/// A limit that is configured at the user-wide level.
		/// </summary>
		[EnumMember(Value = "USER")]
		User,
	}
}
