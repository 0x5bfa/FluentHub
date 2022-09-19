using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.Search;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.Views.Search
{
    public sealed partial class MainSearchPage : Page
    {
        public MainSearchPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<MainSearchViewModel>();
            navigationService = App.Current.Services.GetRequiredService<INavigationService>();
        }
        private readonly INavigationService navigationService;
        private string query;
        private MainSearchViewModel ViewModel { get; }
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
                query = e.Parameter.ToString();
            else
            {
                throw new Exception("Searching without a query");
            }
            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Search";
            currentItem.Description = "FluentHub search";
            currentItem.Url = "fluenthub://search/";
            currentItem.DisplayUrl = "Search";
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Settings.png"))
            };

            var command = ViewModel.LoadSignedInLoginsCommand;
            if (command.CanExecute(null))
                command.Execute(null);
            SearchNavView.SelectedItem = SearchNavView.MenuItems.First();
            SearchContentFrame.Navigate(typeof(RepoPage));
        }

        private void SearchNavView_OnSelectionChanged(muxc.NavigationView sender, muxc.NavigationViewSelectionChangedEventArgs args)
        {
            var navigateTo = sender.SelectedItem.ToString();
            var pageType = typeof(RepoPage);
            if (args.SelectedItem == RepositoriesItem)
            {
                pageType = typeof(RepoPage);
            }
            else if (args.SelectedItem == CodeItem)
            {
                pageType = typeof(CodePage);
            }
            SearchContentFrame.Navigate(pageType, query);

        }
    }
}