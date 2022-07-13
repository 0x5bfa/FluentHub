using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.Repositories.Projects;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.Views.Repositories.Projects
{
    public sealed partial class ProjectPage : Page
    {
        public ProjectPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<ProjectViewModel>();
            navigationService = provider.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;
        public ProjectViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var url = e.Parameter as string;
            var pathSegments = url.Split("/");

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Project";
            currentItem.Description = "Project";
            currentItem.Url = $"{url}";
            currentItem.DisplayUrl = $"{pathSegments[3]} / {pathSegments[4]} / Projects / {pathSegments[6]}";
            currentItem.Icon = new Microsoft.UI.Xaml.Controls.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Projects.png"))
            };

            var command = ViewModel.LoadProjectPageCommand;
            if (command.CanExecute(url))
                command.Execute(url);
        }
    }
}
