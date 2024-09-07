// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.ViewModels.Organizations;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.Organizations
{
	public sealed partial class OverviewPage : LocatablePage
	{
		public OverviewViewModel ViewModel;

		public OverviewPage()
			: base(NavigationPageKind.Organization, NavigationPageKey.Overview)
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<OverviewViewModel>();
			_pageLoadCommand = ViewModel.LoadOrganizationOverviewPageCommand;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var command = ViewModel.LoadOrganizationOverviewPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}
	}
}
