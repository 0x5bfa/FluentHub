﻿using FluentHub.Uwp.Services;
using FluentHub.Uwp.Utils;
using FluentHub.Uwp.ViewModels.Home;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.Views.Home
{
    public sealed partial class NotificationsPage : Page
    {
        public NotificationsPage()
        {
            var provider = App.Current.Services;
            logger = provider.GetRequiredService<Utils.ILogger>();

            try
            {
                InitializeComponent();

                ViewModel = provider.GetRequiredService<NotificationsViewModel>();
                navigationService = provider.GetRequiredService<INavigationService>();
            }
            catch (Exception ex)
            {
                logger?.Error(nameof(NotificationsPage), ex);
                throw;
            }
        }

        private readonly INavigationService navigationService;
        public NotificationsViewModel ViewModel { get; }
        private readonly ILogger logger;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            logger?.Info("Entered OnNavigatedTo()");

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Notifications";
            currentItem.Description = "Viewer's notifications";
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Notifications.png"))
            };

            var command = ViewModel.RefreshNotificationsCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }

        private async void OnScrollViewerViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            var scrollViewer = (ScrollViewer)sender;
            if (scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
            {
                await ViewModel.LoadFurtherNotificationsAsync();
            }
        }
    }
}
