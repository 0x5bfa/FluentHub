// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Models
{
	public class CustomThemeItem
	{
		public string Name { get; set; } = default!;

		public string Path { get; set; } = default!;

		public string AbsolutePath { get; set; } = default!;

		public string Key { get => Name; }

		public bool IsImportedTheme { get; set; }
	}
}
