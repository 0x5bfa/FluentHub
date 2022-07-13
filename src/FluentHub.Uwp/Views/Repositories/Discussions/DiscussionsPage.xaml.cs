using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.Repositories.Discussions;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.Views.Repositories.Discussions
{
    public sealed partial class DiscussionsPage : Page
    {
        public DiscussionsPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<DiscussionsViewModel>();
            navigationService = provider.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;
        public DiscussionsViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var url = e.Parameter as string;
            var uri = new Uri(url);
            var pathSegments = uri.AbsolutePath.Split("/").ToList();
            pathSegments.RemoveAt(0);

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Discussions";
            currentItem.Description = "Discussions";
            currentItem.Url = url;
            currentItem.DisplayUrl = $"{pathSegments[0]} / {pathSegments[1]} / Discussions";
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Discussions.png"))
            };

            var command = ViewModel.LoadDiscussionsPageCommand;
            if (command.CanExecute($"{pathSegments[0]}/{pathSegments[1]}"))
                command.Execute($"{pathSegments[0]}/{pathSegments[1]}");
        }
    }
}
