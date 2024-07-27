// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A Git push.
	/// </summary>
	public class Push
	{
		/// <summary>
		/// The Node ID of the Push object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// The SHA after the push
		/// </summary>
		public string NextSha { get; set; }

		/// <summary>
		/// The permalink for this push.
		/// </summary>
		public string Permalink { get; set; }

		/// <summary>
		/// The SHA before the push
		/// </summary>
		public string PreviousSha { get; set; }

		/// <summary>
		/// The actor who pushed
		/// </summary>
		public IActor Pusher { get; set; }

		/// <summary>
		/// The repository that was pushed to
		/// </summary>
		public Repository Repository { get; set; }
	}
}
