namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// An object with an ID.
    /// </summary>
    public interface INode
    {
        /// <summary>
        /// ID of the object.
        /// </summary>
        ID Id { get; set; }
    }
}

namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public class Node : INode
    {
        public ID Id { get; set; }
    }
}