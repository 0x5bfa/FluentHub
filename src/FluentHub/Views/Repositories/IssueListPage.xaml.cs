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

namespace FluentHub.Views.Repositories
{
    public sealed partial class IssueListPage : Page
    {
        private long RepoId { get; set; }

        public IssueListPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            RepoId = Convert.ToInt64(e.Parameter as string);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void ItemsRepeater_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.GetRepoIssues(RepoId);
        }
    }
}
