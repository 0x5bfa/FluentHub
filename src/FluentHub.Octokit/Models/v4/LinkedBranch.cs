// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A branch linked to an issue.
	/// </summary>
	public class LinkedBranch
	{
		public ID Id { get; set; }

		/// <summary>
		/// The branch's ref.
		/// </summary>
		public Ref Ref { get; set; }
	}
}
