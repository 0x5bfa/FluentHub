// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents information about the GitHub instance.
	/// </summary>
	public class GitHubMetadata
	{
		/// <summary>
		/// Returns a String that's a SHA of `github-services`
		/// </summary>
		public string GitHubServicesSha { get; set; }

		/// <summary>
		/// IP addresses that users connect to for git operations
		/// </summary>
		public List<string> GitIpAddresses { get; set; }

		/// <summary>
		/// IP addresses that GitHub Enterprise Importer uses for outbound connections
		/// </summary>
		public List<string> GithubEnterpriseImporterIpAddresses { get; set; }

		/// <summary>
		/// IP addresses that service hooks are sent from
		/// </summary>
		public List<string> HookIpAddresses { get; set; }

		/// <summary>
		/// IP addresses that the importer connects from
		/// </summary>
		public List<string> ImporterIpAddresses { get; set; }

		/// <summary>
		/// Whether or not users are verified
		/// </summary>
		public bool IsPasswordAuthenticationVerifiable { get; set; }

		/// <summary>
		/// IP addresses for GitHub Pages' A records
		/// </summary>
		public List<string> PagesIpAddresses { get; set; }
	}
}
