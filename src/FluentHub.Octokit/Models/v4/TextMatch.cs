namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A text match within a search result.
    /// </summary>
    public class TextMatch
    {
        /// <summary>
        /// The specific text fragment within the property matched on.
        /// </summary>
        public string Fragment { get; set; }

        /// <summary>
        /// Highlights within the matched fragment.
        /// </summary>
        public List<TextMatchHighlight> Highlights { get; set; }

        /// <summary>
        /// The property matched on.
        /// </summary>
        public string Property { get; set; }
    }
}