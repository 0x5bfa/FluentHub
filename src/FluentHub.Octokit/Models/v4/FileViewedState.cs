// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible viewed states of a file .
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum FileViewedState
	{
		/// <summary>
		/// The file has new changes since last viewed.
		/// </summary>
		[EnumMember(Value = "DISMISSED")]
		Dismissed,

		/// <summary>
		/// The file has been marked as viewed.
		/// </summary>
		[EnumMember(Value = "VIEWED")]
		Viewed,

		/// <summary>
		/// The file has not been marked as viewed.
		/// </summary>
		[EnumMember(Value = "UNVIEWED")]
		Unviewed,
	}
}
