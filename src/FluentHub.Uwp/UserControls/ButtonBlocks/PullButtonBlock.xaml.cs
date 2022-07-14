using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;
using FluentHub.Uwp.Views.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FluentHub.Uwp.UserControls.ButtonBlocks
{
    public sealed partial class PullButtonBlock : UserControl
    {
        #region propdp
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                  nameof(PullRequest),
                  typeof(PullButtonBlockViewModel),
                  typeof(PullButtonBlock),
                  new PropertyMetadata(null)
                );

        public PullButtonBlockViewModel ViewModel
        {
            get => (PullButtonBlockViewModel)GetValue(ViewModelProperty);
            set
            {
                SetValue(ViewModelProperty, value);
                ViewModel?.LoadContents();
            }
        }
        #endregion

        public PullButtonBlock() => InitializeComponent();

        private void OnClick(object sender, RoutedEventArgs e)
        {
            var navigationService = App.Current.Services.GetRequiredService<INavigationService>();
            navigationService.Navigate<OverviewPage>($"{App.DefaultGitHubDomain}/{ViewModel.PullItem.Repository.Owner.Login}/{ViewModel.PullItem.Repository.Name}/pull/{ViewModel.PullItem.Number}");
        }
    }
}
