namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Types that can be pinned to a profile page.
    /// </summary>
    public class PinnableItem
    {
        
        /// <summary>
        /// A Gist.
        /// </summary>
            public Gist Gist { get; set; }

        /// <summary>
        /// A repository contains the content for a project.
        /// </summary>
            public Repository Repository { get; set; }
    }
}