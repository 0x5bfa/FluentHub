// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents the individual results of a search.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum SearchType
	{
		/// <summary>
		/// Returns results matching issues in repositories.
		/// </summary>
		[EnumMember(Value = "ISSUE")]
		Issue,

		/// <summary>
		/// Returns results matching repositories.
		/// </summary>
		[EnumMember(Value = "REPOSITORY")]
		Repository,

		/// <summary>
		/// Returns results matching users and organizations on GitHub.
		/// </summary>
		[EnumMember(Value = "USER")]
		User,

		/// <summary>
		/// Returns matching discussions in repositories.
		/// </summary>
		[EnumMember(Value = "DISCUSSION")]
		Discussion,
	}
}
