// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Prevent commits that include file paths that exceed a specified character limit from being pushed to the commit graph. NOTE: This rule is in beta and subject to change
	/// </summary>
	public class MaxFilePathLengthParametersInput
	{
		/// <summary>
		/// The maximum amount of characters allowed in file paths
		/// </summary>
		public int MaxFilePathLength { get; set; }
	}
}
