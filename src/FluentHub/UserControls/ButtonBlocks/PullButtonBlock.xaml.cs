using FluentHub.Services;
using FluentHub.ViewModels.UserControls.ButtonBlocks;
using FluentHub.Views.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FluentHub.UserControls.ButtonBlocks
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

        public PullButtonBlock()
        {
            InitializeComponent();
            navigationService = App.Current.Services.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;

        private void OnClick(object sender, RoutedEventArgs e)
        {
            navigationService.Navigate<OverviewPage>($"{App.DefaultGitHubDomain}/{ViewModel.PullItem.OwnerLogin}/{ViewModel.PullItem.Name}/pull/{ViewModel.PullItem.Number}");
        }
    }
}
