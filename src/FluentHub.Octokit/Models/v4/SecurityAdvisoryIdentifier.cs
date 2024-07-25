// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A GitHub Security Advisory Identifier
	/// </summary>
	public class SecurityAdvisoryIdentifier
	{
		/// <summary>
		/// The identifier type, e.g. GHSA, CVE
		/// </summary>
		public string Type { get; set; }

		/// <summary>
		/// The identifier
		/// </summary>
		public string Value { get; set; }
	}
}
