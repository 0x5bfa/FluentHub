// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The type of a project field.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum ProjectV2CustomFieldType
	{
		/// <summary>
		/// Text
		/// </summary>
		[EnumMember(Value = "TEXT")]
		Text,

		/// <summary>
		/// Single Select
		/// </summary>
		[EnumMember(Value = "SINGLE_SELECT")]
		SingleSelect,

		/// <summary>
		/// Number
		/// </summary>
		[EnumMember(Value = "NUMBER")]
		Number,

		/// <summary>
		/// Date
		/// </summary>
		[EnumMember(Value = "DATE")]
		Date,
	}
}
