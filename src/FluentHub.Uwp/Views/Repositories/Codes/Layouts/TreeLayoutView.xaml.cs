using FluentHub.Uwp.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.Services.Navigation;
using FluentHub.Uwp.ViewModels;
using FluentHub.Uwp.ViewModels.Repositories;
using FluentHub.Uwp.ViewModels.Repositories.Codes;
using FluentHub.Uwp.ViewModels.Repositories.Codes.Layouts;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.Views.Repositories.Codes.Layouts
{
    public sealed partial class TreeLayoutView : Page
    {
        public TreeLayoutView()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<TreeLayoutViewModel>();
            navigationService = App.Current.Services.GetRequiredService<INavigationService>();
        }

        private static Repository RepositoryCache { get; set; }
        public TreeLayoutViewModel ViewModel { get; }
        private readonly INavigationService navigationService;

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            var url = e.Parameter as string;
            var uri = new Uri(url);
            var pathSegments = uri.AbsolutePath.Split("/").ToList();
            pathSegments.RemoveAt(0);

            if (RepositoryCache is null || RepositoryCache?.Name != pathSegments[1])
            {
                // Load repository info
                var command1 = ViewModel.LoadRepositoryCommand;
                if (command1.CanExecute(url))
                    await command1.ExecuteAsync(url);

                RepositoryCache = ViewModel.Repository;
            }

            bool isRootDir = false;
            bool isFile = false;
            bool isSubDir = false;
            bool isDir = false;
            string path = "";
            string branchName = "";

            // URL has root path and default branch
            if (pathSegments.Count() == 2) isRootDir = true;
            // URL has different branch name and/or repository content path
            if (pathSegments.Count() > 2)
            {
                isFile = pathSegments[2] == "blob" ? true : false;
                isDir = pathSegments[2] == "tree" ? true : false;

                branchName = pathSegments[3];

                if (pathSegments.Count() == 4) isRootDir = true;

                // URL has path
                if (pathSegments.Count() > 4)
                {
                    pathSegments.RemoveRange(0, 4);
                    path = string.Join("/", pathSegments);
                    if (isDir) isSubDir = true;
                }
            }
            else
            {
                branchName = RepositoryCache.DefaultBranchName;
                path = null;
            }

            ViewModel.ContextViewModel = new()
            {
                Repository = RepositoryCache,

                BranchName = branchName,
                IsDir = isDir,
                IsFile = isFile,
                IsSubDir = isSubDir,
                IsRootDir = isRootDir,
                Path = path,
            };

            #region tabitem
            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;

            if (ViewModel.ContextViewModel.IsRootDir)
            {
                if (string.IsNullOrEmpty(ViewModel.ContextViewModel.Repository.Description))
                    currentItem.Header = $"{ViewModel.ContextViewModel.Repository.Owner.Login}/{ViewModel.ContextViewModel.Repository.Name}";
                else
                    currentItem.Header = $"{ViewModel.ContextViewModel.Repository.Owner.Login}/{ViewModel.ContextViewModel.Repository.Name}: {ViewModel.ContextViewModel.Repository.Description}";
            }
            else
                currentItem.Header = $"{ViewModel.ContextViewModel.Repository.Name}/{ViewModel.ContextViewModel.Path} at {ViewModel.ContextViewModel.BranchName} · {ViewModel.ContextViewModel.Repository.Owner.Login}/{ViewModel.ContextViewModel.Repository.Name}";

            currentItem.Url = url;
            currentItem.Description = currentItem.Header;

            string displayurl;
            if (ViewModel.ContextViewModel.IsRootDir)
            {
                if (ViewModel.ContextViewModel.Repository.DefaultBranchName == ViewModel.ContextViewModel.BranchName)
                    displayurl = $"{ViewModel.ContextViewModel.Repository.Owner.Login} / {ViewModel.ContextViewModel.Repository.Name}";
                else
                    displayurl = $"{ViewModel.ContextViewModel.Repository.Owner.Login} / {ViewModel.ContextViewModel.Repository.Name} / {ViewModel.ContextViewModel.BranchName}";
            }
            else displayurl = $"{ViewModel.ContextViewModel.Repository.Owner.Login} / {ViewModel.ContextViewModel.Repository.Name} / {ViewModel.ContextViewModel.BranchName} / {string.Join(" / ", ViewModel.ContextViewModel.Path.Split("/"))}";

            currentItem.DisplayUrl = displayurl;

            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Repositories.png"))
            };
            #endregion

            var command2 = ViewModel.LoadTreeViewContentsCommand;
            if (command2.CanExecute(url))
                command2.Execute(url);
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
