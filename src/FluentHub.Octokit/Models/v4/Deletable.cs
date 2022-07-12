namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Entities that can be deleted.
    /// </summary>
    public interface IDeletable
    {        /// <summary>
        /// Check if the current viewer can delete this object.
        /// </summary>
        bool ViewerCanDelete { get; set; }
    }
}

namespace FluentHub.Octokit.Models.v4.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    internal class Deletable : IDeletable
    {
        public bool ViewerCanDelete { get; set; }
    }
}