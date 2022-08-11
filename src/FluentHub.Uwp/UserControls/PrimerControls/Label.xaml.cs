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
    /// <summary>
    /// See https://primer.style/react/Label
    /// If you'are looking for issue label control, see Token control
    /// </summary>
    public sealed partial class Label : UserControl
    {
        #region propdp
        public static readonly DependencyProperty VariantProperty =
            DependencyProperty.Register(
                nameof(Variant),
                typeof(string),
                typeof(Label),
                new PropertyMetadata(null)
                );

        public string Variant
        {
            get => (string)GetValue(VariantProperty);
            set => SetValue(VariantProperty, value);
        }

        public static readonly DependencyProperty SizeProperty =
            DependencyProperty.Register(
                nameof(Size),
                typeof(string),
                typeof(Label),
                new PropertyMetadata(null)
                );

        public string Size
        {
            get => (string)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                nameof(Text),
                typeof(string),
                typeof(Label),
                new PropertyMetadata(null)
                );

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        #endregion

        public Label()
            => InitializeComponent();
    }
}
