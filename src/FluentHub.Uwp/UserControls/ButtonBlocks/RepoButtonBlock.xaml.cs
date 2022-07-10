using FluentHub.Octokit.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;
using FluentHub.Uwp.Views.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FluentHub.Uwp.UserControls.ButtonBlocks
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
            set
            {
                SetValue(ViewModelProperty, value);
                ViewModel?.GetColorBrush();
            }
        }
        #endregion

        public RepoButtonBlock() => InitializeComponent();

        private void OnClick(object sender, RoutedEventArgs e)
        {
            var service = App.Current.Services.GetRequiredService<INavigationService>();
            service.Navigate<OverviewPage>($"{App.DefaultGitHubDomain}/{ViewModel.Item.Owner.Login}/{ViewModel.Item.Name}");
        }
    }
}
