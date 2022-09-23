using FluentHub.Uwp.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks.Search;

namespace FluentHub.Uwp.UserControls.ButtonBlocks.Search
{
    public sealed partial class SearchUserButtonBlock : UserControl
    {
        #region properties

        public static readonly DependencyProperty ViewModelProperty
            = DependencyProperty.Register(
                nameof(NotificationButtonBlockViewModel),
                typeof(NotificationButtonBlockViewModel),
                typeof(NotificationButtonBlock),
                new PropertyMetadata(null)
            );

        public SearchUserButtonBlockViewModel ViewModel
        {
            get => (SearchUserButtonBlockViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        #endregion

        public SearchUserButtonBlock() => InitializeComponent();

        private void OnClick(object sender, RoutedEventArgs e)
        {
            var navService = App.Current.Services.GetRequiredService<INavigationService>();
            var param = new FrameNavigationArgs()
            {
                Login = ViewModel.Item.Username,
            };

            navService.Navigate<Views.Users.OverviewPage>(param);
        }
    }
}
