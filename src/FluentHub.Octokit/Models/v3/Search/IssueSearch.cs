using Octokit;

namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// An Issue is a place to discuss ideas, enhancements, tasks, and bugs for a project.
    /// </summary>
    public class IssueSearch
    {
        /// <summary>
        /// Reason that the conversation was locked.
        /// </summary>
        public LockReason? ActiveLockReason { get; set; }

        public string Body { get; set; }

        /// <summary>
        /// The actor who authored the comment.
        /// </summary>
        public OctokitV3.User Author { get; set; }
        public string Name { get; set; }
        public OctokitV3.Repository Repo { get; set; }
        
        public int Id { get; set; }
        
        public int Comments { get; set; }
        
        public DateTimeOffset CreatedAt { get; set; }
        
        public IReadOnlyList<OctokitV3.Label> Labels { get; set; }
        public StringEnum<ItemState> State { get; set; }
    }
}