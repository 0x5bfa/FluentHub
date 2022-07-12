namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents an owner of a project (beta).
    /// </summary>
    public interface IProjectNextOwner
    {        ID Id { get; set; }

        /// <summary>
        /// Find a project by project (beta) number.
        /// </summary>
        /// <param name="number">The project (beta) number.</param>
        ProjectNext ProjectNext { get; set; }

        /// <summary>
        /// A list of projects (beta) under the owner.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="query">A project (beta) to search for under the the owner.</param>
        /// <param name="sortBy">How to order the returned projects (beta).</param>
        ProjectNextConnection ProjectsNext { get; set; }
    }
}

namespace FluentHub.Octokit.Models.v4.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    internal class ProjectNextOwner : IProjectNextOwner
    {
        public ID Id { get; set; }

        public ProjectNext ProjectNext { get; set; }

        public ProjectNextConnection ProjectsNext { get; set; }
    }
}