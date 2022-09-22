using FluentHub.Uwp.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks.Search;

namespace FluentHub.Uwp.UserControls.ButtonBlocks.Search
{
    public sealed partial class SearchIssueButtonBlock : UserControl
    {
        #region propdp

        public static readonly DependencyProperty ViewModelProperty
            = DependencyProperty.Register(
                nameof(NotificationButtonBlockViewModel),
                typeof(NotificationButtonBlockViewModel),
                typeof(NotificationButtonBlock),
                new PropertyMetadata(null)
            );

        public SearchIssueButtonBlockViewModel ViewModel
        {
            get => (SearchIssueButtonBlockViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        #endregion

        public SearchIssueButtonBlock() => InitializeComponent();

        private void OnClick(object sender, RoutedEventArgs e)
        {
            var navService = App.Current.Services.GetRequiredService<INavigationService>();
            var param = new FrameNavigationArgs()
            {
                Login = ViewModel.Item.Repo.Owner.ToString(),
                Name = ViewModel.Item.Repo.Name,
                Number = ViewModel.Item.Id
            };
            navService.Navigate<Views.Repositories.Issues.IssuePage>(param);
        }
    }
}
