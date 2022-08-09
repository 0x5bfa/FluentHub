namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// An object that can be locked.
    /// </summary>
    public interface ILockable
    {
        /// <summary>
        /// Reason that the conversation was locked.
        /// </summary>
        LockReason? ActiveLockReason { get; set; }

        /// <summary>
        /// `true` if the object is locked
        /// </summary>
        bool Locked { get; set; }
    }
}

namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public class Lockable : ILockable
    {
        public LockReason? ActiveLockReason { get; set; }

        public bool Locked { get; set; }
    }
}