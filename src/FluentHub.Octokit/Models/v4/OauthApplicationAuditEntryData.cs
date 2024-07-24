// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{

	/// <summary>
	/// Metadata for an audit entry with action oauth_application.*
	/// </summary>
	public interface IOauthApplicationAuditEntryData
	{
		/// <summary>
		/// The name of the OAuth application.
		/// </summary>
		string OauthApplicationName { get; set; }

		/// <summary>
		/// The HTTP path for the OAuth application
		/// </summary>
		string OauthApplicationResourcePath { get; set; }

		/// <summary>
		/// The HTTP URL for the OAuth application
		/// </summary>
		string OauthApplicationUrl { get; set; }
	}
}

namespace FluentHub.Octokit.Models.v4
{
	public class OauthApplicationAuditEntryData : IOauthApplicationAuditEntryData
	{
		public string OauthApplicationName { get; set; }

		public string OauthApplicationResourcePath { get; set; }

		public string OauthApplicationUrl { get; set; }
	}
}

