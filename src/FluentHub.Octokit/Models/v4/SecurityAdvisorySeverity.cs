// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Severity of the vulnerability.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum SecurityAdvisorySeverity
	{
		/// <summary>
		/// Low.
		/// </summary>
		[EnumMember(Value = "LOW")]
		Low,

		/// <summary>
		/// Moderate.
		/// </summary>
		[EnumMember(Value = "MODERATE")]
		Moderate,

		/// <summary>
		/// High.
		/// </summary>
		[EnumMember(Value = "HIGH")]
		High,

		/// <summary>
		/// Critical.
		/// </summary>
		[EnumMember(Value = "CRITICAL")]
		Critical,
	}
}
