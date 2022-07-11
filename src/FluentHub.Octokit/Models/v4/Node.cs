namespace FluentHub.Octokit.v4.Model
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

namespace FluentHub.Octokit.v4.Model.Internal
{
    using System;
    using System.Collections.Generic;

    internal class StubINode
    {
        

        public ID Id { get; set; }
    }
}