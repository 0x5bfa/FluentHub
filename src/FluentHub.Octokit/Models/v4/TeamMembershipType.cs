// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Defines which types of team members are included in the returned list. Can be one of IMMEDIATE, CHILD_TEAM or ALL.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum TeamMembershipType
	{
		/// <summary>
		/// Includes only immediate members of the team.
		/// </summary>
		[EnumMember(Value = "IMMEDIATE")]
		Immediate,

		/// <summary>
		/// Includes only child team members for the team.
		/// </summary>
		[EnumMember(Value = "CHILD_TEAM")]
		ChildTeam,

		/// <summary>
		/// Includes immediate and child team members for the team.
		/// </summary>
		[EnumMember(Value = "ALL")]
		All,
	}
}
