// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Application.Navigation;

namespace FluentHub.Shell.Navigation;

internal static class NavigationRouteBuilder
{
	public static AppRoute WithSection(AppRoute current, NavigationPageKey key)
		=> current switch
		{
			UserRoute user => user with { Section = ToUserSection(key) },
			OrganizationRoute organization => organization with { Section = ToOrganizationSection(key) },
			_ when TryGetRepository(current, out var repository) => ToRepositoryRoute(repository, key),
			_ => current,
		};

	public static (NavigationPageKind Kind, NavigationPageKey Key) GetNavigationSelection(AppRoute route)
		=> route switch
		{
			UserRoute { AsViewer: true } => (NavigationPageKind.None, NavigationPageKey.None),
			UserRoute user => (NavigationPageKind.User, ToNavigationKey(user.Section)),
			OrganizationRoute organization => (NavigationPageKind.Organization, ToNavigationKey(organization.Section)),
			RepositoryCodeRoute or RepositoryCommitsRoute or RepositoryCommitRoute =>
				(NavigationPageKind.Repository, NavigationPageKey.Code),
			RepositoryIssueRoute => (NavigationPageKind.Repository, NavigationPageKey.Issues),
			RepositoryPullRequestRoute or RepositoryPullRequestCommitRoute =>
				(NavigationPageKind.Repository, NavigationPageKey.PullRequests),
			RepositoryDiscussionRoute => (NavigationPageKind.Repository, NavigationPageKey.Discussions),
			RepositoryRoute repository => (NavigationPageKind.Repository, ToNavigationKey(repository.Section)),
			RepositoryReleaseRoute => (NavigationPageKind.Repository, NavigationPageKey.None),
			_ => (NavigationPageKind.None, NavigationPageKey.None),
		};

	private static AppRoute ToRepositoryRoute(RepositorySlug repository, NavigationPageKey key)
		=> key switch
		{
			NavigationPageKey.Code => new RepositoryCodeRoute(repository),
			NavigationPageKey.Issues => new RepositoryRoute(repository, RepositorySection.Issues),
			NavigationPageKey.PullRequests => new RepositoryRoute(repository, RepositorySection.PullRequests),
			NavigationPageKey.Discussions => new RepositoryRoute(repository, RepositorySection.Discussions),
			NavigationPageKey.Projects => new RepositoryRoute(repository, RepositorySection.Projects),
			_ => new RepositoryCodeRoute(repository),
		};

	private static bool TryGetRepository(AppRoute route, out RepositorySlug repository)
	{
		repository = route switch
		{
			RepositoryRoute value => value.Repository,
			RepositoryCodeRoute value => value.Repository,
			RepositoryCommitsRoute value => value.Repository,
			RepositoryCommitRoute value => value.Repository,
			RepositoryIssueRoute value => value.Repository,
			RepositoryPullRequestRoute value => value.Repository,
			RepositoryPullRequestCommitRoute value => value.Repository,
			RepositoryDiscussionRoute value => value.Repository,
			RepositoryReleaseRoute value => value.Repository,
			_ => default,
		};

		return !string.IsNullOrWhiteSpace(repository.Owner);
	}

	private static UserSection ToUserSection(NavigationPageKey key)
		=> key switch
		{
			NavigationPageKey.Repositories => UserSection.Repositories,
			NavigationPageKey.Stars => UserSection.Stars,
			NavigationPageKey.Issues => UserSection.Issues,
			NavigationPageKey.PullRequests => UserSection.PullRequests,
			NavigationPageKey.Discussions => UserSection.Discussions,
			NavigationPageKey.Projects => UserSection.Projects,
			NavigationPageKey.Organizations => UserSection.Organizations,
			NavigationPageKey.Followers => UserSection.Followers,
			NavigationPageKey.Following => UserSection.Following,
			_ => UserSection.Overview,
		};

	private static OrganizationSection ToOrganizationSection(NavigationPageKey key)
		=> key == NavigationPageKey.Repositories
			? OrganizationSection.Repositories
			: OrganizationSection.Overview;

	private static NavigationPageKey ToNavigationKey(UserSection section)
		=> section switch
		{
			UserSection.Repositories => NavigationPageKey.Repositories,
			UserSection.Stars => NavigationPageKey.Stars,
			UserSection.Issues => NavigationPageKey.Issues,
			UserSection.PullRequests => NavigationPageKey.PullRequests,
			UserSection.Discussions => NavigationPageKey.Discussions,
			UserSection.Projects => NavigationPageKey.Projects,
			UserSection.Organizations => NavigationPageKey.Organizations,
			UserSection.Followers => NavigationPageKey.Followers,
			UserSection.Following => NavigationPageKey.Following,
			_ => NavigationPageKey.Overview,
		};

	private static NavigationPageKey ToNavigationKey(OrganizationSection section)
		=> section == OrganizationSection.Repositories
			? NavigationPageKey.Repositories
			: NavigationPageKey.Overview;

	private static NavigationPageKey ToNavigationKey(RepositorySection section)
		=> section switch
		{
			RepositorySection.Issues => NavigationPageKey.Issues,
			RepositorySection.PullRequests => NavigationPageKey.PullRequests,
			RepositorySection.Discussions => NavigationPageKey.Discussions,
			RepositorySection.Projects => NavigationPageKey.Projects,
			RepositorySection.Insights => NavigationPageKey.Insights,
			RepositorySection.Settings => NavigationPageKey.Settings,
			_ => NavigationPageKey.None,
		};
}
