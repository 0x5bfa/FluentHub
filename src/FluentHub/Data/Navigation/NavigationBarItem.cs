// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Data.Navigation;

public sealed class NavigationBarItem
{
	public NavigationBarItem()
	{
	}

	public NavigationBarItem(string text, NavigationPageKind pageKind, NavigationPageKey pageItemKey)
	{
		Text = text;
		PageKind = pageKind;
		PageItemKey = pageItemKey;
	}

	public string Text { get; set; } = string.Empty;

	public NavigationPageKind PageKind { get; set; }

	public NavigationPageKey PageItemKey { get; set; }
}
