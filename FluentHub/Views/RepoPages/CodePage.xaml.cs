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


namespace FluentHub.Views.RepoPages
{
    public sealed partial class CodePage : Page
    {
        private long RepoId { get; set; }

        public CodePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            RepoId = Convert.ToInt64(e.Parameter as string);
        }

        private void OverviewColumnCloseButton_Click(object sender, RoutedEventArgs e)
        {
            OverviewColumn.Width = new GridLength(0);
            OverviewColumnOpenButton.Visibility = Visibility.Visible;
        }

        private void OverviewColumnOpenButton_Click(object sender, RoutedEventArgs e)
        {
            OverviewColumn.Width = new GridLength(256);
            OverviewColumnOpenButton.Visibility = Visibility.Collapsed;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var repo = await App.Client.Repository.Get(RepoId);

            RepoDescription.Text = repo.Description;
        }
    }
}
