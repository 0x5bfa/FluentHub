namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Entities that can be minimized.
    /// </summary>
    public interface IMinimizable
    {        /// <summary>
        /// Returns whether or not a comment has been minimized.
        /// </summary>
        bool IsMinimized { get; set; }

        /// <summary>
        /// Returns why the comment was minimized.
        /// </summary>
        string MinimizedReason { get; set; }

        /// <summary>
        /// Check if the current viewer can minimize this object.
        /// </summary>
        bool ViewerCanMinimize { get; set; }
    }
}

namespace FluentHub.Octokit.Models.v4.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    internal class Minimizable : IMinimizable
    {
        public bool IsMinimized { get; set; }

        public string MinimizedReason { get; set; }

        public bool ViewerCanMinimize { get; set; }
    }
}