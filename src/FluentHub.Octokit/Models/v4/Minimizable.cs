namespace FluentHub.Octokit.v4.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Entities that can be minimized.
    /// </summary>
    public interface IMinimizable
    {
        /// <summary>
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

namespace FluentHub.Octokit.v4.Model.Internal
{
    using System;
    using System.Collections.Generic;

    internal class StubIMinimizable
    {
        

        public bool IsMinimized { get; set; }

        public string MinimizedReason { get; set; }

        public bool ViewerCanMinimize { get; set; }
    }
}