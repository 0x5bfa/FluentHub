namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a comparison between two commit revisions.
    /// </summary>
    public class Comparison : QueryableValue<Comparison>
    {
        internal Comparison(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The number of commits ahead of the base branch.
        /// </summary>
        public int AheadBy { get; }

        /// <summary>
        /// The base revision of this comparison.
        /// </summary>
        public IGitObject BaseTarget => this.CreateProperty(x => x.BaseTarget, Octokit.GraphQL.Model.Internal.StubIGitObject.Create);

        /// <summary>
        /// The number of commits behind the base branch.
        /// </summary>
        public int BehindBy { get; }

        /// <summary>
        /// The commits which compose this comparison.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public ComparisonCommitConnection Commits(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Commits(first, after, last, before), Octokit.GraphQL.Model.ComparisonCommitConnection.Create);

        /// <summary>
        /// The head revision of this comparison.
        /// </summary>
        public IGitObject HeadTarget => this.CreateProperty(x => x.HeadTarget, Octokit.GraphQL.Model.Internal.StubIGitObject.Create);

        /// <summary>
        /// The Node ID of the Comparison object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The status of this comparison.
        /// </summary>
        public ComparisonStatus Status { get; }

        internal static Comparison Create(Expression expression)
        {
            return new Comparison(expression);
        }
    }
}