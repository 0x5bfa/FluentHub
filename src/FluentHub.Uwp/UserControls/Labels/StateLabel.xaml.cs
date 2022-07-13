using System.Runtime.InteropServices.WindowsRuntime;
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
        public static readonly DependencyProperty StateProperty =
            DependencyProperty.Register(
                nameof(State),
                typeof(string),
                typeof(StateLabel),
                new PropertyMetadata(null));

        public string State
        {
            get => (string)GetValue(StateProperty);
            set
            {
                SetValue(StateProperty, value);
                ViewModel.LoadContents(State);
            }
        }
        #endregion

        public StateLabel() => InitializeComponent();
    }
}
