// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Core.Application.Models
{
	public sealed class CommitChanges
	{
		public IReadOnlyList<FileChange> Files { get; init; } = [];
	}

	public sealed class FileChange
	{
		public int Additions { get; init; }

		public int Changes { get; init; }

		public int Deletions { get; init; }

		public string BlobUrl { get; init; } = string.Empty;

		public string ContentsUrl { get; init; } = string.Empty;

		public string Filename { get; init; } = string.Empty;

		public string Patch { get; init; } = string.Empty;

		public string? PreviousFileName { get; init; }

		public string RawUrl { get; init; } = string.Empty;

		public string Sha { get; init; } = string.Empty;

		public string Status { get; init; } = string.Empty;
	}
}
