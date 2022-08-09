namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

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

        /// <summary>
        /// Humanized string of "Identifies the date and time when the object was closed."
        /// <summary>
        string ClosedAtHumanized { get; set; }
    }
}

namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public class Closable : IClosable
    {
        public bool Closed { get; set; }

        public DateTimeOffset? ClosedAt { get; set; }

        public string ClosedAtHumanized { get; set; }
    }
}