// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.ViewModels.Users;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.Users
{
	public sealed partial class RepositoriesPage : LocatablePage
	{
		public RepositoriesViewModel ViewModel { get; }

		public RepositoriesPage()
			: base(NavigationPageKind.User, NavigationPageKey.Repositories)
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<RepositoriesViewModel>();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var command = ViewModel.LoadUserRepositoriesPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}
	}
}
