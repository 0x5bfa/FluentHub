namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Metadata for an audit entry with action team.*
    /// </summary>
    public interface ITeamAuditEntryData
    {
        /// <summary>
        /// The team associated with the action
        /// </summary>
        Team Team { get; set; }

        /// <summary>
        /// The name of the team
        /// </summary>
        string TeamName { get; set; }

        /// <summary>
        /// The HTTP path for this team
        /// </summary>
        string TeamResourcePath { get; set; }

        /// <summary>
        /// The HTTP URL for this team
        /// </summary>
        string TeamUrl { get; set; }
    }
}

namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public class TeamAuditEntryData : ITeamAuditEntryData
    {
        public Team Team { get; set; }

        public string TeamName { get; set; }

        public string TeamResourcePath { get; set; }

        public string TeamUrl { get; set; }
    }
}