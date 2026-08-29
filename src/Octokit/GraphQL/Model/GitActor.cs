namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents an actor in a Git commit (ie. an author or committer).
    /// </summary>
    public class GitActor : QueryableValue<GitActor>
    {
        internal GitActor(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A URL pointing to the author's public avatar.
        /// </summary>
        /// <param name="size">The size of the resulting square image.</param>
        public string AvatarUrl(Arg<int>? size = null) => default;

        /// <summary>
        /// The timestamp of the Git action (authoring or committing).
        /// </summary>
        public string Date { get; }

        /// <summary>
        /// The email in the Git commit.
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// The name in the Git commit.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The GitHub user corresponding to the email field. Null if no such user exists.
        /// </summary>
        public User User => this.CreateProperty(x => x.User, Octokit.GraphQL.Model.User.Create);

        internal static GitActor Create(Expression expression)
        {
            return new GitActor(expression);
        }
    }
}