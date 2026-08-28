// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Services;
using FluentHub.ViewModels.Repositories.PullRequests;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.Views.Repositories.PullRequests
{
	public sealed partial class CommitsPage : NavigableView
	{
		public CommitsViewModel ViewModel;

		public CommitsPage()
			: base(NavigationPageKind.Repository, NavigationPageKey.PullRequests)
		{
			InitializeComponent();

			ViewModel = GetRequiredService<CommitsViewModel>();
			_pageLoadCommand = ViewModel.LoadRepositoryPullRequestCommitsPageCommand;
			_screenViewModel = ViewModel;
		}

		protected override void OnActivated(AppRoute route)
		{
			var command = ViewModel.LoadRepositoryPullRequestCommitsPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}
	}
}
