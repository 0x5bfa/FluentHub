using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Uwp.UserControls.Labels
{
    public sealed partial class StateLabel : UserControl
    {
        #region propdp
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register(
                nameof(Type),
                typeof(string),
                typeof(StateLabel),
                new PropertyMetadata(null));

        public string Type
        {
            get => (string)GetValue(TypeProperty);
            set
            {
                SetValue(TypeProperty, value);
                ViewModel?.LoadContents(Type);
            }
        }
        #endregion

        public StateLabel() => InitializeComponent();
    }
}
