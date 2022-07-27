using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.UserControls;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.UserControls
{
    public sealed partial class UserProfileOverview : UserControl
    {
        #region propdp
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                nameof(ViewModel),
                typeof(UserProfileOverviewViewModel),
                typeof(UserProfileOverviewViewModel),
                new PropertyMetadata(null));

        public UserProfileOverviewViewModel ViewModel
        {
            get => (UserProfileOverviewViewModel)GetValue(ViewModelProperty);
            set
            {
                SetValue(ViewModelProperty, value);
                if (ViewModel is not null)
                    SelectItemByTag(ViewModel.SelectedTag);
            }
        }
        #endregion

        public UserProfileOverview() => InitializeComponent();

        private void SelectItemByTag(string tag)
        {
            var defaultItem
                = UserProfileNavView
                .MenuItems
                .OfType<muxc.NavigationViewItem>()
                .FirstOrDefault();

            UserProfileNavView.SelectedItem
                = UserProfileNavView
                .MenuItems
                .OfType<muxc.NavigationViewItem>()
                .FirstOrDefault(x => string.Compare(x.Tag.ToString(), tag?.ToString(), true) == 0)
                ?? defaultItem;
        }

        private void OnUserNavViewItemInvoked(muxc.NavigationView sender, muxc.NavigationViewItemInvokedEventArgs args)
        {
        }

        private void UserFollowersButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void UserFollowingButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private async void EditProfileButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Dialogs.UserProfileEditor();
            _ = await dialog.ShowAsync();
        }

        private void LocationHyperlink()
        {
            var LocationHyperlink = "https://www.bing.com/maps?q=" + ViewModel.User.Location;
        }
    }
}
