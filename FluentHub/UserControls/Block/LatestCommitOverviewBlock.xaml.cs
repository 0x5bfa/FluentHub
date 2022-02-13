using Humanizer;
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

namespace FluentHub.UserControls.Block
{
    public sealed partial class LatestCommitOverviewBlock : UserControl
    {
        #region RepositoryIdProperty
        public static readonly DependencyProperty RepositoryIdProperty
            = DependencyProperty.Register(
                  nameof(RepositoryId),
                  typeof(long),
                  typeof(LatestCommitOverviewBlock),
                  new PropertyMetadata(null)
                );

        public long RepositoryId
        {
            get => (long)GetValue(RepositoryIdProperty);
            set
            {
                SetValue(RepositoryIdProperty, value);
                GetSources();
            }
        }
        #endregion

        #region PathProperty
        public static readonly DependencyProperty PathProperty
            = DependencyProperty.Register(
                  nameof(Path),
                  typeof(string),
                  typeof(LatestCommitOverviewBlock),
                  new PropertyMetadata(null)
                );

        public string Path
        {
            get => (string)GetValue(PathProperty);
            set => SetValue(PathProperty, value);
        }
        #endregion

        public LatestCommitOverviewBlock()
        {
            this.InitializeComponent();
        }

        private async void GetSources()
        {
            if (RepositoryId == 0) return;

            CommitRequest request = new CommitRequest();

            if (string.IsNullOrEmpty(Path))
            {
                request.Path = Path;
            }

            var commits = await App.Client.Repository.Commit.GetAll(RepositoryId, request);

            RepoLatestCommitAuthorAvatar.Source = new BitmapImage(new Uri(commits[0].Author.AvatarUrl));

            RepoLatestCommitAuthorName.Text = commits[0].Author.Login;

            RepoLatestCommitMessage.Text = commits[0].Commit.Message.Split("\n")[0];

            RepoLatestCommitSha.Text = commits[0].Sha.Substring(0, 7);

            RepoLatestCommitUpdatedAtHumanized.Text = commits[0].Commit.Author.Date.Humanize();

            RepoCommitsCount.Text = commits.Count().ToString();
        }
    }
}
