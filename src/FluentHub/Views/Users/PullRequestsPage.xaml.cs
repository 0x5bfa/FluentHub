// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Data.Parameters;
using FluentHub.Services;
using FluentHub.ViewModels.Users;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.Views.Users
{
	public sealed partial class PullRequestsPage : LocatablePage
	{
		public PullRequestsViewModel ViewModel { get; }

		public PullRequestsPage()
			: base(NavigationPageKind.User, NavigationPageKey.PullRequests)
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<PullRequestsViewModel>();
			_pageLoadCommand = ViewModel.LoadUserPullRequestsPageCommand;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var command = ViewModel.LoadUserPullRequestsPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}

		private void OnScrollViewerViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
		{
			var scrollViewer = (ScrollViewer)sender;
			if (scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
			{
				var command = ViewModel.LoadUserPullRequestsFurtherCommand;
				if (command.CanExecute(null))
					command.Execute(null);
			}
		}
	}
}
