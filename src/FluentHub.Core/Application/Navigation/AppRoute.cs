// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Core.Application.Navigation;

/// <summary>
/// Identifies a destination without referencing a UI framework type or an API response object.
/// </summary>
public abstract record AppRoute;

public sealed record SignInRoute : AppRoute;

public sealed record DashboardRoute : AppRoute;

public sealed record NotificationsRoute : AppRoute;

public sealed record AppSettingsRoute(AppSettingsSection Section = AppSettingsSection.General) : AppRoute;

public sealed record UserRoute(
	string Login,
	UserSection Section = UserSection.Overview,
	bool AsViewer = false) : AppRoute;

public sealed record OrganizationRoute(
	string Login,
	OrganizationSection Section = OrganizationSection.Overview) : AppRoute;

public sealed record RepositoryRoute(
	RepositorySlug Repository,
	RepositorySection Section) : AppRoute;

public sealed record RepositoryCodeRoute(
	RepositorySlug Repository,
	string? GitRef = null,
	string? Path = null,
	RepositoryCodeLayout Layout = RepositoryCodeLayout.Details,
	RepositoryCodeTarget Target = RepositoryCodeTarget.Directory) : AppRoute;

public sealed record RepositoryCommitsRoute(
	RepositorySlug Repository,
	string? GitRef = null,
	string? Path = null) : AppRoute;

public sealed record RepositoryCommitRoute(
	RepositorySlug Repository,
	string Sha) : AppRoute;

public sealed record RepositoryIssueRoute(
	RepositorySlug Repository,
	int Number) : AppRoute;

public sealed record RepositoryPullRequestRoute(
	RepositorySlug Repository,
	int Number,
	PullRequestSection Section = PullRequestSection.Conversation) : AppRoute;

public sealed record RepositoryPullRequestCommitRoute(
	RepositorySlug Repository,
	int PullRequestNumber,
	string Sha) : AppRoute;

public sealed record RepositoryDiscussionRoute(
	RepositorySlug Repository,
	int Number) : AppRoute;

public sealed record RepositoryReleaseRoute(
	RepositorySlug Repository,
	string Tag) : AppRoute;

public sealed record SearchRoute(
	SearchKind Kind,
	string Query) : AppRoute;

public readonly record struct RepositorySlug
{
	public RepositorySlug(string owner, string name)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(owner);
		ArgumentException.ThrowIfNullOrWhiteSpace(name);

		Owner = owner;
		Name = name;
	}

	public string Owner { get; }

	public string Name { get; }

	public override string ToString()
		=> $"{Owner}/{Name}";
}

public enum AppSettingsSection
{
	General,
}

public enum UserSection
{
	Overview,
	Contributions,
	Repositories,
	Stars,
	Issues,
	PullRequests,
	Discussions,
	Projects,
	Packages,
	Organizations,
	Followers,
	Following,
}

public enum OrganizationSection
{
	Overview,
	Repositories,
	Discussions,
	Projects,
	Packages,
	Membership,
	Settings,
}

public enum RepositorySection
{
	Issues,
	PullRequests,
	Discussions,
	Projects,
	Releases,
	Insights,
	Settings,
}

public enum RepositoryCodeLayout
{
	Details,
	Tree,
}

public enum RepositoryCodeTarget
{
	Directory,
	File,
}

public enum PullRequestSection
{
	Conversation,
	Commits,
	Checks,
	Files,
}

public enum SearchKind
{
	Code,
	Issues,
	Repositories,
	Users,
}
