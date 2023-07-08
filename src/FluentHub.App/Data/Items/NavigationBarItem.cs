// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.App.Data.Items
{
	public class NavigationBarItem
	{
		public string? Text{ get; set; }

		public string? Glyph { get; set; }

		public Type PageToNavigate { get; set; } = typeof(Views.Viewers.DashBoardPage);

		public NavigationBarPageKind PageKind { get; set; }

		public NavigationBarItemKey PageItemKey { get; set; }
	}
}
