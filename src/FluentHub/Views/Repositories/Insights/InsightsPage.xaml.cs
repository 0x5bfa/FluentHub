using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;
using FluentHub.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FluentHub.Views.Repositories.Insights
{
    public sealed partial class InsightsPage : Page
    {
        public InsightsPage()
        {
            this.InitializeComponent();

            var provider = App.Current.Services;
            navigationService = provider.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var nameWithOwner = e.Parameter as string;
            var nameAndOwner = nameWithOwner.Split("/");

            var queries = new Octokit.Queries.Repositories.InsightQueries();
            await queries.GetContributors(nameAndOwner[0], nameAndOwner[1]);

            var url = e.Parameter as string;
            var urlSegments = url.Split("/");

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Insights";
            currentItem.Description = "Insights";
            currentItem.Url = $"{url}";
            currentItem.DisplayUrl = $"{urlSegments[0]} / {urlSegments[1]} / Insights";
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Insights.targetsize-96.png"))
            };
        }
    }
}
