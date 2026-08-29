namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents information about the GitHub instance.
    /// </summary>
    public class GitHubMetadata : QueryableValue<GitHubMetadata>
    {
        internal GitHubMetadata(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Returns a String that's a SHA of `github-services`
        /// </summary>
        public string GitHubServicesSha { get; }

        /// <summary>
        /// IP addresses that users connect to for git operations
        /// </summary>
        public IEnumerable<string> GitIpAddresses { get; }

        /// <summary>
        /// IP addresses that GitHub Enterprise Importer uses for outbound connections
        /// </summary>
        public IEnumerable<string> GithubEnterpriseImporterIpAddresses { get; }

        /// <summary>
        /// IP addresses that service hooks are sent from
        /// </summary>
        public IEnumerable<string> HookIpAddresses { get; }

        /// <summary>
        /// IP addresses that the importer connects from
        /// </summary>
        public IEnumerable<string> ImporterIpAddresses { get; }

        /// <summary>
        /// Whether or not users are verified
        /// </summary>
        public bool IsPasswordAuthenticationVerifiable { get; }

        /// <summary>
        /// IP addresses for GitHub Pages' A records
        /// </summary>
        public IEnumerable<string> PagesIpAddresses { get; }

        internal static GitHubMetadata Create(Expression expression)
        {
            return new GitHubMetadata(expression);
        }
    }
}