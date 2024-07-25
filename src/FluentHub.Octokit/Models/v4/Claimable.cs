// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// An object which can have its data claimed or claim data from another.
	/// </summary>
	public class Claimable
	{
		/// <summary>
		/// A placeholder user for attribution of imported data on GitHub.
		/// </summary>
		public Mannequin Mannequin { get; set; }

		/// <summary>
		/// A user is an individual's account on GitHub that owns repositories and can make new content.
		/// </summary>
		public User User { get; set; }
	}
}
