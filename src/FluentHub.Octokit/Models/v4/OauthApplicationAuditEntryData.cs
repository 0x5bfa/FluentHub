namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Metadata for an audit entry with action oauth_application.*
    /// </summary>
    public interface IOauthApplicationAuditEntryData
    {
        /// <summary>
        /// The name of the OAuth Application.
        /// </summary>
        string OauthApplicationName { get; set; }

        /// <summary>
        /// The HTTP path for the OAuth Application
        /// </summary>
        string OauthApplicationResourcePath { get; set; }

        /// <summary>
        /// The HTTP URL for the OAuth Application
        /// </summary>
        string OauthApplicationUrl { get; set; }
    }
}

namespace FluentHub.Octokit.Models.v4.Internal
{
    using System;
    using System.Collections.Generic;

    internal class StubIOauthApplicationAuditEntryData
    {
        

        public string OauthApplicationName { get; set; }

        public string OauthApplicationResourcePath { get; set; }

        public string OauthApplicationUrl { get; set; }
    }
}