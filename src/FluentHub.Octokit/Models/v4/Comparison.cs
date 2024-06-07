// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Represents a comparison between two commit revisions.
	/// </summary>
	public class Comparison
	{
		/// <summary>
		/// The number of commits ahead of the base branch.
		/// </summary>
		public int AheadBy { get; set; }

		/// <summary>
		/// The base revision of this comparison.
		/// </summary>
		public IGitObject BaseTarget { get; set; }

		/// <summary>
		/// The number of commits behind the base branch.
		/// </summary>
		public int BehindBy { get; set; }

		/// <summary>
		/// The commits which compose this comparison.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		public ComparisonCommitConnection Commits { get; set; }

		/// <summary>
		/// The head revision of this comparison.
		/// </summary>
		public IGitObject HeadTarget { get; set; }

		/// <summary>
		/// The Node ID of the Comparison object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// The status of this comparison.
		/// </summary>
		public ComparisonStatus Status { get; set; }
	}
}
