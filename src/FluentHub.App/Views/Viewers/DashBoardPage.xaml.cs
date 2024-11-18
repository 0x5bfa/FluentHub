// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.ViewModels.Viewers;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.Viewers
{
	public sealed partial class DashBoardPage : LocatablePage
	{
		private readonly DashBoardViewModel ViewModel;

		private readonly INavigationService _navigation;

		public DashBoardPage()
			: base(NavigationPageKind.None, NavigationPageKey.None)
		{
			InitializeComponent();

			// Dependency injection
			ViewModel = Ioc.Default.GetRequiredService<DashBoardViewModel>();
			_navigation = Ioc.Default.GetRequiredService<INavigationService>();
			_pageLoadCommand = ViewModel.LoadUserHomePageCommand;

			_navigation.TabView.SelectedItem.NavigationHistory.CurrentItem.Context =
				_navigation.TabView.SelectedItem.NavigationBar.Context = new()
				{
					PrimaryText = "Dashboard"
				};
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
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
