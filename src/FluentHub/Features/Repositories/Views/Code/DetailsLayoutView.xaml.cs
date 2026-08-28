// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Models;
using FluentHub.Shared.Dialogs.Views;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using FluentHub.Features.Repositories.ViewModels.Codes;

namespace FluentHub.Features.Repositories.Views.Code
{
	public sealed partial class DetailsLayoutView : NavigableView
	{
		public DetailsLayoutViewModel ViewModel { get; }

		private readonly INavigationService _navigation;

		public DetailsLayoutView()
			: base(NavigationPageKind.Repository, NavigationPageKey.Code)
		{
			InitializeComponent();

			ViewModel = GetRequiredService<DetailsLayoutViewModel>();
			_navigation = GetRequiredService<INavigationService>();
			_pageLoadCommand = ViewModel.LoadDetailsViewPageCommand;
			_screenViewModel = ViewModel;
		}

		protected override void OnActivated(AppRoute route)
		{
			var command = ViewModel.LoadDetailsViewPageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}

		private async void OnDirListViewDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
		{
			if (DirListView.SelectedItem is not DetailsLayoutListViewModel item)
				return;

			string path = ViewModel.ContextViewModel.Path;

			if (string.IsNullOrEmpty(path) is false)
				path += "/";

			path += item.Name;

			var target = item.Type.Equals("blob", StringComparison.OrdinalIgnoreCase)
				? RepositoryCodeTarget.File
				: RepositoryCodeTarget.Directory;
			await _navigation.NavigateAsync(
				new RepositoryCodeRoute(
					new RepositorySlug(ViewModel.Login, ViewModel.Name),
					ViewModel.ContextViewModel.BranchName,
					path,
					Target: target));
		}

		private async void OnLatestReleaseClick(object sender, RoutedEventArgs e)
		{
			await _navigation.NavigateAsync(
				new RepositoryRoute(
					new RepositorySlug(ViewModel.Login, ViewModel.Name),
					RepositorySection.Releases));
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

				await _navigation.NavigateAsync(
					new RepositoryCodeRoute(new RepositorySlug(fork.Owner, fork.Name)));
			}
			finally
			{
				button.IsEnabled = ViewModel.CanFork;
			}
		}
	}
}
