namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for gist connections
    /// </summary>
    public class GistOrder
    {
        /// <summary>
        /// The field to order repositories by.
        /// </summary>
        public GistOrderField Field { get; set; }

        /// <summary>
        /// The ordering direction.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}