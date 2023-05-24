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
                    PageToNavigate = typeof(Views.Organizations.OverviewPage)
                },
                new()
                {
                    Text = "Repositories",
                    PageToNavigate = typeof(Views.Organizations.RepositoriesPage)
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
                    PageToNavigate = typeof(Views.Repositories.Code.Layouts.DetailsLayoutView)
                },
                new()
                {
                    Text = "Issues",
                    PageToNavigate = typeof(Views.Repositories.Issues.IssuesPage)
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
                    PageToNavigate = typeof(Views.Users.OverviewPage)
                },
                new()
                {
                    Text = "Repositories",
                    PageToNavigate = typeof(Views.Users.RepositoriesPage)
                },
                new()
                {
                    Text = "Stars",
                    PageToNavigate = typeof(Views.Users.StarredReposPage)
                },
            };
        }
    }
}
