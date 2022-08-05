using FluentHub.Uwp.Models;
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

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var param = e.Parameter as FrameNavigationArgs;
            ViewModel.Number = param.Number;

            await ViewModel.LoadRepositoryAsync(param.Login, param.Name);

            SetCurrentTabItem();

            var command = ViewModel.LoadProjectPageCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }

        private void SetCurrentTabItem()
        {
            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Project";
            currentItem.Description = "Project";
            currentItem.Icon = new Microsoft.UI.Xaml.Controls.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Projects.png"))
            };
        }
    }
}
