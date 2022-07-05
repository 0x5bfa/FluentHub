using FluentHub.ViewModels.UserControls.Blocks;
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

namespace FluentHub.UserControls.Blocks
{
    public sealed partial class ActivityBlock : UserControl
    {
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
               nameof(ViewModel),
               typeof(ActivityBlockViewModel),
               typeof(ActivityBlock),
               new PropertyMetadata(null));

        public ActivityBlockViewModel ViewModel
        {
            get => (ActivityBlockViewModel)GetValue(ViewModelProperty);
            set
            {
                SetValue(ViewModelProperty, value);
                ViewModel?.LoadContentsAsync();
            }
        }

        public ActivityBlock() => InitializeComponent();
    }
}
