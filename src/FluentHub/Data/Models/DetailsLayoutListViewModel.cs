// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Models
{
	public class DetailsLayoutListViewModel
	{
		public string IconGlyph { get; set; } = default!;

		public string Name { get; set; } = default!;

		public string LatestCommitMessage { get; set; } = default!;

		public string Type { get; set; } = default!;

		public DateTimeOffset UpdatedAt { get; set; }

		public string UpdatedAtHumanized { get; set; } = default!;
	}
}
