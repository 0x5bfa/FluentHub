// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.Models;
using FluentHub.App.ViewModels.Repositories.Codes;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.Repositories.Code
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
			var item = DirListView.SelectedItem as DetailsLayoutListViewModel;
			var objType = item?.Type;

			string path = ViewModel.ContextViewModel.Path;

			if (string.IsNullOrEmpty(path) is false)
				path += "/";

			path += item.Name;

			string param = $"{objType}/{ViewModel.ContextViewModel.BranchName}/{path}";

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
	}
}
