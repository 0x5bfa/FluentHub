// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.App.Data.Items
{
	public class NavigationBarItem
	{
		public string? Text { get; set; }

		public string? Glyph { get; set; }

		public Type PageToNavigate { get; set; } = typeof(Views.Viewers.DashBoardPage);

		public NavigationPageKind PageKind { get; set; }

		public NavigationPageKey PageItemKey { get; set; }
	}
}
