using FluentHub.Models.Items;
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

namespace FluentHub.Views.Repositories.Codes
{
    public sealed partial class ReleasesPage : Page
    {
        public ReleasesPage()
        {
            this.InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<ReleasesViewModel>();
            navigationService = App.Current.Services.GetRequiredService<INavigationService>();
        }

        public ReleasesViewModel ViewModel { get; }
        private readonly INavigationService navigationService;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.ContextViewModel = ViewModel.ContextViewModel = e.Parameter as RepoContextViewModel;
            DataContext = ViewModel;

            #region tabitem
            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = $"Releases · {ViewModel.ContextViewModel.Owner}/{ViewModel.ContextViewModel.Name}";
            currentItem.Description = currentItem.Header;
            currentItem.Url = $"https://github.com/{ViewModel.ContextViewModel.Owner}/{ViewModel.ContextViewModel.Name}/releases";
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Repositories.png"))
            };
            #endregion

            var command = ViewModel.LoadReleasesPageCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }

        private void OnReleaseListViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item in e.AddedItems)
            {
                ListViewItem lvi = (sender as ListView).ContainerFromItem(item) as ListViewItem;
                lvi.ContentTemplate = (DataTemplate)this.Resources["ExpandedReleaseListViewItem"];
            }
            foreach (var item in e.RemovedItems)
            {
                ListViewItem lvi = (sender as ListView).ContainerFromItem(item) as ListViewItem;
                lvi.ContentTemplate = (DataTemplate)this.Resources["CollapsedReleaseListViewItem"];
            }
        }

        private async void OnDescriptionWebViewLoaded(object sender, RoutedEventArgs e)
        {
            Octokit.Queries.Repositories.MarkdownQueries queries = new();
            var missedPath = $"https://github.com/{ViewModel.ContextViewModel.Owner}/{ViewModel.ContextViewModel.Name}/releases";
            var fullHtml = await queries.GetHtmlAsync(ViewModel.Items[ReleaseListView.SelectedIndex].DescriptionHTML, missedPath, Helpers.ThemeHelper.ActualTheme.ToString().ToLower(), true);

            (sender as WebView).NavigateToString(fullHtml);
        }
    }
}
