namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A file in a gist.
    /// </summary>
    public class GistFile : QueryableValue<GistFile>
    {
        internal GistFile(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The file name encoded to remove characters that are invalid in URL paths.
        /// </summary>
        public string EncodedName { get; }

        /// <summary>
        /// The gist file encoding.
        /// </summary>
        public string Encoding { get; }

        /// <summary>
        /// The file extension from the file name.
        /// </summary>
        public string Extension { get; }

        /// <summary>
        /// Indicates if this file is an image.
        /// </summary>
        public bool IsImage { get; }

        /// <summary>
        /// Whether the file's contents were truncated.
        /// </summary>
        public bool IsTruncated { get; }

        /// <summary>
        /// The programming language this file is written in.
        /// </summary>
        public Language Language => this.CreateProperty(x => x.Language, Octokit.GraphQL.Model.Language.Create);

        /// <summary>
        /// The gist file name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The gist file size in bytes.
        /// </summary>
        public int? Size { get; }

        /// <summary>
        /// UTF8 text data or null if the file is binary
        /// </summary>
        /// <param name="truncate">Optionally truncate the returned file to this length.</param>
        public string Text(Arg<int>? truncate = null) => default;

        internal static GistFile Create(Expression expression)
        {
            return new GistFile(expression);
        }
    }
}