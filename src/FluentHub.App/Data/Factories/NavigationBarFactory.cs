// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.App.Data.Factories
{
	public static class NavigationBarFactory
	{
		public static IList<NavigationBarItem> GetOrganizationNavigationBarItems()
		{
			return new List<NavigationBarItem>()
			{
				new()
				{
					Text = "Overview",
					PageToNavigate = typeof(Views.Organizations.OverviewPage),
					PageItemKey = NavigationPageKey.Overview,
					PageKind = NavigationPageKind.Organization,
					Glyph = "\uE922",
				},
				new()
				{
					Text = "Repositories",
					PageToNavigate = typeof(Views.Organizations.RepositoriesPage),
					PageItemKey = NavigationPageKey.Repositories,
					PageKind = NavigationPageKind.Organization,
					Glyph = "\uEA52",
				},
			};
		}

		public static IList<NavigationBarItem> GetRepositoryNavigationBarItems()
		{
			return new List<NavigationBarItem>()
			{
				new()
				{
					Text = "Code",
					PageToNavigate = typeof(Views.Repositories.Code.DetailsLayoutView),
					PageItemKey = NavigationPageKey.Code,
					PageKind = NavigationPageKind.Repository,
					Glyph = "\uE922",
				},
				new()
				{
					Text = "Issues",
					PageToNavigate = typeof(Views.Repositories.Issues.IssuesPage),
					PageItemKey = NavigationPageKey.Issues,
					PageKind = NavigationPageKind.Repository,
					Glyph = "\uE9EA",
				},
				new()
				{
					Text = "Pull Requests",
					PageToNavigate = typeof(Views.Repositories.PullRequests.PullRequestsPage),
					PageItemKey = NavigationPageKey.PullRequests,
					PageKind = NavigationPageKind.Repository,
					Glyph = "\uE9BF",
				},
				new()
				{
					Text = "Discussions",
					PageToNavigate = typeof(Views.Repositories.Discussions.DiscussionsPage),
					PageItemKey = NavigationPageKey.Discussions,
					PageKind = NavigationPageKind.Repository,
					Glyph = "\uE95D",
				},
				new()
				{
					Text = "Projects",
					PageToNavigate = typeof(Views.Repositories.Projects.ProjectsPage),
					PageItemKey = NavigationPageKey.Projects,
					PageKind = NavigationPageKind.Repository,
					Glyph = "\uEAA3",
				},
			};
		}

		public static IList<NavigationBarItem> GetUserNavigationBarItems()
		{
			return new List<NavigationBarItem>()
			{
				new()
				{
					Text = "Overview",
					PageToNavigate = typeof(Views.Users.OverviewPage),
					PageItemKey = NavigationPageKey.Overview,
					PageKind = NavigationPageKind.User,
					Glyph = "\uE922",
				},
				new()
				{
					Text = "Repositories",
					PageToNavigate = typeof(Views.Users.RepositoriesPage),
					PageItemKey = NavigationPageKey.Repositories,
					PageKind = NavigationPageKind.User,
					Glyph = "\uEA52",
				},
				new()
				{
					Text = "Stars",
					PageToNavigate = typeof(Views.Users.StarsPage),
					PageItemKey = NavigationPageKey.Stars,
					PageKind = NavigationPageKind.User,
					Glyph = "\uEA94",
				},
				new()
				{
					Text = "Issues",
					PageToNavigate = typeof(Views.Users.IssuesPage),
					PageItemKey = NavigationPageKey.Issues,
					PageKind = NavigationPageKind.User,
					Glyph = "\uE9EA",
				},
				new()
				{
					Text = "Pull requests",
					PageToNavigate = typeof(Views.Users.PullRequestsPage),
					PageItemKey = NavigationPageKey.PullRequests,
					PageKind = NavigationPageKind.User,
					Glyph = "\uE9BF",
				},
				new()
				{
					Text = "Discussions",
					PageToNavigate = typeof(Views.Users.DiscussionsPage),
					PageItemKey = NavigationPageKey.Discussions,
					PageKind = NavigationPageKind.User,
					Glyph = "\uE95D",
				},
				new()
				{
					Text = "Projects",
					PageToNavigate = typeof(Views.Users.ProjectsPage),
					PageItemKey = NavigationPageKey.Projects,
					PageKind = NavigationPageKind.User,
					Glyph = "\uEAA3",
				},
				new()
				{
					Text = "Organizations",
					PageToNavigate = typeof(Views.Users.OrganizationsPage),
					PageItemKey = NavigationPageKey.Organizations,
					PageKind = NavigationPageKind.User,
					Glyph = "\uEA27",
				},
				new()
				{
					Text = "Followers",
					PageToNavigate = typeof(Views.Users.FollowersPage),
					PageItemKey = NavigationPageKey.Followers,
					PageKind = NavigationPageKind.User,
					Glyph = "\uEA36",
				},
				new()
				{
					Text = "Following",
					PageToNavigate = typeof(Views.Users.FollowingPage),
					PageItemKey = NavigationPageKey.Following,
					PageKind = NavigationPageKind.User,
					Glyph = "\uEA36",
				},
			};
		}
	}
}
