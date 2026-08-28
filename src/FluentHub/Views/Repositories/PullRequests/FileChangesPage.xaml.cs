// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Services;
using FluentHub.ViewModels.Repositories.PullRequests;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.Views.Repositories.PullRequests
{
	public sealed partial class FileChangesPage : NavigableView
	{
		public FileChangesViewModel ViewModel { get; }

		public FileChangesPage()
			: base(NavigationPageKind.Repository, NavigationPageKey.PullRequests)
		{
			InitializeComponent();

			ViewModel = GetRequiredService<FileChangesViewModel>();
			_pageLoadCommand = ViewModel.LoadRepositoryPullRequestFileChangesPageCommand;
			_screenViewModel = ViewModel;
		}

		protected override void OnActivated(AppRoute route)
		{
			var command = ViewModel.LoadRepositoryPullRequestFileChangesPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}
	}
}
