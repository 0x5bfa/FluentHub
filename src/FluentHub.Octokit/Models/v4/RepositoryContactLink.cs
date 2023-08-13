// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A repository contact link.
	/// </summary>
	public class RepositoryContactLink
	{
		/// <summary>
		/// The contact link purpose.
		/// </summary>
		public string About { get; set; }

		/// <summary>
		/// The contact link name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The contact link URL.
		/// </summary>
		public string Url { get; set; }
	}
}
