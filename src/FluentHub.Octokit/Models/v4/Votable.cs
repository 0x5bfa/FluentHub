namespace FluentHub.Octokit.v4.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A subject that may be upvoted.
    /// </summary>
    public interface IVotable
    {
        /// <summary>
        /// Number of upvotes that this subject has received.
        /// </summary>
        int UpvoteCount { get; set; }

        /// <summary>
        /// Whether or not the current user can add or remove an upvote on this subject.
        /// </summary>
        bool ViewerCanUpvote { get; set; }

        /// <summary>
        /// Whether or not the current user has already upvoted this subject.
        /// </summary>
        bool ViewerHasUpvoted { get; set; }
    }
}

namespace FluentHub.Octokit.v4.Model.Internal
{
    using System;
    using System.Collections.Generic;

    internal class StubIVotable
    {
        

        public int UpvoteCount { get; set; }

        public bool ViewerCanUpvote { get; set; }

        public bool ViewerHasUpvoted { get; set; }
    }
}