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

        public MainSearchViewModel ViewModel { get; }
        
        // protected override void OnNavigatedTo(NavigationEventArgs e)
        // {
        //     var url = e.Parameter as string;
        //     var uri = new Uri(url);
        //     var pathSegments = url.Split("/").ToList();
        //     pathSegments.RemoveRange(0, 3);
        //
        //     var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
        //     currentItem.Header = "Search";
        //     currentItem.Description = "FluentHub search";
        //     currentItem.Url = "fluenthub://search";
        //     currentItem.DisplayUrl = "Search";
        //     currentItem.Icon = new muxc.ImageIconSource
        //     {
        //         ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Settings.png"))
        //     };
        //
        //     var defaultItem
        //         = SettingsNavView
        //             .MenuItems
        //             .OfType<muxc.NavigationViewItem>()
        //             .FirstOrDefault();
        //
        //     SettingsNavView.SelectedItem
        //         = SettingsNavView
        //               .MenuItems
        //               .Concat(SettingsNavView.FooterMenuItems)
        //               .OfType<muxc.NavigationViewItem>()
        //               .FirstOrDefault(x => string.Compare(x.Tag.ToString(), pathSegments.FirstOrDefault(), true) == 0)
        //           ?? defaultItem;
        //     
        //     var command = ViewModel.LoadSignedInLoginsCommand;
        //     if (command.CanExecute(null))
        //         command.Execute(null);
        // }
    }
}