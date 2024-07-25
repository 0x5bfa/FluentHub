// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible values for an enabled/no policy enterprise setting.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum EnterpriseEnabledSettingValue
	{
		/// <summary>
		/// The setting is enabled for organizations in the enterprise.
		/// </summary>
		[EnumMember(Value = "ENABLED")]
		Enabled,

		/// <summary>
		/// There is no policy set for organizations in the enterprise.
		/// </summary>
		[EnumMember(Value = "NO_POLICY")]
		NoPolicy,
	}
}
