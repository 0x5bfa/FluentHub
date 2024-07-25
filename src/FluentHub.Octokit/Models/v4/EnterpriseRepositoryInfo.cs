// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A subset of repository information queryable from an enterprise.
	/// </summary>
	public class EnterpriseRepositoryInfo
	{
		/// <summary>
		/// The Node ID of the EnterpriseRepositoryInfo object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// Identifies if the repository is private or internal.
		/// </summary>
		public bool IsPrivate { get; set; }

		/// <summary>
		/// The repository's name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The repository's name with owner.
		/// </summary>
		public string NameWithOwner { get; set; }
	}
}
