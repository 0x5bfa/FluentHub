namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Types that can be inside a StatusCheckRollup context.
    /// </summary>
    public class StatusCheckRollupContext
    {
        
        /// <summary>
        /// A check run.
        /// </summary>
            public CheckRun CheckRun { get; set; }

        /// <summary>
        /// Represents an individual commit status context
        /// </summary>
            public StatusContext StatusContext { get; set; }
    }
}