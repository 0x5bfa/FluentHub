// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible base permissions for repositories.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum DefaultRepositoryPermissionField
	{
		/// <summary>
		/// No access
		/// </summary>
		[EnumMember(Value = "NONE")]
		None,

		/// <summary>
		/// Can read repos by default
		/// </summary>
		[EnumMember(Value = "READ")]
		Read,

		/// <summary>
		/// Can read and write repos by default
		/// </summary>
		[EnumMember(Value = "WRITE")]
		Write,

		/// <summary>
		/// Can read, write, and administrate repos by default
		/// </summary>
		[EnumMember(Value = "ADMIN")]
		Admin,
	}
}
