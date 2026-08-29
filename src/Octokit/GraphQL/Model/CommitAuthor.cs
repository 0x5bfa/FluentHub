namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Specifies an author for filtering Git commits.
    /// </summary>
    public class CommitAuthor
    {
        /// <summary>
        /// ID of a User to filter by. If non-null, only commits authored by this user will be returned. This field takes precedence over emails.
        /// </summary>
        public ID? Id { get; set; }

        /// <summary>
        /// Email addresses to filter by. Commits authored by any of the specified email addresses will be returned.
        /// </summary>
        public IEnumerable<string> Emails { get; set; }
    }
}