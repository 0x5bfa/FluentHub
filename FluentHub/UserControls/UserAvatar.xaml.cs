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

namespace FluentHub.UserControls
{
    public sealed partial class UserAvatar : UserControl
    {
        #region UserNameProperty
        public static readonly DependencyProperty UserNameProperty
            = DependencyProperty.Register(
                  nameof(UserName),
                  typeof(string),
                  typeof(UserAvatar),
                  new PropertyMetadata(null)
                );

        public string UserName
        {
            get => (string)GetValue(UserNameProperty);
            set
            {
                SetValue(UserNameProperty, value);
                SetAvatar();
            }
        }
        #endregion


        public UserAvatar()
        {
            this.InitializeComponent();
        }

        private void SetAvatar()
        {
            var user = App.Client.User.Get(UserName);
        }
    }
}
