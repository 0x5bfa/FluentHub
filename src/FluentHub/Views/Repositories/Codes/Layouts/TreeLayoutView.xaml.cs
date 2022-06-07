using FluentHub.Models;
using FluentHub.Services;
using FluentHub.Services.Navigation;
using FluentHub.ViewModels;
using FluentHub.ViewModels.Repositories;
using FluentHub.ViewModels.Repositories.Codes;
using FluentHub.ViewModels.Repositories.Codes.Layouts;
using Microsoft.Extensions.DependencyInjection;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views.Repositories.Codes.Layouts
{
    public sealed partial class TreeLayoutView : Page
    {
        public TreeLayoutView()
        {
            this.InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<TreeLayoutViewModel>();
            navigationService = App.Current.Services.GetRequiredService<INavigationService>();
        }

        private RepoContextViewModel ContextViewModel { get; set; }
        public TreeLayoutViewModel ViewModel { get; }
        private readonly INavigationService navigationService;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ContextViewModel = e.Parameter as RepoContextViewModel;
            ViewModel.ContextViewModel = ContextViewModel;
            DataContext = ViewModel;

            #region tabitem
            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;

            string header;
            if (ContextViewModel.IsRootDir)
            {
                if (string.IsNullOrEmpty(ContextViewModel.Repository.Description))
                {
                    header = $"{ContextViewModel.Repository.Owner.Login}/{ContextViewModel.Repository.Name}";
                }
                else
                {
                    header = $"{ContextViewModel.Repository.Owner.Login}/{ContextViewModel.Repository.Name}: {ContextViewModel.Repository.Description}";
                }
            }
            else
            {
                string middlePath = ContextViewModel.Path.Remove(0, 1);
                middlePath = middlePath.Remove(middlePath.Length - 1, 1);
                header = $"{ContextViewModel.Repository.Name}/{middlePath} at {ContextViewModel.BranchName} · {ContextViewModel.Repository.Owner.Login}/{ContextViewModel.Repository.Name}";
            }

            currentItem.Header = header;
            currentItem.Description = currentItem.Header;

            string url;
            if (ContextViewModel.IsFile)
            {
                url = $"https://github.com/{ContextViewModel.Repository.Owner.Login}/{ContextViewModel.Repository.Name}/blob/{ContextViewModel.BranchName}{ContextViewModel.Path.TrimEnd('/')}";
            }
            else
            {
                url = $"https://github.com/{ContextViewModel.Repository.Owner.Login}/{ContextViewModel.Repository.Name}/tree/{ContextViewModel.BranchName}{ContextViewModel.Path.TrimEnd('/')}";
            }

            currentItem.Url = url;
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Repositories.png"))
            };
            #endregion

            var command = ViewModel.LoadTreeViewContentsCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }

        private async void OnDirTreeViewExpanding(muxc.TreeView sender, muxc.TreeViewExpandingEventArgs args)
        {
            if (args.Node.HasUnrealizedChildren && !(args.Item as TreeLayoutPageModel).IsBolb)
            {
                ViewModel.IsLoading = true;

                var item = args.Item as TreeLayoutPageModel;
                string path = item?.Path;

                var result = await ViewModel.LoadSubItemsAsync(path);

                item.Children.Clear();
                foreach (var res in result) item.Children.Add(res);

                args.Node.HasUnrealizedChildren = false;
                ViewModel.IsLoading = false;
            }
        }

        private void OnDirTreeViewItemInvoked(muxc.TreeView sender, muxc.TreeViewItemInvokedEventArgs args)
        {
            var item = args.InvokedItem as TreeLayoutPageModel;

            ViewModel.BlobSelected = false;
            if (!item.IsBolb) return;

            ViewModel.IsLoading = true;
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
            ViewModel.IsLoading = false;
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
