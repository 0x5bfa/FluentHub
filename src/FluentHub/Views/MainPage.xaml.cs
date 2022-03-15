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
            InitializeComponent();

            App.MainViewModel.MainFrame.Navigating += ViewModelMainFrame_Navigating;
            CoreApplication.GetCurrentView().TitleBar.LayoutMetricsChanged += TitleBar_LayoutMetricsChanged;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            // Configure Jumplist
            await JumpListHelper.ConfigureDefaultJumpListAsync();
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
