namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;

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

namespace FluentHub.Octokit.Models.v4.Internal
{
    using System;
    using System.Collections.Generic;

    internal class StubILockable
    {
        

        public LockReason? ActiveLockReason { get; set; }

        public bool Locked { get; set; }
    }
}