using FluentHub.Uwp.Services;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;
using FluentHub.Uwp.ViewModels.Search;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.Views.Search
{
    public sealed partial class IssuePage : Page
    {
        public IssuePage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<IssueViewModel>(); // TODO: Change View Model to Issue
            navigationService = provider.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;

        private string query;
        
        public IssueViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
                query = e.Parameter.ToString();
            else
            {
                throw new Exception("Searching without a query");
            }
            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Issue Results";
            currentItem.Description = "Issue Results for \"" + query + "\"";
            currentItem.Url = "fluenthub://search/issues/" + query.Replace(" ", "&");
            currentItem.DisplayUrl = $"Search / issues";
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Search.png"))
            };
        }
    }
}
