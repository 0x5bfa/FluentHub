namespace FluentHub.Octokit.v4.Model
{
    using System;

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