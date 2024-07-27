// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// State of the project; either 'open' or 'closed'
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum ProjectState
	{
		/// <summary>
		/// The project is open.
		/// </summary>
		[EnumMember(Value = "OPEN")]
		Open,

		/// <summary>
		/// The project is closed.
		/// </summary>
		[EnumMember(Value = "CLOSED")]
		Closed,
	}
}
