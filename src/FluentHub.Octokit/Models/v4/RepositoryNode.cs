namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a object that belongs to a repository.
    /// </summary>
    public interface IRepositoryNode
    {        /// <summary>
        /// The repository associated with this node.
        /// </summary>
        Repository Repository { get; set; }
    }
}

namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public class RepositoryNode : IRepositoryNode
    {
        public Repository Repository { get; set; }
    }
}