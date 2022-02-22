using FluentHub.ViewModels.Repositories;
using FluentHub.ViewModels.UserControls.Repository;
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

            if (CommonRepoViewModel.IsFile == true) return;

            var ItemsCount = await ViewModel.EnumRepositoryContents();

            if (RepoReadmeBlock != null)
            {
                RepoReadmeBlock.RepositoryId = CommonRepoViewModel.RepositoryId;
            }
        }

        private void DirListView_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            var commonRepoViewModel = new CommonRepoViewModel();

            commonRepoViewModel.RepositoryId = CommonRepoViewModel.RepositoryId;
            commonRepoViewModel.IsRootDir = false;

            var clickedItem = e.OriginalSource as FrameworkElement;

            var grid = clickedItem.FindName("ListViewItemGrid") as Grid;

            if (grid == null || grid.Tag == null) return;

            string tag = grid.Tag.ToString();

            var tagItem = tag.Split("/");

            if (tagItem[0] != "dir")
            {
                commonRepoViewModel.IsFile = true;
                commonRepoViewModel.Path = CommonRepoViewModel.Path + tagItem[1];
            }
            else
            {
                commonRepoViewModel.Path = CommonRepoViewModel.Path + tagItem[1] + "/";
            }

            App.MainViewModel.RepoMainFrame.Navigate(typeof(CodePage), commonRepoViewModel);
        }
    }
}
