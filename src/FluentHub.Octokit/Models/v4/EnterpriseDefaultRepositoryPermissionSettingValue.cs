// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible values for the enterprise base repository permission setting.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum EnterpriseDefaultRepositoryPermissionSettingValue
	{
		/// <summary>
		/// Organizations in the enterprise choose base repository permissions for their members.
		/// </summary>
		[EnumMember(Value = "NO_POLICY")]
		NoPolicy,

		/// <summary>
		/// Organization members will be able to clone, pull, push, and add new collaborators to all organization repositories.
		/// </summary>
		[EnumMember(Value = "ADMIN")]
		Admin,

		/// <summary>
		/// Organization members will be able to clone, pull, and push all organization repositories.
		/// </summary>
		[EnumMember(Value = "WRITE")]
		Write,

		/// <summary>
		/// Organization members will be able to clone and pull all organization repositories.
		/// </summary>
		[EnumMember(Value = "READ")]
		Read,

		/// <summary>
		/// Organization members will only be able to clone and pull public repositories.
		/// </summary>
		[EnumMember(Value = "NONE")]
		None,
	}
}
