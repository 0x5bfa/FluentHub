// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.ViewModels.Users;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.Users
{
	public sealed partial class IssuesPage : LocatablePage
	{
		public IssuesViewModel ViewModel { get; }

		public IssuesPage()
			: base(NavigationPageKind.User, NavigationPageKey.Issues)
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<IssuesViewModel>();
			_pageLoadCommand = ViewModel.LoadUserIssuesPageCommand;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var command = ViewModel.LoadUserIssuesPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}

		private void OnScrollViewerViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
		{
			var scrollViewer = (ScrollViewer)sender;
			if (scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
			{
				var command = ViewModel.LoadUserIssuesFurtherCommand;
				if (command.CanExecute(null))
					command.Execute(null);
			}
		}
	}
}
