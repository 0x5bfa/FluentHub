// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents a Git commit.
	/// </summary>
	public class Commit
	{
		/// <summary>
		/// An abbreviated version of the Git object ID
		/// </summary>
		public string AbbreviatedOid { get; set; }

		/// <summary>
		/// The number of additions in this commit.
		/// </summary>
		public int Additions { get; set; }

		/// <summary>
		/// The merged Pull Request that introduced the commit to the repository. If the commit is not present in the default branch, additionally returns open Pull Requests associated with the commit
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering options for pull requests.</param>
		public PullRequestConnection AssociatedPullRequests { get; set; }

		/// <summary>
		/// Authorship details of the commit.
		/// </summary>
		public GitActor Author { get; set; }

		/// <summary>
		/// Check if the committer and the author match.
		/// </summary>
		public bool AuthoredByCommitter { get; set; }

		/// <summary>
		/// The datetime when this commit was authored.
		/// </summary>
		public DateTimeOffset AuthoredDate { get; set; }

		/// <summary>
		/// Humanized string of "The datetime when this commit was authored."
		/// <summary>
		public string AuthoredDateHumanized { get; set; }

		/// <summary>
		/// The list of authors for this commit based on the git author and the Co-authored-by
		/// message trailer. The git author will always be first.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		public GitActorConnection Authors { get; set; }

		/// <summary>
		/// Fetches `git blame` information.
		/// </summary>
		/// <param name="path">The file whose Git blame information you want.</param>
		public Blame Blame { get; set; }

		/// <summary>
		/// We recommend using the `changedFilesIfAvailable` field instead of `changedFiles`, as `changedFiles` will cause your request to return an error if GitHub is unable to calculate the number of changed files.
		/// </summary>
		[Obsolete(@"`changedFiles` will be removed. Use `changedFilesIfAvailable` instead. Removal on 2023-01-01 UTC.")]
		public int ChangedFiles { get; set; }

		/// <summary>
		/// The number of changed files in this commit. If GitHub is unable to calculate the number of changed files (for example due to a timeout), this will return `null`. We recommend using this field instead of `changedFiles`.
		/// </summary>
		public int? ChangedFilesIfAvailable { get; set; }

		/// <summary>
		/// The check suites associated with a commit.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="filterBy">Filters the check suites by this type.</param>
		public CheckSuiteConnection CheckSuites { get; set; }

		/// <summary>
		/// Comments made on the commit.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		public CommitCommentConnection Comments { get; set; }

		/// <summary>
		/// The HTTP path for this Git object
		/// </summary>
		public string CommitResourcePath { get; set; }

		/// <summary>
		/// The HTTP URL for this Git object
		/// </summary>
		public string CommitUrl { get; set; }

		/// <summary>
		/// The datetime when this commit was committed.
		/// </summary>
		public DateTimeOffset CommittedDate { get; set; }

		/// <summary>
		/// Humanized string of "The datetime when this commit was committed."
		/// <summary>
		public string CommittedDateHumanized { get; set; }

		/// <summary>
		/// Check if committed via GitHub web UI.
		/// </summary>
		public bool CommittedViaWeb { get; set; }

		/// <summary>
		/// Committer details of the commit.
		/// </summary>
		public GitActor Committer { get; set; }

		/// <summary>
		/// The number of deletions in this commit.
		/// </summary>
		public int Deletions { get; set; }

		/// <summary>
		/// The deployments associated with a commit.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="environments">Environments to list deployments for</param>
		/// <param name="orderBy">Ordering options for deployments returned from the connection.</param>
		public DeploymentConnection Deployments { get; set; }

		/// <summary>
		/// The tree entry representing the file located at the given path.
		/// </summary>
		/// <param name="path">The path for the file</param>
		public TreeEntry File { get; set; }

		/// <summary>
		/// The linear commit history starting from (and including) this commit, in the same order as `git log`.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="author">If non-null, filters history to only show commits with matching authorship.</param>
		/// <param name="path">If non-null, filters history to only show commits touching files under this path.</param>
		/// <param name="since">Allows specifying a beginning time or date for fetching commits.</param>
		/// <param name="until">Allows specifying an ending time or date for fetching commits.</param>
		public CommitHistoryConnection History { get; set; }

		/// <summary>
		/// The Node ID of the Commit object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// The Git commit message
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// The Git commit message body
		/// </summary>
		public string MessageBody { get; set; }

		/// <summary>
		/// The commit message body rendered to HTML.
		/// </summary>
		public string MessageBodyHTML { get; set; }

		/// <summary>
		/// The Git commit message headline
		/// </summary>
		public string MessageHeadline { get; set; }

		/// <summary>
		/// The commit message headline rendered to HTML.
		/// </summary>
		public string MessageHeadlineHTML { get; set; }

		/// <summary>
		/// The Git object ID
		/// </summary>
		public string Oid { get; set; }

		/// <summary>
		/// The organization this commit was made on behalf of.
		/// </summary>
		public Organization OnBehalfOf { get; set; }

		/// <summary>
		/// The parents of a commit.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		public CommitConnection Parents { get; set; }

		/// <summary>
		/// The datetime when this commit was pushed.
		/// </summary>
		[Obsolete(@"`pushedDate` is no longer supported. Removal on 2023-07-01 UTC.")]
		public DateTimeOffset? PushedDate { get; set; }

		/// <summary>
		/// Humanized string of "The datetime when this commit was pushed."
		/// <summary>
		public string PushedDateHumanized { get; set; }

		/// <summary>
		/// The Repository this commit belongs to
		/// </summary>
		public Repository Repository { get; set; }

		/// <summary>
		/// The HTTP path for this commit
		/// </summary>
		public string ResourcePath { get; set; }

		/// <summary>
		/// Commit signing information, if present.
		/// </summary>
		public IGitSignature Signature { get; set; }

		/// <summary>
		/// Status information for this commit
		/// </summary>
		public Status Status { get; set; }

		/// <summary>
		/// Check and Status rollup information for this commit.
		/// </summary>
		public StatusCheckRollup StatusCheckRollup { get; set; }

		/// <summary>
		/// Returns a list of all submodules in this repository as of this Commit parsed from the .gitmodules file.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		public SubmoduleConnection Submodules { get; set; }

		/// <summary>
		/// Returns a URL to download a tarball archive for a repository.
		/// Note: For private repositories, these links are temporary and expire after five minutes.
		/// </summary>
		public string TarballUrl { get; set; }

		/// <summary>
		/// Commit's root Tree
		/// </summary>
		public Tree Tree { get; set; }

		/// <summary>
		/// The HTTP path for the tree of this commit
		/// </summary>
		public string TreeResourcePath { get; set; }

		/// <summary>
		/// The HTTP URL for the tree of this commit
		/// </summary>
		public string TreeUrl { get; set; }

		/// <summary>
		/// The HTTP URL for this commit
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		/// Check if the viewer is able to change their subscription status for the repository.
		/// </summary>
		public bool ViewerCanSubscribe { get; set; }

		/// <summary>
		/// Identifies if the viewer is watching, not watching, or ignoring the subscribable entity.
		/// </summary>
		public SubscriptionState? ViewerSubscription { get; set; }

		/// <summary>
		/// Returns a URL to download a zipball archive for a repository.
		/// Note: For private repositories, these links are temporary and expire after five minutes.
		/// </summary>
		public string ZipballUrl { get; set; }
	}
}
