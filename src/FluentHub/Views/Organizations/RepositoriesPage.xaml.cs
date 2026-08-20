// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Data.Parameters;
using FluentHub.Services;
using FluentHub.ViewModels.Organizations;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.Views.Organizations
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
