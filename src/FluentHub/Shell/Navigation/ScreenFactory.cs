// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Application.Navigation;
using Microsoft.UI.Xaml.Controls;
using System.Diagnostics.CodeAnalysis;

namespace FluentHub.Shell.Navigation;

/// <summary>
/// Explicit route registry. Adding a route requires an intentional screen mapping here.
/// </summary>
public sealed class ScreenFactory(
	IServiceProvider services,
	ICurrentRouteAccessor currentRouteAccessor) : IScreenFactory
{
	public async Task<ScreenInstance> CreateAsync(AppRoute route, CancellationToken cancellationToken)
	{
		var scope = services.CreateAsyncScope();

		try
		{
			using var routeScope = currentRouteAccessor.Push(route);
			using var serviceScope = ScreenView.PushServices(scope.ServiceProvider);
			var view = CreateView(route, scope.ServiceProvider);
			if (view is not IScreen screen)
				throw new InvalidOperationException($"The view for {route.GetType().Name} does not implement {nameof(IScreen)}.");

			var instance = new ScreenInstance(route, view, screen, scope);
			await screen.ActivateAsync(route, cancellationToken);
			return instance;
		}
		catch
		{
			await scope.DisposeAsync();
			throw;
		}
	}

	private static UserControl CreateView(AppRoute route, IServiceProvider services)
		=> route switch
		{
			SignInRoute => Create<global::FluentHub.Features.SignIn.Views.IntroPage>(services),
			DashboardRoute => Create<global::FluentHub.Features.Viewers.Views.DashBoardPage>(services),
			NotificationsRoute => Create<global::FluentHub.Features.Viewers.Views.NotificationsPage>(services),
			AppSettingsRoute => Create<global::FluentHub.Features.AppSettings.Views.GeneralPage>(services),
			UserRoute user => CreateUserView(user.Section, services),
			OrganizationRoute organization => CreateOrganizationView(organization.Section, services),
			RepositoryCodeRoute code when code.Layout == RepositoryCodeLayout.Tree =>
				Create<global::FluentHub.Features.Repositories.Views.Code.TreeLayoutView>(services),
			RepositoryCodeRoute => Create<global::FluentHub.Features.Repositories.Views.Code.DetailsLayoutView>(services),
			RepositoryCommitsRoute => Create<global::FluentHub.Features.Repositories.Views.Commits.CommitsPage>(services),
			RepositoryCommitRoute => Create<global::FluentHub.Features.Repositories.Views.Commits.CommitPage>(services),
			RepositoryIssueRoute => Create<global::FluentHub.Features.Repositories.Views.Issues.IssuePage>(services),
			RepositoryPullRequestRoute pullRequest => CreatePullRequestView(pullRequest.Section, services),
			RepositoryPullRequestCommitRoute => Create<global::FluentHub.Features.Repositories.Views.PullRequests.CommitPage>(services),
			RepositoryDiscussionRoute => Create<global::FluentHub.Features.Repositories.Views.Discussions.DiscussionPage>(services),
			RepositoryReleaseRoute => Create<global::FluentHub.Features.Repositories.Views.Releases.ReleasePage>(services),
			RepositoryRoute repository => CreateRepositoryView(repository.Section, services),
			SearchRoute search => CreateSearchView(search.Kind, services),
			_ => throw new ArgumentOutOfRangeException(nameof(route), route, "No screen is registered for this route."),
		};

	private static UserControl CreateUserView(UserSection section, IServiceProvider services)
		=> section switch
		{
			UserSection.Overview => Create<global::FluentHub.Features.Users.Views.OverviewPage>(services),
			UserSection.Contributions => Create<global::FluentHub.Features.Users.Views.ContributionsPage>(services),
			UserSection.Repositories => Create<global::FluentHub.Features.Users.Views.RepositoriesPage>(services),
			UserSection.Stars => Create<global::FluentHub.Features.Users.Views.StarsPage>(services),
			UserSection.Issues => Create<global::FluentHub.Features.Users.Views.IssuesPage>(services),
			UserSection.PullRequests => Create<global::FluentHub.Features.Users.Views.PullRequestsPage>(services),
			UserSection.Discussions => Create<global::FluentHub.Features.Users.Views.DiscussionsPage>(services),
			UserSection.Projects => Create<global::FluentHub.Features.Users.Views.ProjectsPage>(services),
			UserSection.Packages => Create<global::FluentHub.Features.Users.Views.PackagesPage>(services),
			UserSection.Organizations => Create<global::FluentHub.Features.Users.Views.OrganizationsPage>(services),
			UserSection.Followers => Create<global::FluentHub.Features.Users.Views.FollowersPage>(services),
			UserSection.Following => Create<global::FluentHub.Features.Users.Views.FollowingPage>(services),
			_ => throw new ArgumentOutOfRangeException(nameof(section), section, null),
		};

	private static UserControl CreateOrganizationView(OrganizationSection section, IServiceProvider services)
		=> section switch
		{
			OrganizationSection.Overview => Create<global::FluentHub.Features.Organizations.Views.OverviewPage>(services),
			OrganizationSection.Repositories => Create<global::FluentHub.Features.Organizations.Views.RepositoriesPage>(services),
			OrganizationSection.Discussions => Create<global::FluentHub.Features.Organizations.Views.DiscussionsPage>(services),
			OrganizationSection.Projects => Create<global::FluentHub.Features.Organizations.Views.ProjectsPage>(services),
			OrganizationSection.Packages => Create<global::FluentHub.Features.Organizations.Views.PackagesPage>(services),
			OrganizationSection.Membership => Create<global::FluentHub.Features.Organizations.Views.MembershipPage>(services),
			OrganizationSection.Settings => Create<global::FluentHub.Features.Organizations.Views.Settings.GeneralPage>(services),
			_ => throw new ArgumentOutOfRangeException(nameof(section), section, null),
		};

	private static UserControl CreateRepositoryView(RepositorySection section, IServiceProvider services)
		=> section switch
		{
			RepositorySection.Issues => Create<global::FluentHub.Features.Repositories.Views.Issues.IssuesPage>(services),
			RepositorySection.PullRequests => Create<global::FluentHub.Features.Repositories.Views.PullRequests.PullRequestsPage>(services),
			RepositorySection.Discussions => Create<global::FluentHub.Features.Repositories.Views.Discussions.DiscussionsPage>(services),
			RepositorySection.Projects => Create<global::FluentHub.Features.Repositories.Views.Projects.ProjectsPage>(services),
			RepositorySection.Releases => Create<global::FluentHub.Features.Repositories.Views.Releases.ReleasesPage>(services),
			RepositorySection.Insights => Create<global::FluentHub.Features.Repositories.Views.Insights.InsightsPage>(services),
			RepositorySection.Settings => Create<global::FluentHub.Features.Repositories.Views.Settings.SettingsPage>(services),
			_ => throw new ArgumentOutOfRangeException(nameof(section), section, null),
		};

	private static UserControl CreatePullRequestView(PullRequestSection section, IServiceProvider services)
		=> section switch
		{
			PullRequestSection.Conversation => Create<global::FluentHub.Features.Repositories.Views.PullRequests.ConversationPage>(services),
			PullRequestSection.Commits => Create<global::FluentHub.Features.Repositories.Views.PullRequests.CommitsPage>(services),
			PullRequestSection.Checks => Create<global::FluentHub.Features.Repositories.Views.PullRequests.ChecksPage>(services),
			PullRequestSection.Files => Create<global::FluentHub.Features.Repositories.Views.PullRequests.FileChangesPage>(services),
			_ => throw new ArgumentOutOfRangeException(nameof(section), section, null),
		};

	private static UserControl CreateSearchView(SearchKind kind, IServiceProvider services)
		=> kind switch
		{
			SearchKind.Code => Create<global::FluentHub.Features.Searches.Views.CodePage>(services),
			SearchKind.Issues => Create<global::FluentHub.Features.Searches.Views.IssuesPage>(services),
			SearchKind.Repositories => Create<global::FluentHub.Features.Searches.Views.RepositoriesPage>(services),
			SearchKind.Users => Create<global::FluentHub.Features.Searches.Views.UsersPage>(services),
			_ => throw new ArgumentOutOfRangeException(nameof(kind), kind, null),
		};

	private static TView Create<
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TView>(
		IServiceProvider services)
		where TView : UserControl
		=> ActivatorUtilities.CreateInstance<TView>(services);
}
