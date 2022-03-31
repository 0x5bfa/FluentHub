using FluentHub.Services;
using FluentHub.ViewModels.Users;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Views.Users
{
    public sealed partial class FollowingPage : Page
    {
        public FollowingPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<FollowingViewModel>();
            navigationService = provider.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;
        public FollowingViewModel ViewModel { get; }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            DataContext = e.Parameter;

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = $"Following";
            currentItem.Description = $"{DataContext}'s followers";
            currentItem.Url = $"https://github.com/{DataContext}?tab=following";
            currentItem.Icon = new Microsoft.UI.Xaml.Controls.FontIconSource
            {
                Glyph = "\uE737"
            };

            var command = ViewModel.RefreshFollowingCommand;
            if (command.CanExecute(DataContext))
                await command.ExecuteAsync(DataContext);
        }
    }
}