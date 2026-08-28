// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Core.Application.Models
{
	public sealed class ProfileReadme
	{
		public string DefaultBranchName { get; set; } = string.Empty;
		public string Markdown { get; set; } = string.Empty;
		public string OwnerLogin { get; set; } = string.Empty;
		public string RepositoryName { get; set; } = string.Empty;
	}
}
