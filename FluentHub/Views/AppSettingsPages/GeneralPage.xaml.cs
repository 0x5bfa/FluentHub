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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FluentHub.Views.AppSettingsPages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GeneralPage : Page
    {
        public GeneralPage()
        {
            this.InitializeComponent();
        }

        private void SettingsNavView_SelectionChanged(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            string tag = args.SelectedItemContainer.Tag.ToString();

            switch (tag)
            {
                case "General":
                    SettingsContentFrame.Navigate(typeof(GeneralSettingsPage));
                    NavViewFrameTitleTextBlock.Text = "General";
                    break;
                case "AboutUs":
                    SettingsContentFrame.Navigate(typeof(AboutUsPage));
                    NavViewFrameTitleTextBlock.Text = "About Us";
                    break;
            }

        }
    }
}
