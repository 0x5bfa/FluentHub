using FluentHub.Uwp.Services;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;
using FluentHub.Uwp.ViewModels.Search;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.Views.Search
{
    public sealed partial class RepoPage : Page
    {
        public RepoPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<RepoViewModel>();
            navigationService = provider.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;

        private string query;
        
        public RepoViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null) {
                query = e.Parameter.ToString();
                ViewModel.query = query;
            }
            else
            {
                throw new Exception("Searching without a query");
            }
            var command = ViewModel.LoadSearchResultsPageCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }
        
        private void OnScrollViewerViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            var scrollViewer = (ScrollViewer)sender;
            if (scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
            {
                var command = ViewModel.LoadFurtherSearchResultsPageCommand;
                if (command.CanExecute(null))
                    command.Execute(null);
            }
        }
    }
}
