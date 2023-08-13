// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A release contains the content for a release.
	/// </summary>
	public class Release
	{
		/// <summary>
		/// The author of the release
		/// </summary>
		public User Author { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was created.
		/// </summary>
		public DateTimeOffset CreatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was created."
		/// <summary>
		public string CreatedAtHumanized { get; set; }

		/// <summary>
		/// Identifies the primary key from the database.
		/// </summary>
		public int? DatabaseId { get; set; }

		/// <summary>
		/// The description of the release.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// The description of this release rendered to HTML.
		/// </summary>
		public string DescriptionHTML { get; set; }

		public ID Id { get; set; }

		/// <summary>
		/// Whether or not the release is a draft
		/// </summary>
		public bool IsDraft { get; set; }

		/// <summary>
		/// Whether or not the release is the latest releast
		/// </summary>
		public bool IsLatest { get; set; }

		/// <summary>
		/// Whether or not the release is a prerelease
		/// </summary>
		public bool IsPrerelease { get; set; }

		/// <summary>
		/// A list of users mentioned in the release description
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		public UserConnection Mentions { get; set; }

		/// <summary>
		/// The title of the release.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Identifies the date and time when the release was created.
		/// </summary>
		public DateTimeOffset? PublishedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the release was created."
		/// <summary>
		public string PublishedAtHumanized { get; set; }

		/// <summary>
		/// A list of reactions grouped by content left on the subject.
		/// </summary>
		public List<ReactionGroup> ReactionGroups { get; set; }

		/// <summary>
		/// A list of Reactions left on the Issue.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="content">Allows filtering Reactions by emoji.</param>
		/// <param name="orderBy">Allows specifying the order in which reactions are returned.</param>
		public ReactionConnection Reactions { get; set; }

		/// <summary>
		/// List of releases assets which are dependent on this release.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="name">A list of names to filter the assets by.</param>
		public ReleaseAssetConnection ReleaseAssets { get; set; }

		/// <summary>
		/// The repository that the release belongs to.
		/// </summary>
		public Repository Repository { get; set; }

		/// <summary>
		/// The HTTP path for this issue
		/// </summary>
		public string ResourcePath { get; set; }

		/// <summary>
		/// A description of the release, rendered to HTML without any links in it.
		/// </summary>
		/// <param name="limit">How many characters to return.</param>
		public string ShortDescriptionHTML { get; set; }

		/// <summary>
		/// The Git tag the release points to
		/// </summary>
		public Ref Tag { get; set; }

		/// <summary>
		/// The tag commit for this release.
		/// </summary>
		public Commit TagCommit { get; set; }

		/// <summary>
		/// The name of the release's Git tag
		/// </summary>
		public string TagName { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was last updated.
		/// </summary>
		public DateTimeOffset UpdatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was last updated."
		/// <summary>
		public string UpdatedAtHumanized { get; set; }

		/// <summary>
		/// The HTTP URL for this issue
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		/// Can user react to this subject
		/// </summary>
		public bool ViewerCanReact { get; set; }
	}
}
