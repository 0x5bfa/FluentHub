// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Application.Navigation;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.ViewModels;
using FluentHub.Views;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Services;

public sealed class AppRouteViewFactory
{
	private readonly IFluentHubGitHubClient _gitHub;

	public AppRouteViewFactory(IFluentHubGitHubClient gitHub)
		=> _gitHub = gitHub ?? throw new ArgumentNullException(nameof(gitHub));

	public UIElement Create(AppRoute route, Action<UIElement, AppRoute> navigate)
	{
		ArgumentNullException.ThrowIfNull(route);
		ArgumentNullException.ThrowIfNull(navigate);

		return route switch
		{
			RepositoryIssueRoute issue => new IssueView(
				_gitHub,
				issue,
				navigate),
			RepositoryPullRequestRoute pullRequest => new PullRequestView(
				_gitHub,
				pullRequest),
			RepositoryDiscussionRoute discussion => new DiscussionView(
				_gitHub,
				discussion,
				navigate),
			RepositoryCommitRoute commit => new ExternalRouteView(
				string.Format(
					CultureInfo.CurrentCulture,
					Strings.AppRouteViewFactory_CommitTitle.GetLocalized(),
					commit.Sha),
				$"https://github.com/{Uri.EscapeDataString(commit.Repository.Owner)}/{Uri.EscapeDataString(commit.Repository.Name)}/commit/{Uri.EscapeDataString(commit.Sha)}"),
			RepositoryReleaseRoute release => new ExternalRouteView(
				string.Format(
					CultureInfo.CurrentCulture,
					Strings.AppRouteViewFactory_ReleaseTitle.GetLocalized(),
					release.Tag),
				$"https://github.com/{Uri.EscapeDataString(release.Repository.Owner)}/{Uri.EscapeDataString(release.Repository.Name)}/releases/tag/{Uri.EscapeDataString(release.Tag)}"),
			RepositoryRoute repository when repository.Section == RepositorySection.Overview
				=> new RepositoryView(
					_gitHub,
					repository,
					navigate),
			RepositoryRoute repository when repository.Section == RepositorySection.Issues
				=> new RepositoryItemListView(
					_gitHub,
					repository,
					RepositoryItemListKind.Issues,
					navigate),
			RepositoryRoute repository when repository.Section == RepositorySection.PullRequests
				=> new RepositoryItemListView(
					_gitHub,
					repository,
					RepositoryItemListKind.PullRequests,
					navigate),
			RepositoryRoute repository when repository.Section == RepositorySection.Discussions
				=> new RepositoryItemListView(
					_gitHub,
					repository,
					RepositoryItemListKind.Discussions,
					navigate),
			RepositoryRoute repository when repository.Section == RepositorySection.Actions
				=> new ActionsView(new ActionsViewModel(repository)),
			UserRoute user when user.Section == UserSection.Overview
				=> new UserView(
					_gitHub,
					user,
					navigate),
			UserRoute user when user.Section == UserSection.Repositories
				=> new RepositoryListView(
					_gitHub,
					user.Login,
					RepositoryListOwnerKind.User,
					navigate),
			UserRoute user when user.Section == UserSection.Issues
				=> new RepositoryItemListView(
					_gitHub,
					user,
					RepositoryItemListKind.Issues,
					navigate),
			UserRoute user when user.Section == UserSection.PullRequests
				=> new RepositoryItemListView(
					_gitHub,
					user,
					RepositoryItemListKind.PullRequests,
					navigate),
			UserRoute user when user.Section == UserSection.Discussions
				=> new RepositoryItemListView(
					_gitHub,
					user,
					RepositoryItemListKind.Discussions,
					navigate),
			OrganizationRoute organization when organization.Section == OrganizationSection.Overview
				=> new OrganizationView(
					_gitHub,
					organization,
					navigate),
			OrganizationRoute organization when organization.Section == OrganizationSection.Repositories
				=> new RepositoryListView(
					_gitHub,
					organization.Login,
					RepositoryListOwnerKind.Organization,
					navigate),
			_ => new TextBlock
			{
				Text = route.GetType().Name.Replace("Route", string.Empty, StringComparison.Ordinal),
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center,
			},
		};
	}
}
