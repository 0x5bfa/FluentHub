// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Properties by which project v2 view connections can be ordered.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum ProjectV2ViewOrderField
	{
		/// <summary>
		/// Order project v2 views by position
		/// </summary>
		[EnumMember(Value = "POSITION")]
		Position,

		/// <summary>
		/// Order project v2 views by creation time
		/// </summary>
		[EnumMember(Value = "CREATED_AT")]
		CreatedAt,

		/// <summary>
		/// Order project v2 views by name
		/// </summary>
		[EnumMember(Value = "NAME")]
		Name,
	}
}
