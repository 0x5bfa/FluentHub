// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The OIDC identity provider type
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum OIDCProviderType
	{
		/// <summary>
		/// Azure Active Directory
		/// </summary>
		[EnumMember(Value = "AAD")]
		Aad,
	}
}
