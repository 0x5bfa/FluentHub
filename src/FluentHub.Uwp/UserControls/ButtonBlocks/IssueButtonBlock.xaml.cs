using FluentHub.Octokit.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;
using FluentHub.Uwp.Views.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FluentHub.Uwp.UserControls.ButtonBlocks
{
    public sealed partial class IssueButtonBlock : UserControl
    {
        #region propdp
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                  nameof(IssueButtonBlockViewModel),
                  typeof(IssueButtonBlockViewModel),
                  typeof(IssueButtonBlock),
                  new PropertyMetadata(null)
                );

        public IssueButtonBlockViewModel ViewModel
        {
            get => (IssueButtonBlockViewModel)GetValue(ViewModelProperty);
            set
            {
                SetValue(ViewModelProperty, value);
                ViewModel?.LoadContents();
            }
        }
        #endregion
       
        public IssueButtonBlock() => InitializeComponent();

        private void OnClick(object sender, RoutedEventArgs e)
        {
            var navigationService = App.Current.Services.GetRequiredService<INavigationService>();
            navigationService.Navigate<OverviewPage>($"{App.DefaultGitHubDomain}/{ViewModel.IssueItem.Repository.Owner.Login}/{ViewModel.IssueItem.Repository.Name}/issues/{ViewModel.IssueItem.Number}");
        }
    }
}
