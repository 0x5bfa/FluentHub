// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.ViewModels.Users;

namespace FluentHub.Views.Users
{
	public sealed partial class OverviewPage : NavigableView
	{
		public OverviewViewModel ViewModel { get; }

		public OverviewPage()
			: base(NavigationPageKind.User, NavigationPageKey.Overview)
		{
			InitializeComponent();

			ViewModel = GetRequiredService<OverviewViewModel>();
			_pageLoadCommand = ViewModel.LoadUserOverviewCommand;
			_screenViewModel = ViewModel;
		}

		protected override void OnActivated(AppRoute route)
		{
			var command = ViewModel.LoadUserOverviewCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}
	}
}
