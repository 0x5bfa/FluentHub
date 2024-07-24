// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Information extracted from a repository's `CODEOWNERS` file.
	/// </summary>
	public class RepositoryCodeowners
	{
		/// <summary>
		/// Any problems that were encountered while parsing the `CODEOWNERS` file.
		/// </summary>
		public List<RepositoryCodeownersError> Errors { get; set; }
	}
}
