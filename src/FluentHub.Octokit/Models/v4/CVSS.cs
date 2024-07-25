// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The Common Vulnerability Scoring System
	/// </summary>
	public class CVSS
	{
		/// <summary>
		/// The CVSS score associated with this advisory
		/// </summary>
		public double Score { get; set; }

		/// <summary>
		/// The CVSS vector string associated with this advisory
		/// </summary>
		public string VectorString { get; set; }
	}
}
