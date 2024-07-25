// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible values for an enabled/disabled enterprise setting.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum EnterpriseEnabledDisabledSettingValue
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

		/// <summary>
		/// There is no policy set for organizations in the enterprise.
		/// </summary>
		[EnumMember(Value = "NO_POLICY")]
		NoPolicy,
	}
}
