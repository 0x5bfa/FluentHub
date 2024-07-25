// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The permissions available to members on an Organization.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum OrgUpdateMemberAuditEntryPermission
	{
		/// <summary>
		/// Can read and clone repositories.
		/// </summary>
		[EnumMember(Value = "READ")]
		Read,

		/// <summary>
		/// Can read, clone, push, and add collaborators to repositories.
		/// </summary>
		[EnumMember(Value = "ADMIN")]
		Admin,
	}
}
