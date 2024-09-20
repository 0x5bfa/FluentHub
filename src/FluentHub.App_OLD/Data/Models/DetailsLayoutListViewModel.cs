// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.App.Models
{
	public class DetailsLayoutListViewModel
	{
		public string IconGlyph { get; set; }

		public string Name { get; set; }

		public string LatestCommitMessage { get; set; }

		public string Type { get; set; }

		public DateTimeOffset UpdatedAt { get; set; }

		public string UpdatedAtHumanized { get; set; }
	}
}
