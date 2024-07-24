// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible values for the IP allow list enabled setting.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum IpAllowListEnabledSettingValue
	{
		/// <summary>
		/// The setting is enabled for the owner.
		/// </summary>
		[EnumMember(Value = "ENABLED")]
		Enabled,

		/// <summary>
		/// The setting is disabled for the owner.
		/// </summary>
		[EnumMember(Value = "DISABLED")]
		Disabled,
	}
}
