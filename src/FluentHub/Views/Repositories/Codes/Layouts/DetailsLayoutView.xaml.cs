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

        private static RepoContextViewModel ContextViewModel { get; set; }
        public DetailsLayoutViewModel ViewModel { get; }
        private readonly INavigationService navigationService;

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var url = e.Parameter as string;
            var uri = new Uri(url);
            var pathSegments = uri.AbsolutePath.Split("/").ToList();
            pathSegments.RemoveAt(0);

            if (ContextViewModel != null && pathSegments.Count() != 2)
            {
                ViewModel.ContextViewModel = ContextViewModel;
            }
            else
            {
                var command1 = ViewModel.LoadRepositoryCommand;
                if (command1.CanExecute(url))
                    await command1.ExecuteAsync(url);

                ViewModel.ContextViewModel = ContextViewModel = new()
                {
                    IsRootDir = true,
                    Repository = ViewModel.Repository,
                    BranchName = ViewModel.Repository.DefaultBranchName,
                };
            }

            #region tabitem
            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;

            if (ContextViewModel.IsRootDir)
            {
                if (string.IsNullOrEmpty(ContextViewModel.Repository.Description))
                    currentItem.Header = $"{ContextViewModel.Repository.Owner.Login}/{ContextViewModel.Repository.Name}";
                else
                    currentItem.Header = $"{ContextViewModel.Repository.Owner.Login}/{ContextViewModel.Repository.Name}: {ContextViewModel.Repository.Description}";
            }
            else
                currentItem.Header = $"{ContextViewModel.Repository.Name}/{ContextViewModel.Path} at {ContextViewModel.BranchName} · {ContextViewModel.Repository.Owner.Login}/{ContextViewModel.Repository.Name}";

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
            var repoContextViewModel = new RepoContextViewModel()
            {
                Repository = ViewModel.ContextViewModel.Repository,
                BranchName = ViewModel.ContextViewModel.BranchName,
            };

            var item = DirListView.SelectedItem as DetailsLayoutListViewModel;
            var tagItem = item?.ObjectTag?.Split("/");

            if (tagItem.First() == "tree")
            {
                repoContextViewModel.IsSubDir = true;
                repoContextViewModel.Path = ContextViewModel.Path;

                if (!string.IsNullOrEmpty(repoContextViewModel.Path)) repoContextViewModel.Path += "/";
                repoContextViewModel.Path += tagItem.Last();
            }
            else // blob
            {
                repoContextViewModel.IsFile = true;
                repoContextViewModel.Path = ContextViewModel.Path;

                if (!string.IsNullOrEmpty(repoContextViewModel.Path)) repoContextViewModel.Path += "/";
                repoContextViewModel.Path += tagItem.Last();
            }

            var objType = ContextViewModel.IsFile ? "blob" : "tree";

            string url = $"{App.DefaultGitHubDomain}/{repoContextViewModel.Repository.Name}/{repoContextViewModel.Repository.Owner.Login}/{objType}/{repoContextViewModel.BranchName}/{repoContextViewModel.Path}";

            // Sync
            ContextViewModel = repoContextViewModel;

            MainPageViewModel.RepositoryContentFrame.Navigate(typeof(DetailsLayoutView), url);
        }

        private void OnLatestReleaseClick(object sender, RoutedEventArgs e)
        {
            MainPageViewModel.RepositoryContentFrame.Navigate(typeof(ReleasesPage), ViewModel.ContextViewModel);
        }
    }
}
