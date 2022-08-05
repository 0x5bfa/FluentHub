using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.Users;
using Microsoft.Extensions.DependencyInjection;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.Views.Users
{
    public sealed partial class IssuesPage : Page
    {
        public IssuesPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<IssuesViewModel>();
            navigationService = provider.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;
        public IssuesViewModel ViewModel { get; }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var param = e.Parameter as Models.FrameNavigationArgs;
            ViewModel.Login = param.Login;
            ViewModel.DisplayTitle = true;

            await ViewModel.LoadUserAsync(param.Login);

            SetCurrentTabItem();

            var command = ViewModel.RefreshIssuesPageCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }

        private void SetCurrentTabItem()
        {
            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Issues";
            currentItem.Description = $"{ViewModel.User.Login}'s issues";
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Issues.png"))
            };
        }
    }
}
