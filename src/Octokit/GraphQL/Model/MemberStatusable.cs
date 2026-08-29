namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Model;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Entities that have members who can set status messages.
    /// </summary>
    [GraphQLIdentifier("MemberStatusable")]
    public interface IMemberStatusable : IQueryableValue<IMemberStatusable>, IQueryableInterface
    {
        /// <summary>
        /// Get the status messages members of this entity have set that are either public or visible only to the organization.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for user statuses returned from the connection.</param>
        UserStatusConnection MemberStatuses(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<UserStatusOrder>? orderBy = null);
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    [GraphQLIdentifier("MemberStatusable")]
    internal class StubIMemberStatusable : QueryableValue<StubIMemberStatusable>, IMemberStatusable
    {
        internal StubIMemberStatusable(Expression expression) : base(expression)
        {
        }

        public UserStatusConnection MemberStatuses(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<UserStatusOrder>? orderBy = null) => this.CreateMethodCall(x => x.MemberStatuses(first, after, last, before, orderBy), Octokit.GraphQL.Model.UserStatusConnection.Create);

        internal static StubIMemberStatusable Create(Expression expression)
        {
            return new StubIMemberStatusable(expression);
        }
    }
}