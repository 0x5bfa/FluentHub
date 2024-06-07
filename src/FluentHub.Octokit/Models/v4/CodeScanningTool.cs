// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A tool that must provide code scanning results for this rule to pass.
	/// </summary>
	public class CodeScanningTool
	{
		/// <summary>
		/// The severity level at which code scanning results that raise alerts block a reference update. For more information on alert severity levels, see "[About code scanning alerts](${externalDocsUrl}/code-security/code-scanning/managing-code-scanning-alerts/about-code-scanning-alerts#about-alert-severity-and-security-severity-levels)."
		/// </summary>
		public string AlertsThreshold { get; set; }

		/// <summary>
		/// The severity level at which code scanning results that raise security alerts block a reference update. For more information on security severity levels, see "[About code scanning alerts](${externalDocsUrl}/code-security/code-scanning/managing-code-scanning-alerts/about-code-scanning-alerts#about-alert-severity-and-security-severity-levels)."
		/// </summary>
		public string SecurityAlertsThreshold { get; set; }

		/// <summary>
		/// The name of a code scanning tool
		/// </summary>
		public string Tool { get; set; }
	}
}
