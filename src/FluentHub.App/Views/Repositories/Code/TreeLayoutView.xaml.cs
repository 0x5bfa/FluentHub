// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.Models;
using FluentHub.App.ViewModels.Repositories;
using FluentHub.App.ViewModels.Repositories.Codes;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.Repositories.Code
{
	public sealed partial class TreeLayoutView : LocatablePage
	{
		private static Repository RepositoryCache { get; set; }

		public TreeLayoutViewModel ViewModel { get; }

		private readonly INavigationService navigationService;

		public TreeLayoutView()
			: base(NavigationPageKind.Repository, NavigationPageKey.Code)
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<TreeLayoutViewModel>();
			navigationService = Ioc.Default.GetRequiredService<INavigationService>();
			//_pageLoadCommand = ViewModel.LoadUserStarredRepositoriesPageCommand;
		}

		protected async override void OnNavigatedTo(NavigationEventArgs e)
		{
		}

		private async void OnDirTreeViewExpanding(TreeView sender, TreeViewExpandingEventArgs args)
		{
			if (args.Node.HasUnrealizedChildren && !(args.Item as TreeLayoutPageModel).IsBolb)
			{
				var item = args.Item as TreeLayoutPageModel;
				string path = item?.Path;

				var result = await ViewModel.LoadSubItemsAsync(path);

				item.Children.Clear();
				foreach (var res in result) item.Children.Add(res);

				args.Node.HasUnrealizedChildren = false;
			}
		}

		private void OnDirTreeViewItemInvoked(TreeView sender, TreeViewItemInvokedEventArgs args)
		{
			var item = args.InvokedItem as TreeLayoutPageModel;

			ViewModel.BlobSelected = false;
			if (!item.IsBolb) return;

			ViewModel.BlobSelected = true;

			RepoContextViewModel viewmodel = new()
			{
				IsDir = false,
				IsFile = true,
				IsRootDir = false,
				IsSubDir = false,
				Repository = ViewModel.ContextViewModel.Repository,
				BranchName = ViewModel.ContextViewModel.BranchName,
				Path = "/" + item?.Path,
			};

			ViewModel.SelectedContextViewModel = viewmodel;
		}
	}

	class ExplorerItemTemplateSelector : DataTemplateSelector
	{
		public DataTemplate FolderTemplate { get; set; }
		public DataTemplate FileTemplate { get; set; }

		protected override DataTemplate SelectTemplateCore(object item)
		{
			var explorerItem = (TreeLayoutPageModel)item;
			return explorerItem.IsBolb ? FileTemplate : FolderTemplate;
		}
	}
}
