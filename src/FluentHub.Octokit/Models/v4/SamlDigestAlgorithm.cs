// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible digest algorithms used to sign SAML requests for an identity provider.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum SamlDigestAlgorithm
	{
		/// <summary>
		/// SHA1
		/// </summary>
		[EnumMember(Value = "SHA1")]
		Sha1,

		/// <summary>
		/// SHA256
		/// </summary>
		[EnumMember(Value = "SHA256")]
		Sha256,

		/// <summary>
		/// SHA384
		/// </summary>
		[EnumMember(Value = "SHA384")]
		Sha384,

		/// <summary>
		/// SHA512
		/// </summary>
		[EnumMember(Value = "SHA512")]
		Sha512,
	}
}
