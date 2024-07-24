// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A repository issue template.
	/// </summary>
	public class IssueTemplate
	{
		/// <summary>
		/// The template purpose.
		/// </summary>
		public string About { get; set; }

		/// <summary>
		/// The suggested assignees.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		public UserConnection Assignees { get; set; }

		/// <summary>
		/// The suggested issue body.
		/// </summary>
		public string Body { get; set; }

		/// <summary>
		/// The template filename.
		/// </summary>
		public string Filename { get; set; }

		/// <summary>
		/// The suggested issue labels
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering options for labels returned from the connection.</param>
		public LabelConnection Labels { get; set; }

		/// <summary>
		/// The template name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The suggested issue title.
		/// </summary>
		public string Title { get; set; }
	}
}
