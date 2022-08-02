namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a 'renamed' event on a given issue or pull request
    /// </summary>
    public class RenamedTitleEvent
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
        /// Identifies the current title of the issue or pull request.
        /// </summary>
        public string CurrentTitle { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// Identifies the previous title of the issue or pull request.
        /// </summary>
        public string PreviousTitle { get; set; }

        /// <summary>
        /// Subject that was renamed.
        /// </summary>
        public RenamedTitleSubject Subject { get; set; }
    }
}