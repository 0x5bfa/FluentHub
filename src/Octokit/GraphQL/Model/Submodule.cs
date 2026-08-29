namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A pointer to a repository at a specific revision embedded inside another repository.
    /// </summary>
    public class Submodule : QueryableValue<Submodule>
    {
        internal Submodule(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The branch of the upstream submodule for tracking updates
        /// </summary>
        public string Branch { get; }

        /// <summary>
        /// The git URL of the submodule repository
        /// </summary>
        public string GitUrl { get; }

        /// <summary>
        /// The name of the submodule in .gitmodules
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The name of the submodule in .gitmodules (Base64-encoded)
        /// </summary>
        public string NameRaw { get; }

        /// <summary>
        /// The path in the superproject that this submodule is located in
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// The path in the superproject that this submodule is located in (Base64-encoded)
        /// </summary>
        public string PathRaw { get; }

        /// <summary>
        /// The commit revision of the subproject repository being tracked by the submodule
        /// </summary>
        public string SubprojectCommitOid { get; }

        internal static Submodule Create(Expression expression)
        {
            return new Submodule(expression);
        }
    }
}