namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Detail needed to display a hovercard for a user
    /// </summary>
    public class Hovercard
    {
        /// <summary>
        /// Each of the contexts for this hovercard
        /// </summary>
        public List<IHovercardContext> Contexts { get; set; }
    }
}