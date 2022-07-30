using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.Users;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.Views.Users
{
    public sealed partial class StarredReposPage : Page
    {
        public StarredReposPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<StarredReposViewModel>();
            navigationService = provider.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;
        public StarredReposViewModel ViewModel { get; }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var param = e.Parameter as Models.FrameNavigationArgs;
            ViewModel.Login = param.Login;

            if (param.Parameters.ElementAtOrDefault(0) as string is "AsViewer")
            {
                ViewModel.DisplayTitle = true;
            }

            await ViewModel.LoadUserAsync(param.Login);

            SetCurrentTabItem();

            var command = ViewModel.RefreshRepositoriesCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }

        private void SetCurrentTabItem()
        {
            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = $"Stars";
            currentItem.Description = $"{ViewModel.Login}'s stars";
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Starred.png"))
            };
        }
    }
}
