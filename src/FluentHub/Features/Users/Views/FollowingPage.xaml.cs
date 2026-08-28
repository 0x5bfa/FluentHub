// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Services;
using FluentHub.Features.Users.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.Features.Users.Views
{
	public sealed partial class FollowingPage : NavigableView
	{
		public FollowingViewModel ViewModel { get; }

		public FollowingPage()
			: base(NavigationPageKind.User, NavigationPageKey.Following)
		{
			InitializeComponent();

			ViewModel = GetRequiredService<FollowingViewModel>();
			_pageLoadCommand = ViewModel.LoadUserFollowingPageCommand;
			_screenViewModel = ViewModel;
		}

		protected override void OnActivated(AppRoute route)
		{
			var command = ViewModel.LoadUserFollowingPageCommand;
			if (command.CanExecute(null))
				command.ExecuteAsync(null);
		}

		private async void OnSearchQuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
			=> await ViewModel.ApplySearchAsync(sender.Text);

		private void OnScrollViewerViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
		{
			var scrollViewer = (ScrollViewer)sender;
			if (scrollViewer.ScrollableHeight - scrollViewer.VerticalOffset
				<= Math.Max(200, scrollViewer.ViewportHeight / 2))
			{
				var command = ViewModel.LoadUserFollowingFurtherCommand;
				if (command.CanExecute(null))
					command.Execute(null);
			}
		}
	}
}
