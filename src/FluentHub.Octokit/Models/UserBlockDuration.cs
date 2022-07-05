using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Models
{
    /// <summary>
    /// The possible durations that a user can be blocked for.
    /// </summary>
    public enum UserBlockDuration
    {
        /// <summary>
        /// The user was blocked for 1 day
        /// </summary>
        OneDay,

        /// <summary>
        /// The user was blocked for 3 days
        /// </summary>
        ThreeDays,

        /// <summary>
        /// The user was blocked for 7 days
        /// </summary>
        OneWeek,

        /// <summary>
        /// The user was blocked for 30 days
        /// </summary>
        OneMonth,

        /// <summary>
        /// The user was blocked permanently
        /// </summary>
        Permanent,
    }
}
