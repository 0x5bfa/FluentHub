// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible sides of a diff.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum DiffSide
	{
		/// <summary>
		/// The left side of the diff.
		/// </summary>
		[EnumMember(Value = "LEFT")]
		Left,

		/// <summary>
		/// The right side of the diff.
		/// </summary>
		[EnumMember(Value = "RIGHT")]
		Right,
	}
}
