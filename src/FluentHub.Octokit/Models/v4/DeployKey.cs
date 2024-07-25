// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A repository deploy key.
	/// </summary>
	public class DeployKey
	{
		/// <summary>
		/// Identifies the date and time when the object was created.
		/// </summary>
		public DateTimeOffset CreatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was created."
		/// <summary>
		public string CreatedAtHumanized { get; set; }

		/// <summary>
		/// The Node ID of the DeployKey object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// The deploy key.
		/// </summary>
		public string Key { get; set; }

		/// <summary>
		/// Whether or not the deploy key is read only.
		/// </summary>
		public bool ReadOnly { get; set; }

		/// <summary>
		/// The deploy key title.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Whether or not the deploy key has been verified.
		/// </summary>
		public bool Verified { get; set; }
	}
}
