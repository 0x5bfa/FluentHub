using FluentHub.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace FluentHub.UserControls.Labels
{
    public sealed partial class LabelControl : UserControl
    {
        public static readonly DependencyProperty IconProperty
            = DependencyProperty.Register(nameof(Icon), typeof(IconElement), typeof(LabelControl), new PropertyMetadata(null));

        public IconElement Icon
        {
            get => (IconElement)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public static readonly DependencyProperty LabelTextProperty
            = DependencyProperty.Register(nameof(LabelText), typeof(string), typeof(LabelControl), new PropertyMetadata(null));

        public string LabelText
        {
            get => (string)GetValue(LabelTextProperty);
            set => SetValue(LabelTextProperty, value);
        }

        public static readonly DependencyProperty LabelBackgroundProperty
            = DependencyProperty.Register(nameof(LabelBackground), typeof(Brush), typeof(LabelControl), new PropertyMetadata(null));

        public Brush LabelBackground
        {
            get => (Brush)GetValue(LabelBackgroundProperty);
            set => SetValue(LabelBackgroundProperty, value);
        }

        public static readonly DependencyProperty IsOutlineEnableProperty
            = DependencyProperty.Register(nameof(IsOutlineEnable), typeof(bool), typeof(LabelControl), new PropertyMetadata(null));

        public bool IsOutlineEnable
        {
            get => (bool)GetValue(IsOutlineEnableProperty);
            set => SetValue(IsOutlineEnableProperty, value);
        }

        public LabelControl()
        {
            this.InitializeComponent();
        }

        private void OnLabelControlLoaded(object sender, RoutedEventArgs e)
        {
            if (IsOutlineEnable == false)
            {
                LabelBackgroundGrid.Background = LabelBackground;
            }
            else
            {
                LabelTextTextBlock.Foreground = LabelBackground;
            }
        }
    }
}
