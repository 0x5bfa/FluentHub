// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible team member roles; either 'maintainer' or 'member'.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum TeamMemberRole
	{
		/// <summary>
		/// A team maintainer has permission to add and remove team members.
		/// </summary>
		[EnumMember(Value = "MAINTAINER")]
		Maintainer,

		/// <summary>
		/// A team member has no administrative permissions on the team.
		/// </summary>
		[EnumMember(Value = "MEMBER")]
		Member,
	}
}
