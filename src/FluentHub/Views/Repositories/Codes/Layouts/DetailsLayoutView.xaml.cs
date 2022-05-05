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
            this.InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<DetailsLayoutViewModel>();
            navigationService = App.Current.Services.GetRequiredService<INavigationService>();
        }

        private RepoContextViewModel ContextViewModel { get; set; }
        public DetailsLayoutViewModel ViewModel { get; }
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
                    header = $"{ContextViewModel.Owner}/{ContextViewModel.Name}";
                }
                else
                {
                    header = $"{ContextViewModel.Owner}/{ContextViewModel.Name}: {ContextViewModel.Repository.Description}";
                }
            }
            else
            {
                string middlePath = ContextViewModel.Path.Remove(0, 1);
                middlePath = middlePath.Remove(middlePath.Length - 1, 1);
                header = $"{ContextViewModel.Name}/{middlePath} at {ContextViewModel.BranchName} · {ContextViewModel.Owner}/{ContextViewModel.Name}";
            }

            currentItem.Header = header;
            currentItem.Description = currentItem.Header;

            string url;
            if (ContextViewModel.IsFile)
            {
                url = $"https://github.com/{ContextViewModel.Owner}/{ContextViewModel.Name}/blob/{ContextViewModel.BranchName}{ContextViewModel.Path.TrimEnd('/')}";
            }
            else
            {
                url = $"https://github.com/{ContextViewModel.Owner}/{ContextViewModel.Name}/tree/{ContextViewModel.BranchName}{ContextViewModel.Path.TrimEnd('/')}";
            }

            currentItem.Url = url;
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Repositories.png"))
            };
            #endregion

            var command = ViewModel.RefreshDetailsLayoutPageCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }

        private void OnDirListViewDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            var repoContextViewModel = new RepoContextViewModel()
            {
                IsRootDir = true,
                Name = ViewModel.ContextViewModel.Repository.Name,
                Owner = ViewModel.ContextViewModel.Repository.Owner.Login,
                Repository = ViewModel.ContextViewModel.Repository,
                BranchName = ViewModel.ContextViewModel.BranchName,
            };

            var item = DirListView.SelectedItem as DetailsLayoutListViewModel;

            var tagItem = item.ObjectTag.Split("/");

            if (tagItem[0] == "tree")
            {
                repoContextViewModel.IsSubDir = true;
                repoContextViewModel.Path = ContextViewModel.Path + tagItem[1] + "/";
            }
            else // blob
            {
                repoContextViewModel.IsFile = true;
                repoContextViewModel.Path = ContextViewModel.Path + tagItem[1];
            }

            MainPageViewModel.RepositoryContentFrame.Navigate(typeof(DetailsLayoutView), repoContextViewModel);
        }

        private void OnLatestReleaseClick(object sender, RoutedEventArgs e)
        {
            MainPageViewModel.RepositoryContentFrame.Navigate(typeof(ReleasesPage), ViewModel.ContextViewModel);
        }
    }
}
