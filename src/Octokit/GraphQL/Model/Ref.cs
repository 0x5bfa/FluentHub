namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a Git reference.
    /// </summary>
    public class Ref : QueryableValue<Ref>
    {
        internal Ref(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A list of pull requests with this ref as the head ref.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="baseRefName">The base ref name to filter the pull requests by.</param>
        /// <param name="headRefName">The head ref name to filter the pull requests by.</param>
        /// <param name="labels">A list of label names to filter the pull requests by.</param>
        /// <param name="orderBy">Ordering options for pull requests returned from the connection.</param>
        /// <param name="states">A list of states to filter the pull requests by.</param>
        public PullRequestConnection AssociatedPullRequests(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<string>? baseRefName = null, Arg<string>? headRefName = null, Arg<IEnumerable<string>>? labels = null, Arg<IssueOrder>? orderBy = null, Arg<IEnumerable<PullRequestState>>? states = null) => this.CreateMethodCall(x => x.AssociatedPullRequests(first, after, last, before, baseRefName, headRefName, labels, orderBy, states), Octokit.GraphQL.Model.PullRequestConnection.Create);

        /// <summary>
        /// Branch protection rules for this ref
        /// </summary>
        public BranchProtectionRule BranchProtectionRule => this.CreateProperty(x => x.BranchProtectionRule, Octokit.GraphQL.Model.BranchProtectionRule.Create);

        /// <summary>
        /// Compares the current ref as a base ref to another head ref, if the comparison can be made.
        /// </summary>
        /// <param name="headRef">The head ref to compare against.</param>
        public Comparison Compare(Arg<string> headRef) => this.CreateMethodCall(x => x.Compare(headRef), Octokit.GraphQL.Model.Comparison.Create);

        /// <summary>
        /// The Node ID of the Ref object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The ref name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The ref's prefix, such as `refs/heads/` or `refs/tags/`.
        /// </summary>
        public string Prefix { get; }

        /// <summary>
        /// Branch protection rules that are viewable by non-admins
        /// </summary>
        public RefUpdateRule RefUpdateRule => this.CreateProperty(x => x.RefUpdateRule, Octokit.GraphQL.Model.RefUpdateRule.Create);

        /// <summary>
        /// The repository the ref belongs to.
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        /// <summary>
        /// A list of rules from active Repository and Organization rulesets that apply to this ref.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for repository rules.</param>
        public RepositoryRuleConnection Rules(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<RepositoryRuleOrder>? orderBy = null) => this.CreateMethodCall(x => x.Rules(first, after, last, before, orderBy), Octokit.GraphQL.Model.RepositoryRuleConnection.Create);

        /// <summary>
        /// The object the ref points to. Returns null when object does not exist.
        /// </summary>
        public IGitObject Target => this.CreateProperty(x => x.Target, Octokit.GraphQL.Model.Internal.StubIGitObject.Create);

        internal static Ref Create(Expression expression)
        {
            return new Ref(expression);
        }
    }
}