using FluentHub.Helpers;
using FluentHub.Views.Home;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            App.MainViewModel.MainFrame.Navigating += ViewModelMainFrame_Navigating;
            CoreApplication.GetCurrentView().TitleBar.LayoutMetricsChanged += TitleBar_LayoutMetricsChanged;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            // Configure Jumplist
            //JumpListHelper.AddToJumpList("FluentHub", "ms-appx:///Assets/AppTiles/StoreLogo.png", "", "FluentHub", "FluentHub");
            await JumpListHelper.AddToJumpListAsync("Profile", "ms-appx:///Assets/AppTiles/StoreLogo.png", "Profile", "", "Profile");
            await JumpListHelper.AddToJumpListAsync("Notifications", "ms-appx:///Assets/AppTiles/StoreLogo.png", "Notifications", "", "Profile");
            await JumpListHelper.AddToJumpListAsync("Activities", "ms-appx:///Assets/AppTiles/StoreLogo.png", "Activities", "", "Profile");

            await JumpListHelper.AddToJumpListAsync("Issues", "ms-appx:///Assets/AppTiles/StoreLogo.png", "Issues", "", "My Work");
            await JumpListHelper.AddToJumpListAsync("Pull Requests", "ms-appx:///Assets/AppTiles/StoreLogo.png", "Pull Requests", "", "My Work");
            await JumpListHelper.AddToJumpListAsync("Discussions", "ms-appx:///Assets/AppTiles/StoreLogo.png", "Discussions", "", "My Work");
            await JumpListHelper.AddToJumpListAsync("Repositories", "ms-appx:///Assets/AppTiles/StoreLogo.png", "Repositories", "", "My Work");
            await JumpListHelper.AddToJumpListAsync("Organizations", "ms-appx:///Assets/AppTiles/StoreLogo.png", "Organizations", "", "My Work");
            await JumpListHelper.AddToJumpListAsync("Starred", "ms-appx:///Assets/AppTiles/StoreLogo.png", "Starred", "", "My Work");

        }

        private void TitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
        {
            RightPaddingColumn.Width = new GridLength(sender.SystemOverlayRightInset);
        }

        private void ViewModelMainFrame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            MainFrame.Navigate(e.SourcePageType, e.Parameter);

            e.Cancel = true;
        }

        private void DragArea_Loaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SetTitleBar(DragArea);
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            App.MainViewModel.MainFrame.Navigate(typeof(UserHomePage));
        }

        private void ToolbarAppSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            App.MainViewModel.MainFrame.Navigate(typeof(AppSettings.MainSettingsPage));
        }
    }
}
