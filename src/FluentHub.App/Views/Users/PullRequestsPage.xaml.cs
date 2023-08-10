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
	public sealed partial class PullRequestsPage : LocatablePage
	{
		public PullRequestsViewModel ViewModel { get; }

		public PullRequestsPage()
			: base(NavigationPageKind.User, NavigationPageKey.Projects)
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<PullRequestsViewModel>();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var command = ViewModel.LoadUserPullRequestsPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}
	}
}
