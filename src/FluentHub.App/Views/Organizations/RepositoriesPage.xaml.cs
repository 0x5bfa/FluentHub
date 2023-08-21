// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.Data.Parameters;
using FluentHub.App.Services;
using FluentHub.App.ViewModels.Organizations;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.Organizations
{
	public sealed partial class RepositoriesPage : LocatablePage
	{
		public RepositoriesViewModel ViewModel;

		public RepositoriesPage()
			: base(NavigationPageKind.Organization, NavigationPageKey.Repositories)
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<RepositoriesViewModel>();
			_pageLoadCommand = ViewModel.LoadOrganizationRepositoriesPageCommand;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var command = ViewModel.LoadOrganizationRepositoriesPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}
	}
}
