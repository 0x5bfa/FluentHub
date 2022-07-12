namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a 'auto_merge_disabled' event on a given pull request.
    /// </summary>
    public class AutoMergeDisabledEvent
    {
        /// <summary>
        /// Identifies the actor who performed the event.
        /// </summary>
        public IActor Actor { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The user who disabled auto-merge for this Pull Request
        /// </summary>
        public User Disabler { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// PullRequest referenced by event
        /// </summary>
        public PullRequest PullRequest { get; set; }

        /// <summary>
        /// The reason auto-merge was disabled
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// The reason_code relating to why auto-merge was disabled
        /// </summary>
        public string ReasonCode { get; set; }
    }
}