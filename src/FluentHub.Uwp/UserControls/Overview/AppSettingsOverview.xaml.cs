using FluentHub.Uwp.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.UserControls.Overview;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.UserControls.Overview
{
    public sealed partial class AppSettingsOverview : UserControl
    {
        #region propdp
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                nameof(ViewModel),
                typeof(AppSettingsOverviewViewModel),
                typeof(AppSettingsOverviewViewModel),
                new PropertyMetadata(null));

        public AppSettingsOverviewViewModel ViewModel
        {
            get => (AppSettingsOverviewViewModel)GetValue(ViewModelProperty);
            set
            {
                SetValue(ViewModelProperty, value);
                if (ViewModel is not null)
                    SelectItemByTag(ViewModel.SelectedTag);
            }
        }
        #endregion

        public AppSettingsOverview()
        {
            InitializeComponent();

            _navigation = App.Current.Services.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService _navigation;

        private void SelectItemByTag(string tag)
        {
            var defaultItem
                = SettingsNavView
                .MenuItems
                .OfType<NavigationViewItem>()
                .FirstOrDefault();

            SettingsNavView.SelectedItem
                = SettingsNavView
                .MenuItems
                .OfType<NavigationViewItem>()
                .FirstOrDefault(x => string.Compare(x.Tag.ToString(), tag?.ToString(), true) == 0)
                ?? defaultItem;
        }

        private void OnSettingsNavViewItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            // Parameter (should not use)
            Models.FrameNavigationArgs parameter = new()
            {
                Login = ViewModel?.User?.Login,
            };

            switch (args.InvokedItemContainer.Tag.ToString().ToLower())
            {
                case "appearance":
                    _navigation.Navigate<Views.AppSettings.AppearancePage>();
                    break;
                case "repository":
                    _navigation.Navigate<Views.AppSettings.RepositoryPage>();
                    break;
                case "notifications":
                    _navigation.Navigate<Views.AppSettings.NotificationsPage>();
                    break;
                case "about":
                    _navigation.Navigate<Views.AppSettings.AboutPage>();
                    break;
            }
        }
    }
}
