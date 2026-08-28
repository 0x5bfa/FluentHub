// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Services;
using FluentHub.ViewModels.Repositories.PullRequests;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media.Imaging;
using FluentHub.Core.Application.Models;

namespace FluentHub.Views.Repositories.PullRequests
{
	public sealed partial class CommitPage : NavigableView
	{
		public CommitViewModel ViewModel;

		public CommitPage()
			: base(NavigationPageKind.Repository, NavigationPageKey.PullRequests)
		{
			InitializeComponent();

			ViewModel = GetRequiredService<CommitViewModel>();
			_pageLoadCommand = ViewModel.LoadRepositoryPullRequestCommitPageCommand;
			_screenViewModel = ViewModel;
		}

		protected override void OnActivated(AppRoute route)
		{
			if (route is not RepositoryPullRequestCommitRoute commit)
				return;

			ViewModel.CommitItem = new Commit { Oid = commit.Sha };

			var command = ViewModel.LoadRepositoryPullRequestCommitPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}
	}
}
