using Humanizer;
using FluentHub.Helpers;
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


namespace FluentHub.UserControls.Block
{
    public sealed partial class RepoBlock : UserControl
    {
        public static readonly DependencyProperty RepositoryIdProperty
            = DependencyProperty.Register(
                  nameof(RepositoryId),
                  typeof(long),
                  typeof(RepoBlock),
                  new PropertyMetadata(null)
                );

        public long RepositoryId
        {
            get => (long)GetValue(RepositoryIdProperty);
            set => SetValue(RepositoryIdProperty, value);
        }

        public static readonly DependencyProperty DisplayDitailsProperty
            = DependencyProperty.Register(
                  nameof(DisplayDitails),
                  typeof(bool),
                  typeof(RepoBlock),
                  new PropertyMetadata(null)
                );

        public bool DisplayDitails
        {
            get => (bool)GetValue(DisplayDitailsProperty);
            set => SetValue(DisplayDitailsProperty, value);
        }

        public RepoBlock()
        {
            this.InitializeComponent();
        }

        private async void RepoBlockUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var repo = await App.Client.Repository.Get(RepositoryId);

            RepoBlockButton.Tag = RepositoryId;

            RepoName.Text = repo.Name;

            if (repo.Description != null)
            {
                Description.Text = repo.Description;
                Description.Visibility = Visibility.Visible;
            }
            
            StarCount.Text = repo.StargazersCount.ToString();

            if (repo.Language != null)
            {
                var brush = LanguageColorHelpers.Get(repo.Language);

                if (brush != null)
                {
                    LanguageColor.Background = brush;
                }

                Language.Text = repo.Language;
                LanguageBlock.Visibility = Visibility.Visible;
            }

            if (DisplayDitails == false)
            {
                return;
            }

            if (repo.License != null)
            {
                License.Text = repo.License.Name;
                LicenseBlock.Visibility = Visibility.Visible;
            }

            ForkCount.Text = repo.ForksCount.ToString();
            ForkCountBlock.Visibility = Visibility.Visible;

            IssueCount.Text = repo.OpenIssuesCount.ToString();
            IssueCountBlock.Visibility = Visibility.Visible;

            var pulls = await App.Client.PullRequest.GetAllForRepository(RepositoryId);
            PRCount.Text = pulls.Count().ToString();
            PRCountBlock.Visibility = Visibility.Visible;

            UpdatedAt.Text = " " + repo.UpdatedAt.Humanize();
            UpdatedAtBlock.Visibility = Visibility.Visible;
        }

        private void StarButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RepoBlockButton_Click(object sender, RoutedEventArgs e)
        {
            App.MainViewModel.MainFrame.Navigate(typeof(Views.RepoPages.OverviewPage), RepoBlockButton.Tag.ToString());
        }
    }
}
