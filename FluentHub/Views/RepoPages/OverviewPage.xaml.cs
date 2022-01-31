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
    public sealed partial class OverviewPage : Page
    {
        private long RepoId { get; set; }

        public OverviewPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            RepoId = Convert.ToInt64(e.Parameter as string);
        }

        private void RepoPageNavView_SelectionChanged(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            switch (args.SelectedItemContainer.Tag.ToString())
            {
                case "Code":
                    RepoPageNavViewFrame.Navigate(typeof(CodePage));
                    break;
                case "Issues":
                    RepoPageNavViewFrame.Navigate(typeof(IssueListPage));
                    break;
                case "PRs":
                    RepoPageNavViewFrame.Navigate(typeof(PullRequestListPage));
                    break;
                case "Settings":
                    RepoPageNavViewFrame.Navigate(typeof(Settings));
                    break;
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var repo = await App.Client.Repository.Get(RepoId);

            RepoOwnerName.Text = repo.Owner.Login;

            RepoName.Text = repo.Name;
        }
    }
}
