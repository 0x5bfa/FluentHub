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
    /// An object that can have labels assigned to it.
    /// </summary>
    [GraphQLIdentifier("Labelable")]
    public interface ILabelable : IQueryableValue<ILabelable>, IQueryableInterface
    {
        /// <summary>
        /// A list of labels associated with the object.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for labels returned from the connection.</param>
        LabelConnection Labels(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<LabelOrder>? orderBy = null);
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    [GraphQLIdentifier("Labelable")]
    internal class StubILabelable : QueryableValue<StubILabelable>, ILabelable
    {
        internal StubILabelable(Expression expression) : base(expression)
        {
        }

        public LabelConnection Labels(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<LabelOrder>? orderBy = null) => this.CreateMethodCall(x => x.Labels(first, after, last, before, orderBy), Octokit.GraphQL.Model.LabelConnection.Create);

        internal static StubILabelable Create(Expression expression)
        {
            return new StubILabelable(expression);
        }
    }
}