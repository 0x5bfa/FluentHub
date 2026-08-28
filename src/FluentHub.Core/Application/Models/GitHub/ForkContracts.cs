// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Core.Application.Models
{
	public sealed class ForkOwner
	{
		public string AvatarUrl { get; set; } = string.Empty;
		public bool IsOrganization { get; set; }
		public string Login { get; set; } = string.Empty;
	}

	public sealed class CreateForkRequest
	{
		public bool DefaultBranchOnly { get; set; }
		public string? Description { get; set; }
		public ForkOwner DestinationOwner { get; set; } = default!;
		public string RepositoryName { get; set; } = string.Empty;
		public string SourceName { get; set; } = string.Empty;
		public string SourceOwner { get; set; } = string.Empty;
	}

	public sealed class CreateForkResult
	{
		public string FullName { get; set; } = string.Empty;
		public string Name { get; set; } = string.Empty;
		public string Owner { get; set; } = string.Empty;
	}
}
