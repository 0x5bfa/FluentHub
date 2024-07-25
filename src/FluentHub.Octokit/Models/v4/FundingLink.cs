// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A funding platform link for a repository.
	/// </summary>
	public class FundingLink
	{
		/// <summary>
		/// The funding platform this link is for.
		/// </summary>
		public FundingPlatform Platform { get; set; }

		/// <summary>
		/// The configured URL for this funding link.
		/// </summary>
		public string Url { get; set; }
	}
}
