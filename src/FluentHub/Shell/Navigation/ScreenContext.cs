// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Application.Navigation;

namespace FluentHub.Shell.Navigation;

public sealed record ScreenContext(
	AppRoute Route,
	string? PrimaryText,
	string? SecondaryText,
	int Number,
	bool AsViewer)
{
	public static ScreenContext FromRoute(AppRoute route)
		=> route switch
		{
			UserRoute user => new(route, user.Login, null, 0, user.AsViewer),
			OrganizationRoute organization => new(route, organization.Login, null, 0, false),
			RepositoryRoute repository => ForRepository(route, repository.Repository),
			RepositoryCodeRoute code => ForRepository(route, code.Repository),
			RepositoryCommitsRoute commits => ForRepository(route, commits.Repository),
			RepositoryCommitRoute commit => ForRepository(route, commit.Repository),
			RepositoryIssueRoute issue => ForRepository(route, issue.Repository, issue.Number),
			RepositoryPullRequestRoute pullRequest => ForRepository(route, pullRequest.Repository, pullRequest.Number),
			RepositoryPullRequestCommitRoute commit => ForRepository(route, commit.Repository, commit.PullRequestNumber),
			RepositoryDiscussionRoute discussion => ForRepository(route, discussion.Repository, discussion.Number),
			RepositoryReleaseRoute release => ForRepository(route, release.Repository),
			_ => new(route, null, null, 0, false),
		};

	private static ScreenContext ForRepository(AppRoute route, RepositorySlug repository, int number = 0)
		=> new(route, repository.Owner, repository.Name, number, false);
}
