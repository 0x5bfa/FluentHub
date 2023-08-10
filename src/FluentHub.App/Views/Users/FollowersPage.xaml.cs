// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.Data.Parameters;
using FluentHub.App.Services;
using FluentHub.App.ViewModels.Users;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.Users
{
	public sealed partial class FollowersPage : LocatablePage
	{
		public FollowersViewModel ViewModel { get; }

		public FollowersPage()
			: base(NavigationPageKind.User, NavigationPageKey.Followers)
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<FollowersViewModel>();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var command = ViewModel.LoadUserFollowersPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}
	}
}
