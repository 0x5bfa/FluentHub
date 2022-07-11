namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;

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

namespace FluentHub.Octokit.Models.v4.Internal
{
    using System;
    using System.Collections.Generic;

    internal class StubINode
    {
        

        public ID Id { get; set; }
    }
}