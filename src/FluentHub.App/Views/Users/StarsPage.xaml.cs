// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.ViewModels.Users;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.Users
{
	public sealed partial class StarsPage : LocatablePage
	{
		public StarredReposViewModel ViewModel { get; }

		public StarsPage()
			: base(NavigationPageKind.User, NavigationPageKey.Stars)
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<StarredReposViewModel>();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var command = ViewModel.LoadUserStarredRepositoriesPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}

		private void OnScrollViewerViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
		{
			var scrollViewer = (ScrollViewer)sender;
			if (scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
			{
				var command = ViewModel.LoadUserStarredRepositoriesFurtherCommand;
				if (command.CanExecute(null))
					command.Execute(null);
			}
		}
	}
}
