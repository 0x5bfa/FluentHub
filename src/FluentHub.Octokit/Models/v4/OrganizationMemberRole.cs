// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible roles within an organization for its members.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum OrganizationMemberRole
	{
		/// <summary>
		/// The user is a member of the organization.
		/// </summary>
		[EnumMember(Value = "MEMBER")]
		Member,

		/// <summary>
		/// The user is an administrator of the organization.
		/// </summary>
		[EnumMember(Value = "ADMIN")]
		Admin,
	}
}
