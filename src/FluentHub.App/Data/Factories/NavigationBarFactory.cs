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
                    PageItemKey = Core.Data.Enums.NavigationBarItemKey.Overview,
                },
                new()
                {
                    Text = "Repositories",
                    PageToNavigate = typeof(Views.Organizations.RepositoriesPage),
                    PageItemKey = Core.Data.Enums.NavigationBarItemKey.Repositories,
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
                    PageToNavigate = typeof(Views.Repositories.Code.Layouts.DetailsLayoutView),
                    PageItemKey = Core.Data.Enums.NavigationBarItemKey.Code,
                },
                new()
                {
                    Text = "Issues",
                    PageToNavigate = typeof(Views.Repositories.Issues.IssuesPage),
                    PageItemKey = Core.Data.Enums.NavigationBarItemKey.Issues,
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
                    PageItemKey = Core.Data.Enums.NavigationBarItemKey.Overview,
                },
                new()
                {
                    Text = "Repositories",
                    PageToNavigate = typeof(Views.Users.RepositoriesPage),
                    PageItemKey = Core.Data.Enums.NavigationBarItemKey.Repositories,
                },
                new()
                {
                    Text = "Stars",
                    PageToNavigate = typeof(Views.Users.StarredReposPage),
                    PageItemKey = Core.Data.Enums.NavigationBarItemKey.Stars,
                },
            };
        }
    }
}
