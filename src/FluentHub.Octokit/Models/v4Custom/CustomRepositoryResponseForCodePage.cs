// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4Custom
{
	public class CustomRepositoryResponseForCodePage
	{
		public Repository Repository { get; set; }

		public int TagsTotalCount { get; set; }

		public int BranchesTotalCount { get; set; }
	}
}
