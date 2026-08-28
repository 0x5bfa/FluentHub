// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Services;
using FluentHub.ViewModels.Organizations;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views.Organizations
{
	public sealed partial class OverviewPage : NavigableView
	{
		public OverviewViewModel ViewModel;

		public OverviewPage()
			: base(NavigationPageKind.Organization, NavigationPageKey.Overview)
		{
			InitializeComponent();

			ViewModel = GetRequiredService<OverviewViewModel>();
			_pageLoadCommand = ViewModel.LoadOrganizationOverviewPageCommand;
			_screenViewModel = ViewModel;
		}

		protected override void OnActivated(AppRoute route)
		{
			var command = ViewModel.LoadOrganizationOverviewPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}
	}
}
