// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible states in which authentication can be configured with an identity provider.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum IdentityProviderConfigurationState
	{
		/// <summary>
		/// Authentication with an identity provider is configured and enforced.
		/// </summary>
		[EnumMember(Value = "ENFORCED")]
		Enforced,

		/// <summary>
		/// Authentication with an identity provider is configured but not enforced.
		/// </summary>
		[EnumMember(Value = "CONFIGURED")]
		Configured,

		/// <summary>
		/// Authentication with an identity provider is not configured.
		/// </summary>
		[EnumMember(Value = "UNCONFIGURED")]
		Unconfigured,
	}
}
