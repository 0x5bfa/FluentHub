// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.Data.Parameters;
using FluentHub.App.Services;
using FluentHub.App.ViewModels.Repositories.PullRequests;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.Repositories.PullRequests
{
	public sealed partial class CommitsPage : LocatablePage
	{
		public CommitsViewModel ViewModel;

		public CommitsPage()
			: base(NavigationPageKind.Repository, NavigationPageKey.PullRequests)
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<CommitsViewModel>();
			_pageLoadCommand = ViewModel.LoadRepositoryPullRequestCommitsPageCommand;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var command = ViewModel.LoadRepositoryPullRequestCommitsPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}
	}
}
