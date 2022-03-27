using FluentHub.Models.Items;
using FluentHub.Services;
using FluentHub.ViewModels.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using muxc = Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using FluentHub.Services.Navigation;

namespace FluentHub.Views.Repositories.Layouts
{
    public sealed partial class DetailsLayoutView : Page
    {
        private CommonRepoViewModel CommonRepoViewModel { get; set; }

        public DetailsLayoutView()
        {
            this.InitializeComponent();
            navigationService = App.Current.Services.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            CommonRepoViewModel = e.Parameter as CommonRepoViewModel;
            ViewModel.CommonRepoViewModel = CommonRepoViewModel;

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = $"{CommonRepoViewModel.Path.Remove(0, 1)} at {CommonRepoViewModel.BranchName} • {CommonRepoViewModel.Owner}/{CommonRepoViewModel.Name}";
            currentItem.Description = "{org}'s overview";
            currentItem.Url = $"https://github.com/{CommonRepoViewModel.Owner}/{CommonRepoViewModel.Name}/tree/{CommonRepoViewModel.BranchName}{CommonRepoViewModel.Path}";
            currentItem.Icon = new muxc.FontIconSource
            {
                Glyph = "\uEA52",
                FontFamily = new Windows.UI.Xaml.Media.FontFamily("/Assets/Glyphs/Octions.ttf#octions")
            };
        }

        private async void OnPageLoaded(object sender, RoutedEventArgs e)
        {
            if (CommonRepoViewModel.IsFile == true)
            {
                DirListViewLoadingProgreeBar.IsIndeterminate = false;
                DirListViewLoadingProgreeBar.Visibility = Visibility.Collapsed;
                return;
            }

            await ViewModel.EnumRepositoryContents();

            DirListViewLoadingProgreeBar.IsIndeterminate = false;
            DirListViewLoadingProgreeBar.Visibility = Visibility.Collapsed;
        }

        private void DirListView_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            var commonRepoViewModel = new CommonRepoViewModel()
            {
                Name = CommonRepoViewModel.Name,
                Owner = CommonRepoViewModel.Owner,
                BranchName = CommonRepoViewModel.BranchName,
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
            navigationService.Navigate<CodePage>(commonRepoViewModel);
            //App.MainViewModel.RepoMainFrame.Navigate(typeof(CodePage), commonRepoViewModel);
        }
    }
}