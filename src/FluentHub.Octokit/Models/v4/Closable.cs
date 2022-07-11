namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// An object that can be closed
    /// </summary>
    public interface IClosable
    {
        /// <summary>
        /// `true` if the object is closed (definition of closed may depend on type)
        /// </summary>
        bool Closed { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was closed.
        /// </summary>
        DateTimeOffset? ClosedAt { get; set; }
    }
}

namespace FluentHub.Octokit.Models.v4.Internal
{
    using System;
    using System.Collections.Generic;

    internal class StubIClosable
    {
        

        public bool Closed { get; set; }

        public DateTimeOffset? ClosedAt { get; set; }
    }
}