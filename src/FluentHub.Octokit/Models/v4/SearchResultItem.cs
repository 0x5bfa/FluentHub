// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The results of a search.
	/// </summary>
	public class SearchResultItem
	{
		/// <summary>
		/// A GitHub App.
		/// </summary>
		public App App { get; set; }

		/// <summary>
		/// A discussion in a repository.
		/// </summary>
		public Discussion Discussion { get; set; }

		/// <summary>
		/// An Issue is a place to discuss ideas, enhancements, tasks, and bugs for a project.
		/// </summary>
		public Issue Issue { get; set; }

		/// <summary>
		/// A listing in the GitHub integration marketplace.
		/// </summary>
		public MarketplaceListing MarketplaceListing { get; set; }

		/// <summary>
		/// An account on GitHub, with one or more owners, that has repositories, members and teams.
		/// </summary>
		public Organization Organization { get; set; }

		/// <summary>
		/// A repository pull request.
		/// </summary>
		public PullRequest PullRequest { get; set; }

		/// <summary>
		/// A repository contains the content for a project.
		/// </summary>
		public Repository Repository { get; set; }

		/// <summary>
		/// A user is an individual's account on GitHub that owns repositories and can make new content.
		/// </summary>
		public User User { get; set; }
	}
}
