// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.ViewModels.Users;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.Users
{
	public sealed partial class FollowingPage : LocatablePage
	{
		public FollowingViewModel ViewModel { get; }

		public FollowingPage()
			: base(NavigationPageKind.User, NavigationPageKey.Following)
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<FollowingViewModel>();
			_pageLoadCommand = ViewModel.LoadUserFollowingPageCommand;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var command = ViewModel.LoadUserFollowingPageCommand;
			if (command.CanExecute(null))
				command.ExecuteAsync(null);
		}

		private void OnScrollViewerViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
		{
			var scrollViewer = (ScrollViewer)sender;
			if (scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
			{
				var command = ViewModel.LoadUserFollowingFurtherCommand;
				if (command.CanExecute(null))
					command.Execute(null);
			}
		}
	}
}
