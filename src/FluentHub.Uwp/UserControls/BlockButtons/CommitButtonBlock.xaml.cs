using FluentHub.Uwp.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FluentHub.Uwp.UserControls.ButtonBlocks
{
    public sealed partial class CommitButtonBlock : UserControl
    {
        #region propdp
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                nameof(CommitButtonBlockViewModel),
                typeof(CommitButtonBlockViewModel),
                typeof(CommitButtonBlock),
                new PropertyMetadata(null));

        public CommitButtonBlockViewModel ViewModel
        {
            get => (CommitButtonBlockViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
        #endregion

        public CommitButtonBlock() => InitializeComponent();

        private void CommitItemButton_Click(object sender, RoutedEventArgs e)
        {
            Type frameType;

            var param = new FrameNavigationArgs()
            {
                Login = ViewModel.CommitItem.Repository.Owner.Login,
                Name = ViewModel.CommitItem.Repository.Name,
                Parameters = new() { ViewModel.CommitItem },
            };

            if (ViewModel.PullRequest == null)
            {
                frameType = typeof(Views.Repositories.Commits.CommitPage);
            }
            else
            {
                frameType = typeof(Views.Repositories.PullRequests.CommitPage);
                param.Number = ViewModel.PullRequest.Number;
            }

            var navService = App.Current.Services.GetRequiredService<INavigationService>();
            navService.Navigate(frameType, param);
        }
    }
}
