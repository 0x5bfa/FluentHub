namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Things that can be starred.
    /// </summary>
    public interface IStarrable
    {
        ID Id { get; set; }

        /// <summary>
        /// Returns a count of how many stargazers there are on this object
        /// </summary>
        int StargazerCount { get; set; }

        /// <summary>
        /// A list of users who have starred this starrable.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Order for connection</param>
        StargazerConnection Stargazers { get; set; }

        /// <summary>
        /// Returns a boolean indicating whether the viewing user has starred this starrable.
        /// </summary>
        bool ViewerHasStarred { get; set; }
    }
}

namespace FluentHub.Octokit.Models.v4.Internal
{
    using System;
    using System.Collections.Generic;

    internal class StubIStarrable
    {
        

        public ID Id { get; set; }

        public int StargazerCount { get; set; }

        public StargazerConnection Stargazers { get; set; }

        public bool ViewerHasStarred { get; set; }
    }
}