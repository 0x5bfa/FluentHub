using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Uwp.UserControls.PrimerControls
{
    public sealed partial class Timeline : UserControl
    {
        #region propdp
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

        public static readonly DependencyProperty IsCondensedProperty =
            DependencyProperty.Register(
                nameof(IsCondensed),
                typeof(bool),
                typeof(Timeline),
                new PropertyMetadata(false)
                );

        public bool IsCondensed
        {
            get => (bool)GetValue(IsCondensedProperty);
            set => SetValue(IsCondensedProperty, value);
        }
        #endregion

        public Timeline()
            => InitializeComponent();
    }
}
