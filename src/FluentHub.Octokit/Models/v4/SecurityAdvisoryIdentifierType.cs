// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Identifier formats available for advisories.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum SecurityAdvisoryIdentifierType
	{
		/// <summary>
		/// Common Vulnerabilities and Exposures Identifier.
		/// </summary>
		[EnumMember(Value = "CVE")]
		Cve,

		/// <summary>
		/// GitHub Security Advisory ID.
		/// </summary>
		[EnumMember(Value = "GHSA")]
		Ghsa,
	}
}
