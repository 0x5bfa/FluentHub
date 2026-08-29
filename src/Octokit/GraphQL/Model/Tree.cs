namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a Git tree.
    /// </summary>
    public class Tree : QueryableValue<Tree>
    {
        internal Tree(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// An abbreviated version of the Git object ID
        /// </summary>
        public string AbbreviatedOid { get; }

        /// <summary>
        /// The HTTP path for this Git object
        /// </summary>
        public string CommitResourcePath { get; }

        /// <summary>
        /// The HTTP URL for this Git object
        /// </summary>
        public string CommitUrl { get; }

        /// <summary>
        /// A list of tree entries.
        /// </summary>
        public IQueryableList<TreeEntry> Entries => this.CreateProperty(x => x.Entries);

        /// <summary>
        /// The Node ID of the Tree object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The Git object ID
        /// </summary>
        public string Oid { get; }

        /// <summary>
        /// The Repository the Git object belongs to
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        internal static Tree Create(Expression expression)
        {
            return new Tree(expression);
        }
    }
}