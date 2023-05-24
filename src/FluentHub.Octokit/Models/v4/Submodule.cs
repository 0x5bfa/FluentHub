namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A pointer to a repository at a specific revision embedded inside another repository.
    /// </summary>
    public class Submodule
    {
        /// <summary>
        /// The branch of the upstream submodule for tracking updates
        /// </summary>
        public string Branch { get; set; }

        /// <summary>
        /// The git URL of the submodule repository
        /// </summary>
        public string GitUrl { get; set; }

        /// <summary>
        /// The name of the submodule in .gitmodules
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The name of the submodule in .gitmodules (Base64-encoded)
        /// </summary>
        public string NameRaw { get; set; }

        /// <summary>
        /// The path in the superproject that this submodule is located in
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// The path in the superproject that this submodule is located in (Base64-encoded)
        /// </summary>
        public string PathRaw { get; set; }

        /// <summary>
        /// The commit revision of the subproject repository being tracked by the submodule
        /// </summary>
        public string SubprojectCommitOid { get; set; }
    }
}