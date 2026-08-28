// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Services;
using FluentHub.ViewModels.Users;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.Views.Users
{
	public sealed partial class ProjectsPage : NavigableView
	{
		public ProjectsViewModel ViewModel { get; }

		public ProjectsPage()
			: base(NavigationPageKind.User, NavigationPageKey.Projects)
		{
			InitializeComponent();

			ViewModel = GetRequiredService<ProjectsViewModel>();
			_pageLoadCommand = ViewModel.LoadUserProjectsPageCommand;
			_screenViewModel = ViewModel;
		}

		protected override void OnActivated(AppRoute route)
		{
			var command = ViewModel.LoadUserProjectsPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}

		private void OnScrollViewerViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
		{
			var scrollViewer = (ScrollViewer)sender;
			if (scrollViewer.ScrollableHeight - scrollViewer.VerticalOffset
				<= Math.Max(200, scrollViewer.ViewportHeight / 2))
			{
				var command = ViewModel.LoadUserProjectsFurtherCommand;
				if (command.CanExecute(null))
					command.Execute(null);
			}
		}
	}
}
