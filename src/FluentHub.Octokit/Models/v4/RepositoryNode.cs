namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a object that belongs to a repository.
    /// </summary>
    public interface IRepositoryNode
    {
        /// <summary>
        /// The repository associated with this node.
        /// </summary>
        Repository Repository { get; set; }
    }
}

namespace FluentHub.Octokit.Models.v4.Internal
{
    using System;
    using System.Collections.Generic;

    internal class StubIRepositoryNode
    {
        

        public Repository Repository { get; set; }
    }
}