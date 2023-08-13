// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible states of a project v2.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum ProjectV2State
	{
		/// <summary>
		/// A project v2 that is still open
		/// </summary>
		[EnumMember(Value = "OPEN")]
		Open,

		/// <summary>
		/// A project v2 that has been closed
		/// </summary>
		[EnumMember(Value = "CLOSED")]
		Closed,
	}
}
