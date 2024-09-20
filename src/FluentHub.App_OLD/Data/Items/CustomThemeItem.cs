// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.App.Models
{
	public class CustomThemeItem
	{
		public string Name { get; set; }

		public string Path { get; set; }

		public string AbsolutePath { get; set; }

		public string Key { get => Name; }

		public bool IsImportedTheme { get; set; }
	}
}
