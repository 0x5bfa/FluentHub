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
        private RepoContextViewModel ContextViewModel { get; set; }

        public DetailsLayoutView()
        {
            this.InitializeComponent();
            navigationService = App.Current.Services.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ContextViewModel = e.Parameter as RepoContextViewModel;
            ViewModel.ContextViewModel = ContextViewModel;

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;

            string header;
            if (string.IsNullOrEmpty(ContextViewModel.Path.Remove(0, 1)))
            {
                // Root
                header = $"{ContextViewModel.Owner}/{ContextViewModel.Name}";
            }
            else
            {
                header = $"{ContextViewModel.Name}/{ContextViewModel.Path.Remove(0, 1)} at {ContextViewModel.BranchName} • {ContextViewModel.Owner}/{ContextViewModel.Name}";
            }

            currentItem.Header = header;
            currentItem.Description = currentItem.Header;

            string url;
            if (ContextViewModel.IsFile)
            {
                url = $"https://github.com/{ContextViewModel.Owner}/{ContextViewModel.Name}/blob/{ContextViewModel.BranchName}{ContextViewModel.Path}";
            }
            else
            {
                url = $"https://github.com/{ContextViewModel.Owner}/{ContextViewModel.Name}/tree/{ContextViewModel.BranchName}{ContextViewModel.Path}";
            }

            currentItem.Url = url;
            currentItem.Icon = new muxc.FontIconSource
            {
                Glyph = "\uEA52",
                FontFamily = new Windows.UI.Xaml.Media.FontFamily("/Assets/Glyphs/Octions.ttf#octions")
            };
        }

        private async void OnPageLoaded(object sender, RoutedEventArgs e)
        {
            if (ContextViewModel.IsFile == true)
            {
                DirListViewLoadingProgressBar.IsIndeterminate = false;
                DirListViewLoadingProgressBar.Visibility = Visibility.Collapsed;
                return;
            }

            await ViewModel.EnumRepositoryContents();

            DirListViewLoadingProgressBar.IsIndeterminate = false;
            DirListViewLoadingProgressBar.Visibility = Visibility.Collapsed;
        }

        private void DirListView_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            var repoContextViewModel = new RepoContextViewModel()
            {
                Repository = ContextViewModel.Repository,
                Name = ContextViewModel.Name,
                Owner = ContextViewModel.Owner,
                BranchName = ContextViewModel.BranchName,
                IsRootDir = false,
            };

            DetailsLayoutListViewItem item = DirListView.SelectedItem as DetailsLayoutListViewItem;

            var tagItem = item.ObjectTag.Split("/");

            if (tagItem[0] != "tree")
            {
                repoContextViewModel.IsFile = true;
                repoContextViewModel.Path = ContextViewModel.Path + tagItem[1];
            }
            else // tree
            {
                repoContextViewModel.Path = ContextViewModel.Path + tagItem[1] + "/";
            }

            navigationService.Navigate<CodePage>(repoContextViewModel);
        }
    }
}