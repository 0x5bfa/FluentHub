namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A Git push.
    /// </summary>
    public class Push : QueryableValue<Push>
    {
        internal Push(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The Node ID of the Push object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The SHA after the push
        /// </summary>
        public string NextSha { get; }

        /// <summary>
        /// The permalink for this push.
        /// </summary>
        public string Permalink { get; }

        /// <summary>
        /// The SHA before the push
        /// </summary>
        public string PreviousSha { get; }

        /// <summary>
        /// The actor who pushed
        /// </summary>
        public IActor Pusher => this.CreateProperty(x => x.Pusher, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// The repository that was pushed to
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        internal static Push Create(Expression expression)
        {
            return new Push(expression);
        }
    }
}