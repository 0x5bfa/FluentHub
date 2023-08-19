// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.Services;
using FluentHub.App.ViewModels.Users;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using FluentHub.App.Data.Parameters;

namespace FluentHub.App.Views.Users
{
	public sealed partial class OrganizationsPage : LocatablePage
	{
		public OrganizationsViewModel ViewModel { get; }

		public OrganizationsPage()
			: base(NavigationPageKind.User, NavigationPageKey.Organizations)
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<OrganizationsViewModel>();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var command = ViewModel.LoadUserOrganizationsPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}

		private void OnScrollViewerViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
		{
			var scrollViewer = (ScrollViewer)sender;
			if (scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
			{
				var command = ViewModel.LoadUserOrganizationsFurtherCommand;
				if (command.CanExecute(null))
					command.Execute(null);
			}
		}
	}
}
