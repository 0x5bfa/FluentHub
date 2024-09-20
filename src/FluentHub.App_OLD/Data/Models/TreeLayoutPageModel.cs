// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.App.Models
{
	public class TreeLayoutPageModel
	{
		public string Name { get; set; }

		public string Path { get; set; }

		public string Glyph { get; set; }

		public string Tag { get; set; }

		public bool IsBolb { get; set; }

		public ObservableCollection<TreeLayoutPageModel> Children { get; set; } = new();
	}
}
