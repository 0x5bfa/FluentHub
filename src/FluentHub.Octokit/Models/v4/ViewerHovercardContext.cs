namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A hovercard context with a message describing how the viewer is related.
    /// </summary>
    public class ViewerHovercardContext
    {
        /// <summary>
        /// A string describing this context
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// An octicon to accompany this context
        /// </summary>
        public string Octicon { get; set; }

        /// <summary>
        /// Identifies the user who is related to this context.
        /// </summary>
        public User Viewer { get; set; }
    }
}