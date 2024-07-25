// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Possible roles a user may have in relation to an organization.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum RoleInOrganization
	{
		/// <summary>
		/// A user with full administrative access to the organization.
		/// </summary>
		[EnumMember(Value = "OWNER")]
		Owner,

		/// <summary>
		/// A user who is a direct member of the organization.
		/// </summary>
		[EnumMember(Value = "DIRECT_MEMBER")]
		DirectMember,

		/// <summary>
		/// A user who is unaffiliated with the organization.
		/// </summary>
		[EnumMember(Value = "UNAFFILIATED")]
		Unaffiliated,
	}
}
