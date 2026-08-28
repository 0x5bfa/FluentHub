// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Services.Navigation;

public static class NavigationBarFactory
{
	public static IReadOnlyList<NavigationBarItem> Create(NavigationPageKind kind)
		=> kind switch
		{
			NavigationPageKind.Organization =>
			[
				new("Overview", kind, NavigationPageKey.Overview),
				new("Repositories", kind, NavigationPageKey.Repositories),
			],
			NavigationPageKind.Repository =>
			[
				new("Code", kind, NavigationPageKey.Code),
				new("Issues", kind, NavigationPageKey.Issues),
				new("Pull Requests", kind, NavigationPageKey.PullRequests),
				new("Discussions", kind, NavigationPageKey.Discussions),
				new("Projects", kind, NavigationPageKey.Projects),
			],
			NavigationPageKind.User =>
			[
				new("Overview", kind, NavigationPageKey.Overview),
				new("Repositories", kind, NavigationPageKey.Repositories),
				new("Stars", kind, NavigationPageKey.Stars),
				new("Issues", kind, NavigationPageKey.Issues),
				new("Pull requests", kind, NavigationPageKey.PullRequests),
				new("Discussions", kind, NavigationPageKey.Discussions),
				new("Projects", kind, NavigationPageKey.Projects),
				new("Organizations", kind, NavigationPageKey.Organizations),
				new("Followers", kind, NavigationPageKey.Followers),
				new("Following", kind, NavigationPageKey.Following),
			],
			_ => [],
		};
}
