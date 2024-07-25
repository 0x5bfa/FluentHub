// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The semantic purpose of the column - todo, in progress, or done.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum ProjectColumnPurpose
	{
		/// <summary>
		/// The column contains cards still to be worked on
		/// </summary>
		[EnumMember(Value = "TODO")]
		Todo,

		/// <summary>
		/// The column contains cards which are currently being worked on
		/// </summary>
		[EnumMember(Value = "IN_PROGRESS")]
		InProgress,

		/// <summary>
		/// The column contains cards which are complete
		/// </summary>
		[EnumMember(Value = "DONE")]
		Done,
	}
}
