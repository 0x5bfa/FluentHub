// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Possible directions in which to order a list of items when provided an `orderBy` argument.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum OrderDirection
	{
		/// <summary>
		/// Specifies an ascending order for a given `orderBy` argument.
		/// </summary>
		[EnumMember(Value = "ASC")]
		Asc,

		/// <summary>
		/// Specifies a descending order for a given `orderBy` argument.
		/// </summary>
		[EnumMember(Value = "DESC")]
		Desc,
	}
}
