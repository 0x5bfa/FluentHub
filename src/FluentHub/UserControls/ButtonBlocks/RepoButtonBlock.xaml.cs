using FluentHub.Octokit.Models;
using FluentHub.Services;
using FluentHub.ViewModels.UserControls.ButtonBlocks;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FluentHub.UserControls.ButtonBlocks
{
    public sealed partial class RepoButtonBlock : UserControl
    {
        #region propdp
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                nameof(ViewModel),
                typeof(RepoButtonBlockViewModel),
                typeof(RepoButtonBlock),
                new PropertyMetadata(null));

        public RepoButtonBlockViewModel ViewModel
        {
            get => (RepoButtonBlockViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
        #endregion

        public RepoButtonBlock() => InitializeComponent();

        private void OnRepoButtonBlockLoaded(object sender, RoutedEventArgs e)
        {
            ViewModel.GetColorBrush();
        }

        private void RepoBlockButton_Click(object sender, RoutedEventArgs e)
        {
            var service = App.Current.Services.GetRequiredService<INavigationService>();
            service.Navigate<Views.Repositories.OverviewPage>(ViewModel.Item);
        }
    }
}
