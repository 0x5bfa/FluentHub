// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.ViewModels.Viewers;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.Viewers
{
	public sealed partial class NotificationsPage : LocatablePage
	{
		public NotificationsViewModel ViewModel { get; }

		private readonly INavigationService _navigation;

		public NotificationsPage()
			: base(NavigationPageKind.None, NavigationPageKey.None)
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<NotificationsViewModel>();
			_navigation = Ioc.Default.GetRequiredService<INavigationService>();
			_pageLoadCommand = ViewModel.LoadUserNotificationsPageCommand;

			_navigation.TabView.SelectedItem.NavigationHistory.CurrentItem.Context =
				_navigation.TabView.SelectedItem.NavigationBar.Context = new()
				{
					PrimaryText = "Notifications"
				};
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
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
