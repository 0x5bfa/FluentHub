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
	public sealed partial class ProjectsPage : LocatablePage
	{
		private readonly INavigationService _navigationService;

		public ProjectsViewModel ViewModel { get; }

		public ProjectsPage()
			: base(NavigationPageKind.User, NavigationPageKey.None)
		{
			InitializeComponent();

			_navigationService = Ioc.Default.GetRequiredService<INavigationService>();
			ViewModel = Ioc.Default.GetRequiredService<ProjectsViewModel>();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var parameter = (FrameNavigationParameter)e.Parameter;

			ViewModel.Login = parameter.PrimaryText
				?? throw new ArgumentNullException(nameof(parameter.PrimaryText), "Login parameter cannot be null in this context.");

			if (parameter.AsViewer)
			{
				ViewModel.DisplayTitle = true;

				var currentTabItem = _navigationService.TabView.SelectedItem;
				currentTabItem.NavigationBar.PageKind = NavigationPageKind.None;
			}

			var command = ViewModel.LoadUserProjectsPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}
	}
}
