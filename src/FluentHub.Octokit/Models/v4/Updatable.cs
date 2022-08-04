namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

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

namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public class Updatable : IUpdatable
    {
        public bool ViewerCanUpdate { get; set; }
    }
}