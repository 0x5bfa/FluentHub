namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A git ref for a commit to be appended to.
    /// The ref must be a branch, i.e. its fully qualified name must start
    /// with `refs/heads/` (although the input is not required to be fully
    /// qualified).
    /// The Ref may be specified by its global node ID or by the
    /// `repositoryNameWithOwner` and `branchName`.
    /// ### Examples
    /// Specify a branch using a global node ID:
    ///     { "id": "MDM6UmVmMTpyZWZzL2hlYWRzL21haW4=" }
    /// Specify a branch using `repositoryNameWithOwner` and `branchName`:
    ///     {
    ///       "repositoryNameWithOwner": "github/graphql-client",
    ///       "branchName": "main"
    ///     }
    /// </summary>
    public class CommittableBranch
    {
        /// <summary>
        /// The Node ID of the Ref to be updated.
        /// </summary>
        public ID? Id { get; set; }

        /// <summary>
        /// The nameWithOwner of the repository to commit to.
        /// </summary>
        public string RepositoryNameWithOwner { get; set; }

        /// <summary>
        /// The unqualified name of the branch to append the commit to.
        /// </summary>
        public string BranchName { get; set; }
    }
}