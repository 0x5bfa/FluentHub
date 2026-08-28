// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Application;
using FluentHub.Core.Application.Abstractions.Caching;
using FluentHub.Core.DependencyInjection;
using FluentHub.Core.Infrastructure.Caching;
using FluentHub.Services;
using FluentHub.Utils;
using Windows.Storage;
using System.IO;

namespace FluentHub.Bootstrap.DependencyInjection;

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
			.AddSingleton<Shell.ViewModels.MainPageViewModel>()
			.AddTransient<Features.SignIn.ViewModels.IntroViewModel>()
			.AddTransient<Features.AppSettings.ViewModels.GeneralViewModel>()
			.AddTransient<Shared.Dialogs.ViewModels.AccountSwitchingDialogViewModel>()
			.AddTransient<Shared.Dialogs.ViewModels.EditPinnedRepositoriesDialogViewModel>()
			.AddTransient<Shared.Dialogs.ViewModels.EditUserProfileViewModel>()
			.AddTransient<Features.Viewers.ViewModels.DashBoardViewModel>()
			.AddTransient<Features.Viewers.ViewModels.NotificationsViewModel>()
			.AddTransient<Features.Organizations.ViewModels.OverviewViewModel>()
			.AddTransient<Features.Organizations.ViewModels.RepositoriesViewModel>()
			.AddTransient<Features.Repositories.ViewModels.Codes.DetailsLayoutViewModel>()
			.AddTransient<Features.Repositories.ViewModels.Codes.TreeLayoutViewModel>()
			.AddTransient<Features.Repositories.ViewModels.Commits.CommitsViewModel>()
			.AddTransient<Features.Repositories.ViewModels.Commits.CommitViewModel>()
			.AddTransient<Features.Repositories.ViewModels.Discussions.DiscussionsViewModel>()
			.AddTransient<Features.Repositories.ViewModels.Discussions.DiscussionViewModel>()
			.AddTransient<Features.Repositories.ViewModels.Issues.IssueViewModel>()
			.AddTransient<Features.Repositories.ViewModels.Issues.IssuesViewModel>()
			.AddTransient<Features.Repositories.ViewModels.Projects.ProjectsViewModel>()
			.AddTransient<Features.Repositories.ViewModels.PullRequests.ChecksViewModel>()
			.AddTransient<Features.Repositories.ViewModels.PullRequests.ConversationViewModel>()
			.AddTransient<Features.Repositories.ViewModels.PullRequests.CommitViewModel>()
			.AddTransient<Features.Repositories.ViewModels.PullRequests.CommitsViewModel>()
			.AddTransient<Features.Repositories.ViewModels.PullRequests.FileChangesViewModel>()
			.AddTransient<Features.Repositories.ViewModels.PullRequests.PullRequestsViewModel>()
			.AddTransient<Features.Repositories.ViewModels.Releases.ReleasesViewModel>()
			.AddTransient<Features.Repositories.ViewModels.Releases.ReleaseViewModel>()
			.AddTransient<Features.Searches.ViewModels.CodeViewModel>()
			.AddTransient<Features.Searches.ViewModels.IssuesViewModel>()
			.AddTransient<Features.Searches.ViewModels.RepositoriesViewModel>()
			.AddTransient<Features.Searches.ViewModels.UsersViewModel>()
			.AddTransient<Shared.Controls.ViewModels.FileContentBlockViewModel>()
			.AddTransient<Shared.Controls.ViewModels.FileNavigationBlockViewModel>()
			.AddTransient<Shared.Controls.ViewModels.IssueCommentBlockViewModel>()
			.AddTransient<Shared.Controls.ViewModels.ReadmeContentBlockViewModel>()
			.AddTransient<Shared.Controls.ViewModels.LatestCommitBlockViewModel>()
			.AddTransient<Shared.Controls.ViewModels.UserContributionGraphViewModel>()
			.AddTransient<Features.Users.ViewModels.ContributionsViewModel>()
			.AddTransient<Features.Users.ViewModels.DiscussionsViewModel>()
			.AddTransient<Features.Users.ViewModels.FollowersViewModel>()
			.AddTransient<Features.Users.ViewModels.FollowingViewModel>()
			.AddTransient<Features.Users.ViewModels.IssuesViewModel>()
			.AddTransient<Features.Users.ViewModels.OrganizationsViewModel>()
			.AddTransient<Features.Users.ViewModels.OverviewViewModel>()
			.AddTransient<Features.Users.ViewModels.PackagesViewModel>()
			.AddTransient<Features.Users.ViewModels.ProjectsViewModel>()
			.AddTransient<Features.Users.ViewModels.PullRequestsViewModel>()
			.AddTransient<Features.Users.ViewModels.RepositoriesViewModel>()
			.AddTransient<Features.Users.ViewModels.StarredReposViewModel>();
}
