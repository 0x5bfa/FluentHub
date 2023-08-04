// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.ViewModels.Users;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.Users
{
	public sealed partial class StarsPage : LocatablePage
	{
		private readonly INavigationService _navigationService;

		public StarredReposViewModel ViewModel { get; }

		public StarsPage()
			: base(NavigationPageKind.User, NavigationPageKey.Stars)
		{
			InitializeComponent();

			_navigationService = Ioc.Default.GetRequiredService<INavigationService>();
			ViewModel = Ioc.Default.GetRequiredService<StarredReposViewModel>();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var command = ViewModel.LoadUserStarredRepositoriesPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}
	}
}
