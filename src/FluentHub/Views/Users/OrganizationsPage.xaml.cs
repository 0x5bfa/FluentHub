// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Services;
using FluentHub.ViewModels.Users;
using System;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.Views.Users
{
	public sealed partial class OrganizationsPage : NavigableView
	{
		public OrganizationsViewModel ViewModel { get; }

		public OrganizationsPage()
			: base(NavigationPageKind.User, NavigationPageKey.Organizations)
		{
			InitializeComponent();

			ViewModel = GetRequiredService<OrganizationsViewModel>();
			_pageLoadCommand = ViewModel.LoadUserOrganizationsPageCommand;
			_screenViewModel = ViewModel;
		}

		protected override void OnActivated(AppRoute route)
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
