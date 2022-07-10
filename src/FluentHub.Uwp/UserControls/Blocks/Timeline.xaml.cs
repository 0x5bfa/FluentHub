using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.UserControls.Blocks;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FluentHub.Uwp.UserControls.Blocks
{
    public sealed partial class Timeline : UserControl
    {
        #region propdp
        public static readonly DependencyProperty ViewModelProperty = 
            DependencyProperty.Register(
                nameof(ViewModel),
                typeof(TimelineViewModel),
                typeof(Timeline),
                new PropertyMetadata(null)
                );

        public TimelineViewModel ViewModel
        {
            get => (TimelineViewModel)GetValue(ViewModelProperty);
            set
            {
                SetValue(ViewModelProperty, value);
            }
        }

        /*
        public static readonly DependencyProperty BadgeProperty =
            DependencyProperty.Register(
                nameof(Badge),
                typeof(IconElement),
                typeof(Timeline),
                new PropertyMetadata(null)
                );

        public IconElement Badge
        {
            get => (IconElement)GetValue(BadgeProperty);
            set => SetValue(BadgeProperty, value);
        }

        public static readonly DependencyProperty BodyProperty =
            DependencyProperty.Register(
                nameof(Body),
                typeof(FrameworkElement),
                typeof(Timeline),
                new PropertyMetadata(null)
                );

        public FrameworkElement Body
        {
            get => (FrameworkElement)GetValue(BodyProperty);
            set => SetValue(BodyProperty, value);
        }
        */
        #endregion

        public Timeline() => InitializeComponent();
    }
}
