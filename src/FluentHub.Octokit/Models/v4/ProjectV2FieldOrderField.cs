// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Properties by which project v2 field connections can be ordered.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum ProjectV2FieldOrderField
	{
		/// <summary>
		/// Order project v2 fields by position
		/// </summary>
		[EnumMember(Value = "POSITION")]
		Position,

		/// <summary>
		/// Order project v2 fields by creation time
		/// </summary>
		[EnumMember(Value = "CREATED_AT")]
		CreatedAt,

		/// <summary>
		/// Order project v2 fields by name
		/// </summary>
		[EnumMember(Value = "NAME")]
		Name,
	}
}
