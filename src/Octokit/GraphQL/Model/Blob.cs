namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a Git blob.
    /// </summary>
    public class Blob : QueryableValue<Blob>
    {
        internal Blob(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// An abbreviated version of the Git object ID
        /// </summary>
        public string AbbreviatedOid { get; }

        /// <summary>
        /// Byte size of Blob object
        /// </summary>
        public int ByteSize { get; }

        /// <summary>
        /// The HTTP path for this Git object
        /// </summary>
        public string CommitResourcePath { get; }

        /// <summary>
        /// The HTTP URL for this Git object
        /// </summary>
        public string CommitUrl { get; }

        /// <summary>
        /// The Node ID of the Blob object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Indicates whether the Blob is binary or text. Returns null if unable to determine the encoding.
        /// </summary>
        public bool? IsBinary { get; }

        /// <summary>
        /// Indicates whether the contents is truncated
        /// </summary>
        public bool IsTruncated { get; }

        /// <summary>
        /// The Git object ID
        /// </summary>
        public string Oid { get; }

        /// <summary>
        /// The Repository the Git object belongs to
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        /// <summary>
        /// UTF8 text data or null if the Blob is binary
        /// </summary>
        public string Text { get; }

        internal static Blob Create(Expression expression)
        {
            return new Blob(expression);
        }
    }
}