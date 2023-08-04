// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.Data.Items;

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
			};
		}
	}
}
