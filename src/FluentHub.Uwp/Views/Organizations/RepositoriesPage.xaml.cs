using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.Organizations;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.Views.Organizations
{
    public sealed partial class RepositoriesPage : Page
    {
        public RepositoriesPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<RepositoriesViewModel>();
            navigationService = provider.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;
        public RepositoriesViewModel ViewModel { get; }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var param = e.Parameter as Models.FrameNavigationArgs;
            ViewModel.Login = param.Login;

            await ViewModel.LoadOrganizationAsync(param.Login);

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = $"Repositories";
            currentItem.Description = $"{param.Login}'s repositories";
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Repositories.png"))
            };

            var command = ViewModel.RefreshRepositoriesCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }
    }
}
