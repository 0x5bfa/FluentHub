using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.Users;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.Views.Users
{
    public sealed partial class OverviewPage : Page
    {
        public OverviewPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<OverviewViewModel>();
            navigationService = provider.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;
        public OverviewViewModel ViewModel { get; }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var param = e.Parameter as Models.FrameNavigationArgs;
            ViewModel.Login = param.Login;

            ViewModel.ContextViewModel = new ViewModels.Repositories.RepoContextViewModel()
            {
                Repository = new()
                {
                    Name = ViewModel.Login,

                    Owner = new RepositoryOwner()
                    {
                        Login = ViewModel.Login,
                    },
                }
            };

            await ViewModel.LoadUserAsync(param.Login);

            SetCurrentTabItem();

            var command = ViewModel.RefreshRepositoryCommand;
            if (command.CanExecute(param.Login))
                command.Execute(param.Login);
        }

        private void SetCurrentTabItem()
        {
            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = $"{ViewModel.User.Login}";
            currentItem.Description = $"{ViewModel.User.Login}";
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Profile.png"))
            };
        }

        private async void OnEditPinnedReposButtonClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var dialogs = new Dialogs.EditPinnedRepositoriesDialog(ViewModel.Login);
            _ = await dialogs.ShowAsync();
        }
    }
}
