namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A Pinned Discussion is a discussion pinned to a repository's index page.
    /// </summary>
    public class PinnedDiscussion : QueryableValue<PinnedDiscussion>
    {
        internal PinnedDiscussion(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public long? DatabaseId { get; }

        /// <summary>
        /// The discussion that was pinned.
        /// </summary>
        public Discussion Discussion => this.CreateProperty(x => x.Discussion, Octokit.GraphQL.Model.Discussion.Create);

        /// <summary>
        /// Color stops of the chosen gradient
        /// </summary>
        public IEnumerable<string> GradientStopColors { get; }

        /// <summary>
        /// The Node ID of the PinnedDiscussion object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Background texture pattern
        /// </summary>
        public PinnedDiscussionPattern Pattern { get; }

        /// <summary>
        /// The actor that pinned this discussion.
        /// </summary>
        public IActor PinnedBy => this.CreateProperty(x => x.PinnedBy, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// Preconfigured background gradient option
        /// </summary>
        public PinnedDiscussionGradient? PreconfiguredGradient { get; }

        /// <summary>
        /// The repository associated with this node.
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        internal static PinnedDiscussion Create(Expression expression)
        {
            return new PinnedDiscussion(expression);
        }
    }
}