// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Data.Parameters;
using FluentHub.Services;
using FluentHub.ViewModels.Repositories.PullRequests;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.Views.Repositories.PullRequests
{
	public sealed partial class FileChangesPage : LocatablePage
	{
		public FileChangesViewModel ViewModel { get; }

		public FileChangesPage()
			: base(NavigationPageKind.Repository, NavigationPageKey.PullRequests)
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<FileChangesViewModel>();
			_pageLoadCommand = ViewModel.LoadRepositoryPullRequestFileChangesPageCommand;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var command = ViewModel.LoadRepositoryPullRequestFileChangesPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}
	}
}
