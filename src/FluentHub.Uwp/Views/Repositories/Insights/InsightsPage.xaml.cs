using FluentHub.Uwp.Services;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.Views.Repositories.Insights
{
    public sealed partial class InsightsPage : Page
    {
        public InsightsPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            navigationService = provider.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var url = e.Parameter as string;
            var uri = new Uri(url);
            var pathSegments = uri.AbsolutePath.Split("/").ToList();
            pathSegments.RemoveAt(0);

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Insights";
            currentItem.Description = "Insights";
            currentItem.Url = url;
            currentItem.DisplayUrl = $"{pathSegments[0]} / {pathSegments[1]} / Insights";
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Insights.png"))
            };
        }

        private void OnInsightsNavViewItemInvoked(muxc.NavigationView sender, muxc.NavigationViewItemInvokedEventArgs args)
            => OnInsightsNavViewItemSelected(args.InvokedItemContainer.Tag.ToString().ToLower());

        private void OnInsightsNavViewItemSelected(string tag)
        {
            //string newUrl = $"{App.DefaultGitHubDomain}/{ViewModel.Repository.Owner.Login}/{ViewModel.Repository.Name}";

            switch (tag.ToLower())
            {
                default:
                case "overview":
                    InsightsNavViewFrame.Navigate(typeof(OverviewPage));
                    break;
                case "contributors":
                    InsightsNavViewFrame.Navigate(typeof(ContributorsPage));
                    break;
                case "traffic":
                    InsightsNavViewFrame.Navigate(typeof(TrafficPage));
                    break;
                case "commits":
                    InsightsNavViewFrame.Navigate(typeof(CommitsPage));
                    break;
                case "codefrequency":
                    InsightsNavViewFrame.Navigate(typeof(CodeFrequencyPage));
                    break;
            }
        }
    }
}
