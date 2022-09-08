using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace FluentHub.Uwp.UserControls.PrimerControls
{
    /// <summary>
    /// For more information, visit https://primer.style/react/CounterLabel
    /// </summary>
    public sealed partial class CounterLabel : UserControl
    {
        #region propdp
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                nameof(Text),
                typeof(string),
                typeof(CounterLabel),
                new PropertyMetadata(null)
                );

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty SchemeProperty =
            DependencyProperty.Register(
                nameof(Scheme),
                typeof(string),
                typeof(CounterLabel),
                new PropertyMetadata("primary")
                );

        public string Scheme
        {
            get => (string)GetValue(SchemeProperty);
            set => SetValue(SchemeProperty, value);
        }
        #endregion

        public CounterLabel()
        {
            InitializeComponent();
        }
    }
}
