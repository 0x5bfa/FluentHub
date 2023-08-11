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
				},
				new()
				{
					Text = "Repositories",
					PageToNavigate = typeof(Views.Organizations.RepositoriesPage),
					PageItemKey = NavigationPageKey.Repositories,
					PageKind = NavigationPageKind.Organization,
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
				},
				new()
				{
					Text = "Issues",
					PageToNavigate = typeof(Views.Repositories.Issues.IssuesPage),
					PageItemKey = NavigationPageKey.Issues,
					PageKind = NavigationPageKind.Repository,
				},
				new()
				{
					Text = "Pull Requests",
					PageToNavigate = typeof(Views.Repositories.PullRequests.PullRequestsPage),
					PageItemKey = NavigationPageKey.PullRequests,
					PageKind = NavigationPageKind.Repository,
				},
				new()
				{
					Text = "Discussions",
					PageToNavigate = typeof(Views.Repositories.Discussions.DiscussionsPage),
					PageItemKey = NavigationPageKey.Discussions,
					PageKind = NavigationPageKind.Repository,
				},
				new()
				{
					Text = "Projects",
					PageToNavigate = typeof(Views.Repositories.Projects.ProjectsPage),
					PageItemKey = NavigationPageKey.Projects,
					PageKind = NavigationPageKind.Repository,
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
				},
				new()
				{
					Text = "Repositories",
					PageToNavigate = typeof(Views.Users.RepositoriesPage),
					PageItemKey = NavigationPageKey.Repositories,
					PageKind = NavigationPageKind.User,
				},
				new()
				{
					Text = "Stars",
					PageToNavigate = typeof(Views.Users.StarsPage),
					PageItemKey = NavigationPageKey.Stars,
					PageKind = NavigationPageKind.User,
				},
				new()
				{
					Text = "Issues",
					PageToNavigate = typeof(Views.Users.IssuesPage),
					PageItemKey = NavigationPageKey.Issues,
					PageKind = NavigationPageKind.User,
				},
				new()
				{
					Text = "Pull requests",
					PageToNavigate = typeof(Views.Users.PullRequestsPage),
					PageItemKey = NavigationPageKey.PullRequests,
					PageKind = NavigationPageKind.User,
				},
				new()
				{
					Text = "Discussions",
					PageToNavigate = typeof(Views.Users.DiscussionsPage),
					PageItemKey = NavigationPageKey.Discussions,
					PageKind = NavigationPageKind.User,
				},
				new()
				{
					Text = "Projects",
					PageToNavigate = typeof(Views.Users.ProjectsPage),
					PageItemKey = NavigationPageKey.Projects,
					PageKind = NavigationPageKind.User,
				},
				new()
				{
					Text = "Organizations",
					PageToNavigate = typeof(Views.Users.OrganizationsPage),
					PageItemKey = NavigationPageKey.Organizations,
					PageKind = NavigationPageKind.User,
				},
				new()
				{
					Text = "Followers",
					PageToNavigate = typeof(Views.Users.FollowersPage),
					PageItemKey = NavigationPageKey.Followers,
					PageKind = NavigationPageKind.User,
				},
				new()
				{
					Text = "Following",
					PageToNavigate = typeof(Views.Users.FollowingPage),
					PageItemKey = NavigationPageKey.Following,
					PageKind = NavigationPageKind.User,
				},
			};
		}
	}
}
