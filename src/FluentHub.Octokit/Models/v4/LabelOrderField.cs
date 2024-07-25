// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Properties by which label connections can be ordered.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum LabelOrderField
	{
		/// <summary>
		/// Order labels by name 
		/// </summary>
		[EnumMember(Value = "NAME")]
		Name,

		/// <summary>
		/// Order labels by creation time
		/// </summary>
		[EnumMember(Value = "CREATED_AT")]
		CreatedAt,
	}
}
