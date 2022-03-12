using FluentHub.Models.Items;
using FluentHub.ViewModels.Repositories;
using FluentHub.ViewModels.UserControls.Blocks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Views.Repositories.Layouts
{
    public sealed partial class DetailsLayoutView : Page
    {
        private CommonRepoViewModel CommonRepoViewModel { get; set; }

        public DetailsLayoutView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            CommonRepoViewModel = e.Parameter as CommonRepoViewModel;

            LatastCommitBlock.CommonRepoViewModel = CommonRepoViewModel;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.CommonRepoViewModel = CommonRepoViewModel;

            if (CommonRepoViewModel.IsFile == true)
            {
                DirListViewLoadingProgreeBar.IsIndeterminate = false;
                DirListViewLoadingProgreeBar.Visibility = Visibility.Collapsed;
                return;
            }

            await ViewModel.EnumRepositoryContents();

            if (RepoReadmeBlock != null)
            {
                RepoReadmeBlock.RepositoryId = CommonRepoViewModel.RepositoryId;
            }

            DirListViewLoadingProgreeBar.IsIndeterminate = false;
            DirListViewLoadingProgreeBar.Visibility = Visibility.Collapsed;
        }

        private void DirListView_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            var commonRepoViewModel = new CommonRepoViewModel()
            {
                Name = CommonRepoViewModel.Name,
                Owner = CommonRepoViewModel.Owner,
                RepositoryId = CommonRepoViewModel.RepositoryId,
                IsRootDir = false,
            };

            DetailsLayoutListViewItem item = DirListView.SelectedItem as DetailsLayoutListViewItem;

            var tagItem = item.ObjectTag.Split("/");

            if (tagItem[0] != "tree")
            {
                commonRepoViewModel.IsFile = true;
                commonRepoViewModel.Path = CommonRepoViewModel.Path + tagItem[1];
            }
            else // tree
            {
                commonRepoViewModel.Path = CommonRepoViewModel.Path + tagItem[1] + "/";
            }

            App.MainViewModel.RepoMainFrame.Navigate(typeof(CodePage), commonRepoViewModel);
        }
    }
}
