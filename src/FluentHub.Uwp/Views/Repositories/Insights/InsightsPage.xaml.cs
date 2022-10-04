using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
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
            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Insights";
            currentItem.Description = "Insights";
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
