// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Classification of the advisory.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum SecurityAdvisoryClassification
	{
		/// <summary>
		/// Classification of general advisories.
		/// </summary>
		[EnumMember(Value = "GENERAL")]
		General,

		/// <summary>
		/// Classification of malware advisories.
		/// </summary>
		[EnumMember(Value = "MALWARE")]
		Malware,
	}
}
