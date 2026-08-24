// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Services;
using FluentHub.ViewModels.Users;
using System;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using FluentHub.Data.Parameters;

namespace FluentHub.Views.Users
{
	public sealed partial class OrganizationsPage : LocatablePage
	{
		public OrganizationsViewModel ViewModel { get; }

		public OrganizationsPage()
			: base(NavigationPageKind.User, NavigationPageKey.Organizations)
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<OrganizationsViewModel>();
			_pageLoadCommand = ViewModel.LoadUserOrganizationsPageCommand;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var command = ViewModel.LoadUserOrganizationsPageCommand;
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
				var command = ViewModel.LoadUserOrganizationsFurtherCommand;
				if (command.CanExecute(null))
					command.Execute(null);
			}
		}
	}
}
