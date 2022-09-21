using FluentHub.Uwp.ViewModels.UserControls.FeedBlocks;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Uwp.UserControls.FeedBlocks
{
    public sealed partial class SingleCommentBlock : UserControl
    {
        #region propdp
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                nameof(ViewModel),
                typeof(SingleCommentBlockViewModel),
                typeof(SingleCommentBlock),
                new PropertyMetadata(null));

        public SingleCommentBlockViewModel ViewModel
        {
            get => (SingleCommentBlockViewModel)GetValue(ViewModelProperty);
            set
            {
                SetValue(ViewModelProperty, value);
                DataContext = ViewModel;
            }
        }
        #endregion

        public SingleCommentBlock() => InitializeComponent();
    }
}
