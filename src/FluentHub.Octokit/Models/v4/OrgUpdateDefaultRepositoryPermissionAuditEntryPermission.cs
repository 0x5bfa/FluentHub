// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The default permission a repository can have in an Organization.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum OrgUpdateDefaultRepositoryPermissionAuditEntryPermission
	{
		/// <summary>
		/// Can read and clone repositories.
		/// </summary>
		[EnumMember(Value = "READ")]
		Read,

		/// <summary>
		/// Can read, clone and push to repositories.
		/// </summary>
		[EnumMember(Value = "WRITE")]
		Write,

		/// <summary>
		/// Can read, clone, push, and add collaborators to repositories.
		/// </summary>
		[EnumMember(Value = "ADMIN")]
		Admin,

		/// <summary>
		/// No default permission value.
		/// </summary>
		[EnumMember(Value = "NONE")]
		None,
	}
}
