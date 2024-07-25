// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Prevent commits that include changes in specified file paths from being pushed to the commit graph. NOTE: This rule is in beta and subject to change
	/// </summary>
	public class FilePathRestrictionParametersInput
	{
		/// <summary>
		/// The file paths that are restricted from being pushed to the commit graph.
		/// </summary>
		public List<string> RestrictedFilePaths { get; set; }
	}
}
