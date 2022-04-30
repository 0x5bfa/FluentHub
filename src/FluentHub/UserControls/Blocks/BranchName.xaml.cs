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

namespace FluentHub.UserControls.Blocks
{
    public sealed partial class BranchName : UserControl
    {
        #region propdp
        public static readonly DependencyProperty BranchNameProperty =
            DependencyProperty.Register(
                nameof(Branch),
                typeof(string),
                typeof(BranchName),
                new PropertyMetadata(null));

        public string Branch
        {
            get => (string)GetValue(BranchNameProperty);
            set
            {
                SetValue(BranchNameProperty, value);
                DataContext = Branch;
            }
        }
        #endregion

        public BranchName() => this.InitializeComponent();
    }
}
