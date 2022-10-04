using FluentHub.Uwp.ViewModels.UserControls;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

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

        public DiffBlock() => InitializeComponent();

        private void OnToggleExpandCollapseButton(object sender, RoutedEventArgs e)
        {
            ViewModel.BlockIsExpanded = ViewModel.BlockIsExpanded ? false : true;
        }
    }
}
