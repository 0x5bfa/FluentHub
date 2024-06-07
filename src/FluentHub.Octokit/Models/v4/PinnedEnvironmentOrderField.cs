// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Properties by which pinned environments connections can be ordered
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum PinnedEnvironmentOrderField
	{
		/// <summary>
		/// Order pinned environments by position
		/// </summary>
		[EnumMember(Value = "POSITION")]
		Position,
	}
}
