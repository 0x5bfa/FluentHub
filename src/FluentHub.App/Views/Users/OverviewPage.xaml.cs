// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.ViewModels.Users;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.Users
{
	public sealed partial class OverviewPage : LocatablePage
	{
		public OverviewViewModel ViewModel { get; }

		public OverviewPage()
			: base(NavigationPageKind.User, NavigationPageKey.Overview)
		{
			InitializeComponent();

			var provider = App.Current.Services;
			ViewModel = provider.GetRequiredService<OverviewViewModel>();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var command = ViewModel.LoadUserOverviewCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}
	}
}
