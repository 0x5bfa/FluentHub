// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The role of a user on a team.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum TeamRole
	{
		/// <summary>
		/// User has admin rights on the team.
		/// </summary>
		[EnumMember(Value = "ADMIN")]
		Admin,

		/// <summary>
		/// User is a member of the team.
		/// </summary>
		[EnumMember(Value = "MEMBER")]
		Member,
	}
}
