namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A repository issue template.
    /// </summary>
    public class IssueTemplate : QueryableValue<IssueTemplate>
    {
        internal IssueTemplate(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The template purpose.
        /// </summary>
        public string About { get; }

        /// <summary>
        /// The suggested assignees.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public UserConnection Assignees(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Assignees(first, after, last, before), Octokit.GraphQL.Model.UserConnection.Create);

        /// <summary>
        /// The suggested issue body.
        /// </summary>
        public string Body { get; }

        /// <summary>
        /// The template filename.
        /// </summary>
        public string Filename { get; }

        /// <summary>
        /// The suggested issue labels
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for labels returned from the connection.</param>
        public LabelConnection Labels(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<LabelOrder>? orderBy = null) => this.CreateMethodCall(x => x.Labels(first, after, last, before, orderBy), Octokit.GraphQL.Model.LabelConnection.Create);

        /// <summary>
        /// The template name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The suggested issue title.
        /// </summary>
        public string Title { get; }

        internal static IssueTemplate Create(Expression expression)
        {
            return new IssueTemplate(expression);
        }
    }
}