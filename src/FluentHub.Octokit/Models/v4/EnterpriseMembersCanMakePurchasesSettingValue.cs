// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible values for the members can make purchases setting.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum EnterpriseMembersCanMakePurchasesSettingValue
	{
		/// <summary>
		/// The setting is enabled for organizations in the enterprise.
		/// </summary>
		[EnumMember(Value = "ENABLED")]
		Enabled,

		/// <summary>
		/// The setting is disabled for organizations in the enterprise.
		/// </summary>
		[EnumMember(Value = "DISABLED")]
		Disabled,
	}
}
