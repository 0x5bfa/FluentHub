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

namespace FluentHub.Views.HomePages
{
    public sealed partial class StarsPage : Page
    {
        private string UserName { get; set; }

        public StarsPage()
        {
            this.InitializeComponent();

            HomeStarsPageFrame.Navigate(typeof(UserPages.StarListPage));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            UserName = e.Parameter as string;

            HomeStarsPageFrame.Navigate(typeof(UserPages.StarListPage), UserName);

            base.OnNavigatedTo(e);
        }
    }
}
