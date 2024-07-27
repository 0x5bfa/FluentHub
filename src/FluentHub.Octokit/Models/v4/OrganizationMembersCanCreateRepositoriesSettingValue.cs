// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible values for the members can create repositories setting on an organization.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum OrganizationMembersCanCreateRepositoriesSettingValue
	{
		/// <summary>
		/// Members will be able to create public and private repositories.
		/// </summary>
		[EnumMember(Value = "ALL")]
		All,

		/// <summary>
		/// Members will be able to create only private repositories.
		/// </summary>
		[EnumMember(Value = "PRIVATE")]
		Private,

		/// <summary>
		/// Members will be able to create only internal repositories.
		/// </summary>
		[EnumMember(Value = "INTERNAL")]
		Internal,

		/// <summary>
		/// Members will not be able to create public or private repositories.
		/// </summary>
		[EnumMember(Value = "DISABLED")]
		Disabled,
	}
}
