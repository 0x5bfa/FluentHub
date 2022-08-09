namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

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

namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public class RepositoryAuditEntryData : IRepositoryAuditEntryData
    {
        public Repository Repository { get; set; }

        public string RepositoryName { get; set; }

        public string RepositoryResourcePath { get; set; }

        public string RepositoryUrl { get; set; }
    }
}