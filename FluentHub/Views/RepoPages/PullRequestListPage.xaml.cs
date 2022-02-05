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
    public sealed partial class PullRequestListPage : Page
    {
        private long RepoId { get; set; }

        public PullRequestListPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            RepoId = Convert.ToInt64(e.Parameter as string);
        }

        private void ItemsRepeater_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.GetRepoIssues(RepoId);
        }
    }
}
