namespace FluentHub.Octokit.v4.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Metadata for an audit entry with action repo.*
    /// </summary>
    public interface IRepositoryAuditEntryData
    {
        /// <summary>
        /// The repository associated with the action
        /// </summary>
        Repository Repository { get; set; }

        /// <summary>
        /// The name of the repository
        /// </summary>
        string RepositoryName { get; set; }

        /// <summary>
        /// The HTTP path for the repository
        /// </summary>
        string RepositoryResourcePath { get; set; }

        /// <summary>
        /// The HTTP URL for the repository
        /// </summary>
        string RepositoryUrl { get; set; }
    }
}

namespace FluentHub.Octokit.v4.Model.Internal
{
    using System;
    using System.Collections.Generic;

    internal class StubIRepositoryAuditEntryData
    {
        

        public Repository Repository { get; set; }

        public string RepositoryName { get; set; }

        public string RepositoryResourcePath { get; set; }

        public string RepositoryUrl { get; set; }
    }
}