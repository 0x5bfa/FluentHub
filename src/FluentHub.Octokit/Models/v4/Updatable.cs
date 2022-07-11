namespace FluentHub.Octokit.v4.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Entities that can be updated.
    /// </summary>
    public interface IUpdatable
    {
        /// <summary>
        /// Check if the current viewer can update this object.
        /// </summary>
        bool ViewerCanUpdate { get; set; }
    }
}

namespace FluentHub.Octokit.v4.Model.Internal
{
    using System;
    using System.Collections.Generic;

    internal class StubIUpdatable
    {
        

        public bool ViewerCanUpdate { get; set; }
    }
}