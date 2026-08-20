// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Models
{
	public class TreeLayoutPageModel
	{
		public string Name { get; set; } = default!;

		public string Path { get; set; } = default!;

		public string Glyph { get; set; } = default!;

		public string Tag { get; set; } = default!;

		public bool IsBolb { get; set; }

		public ObservableCollection<TreeLayoutPageModel> Children { get; set; } = new();
	}
}
