// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Prevent commits that exceed a specified file size limit from being pushed to the commit. NOTE: This rule is in beta and subject to change
	/// </summary>
	public class MaxFileSizeParameters
	{
		/// <summary>
		/// The maximum file size allowed in megabytes. This limit does not apply to Git Large File Storage (Git LFS).
		/// </summary>
		public int MaxFileSize { get; set; }
	}
}
