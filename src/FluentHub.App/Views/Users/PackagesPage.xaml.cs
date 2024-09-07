// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.ViewModels.Users;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.Users
{
	public sealed partial class PackagesPage : LocatablePage
	{
		public PackagesViewModel ViewModel { get; }

		public PackagesPage()
			: base(NavigationPageKind.User, NavigationPageKey.Packages)
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<PackagesViewModel>();
			_pageLoadCommand = ViewModel.LoadUserPackagesPageCommand;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var command = ViewModel.LoadUserPackagesPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}

		private void OnScrollViewerViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
		{
			var scrollViewer = (ScrollViewer)sender;
			if (scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
			{
				var command = ViewModel.LoadUserPackagesFurtherCommand;
				if (command.CanExecute(null))
					command.Execute(null);
			}
		}
	}
}
