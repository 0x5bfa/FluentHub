// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{

	/// <summary>
	/// Represents a subject that can be reacted on.
	/// </summary>
	public interface IReactable
	{
		/// <summary>
		/// Identifies the primary key from the database.
		/// </summary>
		int? DatabaseId { get; set; }

		/// <summary>
		/// The Node ID of the Reactable object
		/// </summary>
		ID Id { get; set; }

		/// <summary>
		/// A list of reactions grouped by content left on the subject.
		/// </summary>
		List<ReactionGroup> ReactionGroups { get; set; }

		/// <summary>
		/// A list of Reactions left on the Issue.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="content">Allows filtering Reactions by emoji.</param>
		/// <param name="orderBy">Allows specifying the order in which reactions are returned.</param>
		ReactionConnection Reactions { get; set; }

		/// <summary>
		/// Can user react to this subject
		/// </summary>
		bool ViewerCanReact { get; set; }
	}
}

namespace FluentHub.Octokit.Models.v4
{
	public class Reactable : IReactable
	{
		public int? DatabaseId { get; set; }

		public ID Id { get; set; }

		public List<ReactionGroup> ReactionGroups { get; set; }

		public ReactionConnection Reactions { get; set; }

		public bool ViewerCanReact { get; set; }
	}
}

