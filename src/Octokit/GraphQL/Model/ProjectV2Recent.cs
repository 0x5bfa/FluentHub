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
    /// Recent projects for the owner.
    /// </summary>
    [GraphQLIdentifier("ProjectV2Recent")]
    public interface IProjectV2Recent : IQueryableValue<IProjectV2Recent>, IQueryableInterface
    {
        /// <summary>
        /// Recent projects that this user has modified in the context of the owner.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        ProjectV2Connection RecentProjects(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null);
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    [GraphQLIdentifier("ProjectV2Recent")]
    internal class StubIProjectV2Recent : QueryableValue<StubIProjectV2Recent>, IProjectV2Recent
    {
        internal StubIProjectV2Recent(Expression expression) : base(expression)
        {
        }

        public ProjectV2Connection RecentProjects(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.RecentProjects(first, after, last, before), Octokit.GraphQL.Model.ProjectV2Connection.Create);

        internal static StubIProjectV2Recent Create(Expression expression)
        {
            return new StubIProjectV2Recent(expression);
        }
    }
}