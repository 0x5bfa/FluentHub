// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.ViewModels.Users;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.Users
{
	public sealed partial class FollowersPage : LocatablePage
	{
		public FollowersViewModel ViewModel { get; }

		public FollowersPage()
			: base(NavigationPageKind.User, NavigationPageKey.Followers)
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<FollowersViewModel>();
			_pageLoadCommand = ViewModel.LoadUserFollowersPageCommand;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var command = ViewModel.LoadUserFollowersPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}

		private void OnScrollViewerViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
		{
			var scrollViewer = (ScrollViewer)sender;
			if (scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
			{
				var command = ViewModel.LoadUserFollowersFurtherCommand;
				if (command.CanExecute(null))
					command.Execute(null);
			}
		}
	}
}
