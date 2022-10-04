using FluentHub.Uwp.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.UserControls.FeedBlocks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.UserControls.FeedBlocks
{
    public sealed partial class SingleCommitBlock : UserControl
    {
        #region propdp
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                nameof(ViewModel),
                typeof(SingleCommitBlockViewModel),
                typeof(SingleCommitBlock),
                new PropertyMetadata(null));

        public SingleCommitBlockViewModel ViewModel
        {
            get => (SingleCommitBlockViewModel)GetValue(ViewModelProperty);
            set
            {
                SetValue(ViewModelProperty, value);
                DataContext = ViewModel;
            }
        }
        #endregion

        public SingleCommitBlock()
            => InitializeComponent();
    }
}
