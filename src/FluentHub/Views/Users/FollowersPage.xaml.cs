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
			if (scrollViewer.ScrollableHeight - scrollViewer.VerticalOffset
				<= Math.Max(200, scrollViewer.ViewportHeight / 2))
			{
				var command = ViewModel.LoadUserFollowersFurtherCommand;
				if (command.CanExecute(null))
					command.Execute(null);
			}
		}
	}
}
