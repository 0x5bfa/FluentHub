using FluentHub.Octokit.Models;
using FluentHub.Services;
using FluentHub.ViewModels.UserControls.ButtonBlocks;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FluentHub.UserControls.ButtonBlocks
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

        public CommitButtonBlock()
        {
            this.InitializeComponent();
        }

        private void CommitItemButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
