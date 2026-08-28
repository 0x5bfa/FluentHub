// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Features.Users.ViewModels;

namespace FluentHub.Features.Users.Views
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
