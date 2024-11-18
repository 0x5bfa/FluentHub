// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.ViewModels.Repositories.PullRequests;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.Repositories.PullRequests
{
	public sealed partial class ChecksPage : LocatablePage
	{
		public ChecksViewModel ViewModel;

		public ChecksPage()
			: base(NavigationPageKind.Repository, NavigationPageKey.PullRequests)
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<ChecksViewModel>();
			_pageLoadCommand = ViewModel.LoadRepositoryPullRequestChecksPageCommand;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var command = ViewModel.LoadRepositoryPullRequestChecksPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}

		private void OnCheckRunItemButtonClick(object sender, RoutedEventArgs e)
		{
			var target = sender as Button;
			var checkRunItem = target.Tag as CheckRun;

			ViewModel.SelectedCheckRun = checkRunItem;
		}
	}
}
