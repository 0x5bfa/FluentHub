// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Models;
using FluentHub.ViewModels.Repositories;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Media.Imaging;
using FluentHub.ViewModels.Repositories.Codes;
using FluentHub.Core.Contracts;

namespace FluentHub.Views.Repositories.Code
{
	public sealed partial class TreeLayoutView : LocatablePage
	{
		public TreeLayoutViewModel ViewModel { get; }

		public TreeLayoutView()
			: base(NavigationPageKind.Repository, NavigationPageKey.Code)
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<TreeLayoutViewModel>();
		}

		private async void OnDirTreeViewExpanding(TreeView sender, TreeViewExpandingEventArgs args)
		{
			if (args.Node.HasUnrealizedChildren && args.Item is TreeLayoutPageModel { IsBolb: false } item)
			{
				var result = await ViewModel.LoadSubItemsAsync(item.Path);

				item.Children.Clear();
				foreach (var res in result) item.Children.Add(res);

				args.Node.HasUnrealizedChildren = false;
			}
		}

		private void OnDirTreeViewItemInvoked(TreeView sender, TreeViewItemInvokedEventArgs args)
		{
			if (args.InvokedItem is not TreeLayoutPageModel item)
				return;

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
				Path = "/" + item.Path,
			};

			ViewModel.SelectedContextViewModel = viewmodel;
		}
	}

	partial class ExplorerItemTemplateSelector : DataTemplateSelector
	{
		public DataTemplate FolderTemplate { get; set; } = default!;
		public DataTemplate FileTemplate { get; set; } = default!;

		protected override DataTemplate SelectTemplateCore(object item)
		{
			var explorerItem = (TreeLayoutPageModel)item;
			return explorerItem.IsBolb ? FileTemplate : FolderTemplate;
		}
	}
}
