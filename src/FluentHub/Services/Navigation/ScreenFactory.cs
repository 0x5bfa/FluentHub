// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Application.Navigation;
using Microsoft.UI.Xaml.Controls;
using System.Diagnostics.CodeAnalysis;

namespace FluentHub.Services.Navigation;

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
			SignInRoute => Create<global::FluentHub.Views.SignIn.IntroPage>(services),
			DashboardRoute => Create<global::FluentHub.Views.Viewers.DashBoardPage>(services),
			NotificationsRoute => Create<global::FluentHub.Views.Viewers.NotificationsPage>(services),
			AppSettingsRoute => Create<global::FluentHub.Views.AppSettings.GeneralPage>(services),
			UserRoute user => CreateUserView(user.Section, services),
			OrganizationRoute organization => CreateOrganizationView(organization.Section, services),
			RepositoryCodeRoute code when code.Layout == RepositoryCodeLayout.Tree =>
				Create<global::FluentHub.Views.Repositories.Code.TreeLayoutView>(services),
			RepositoryCodeRoute => Create<global::FluentHub.Views.Repositories.Code.DetailsLayoutView>(services),
			RepositoryCommitsRoute => Create<global::FluentHub.Views.Repositories.Commits.CommitsPage>(services),
			RepositoryCommitRoute => Create<global::FluentHub.Views.Repositories.Commits.CommitPage>(services),
			RepositoryIssueRoute => Create<global::FluentHub.Views.Repositories.Issues.IssuePage>(services),
			RepositoryPullRequestRoute pullRequest => CreatePullRequestView(pullRequest.Section, services),
			RepositoryPullRequestCommitRoute => Create<global::FluentHub.Views.Repositories.PullRequests.CommitPage>(services),
			RepositoryDiscussionRoute => Create<global::FluentHub.Views.Repositories.Discussions.DiscussionPage>(services),
			RepositoryReleaseRoute => Create<global::FluentHub.Views.Repositories.Releases.ReleasePage>(services),
			RepositoryRoute repository => CreateRepositoryView(repository.Section, services),
			SearchRoute search => CreateSearchView(search.Kind, services),
			_ => throw new ArgumentOutOfRangeException(nameof(route), route, "No screen is registered for this route."),
		};

	private static UserControl CreateUserView(UserSection section, IServiceProvider services)
		=> section switch
		{
			UserSection.Overview => Create<global::FluentHub.Views.Users.OverviewPage>(services),
			UserSection.Contributions => Create<global::FluentHub.Views.Users.ContributionsPage>(services),
			UserSection.Repositories => Create<global::FluentHub.Views.Users.RepositoriesPage>(services),
			UserSection.Stars => Create<global::FluentHub.Views.Users.StarsPage>(services),
			UserSection.Issues => Create<global::FluentHub.Views.Users.IssuesPage>(services),
			UserSection.PullRequests => Create<global::FluentHub.Views.Users.PullRequestsPage>(services),
			UserSection.Discussions => Create<global::FluentHub.Views.Users.DiscussionsPage>(services),
			UserSection.Projects => Create<global::FluentHub.Views.Users.ProjectsPage>(services),
			UserSection.Packages => Create<global::FluentHub.Views.Users.PackagesPage>(services),
			UserSection.Organizations => Create<global::FluentHub.Views.Users.OrganizationsPage>(services),
			UserSection.Followers => Create<global::FluentHub.Views.Users.FollowersPage>(services),
			UserSection.Following => Create<global::FluentHub.Views.Users.FollowingPage>(services),
			_ => throw new ArgumentOutOfRangeException(nameof(section), section, null),
		};

	private static UserControl CreateOrganizationView(OrganizationSection section, IServiceProvider services)
		=> section switch
		{
			OrganizationSection.Overview => Create<global::FluentHub.Views.Organizations.OverviewPage>(services),
			OrganizationSection.Repositories => Create<global::FluentHub.Views.Organizations.RepositoriesPage>(services),
			OrganizationSection.Discussions => Create<global::FluentHub.Views.Organizations.DiscussionsPage>(services),
			OrganizationSection.Projects => Create<global::FluentHub.Views.Organizations.ProjectsPage>(services),
			OrganizationSection.Packages => Create<global::FluentHub.Views.Organizations.PackagesPage>(services),
			OrganizationSection.Membership => Create<global::FluentHub.Views.Organizations.MembershipPage>(services),
			OrganizationSection.Settings => Create<global::FluentHub.Views.Organizations.Settings.GeneralPage>(services),
			_ => throw new ArgumentOutOfRangeException(nameof(section), section, null),
		};

	private static UserControl CreateRepositoryView(RepositorySection section, IServiceProvider services)
		=> section switch
		{
			RepositorySection.Issues => Create<global::FluentHub.Views.Repositories.Issues.IssuesPage>(services),
			RepositorySection.PullRequests => Create<global::FluentHub.Views.Repositories.PullRequests.PullRequestsPage>(services),
			RepositorySection.Discussions => Create<global::FluentHub.Views.Repositories.Discussions.DiscussionsPage>(services),
			RepositorySection.Projects => Create<global::FluentHub.Views.Repositories.Projects.ProjectsPage>(services),
			RepositorySection.Releases => Create<global::FluentHub.Views.Repositories.Releases.ReleasesPage>(services),
			RepositorySection.Insights => Create<global::FluentHub.Views.Repositories.Insights.InsightsPage>(services),
			RepositorySection.Settings => Create<global::FluentHub.Views.Repositories.Settings.SettingsPage>(services),
			_ => throw new ArgumentOutOfRangeException(nameof(section), section, null),
		};

	private static UserControl CreatePullRequestView(PullRequestSection section, IServiceProvider services)
		=> section switch
		{
			PullRequestSection.Conversation => Create<global::FluentHub.Views.Repositories.PullRequests.ConversationPage>(services),
			PullRequestSection.Commits => Create<global::FluentHub.Views.Repositories.PullRequests.CommitsPage>(services),
			PullRequestSection.Checks => Create<global::FluentHub.Views.Repositories.PullRequests.ChecksPage>(services),
			PullRequestSection.Files => Create<global::FluentHub.Views.Repositories.PullRequests.FileChangesPage>(services),
			_ => throw new ArgumentOutOfRangeException(nameof(section), section, null),
		};

	private static UserControl CreateSearchView(SearchKind kind, IServiceProvider services)
		=> kind switch
		{
			SearchKind.Code => Create<global::FluentHub.Views.Searches.CodePage>(services),
			SearchKind.Issues => Create<global::FluentHub.Views.Searches.IssuesPage>(services),
			SearchKind.Repositories => Create<global::FluentHub.Views.Searches.RepositoriesPage>(services),
			SearchKind.Users => Create<global::FluentHub.Views.Searches.UsersPage>(services),
			_ => throw new ArgumentOutOfRangeException(nameof(kind), kind, null),
		};

	private static TView Create<
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TView>(
		IServiceProvider services)
		where TView : UserControl
		=> ActivatorUtilities.CreateInstance<TView>(services);
}
