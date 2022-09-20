using FluentHub.Uwp.Services;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;
using FluentHub.Uwp.ViewModels.Search;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.Views.Search
{
    public sealed partial class UserPage : Page
    {
        public UserPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<UserViewModel>();
            navigationService = provider.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;
        private string query;
        public UserViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
                query = e.Parameter.ToString();
            else
            {
                throw new Exception("Searching without a query");
            }
            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Code Results";
            currentItem.Description = "Code Results for \"" + query + "\"";
            currentItem.Url = "fluenthub://search/code/" + query.Replace(" ", "&");
            currentItem.DisplayUrl = $"Search / Code";
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Search.png"))
            };
        }
    }
}
