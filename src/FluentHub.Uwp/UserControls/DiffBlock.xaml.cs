using FluentHub.Uwp.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.UserControls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.UserControls
{
    public sealed partial class DiffBlock : UserControl
    {
        #region propdp
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                nameof(ViewModel),
                typeof(DiffBlockViewModel),
                typeof(DiffBlock),
                new PropertyMetadata(null));

        public DiffBlockViewModel ViewModel
        {
            get => (DiffBlockViewModel)GetValue(ViewModelProperty);
            set
            {
                SetValue(ViewModelProperty, value);
                ViewModel?.ParseDiffPatch();
            }
        }
        #endregion

        public DiffBlock()
            => InitializeComponent();

        private void OnToggleExpandCollapseButton(object sender, RoutedEventArgs e)
        {
            ViewModel.BlockIsExpanded = ViewModel.BlockIsExpanded ? false : true;
        }
    }
}
