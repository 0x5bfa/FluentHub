using FluentHub.Uwp.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.UserControls.FeedBlocks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.UserControls.FeedBlocks
{
    public sealed partial class SingleReleaseBlock : UserControl
    {
        #region propdp
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                nameof(ViewModel),
                typeof(SingleReleaseBlockViewModel),
                typeof(SingleReleaseBlock),
                new PropertyMetadata(null));

        public SingleReleaseBlockViewModel ViewModel
        {
            get => (SingleReleaseBlockViewModel)GetValue(ViewModelProperty);
            set
            {
                SetValue(ViewModelProperty, value);
                DataContext = ViewModel;
            }
        }
        #endregion

        public SingleReleaseBlock()
            => InitializeComponent();
    }
}
