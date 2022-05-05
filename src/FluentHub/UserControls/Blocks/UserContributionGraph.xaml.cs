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
    public sealed partial class UserContributionGraph : UserControl
    {
        #region propdp
        public static readonly DependencyProperty LoginNameProperty =
            DependencyProperty.Register(
                nameof(LoginName),
                typeof(string),
                typeof(UserContributionGraph),
                new PropertyMetadata(null));

        public string LoginName
        {
            get => (string)GetValue(LoginNameProperty);
            set
            {
                SetValue(LoginNameProperty, value);
                ViewModel.LoginName = LoginName;
                _ = ViewModel.GetContributionCalendarAsync();
            }
        }
        #endregion

        public UserContributionGraph()
        {
            this.InitializeComponent();
        }
    }
}
