// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A branch linked to an issue.
	/// </summary>
	public class LinkedBranch
	{
		/// <summary>
		/// The Node ID of the LinkedBranch object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// The branch's ref.
		/// </summary>
		public Ref Ref { get; set; }
	}
}
