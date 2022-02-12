using Humanizer;
using FluentHub.Services.OctokitEx;
using Octokit;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;


namespace FluentHub.Views.RepoPages
{
    public sealed partial class CodePage : Windows.UI.Xaml.Controls.Page
    {
        private long RepoId { get; set; }
        private Repository Repository { get; set; }

        public CodePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            RepoId = Convert.ToInt64(e.Parameter as string);
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //Repository = await App.Client.Repository.Get(RepoId);

            //RepoContentsContentBlock.RepositoryId = RepoId;
            //RepoContentsContentBlock.Path = "";

            //// Repository Description
            //string repoDescription = Repository.Description;

            //if(string.IsNullOrEmpty(repoDescription) == false)
            //{
            //    RepoDescription.Text = repoDescription;
            //}
            //else
            //{
            //    RepoDescription.Text = "No description found for this repositiry.";
            //    RepoDescription.FontStyle = Windows.UI.Text.FontStyle.Italic;
            //}

            //// Repository License
            //if (Repository.License != null)
            //{
            //    LicenseName.Text = Repository.License.Name;
            //    OverviewLicenseBlock.Visibility = Visibility.Visible;
            //}

            //// Stars Count
            //StarsCountTextBlock.Text = Repository.StargazersCount.ToString();

            //// Watchers Count (Do not fix CS0618 warning)
            //WatchingCountTextBlock.Text = Repository.SubscribersCount.ToString();

            //// Forks Count
            //ForksCountTextBlock.Text = Repository.ForksCount.ToString();

            //await ViewModel.EnumRepositoryContents(RepoId);
        }
    }
}
