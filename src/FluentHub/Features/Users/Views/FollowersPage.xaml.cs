// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Services;
using FluentHub.Features.Users.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.Features.Users.Views
{
	public sealed partial class FollowersPage : NavigableView
	{
		public FollowersViewModel ViewModel { get; }

		public FollowersPage()
			: base(NavigationPageKind.User, NavigationPageKey.Followers)
		{
			InitializeComponent();

			ViewModel = GetRequiredService<FollowersViewModel>();
			_pageLoadCommand = ViewModel.LoadUserFollowersPageCommand;
			_screenViewModel = ViewModel;
		}

		protected override void OnActivated(AppRoute route)
		{
			var command = ViewModel.LoadUserFollowersPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}

		private async void OnSearchQuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
			=> await ViewModel.ApplySearchAsync(sender.Text);

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
