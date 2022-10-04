using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.Uwp.UserControls
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
