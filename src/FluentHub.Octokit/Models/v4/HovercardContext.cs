namespace FluentHub.Octokit.v4.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// An individual line of a hovercard
    /// </summary>
    public interface IHovercardContext
    {
        /// <summary>
        /// A string describing this context
        /// </summary>
        string Message { get; set; }

        /// <summary>
        /// An octicon to accompany this context
        /// </summary>
        string Octicon { get; set; }
    }
}

namespace FluentHub.Octokit.v4.Model.Internal
{
    using System;
    using System.Collections.Generic;

    internal class StubIHovercardContext
    {
        

        public string Message { get; set; }

        public string Octicon { get; set; }
    }
}