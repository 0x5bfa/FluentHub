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
	public sealed partial class PullRequestsPage : Page
	{
		private readonly INavigationService _navigationService;

		public PullRequestsViewModel ViewModel { get; }

		public PullRequestsPage()
		{
			InitializeComponent();

			_navigationService = Ioc.Default.GetRequiredService<INavigationService>();
			ViewModel = Ioc.Default.GetRequiredService<PullRequestsViewModel>();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var parameter = (FrameNavigationParameter)e.Parameter;

			ViewModel.Login = parameter.UserLogin
				?? throw new ArgumentNullException(nameof(parameter.UserLogin), "Login parameter cannot be null in this context.");

			if (parameter.AsViewer)
			{
				ViewModel.DisplayTitle = true;

				var currentTabItem = _navigationService.TabView.SelectedItem;
				currentTabItem.NavigationBar.PageKind = NavigationPageKind.None;
			}

			var command = ViewModel.LoadUserPullRequestsPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}
	}
}
