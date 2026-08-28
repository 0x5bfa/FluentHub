// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Application;
using FluentHub.Core.Application.Abstractions.Caching;
using FluentHub.Core.DependencyInjection;
using FluentHub.Core.Infrastructure.Caching;
using FluentHub.Services;
using FluentHub.Utils;
using Windows.Storage;
using System.IO;

namespace FluentHub.Extensions;

internal static class ServiceCollectionExtensions
{
	public static IServiceCollection AddFluentHub(this IServiceCollection services)
		=> services
			.AddShell()
			.AddPlatformServices()
			.AddCoreServices()
			.AddViewModels();

	private static IServiceCollection AddShell(this IServiceCollection services)
		=> services
			.AddSingleton<ICurrentRouteAccessor, CurrentRouteAccessor>()
			.AddSingleton<IScreenFactory, ScreenFactory>()
			.AddSingleton<INavigationService, NavigationService>()
			.AddSingleton<ScreenViewModelDependencies>()
			.AddSingleton<IMessenger>(StrongReferenceMessenger.Default);

	private static IServiceCollection AddPlatformServices(this IServiceCollection services)
		=> services
			.AddSingleton<Utils.ILogger>(_ => new FileLogger(
				Path.Combine(ApplicationData.Current.LocalFolder.Path, "FluentHub.Logs", "Log.log")))
			.AddSingleton<ToastService>()
			.AddSingleton<GitHubTokenStore>()
			.AddSingleton<ICacheService>(_ => new FileCacheService(
				Path.Combine(ApplicationData.Current.LocalCacheFolder.Path, "FluentHub.Cache", "v1")));

	private static IServiceCollection AddCoreServices(this IServiceCollection services)
		=> services
			.AddSingleton<IAccountStore>(_ => App.AppSettings)
			.AddFluentHubCore();

	private static IServiceCollection AddViewModels(this IServiceCollection services)
		=> services
			.AddSingleton<ViewModels.MainPageViewModel>()
			.AddTransient<ViewModels.SignIn.IntroViewModel>()
			.AddTransient<ViewModels.AppSettings.GeneralViewModel>()
			.AddTransient<ViewModels.Dialogs.AccountSwitchingDialogViewModel>()
			.AddTransient<ViewModels.Dialogs.EditPinnedRepositoriesDialogViewModel>()
			.AddTransient<ViewModels.Dialogs.EditUserProfileViewModel>()
			.AddTransient<ViewModels.Viewers.DashBoardViewModel>()
			.AddTransient<ViewModels.Viewers.NotificationsViewModel>()
			.AddTransient<ViewModels.Organizations.OverviewViewModel>()
			.AddTransient<ViewModels.Organizations.RepositoriesViewModel>()
			.AddTransient<ViewModels.Repositories.Codes.DetailsLayoutViewModel>()
			.AddTransient<ViewModels.Repositories.Codes.TreeLayoutViewModel>()
			.AddTransient<ViewModels.Repositories.Commits.CommitsViewModel>()
			.AddTransient<ViewModels.Repositories.Commits.CommitViewModel>()
			.AddTransient<ViewModels.Repositories.Discussions.DiscussionsViewModel>()
			.AddTransient<ViewModels.Repositories.Discussions.DiscussionViewModel>()
			.AddTransient<ViewModels.Repositories.Issues.IssueViewModel>()
			.AddTransient<ViewModels.Repositories.Issues.IssuesViewModel>()
			.AddTransient<ViewModels.Repositories.Projects.ProjectsViewModel>()
			.AddTransient<ViewModels.Repositories.PullRequests.ChecksViewModel>()
			.AddTransient<ViewModels.Repositories.PullRequests.ConversationViewModel>()
			.AddTransient<ViewModels.Repositories.PullRequests.CommitViewModel>()
			.AddTransient<ViewModels.Repositories.PullRequests.CommitsViewModel>()
			.AddTransient<ViewModels.Repositories.PullRequests.FileChangesViewModel>()
			.AddTransient<ViewModels.Repositories.PullRequests.PullRequestsViewModel>()
			.AddTransient<ViewModels.Repositories.Releases.ReleasesViewModel>()
			.AddTransient<ViewModels.Repositories.Releases.ReleaseViewModel>()
			.AddTransient<ViewModels.Searches.CodeViewModel>()
			.AddTransient<ViewModels.Searches.IssuesViewModel>()
			.AddTransient<ViewModels.Searches.RepositoriesViewModel>()
			.AddTransient<ViewModels.Searches.UsersViewModel>()
			.AddTransient<ViewModels.Controls.FileContentBlockViewModel>()
			.AddTransient<ViewModels.Controls.FileNavigationBlockViewModel>()
			.AddTransient<ViewModels.Controls.IssueCommentBlockViewModel>()
			.AddTransient<ViewModels.Controls.LatestCommitBlockViewModel>()
			.AddTransient<ViewModels.Controls.UserContributionGraphViewModel>()
			.AddTransient<ViewModels.Users.ContributionsViewModel>()
			.AddTransient<ViewModels.Users.DiscussionsViewModel>()
			.AddTransient<ViewModels.Users.FollowersViewModel>()
			.AddTransient<ViewModels.Users.FollowingViewModel>()
			.AddTransient<ViewModels.Users.IssuesViewModel>()
			.AddTransient<ViewModels.Users.OrganizationsViewModel>()
			.AddTransient<ViewModels.Users.OverviewViewModel>()
			.AddTransient<ViewModels.Users.PackagesViewModel>()
			.AddTransient<ViewModels.Users.ProjectsViewModel>()
			.AddTransient<ViewModels.Users.PullRequestsViewModel>()
			.AddTransient<ViewModels.Users.RepositoriesViewModel>()
			.AddTransient<ViewModels.Users.StarredReposViewModel>();
}
