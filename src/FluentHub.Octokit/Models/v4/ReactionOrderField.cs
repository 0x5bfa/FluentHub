// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A list of fields that reactions can be ordered by.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum ReactionOrderField
	{
		/// <summary>
		/// Allows ordering a list of reactions by when they were created.
		/// </summary>
		[EnumMember(Value = "CREATED_AT")]
		CreatedAt,
	}
}
