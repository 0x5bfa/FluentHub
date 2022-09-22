using FluentHub.Uwp.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks.Search;

namespace FluentHub.Uwp.UserControls.ButtonBlocks.Search
{
    public sealed partial class SearchRepoButtonBlock : UserControl
    {
        #region propdp

        public static readonly DependencyProperty ViewModelProperty
            = DependencyProperty.Register(
                nameof(NotificationButtonBlockViewModel),
                typeof(NotificationButtonBlockViewModel),
                typeof(NotificationButtonBlock),
                new PropertyMetadata(null)
            );

        public SearchRepoButtonBlockViewModel ViewModel
        {
            get => (SearchRepoButtonBlockViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        #endregion

        public SearchRepoButtonBlock() => InitializeComponent();

        private void OnClick(object sender, RoutedEventArgs e)
        {
            var navService = App.Current.Services.GetRequiredService<INavigationService>();
            navService.Navigate<Views.Repositories.Code.Layouts.TreeLayoutView>(new FrameNavigationArgs()
            {
                Login = ViewModel.Item.Owner,
                Name = ViewModel.Item.Name
            });
        }
    }
}
