// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.ViewModels.Viewers;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views.Viewers
{
	public sealed partial class NotificationsPage : NavigableView
	{
		public NotificationsViewModel ViewModel { get; }

		private readonly INavigationService _navigation;

		public NotificationsPage()
			:	base(NavigationPageKind.None, NavigationPageKey.None)
		{
			InitializeComponent();

			ViewModel = GetRequiredService<NotificationsViewModel>();
			_navigation = GetRequiredService<INavigationService>();
			_pageLoadCommand = ViewModel.LoadUserNotificationsPageCommand;
			_screenViewModel = ViewModel;

		}

		protected override void OnActivated(AppRoute route)
		{
			var command = ViewModel.LoadUserNotificationsPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}

		private void OnScrollViewerViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
		{
			var scrollViewer = (ScrollViewer)sender;
			if (scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
			{
				var command = ViewModel.LoadUserNotificationsFurtherCommand;
				if (command.CanExecute(null))
					command.Execute(null);
			}
		}
	}
}
