// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Autogenerated return type of PinEnvironment
	/// </summary>
	public class PinEnvironmentPayload
	{
		/// <summary>
		/// A unique identifier for the client performing the mutation.
		/// </summary>
		public string ClientMutationId { get; set; }

		/// <summary>
		/// The environment that was pinned
		/// </summary>
		public Environment Environment { get; set; }

		/// <summary>
		/// The pinned environment if we pinned
		/// </summary>
		public PinnedEnvironment PinnedEnvironment { get; set; }
	}
}
