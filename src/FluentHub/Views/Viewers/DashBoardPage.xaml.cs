// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.ViewModels.Viewers;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using FluentHub.Core.Application.Models;

namespace FluentHub.Views.Viewers
{
	public sealed partial class DashBoardPage : NavigableView
	{
		private readonly DashBoardViewModel ViewModel;

		private readonly INavigationService _navigation;

		public DashBoardPage()
			: base(NavigationPageKind.None, NavigationPageKey.None)
		{
			InitializeComponent();

			// Dependency injection
			ViewModel = GetRequiredService<DashBoardViewModel>();
			_navigation = GetRequiredService<INavigationService>();
			_pageLoadCommand = ViewModel.LoadUserHomePageCommand;
			_screenViewModel = ViewModel;

		}

		protected override void OnActivated(AppRoute route)
		{
			var command = ViewModel.LoadUserHomePageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}

		private void SidebarRepositoryItemButton_Click(object sender, RoutedEventArgs e)
		{
			if (sender is not Button button || button.DataContext is not Repository repo)
				return;

			var command = ViewModel.GoToSidebarRepositoryCommand;
			if (command.CanExecute(repo))
				command.Execute(repo);
		}

		private void SidebarRecentActivityItemButton_Click(object sender, RoutedEventArgs e)
		{
			if (sender is not Button button || button.DataContext is not Notification notification)
				return;

			var command = ViewModel.GoToSidebarActivityCommand;
			if (command.CanExecute(notification))
				command.Execute(notification);
		}
	}
}
