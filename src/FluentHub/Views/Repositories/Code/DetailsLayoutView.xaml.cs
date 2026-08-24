// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Models;
using FluentHub.Dialogs;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Navigation;
using FluentHub.ViewModels.Repositories.Codes;

namespace FluentHub.Views.Repositories.Code
{
	public sealed partial class DetailsLayoutView : LocatablePage
	{
		public DetailsLayoutViewModel ViewModel { get; }

		private readonly INavigationService _navigation;

		public DetailsLayoutView()
			: base(NavigationPageKind.Repository, NavigationPageKey.Code)
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<DetailsLayoutViewModel>();
			_navigation = Ioc.Default.GetRequiredService<INavigationService>();
			_pageLoadCommand = ViewModel.LoadDetailsViewPageCommand;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var command = ViewModel.LoadDetailsViewPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}

		private void OnDirListViewDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
		{
			if (DirListView.SelectedItem is not DetailsLayoutListViewModel item)
				return;

			string path = ViewModel.ContextViewModel.Path;

			if (string.IsNullOrEmpty(path) is false)
				path += "/";

			path += item.Name;

			string param = $"{item.Type}/{Uri.EscapeDataString(ViewModel.ContextViewModel.BranchName)}/{path}";

			SelectedTabViewItem.NavigationBar.Context = new()
			{
				PrimaryText = ViewModel.Login,
				SecondaryText = ViewModel.Name,
				Parameters = param
			};

			_navigation.Navigate<DetailsLayoutView>();
		}

		private void OnLatestReleaseClick(object sender, RoutedEventArgs e)
		{
			_navigation.Navigate<Releases.ReleasesPage>();
		}

		private async void OnForkRepositoryClick(object sender, RoutedEventArgs e)
		{
			if (sender is not Button button || !ViewModel.CanFork)
				return;

			button.IsEnabled = false;
			try
			{
				var owners = await ViewModel.GetAvailableForkOwnersAsync();
				if (owners.Count == 0)
					return;

				var dialog = new CreateForkDialog(
					owners,
					ViewModel.Repository.Name,
					ViewModel.Repository.Description,
					ViewModel.Repository.DefaultBranchRef?.Name ?? string.Empty)
				{
					XamlRoot = XamlRoot,
				};

				if (await dialog.ShowAsync() != ContentDialogResult.Primary ||
					dialog.SelectedOwner is not { } selectedOwner)
				{
					return;
				}

				var fork = await ViewModel.ForkRepositoryAsync(
					selectedOwner,
					dialog.RepositoryName,
					dialog.Description,
					dialog.DefaultBranchOnly);
				if (fork is null)
					return;

				SelectedTabViewItem.NavigationBar.Context = new()
				{
					PrimaryText = fork.Owner,
					SecondaryText = fork.Name,
				};
				_navigation.Navigate<DetailsLayoutView>();
			}
			finally
			{
				button.IsEnabled = ViewModel.CanFork;
			}
		}
	}
}
