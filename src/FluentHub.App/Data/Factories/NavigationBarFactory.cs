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
                    PageItemKey = NavigationBarItemKey.Overview,
                    PageKind = NavigationBarPageKind.Organization,
                },
                new()
                {
                    Text = "Repositories",
                    PageToNavigate = typeof(Views.Organizations.RepositoriesPage),
                    PageItemKey = NavigationBarItemKey.Repositories,
                    PageKind = NavigationBarPageKind.Organization,
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
                    PageItemKey = NavigationBarItemKey.Code,
                    PageKind = NavigationBarPageKind.Repository,
                },
                new()
                {
                    Text = "Issues",
                    PageToNavigate = typeof(Views.Repositories.Issues.IssuesPage),
                    PageItemKey = NavigationBarItemKey.Issues,
                    PageKind = NavigationBarPageKind.Repository,
                },
                new()
                {
                    Text = "Pull Requests",
                    PageToNavigate = typeof(Views.Repositories.PullRequests.PullRequestsPage),
                    PageItemKey = NavigationBarItemKey.PullRequests,
                    PageKind = NavigationBarPageKind.Repository,
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
                    PageItemKey = NavigationBarItemKey.Overview,
                    PageKind = NavigationBarPageKind.User,
                },
                new()
                {
                    Text = "Repositories",
                    PageToNavigate = typeof(Views.Users.RepositoriesPage),
                    PageItemKey = NavigationBarItemKey.Repositories,
                    PageKind = NavigationBarPageKind.User,
                },
                new()
                {
                    Text = "Stars",
                    PageToNavigate = typeof(Views.Users.StarredReposPage),
                    PageItemKey = NavigationBarItemKey.Stars,
                    PageKind = NavigationBarPageKind.User,
                },
            };
        }
    }
}
