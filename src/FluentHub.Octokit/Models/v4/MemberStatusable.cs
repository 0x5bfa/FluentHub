namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Entities that have members who can set status messages.
    /// </summary>
    public interface IMemberStatusable
    {        /// <summary>
        /// Get the status messages members of this entity have set that are either public or visible only to the organization.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for user statuses returned from the connection.</param>
        UserStatusConnection MemberStatuses { get; set; }
    }
}

namespace FluentHub.Octokit.Models.v4.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    internal class MemberStatusable : IMemberStatusable
    {
        public UserStatusConnection MemberStatuses { get; set; }
    }
}