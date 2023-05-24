namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A branch linked to an issue.
    /// </summary>
    public class LinkedBranch
    {
        public ID Id { get; set; }

        /// <summary>
        /// The branch's ref.
        /// </summary>
        public Ref Ref { get; set; }
    }
}