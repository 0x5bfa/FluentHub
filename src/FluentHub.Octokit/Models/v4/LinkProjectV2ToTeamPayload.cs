// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Autogenerated return type of LinkProjectV2ToTeam
	/// </summary>
	public class LinkProjectV2ToTeamPayload
	{
		/// <summary>
		/// A unique identifier for the client performing the mutation.
		/// </summary>
		public string ClientMutationId { get; set; }

		/// <summary>
		/// The team the project is linked to
		/// </summary>
		public Team Team { get; set; }
	}
}
