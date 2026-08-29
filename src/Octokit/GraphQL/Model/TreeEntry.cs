namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a Git tree entry.
    /// </summary>
    public class TreeEntry : QueryableValue<TreeEntry>
    {
        internal TreeEntry(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The extension of the file
        /// </summary>
        public string Extension { get; }

        /// <summary>
        /// Whether or not this tree entry is generated
        /// </summary>
        public bool IsGenerated { get; }

        /// <summary>
        /// The programming language this file is written in.
        /// </summary>
        public Language Language => this.CreateProperty(x => x.Language, Octokit.GraphQL.Model.Language.Create);

        /// <summary>
        /// Number of lines in the file.
        /// </summary>
        public int? LineCount { get; }

        /// <summary>
        /// Entry file mode.
        /// </summary>
        public int Mode { get; }

        /// <summary>
        /// Entry file name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Entry file name. (Base64-encoded)
        /// </summary>
        public string NameRaw { get; }

        /// <summary>
        /// Entry file object.
        /// </summary>
        public IGitObject Object => this.CreateProperty(x => x.Object, Octokit.GraphQL.Model.Internal.StubIGitObject.Create);

        /// <summary>
        /// Entry file Git object ID.
        /// </summary>
        public string Oid { get; }

        /// <summary>
        /// The full path of the file.
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// The full path of the file. (Base64-encoded)
        /// </summary>
        public string PathRaw { get; }

        /// <summary>
        /// The Repository the tree entry belongs to
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        /// <summary>
        /// Entry byte size
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// If the TreeEntry is for a directory occupied by a submodule project, this returns the corresponding submodule
        /// </summary>
        public Submodule Submodule => this.CreateProperty(x => x.Submodule, Octokit.GraphQL.Model.Submodule.Create);

        /// <summary>
        /// Entry file type.
        /// </summary>
        public string Type { get; }

        internal static TreeEntry Create(Expression expression)
        {
            return new TreeEntry(expression);
        }
    }
}