// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.Utils;
using FluentHub.App.ViewModels;
using Microsoft.Extensions.Hosting;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Serilog;

namespace FluentHub.App.Helpers
{
	internal class AppLifecycleHelper
	{
		internal static void CloseApp()
		{
			MainWindow.Instance.Close();
		}

		internal static AppWindow GetAppWindow(Window w)
		{
			var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(w);
			WindowId windowId = Win32Interop.GetWindowIdFromWindow(hWnd);

			return AppWindow.GetFromWindowId(windowId);
		}

		internal static IHost ConfigureServices()
		{
			return Host.CreateDefaultBuilder()
				.ConfigureServices(services => services
					.AddSingleton<INavigationService, NavigationService>()
					.AddSingleton<Utils.ILogger>(new SerilogWrapperLogger(Log.Logger))
					.AddSingleton<ToastService>()
					.AddSingleton<IMessenger>(StrongReferenceMessenger.Default)
					// ViewModels
					.AddSingleton<MainPageViewModel>()
					.AddSingleton<ViewModels.SignIn.IntroViewModel>()
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
					.AddTransient<ViewModels.Repositories.Projects.ProjectViewModel>()
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
					.AddTransient<ViewModels.UserControls.FileContentBlockViewModel>()
					.AddTransient<ViewModels.UserControls.FileNavigationBlockViewModel>()
					.AddTransient<ViewModels.UserControls.IssueCommentBlockViewModel>()
					.AddTransient<ViewModels.UserControls.ReadmeContentBlockViewModel>()
					.AddTransient<ViewModels.UserControls.LatestCommitBlockViewModel>()
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
					.AddTransient<ViewModels.Users.StarredReposViewModel>()
				).Build();
		}
	}
}
