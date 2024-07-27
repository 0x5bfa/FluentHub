// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// An object that is a member of an enterprise.
	/// </summary>
	public class EnterpriseMember
	{
		/// <summary>
		/// An account for a user who is an admin of an enterprise or a member of an enterprise through one or more organizations.
		/// </summary>
		public EnterpriseUserAccount EnterpriseUserAccount { get; set; }

		/// <summary>
		/// A user is an individual's account on GitHub that owns repositories and can make new content.
		/// </summary>
		public User User { get; set; }
	}
}
