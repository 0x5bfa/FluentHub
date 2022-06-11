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
    public sealed partial class DetailsLayoutView : Page
    {
        public DetailsLayoutView()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<DetailsLayoutViewModel>();
            navigationService = App.Current.Services.GetRequiredService<INavigationService>();
        }

        private static Repository RepositoryCache { get; set; }
        public DetailsLayoutViewModel ViewModel { get; }
        private readonly INavigationService navigationService;

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var url = e.Parameter as string;
            var uri = new Uri(url);
            var pathSegments = uri.AbsolutePath.Split("/").ToList();
            pathSegments.RemoveAt(0);

            if (RepositoryCache is null)
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
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Repositories.png"))
            };
            #endregion

            var command2 = ViewModel.RefreshDetailsLayoutPageCommand;
            if (command2.CanExecute(url))
                command2.Execute(url);
        }

        private void OnDirListViewDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            bool isFile = false;
            string path;

            var item = DirListView.SelectedItem as DetailsLayoutListViewModel;
            var tagItem = item?.ObjectTag?.Split("/");

            if (tagItem.First() == "blob")
                isFile = true;
            // blob
            else isFile = false;

            path = ViewModel.ContextViewModel.Path;
            if (!string.IsNullOrEmpty(path)) path += "/";

            path += tagItem.Last();

            var objType = isFile ? "blob" : "tree";

            string url = $"{App.DefaultGitHubDomain}/{ViewModel.ContextViewModel.Repository.Name}/{ViewModel.ContextViewModel.Repository.Owner.Login}/{objType}/{ViewModel.ContextViewModel.BranchName}/{path}";

            MainPageViewModel.RepositoryContentFrame.Navigate(typeof(DetailsLayoutView), url);
        }

        private void OnLatestReleaseClick(object sender, RoutedEventArgs e)
        {
            MainPageViewModel.RepositoryContentFrame.Navigate(typeof(ReleasesPage), ViewModel.ContextViewModel);
        }
    }
}
