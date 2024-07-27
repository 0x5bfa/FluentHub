// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.Services;
using FluentHub.App.ViewModels.Repositories.PullRequests;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Media.Imaging;
using FluentHub.App.Data.Parameters;

namespace FluentHub.App.Views.Repositories.PullRequests
{
	public sealed partial class CommitPage : LocatablePage
	{
		public CommitViewModel ViewModel;

		public CommitPage()
			: base(NavigationPageKind.Repository, NavigationPageKey.PullRequests)
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<CommitViewModel>();
			_pageLoadCommand = ViewModel.LoadRepositoryPullRequestCommitPageCommand;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var param = e.Parameter as FrameNavigationParameter;
			ViewModel.CommitItem = param.Parameters as Commit;

			var command = ViewModel.LoadRepositoryPullRequestCommitPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}
	}
}
