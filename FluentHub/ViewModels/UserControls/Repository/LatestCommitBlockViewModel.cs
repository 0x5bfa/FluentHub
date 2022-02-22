using FluentHub.ViewModels.Repositories;
using Humanizer;
using Octokit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;

namespace FluentHub.ViewModels.UserControls.Repository
{
    public class LatestCommitBlockViewModel : INotifyPropertyChanged
    {
        private CommonRepoViewModel commonRepoViewModel;
        public CommonRepoViewModel CommonRepoViewModel
        {
            get => commonRepoViewModel;
            set => commonRepoViewModel = value;
        }

        private BitmapImage authorAvatarImage;
        public BitmapImage AuthorAvatarImage { get => authorAvatarImage; set => SetProperty(ref authorAvatarImage, value); }

        private string authorLoginName;
        public string AuthorLoginName { get => authorLoginName; set => SetProperty(ref authorLoginName, value); }

        private string commitMessage;
        public string CommitMessage { get => commitMessage; set => SetProperty(ref commitMessage, value); }

        private string commitSha;
        public string CommitSha { get => commitSha; set => SetProperty(ref commitSha, value); }

        private string commitUpdatedAtHumanized;
        public string CommitUpdatedAtHumanized { get => commitUpdatedAtHumanized; set => SetProperty(ref commitUpdatedAtHumanized, value); }

        private string commitsCount;
        public string CommitsCount { get => commitsCount; set => SetProperty(ref commitsCount, value); }

        private CornerRadius authorAvatarImageCR = new CornerRadius(12d);
        public CornerRadius AuthorAvatarImageCR { get => authorAvatarImageCR; set => SetProperty(ref authorAvatarImageCR, value); }

        public async Task SetContents()
        {
            if (CommonRepoViewModel == null) return;

            CommitRequest request = new CommitRequest();
            request.Path = CommonRepoViewModel.Path;

            var commits = await App.Client.Repository.Commit.GetAll(CommonRepoViewModel.RepositoryId, request);

            AuthorAvatarImage = new BitmapImage(new Uri(commits[0].Author.AvatarUrl));

            if (commits[0].Author.Type != "User") AuthorAvatarImageCR = new CornerRadius(6d);

            AuthorLoginName = commits[0].Author.Login;

            CommitMessage = commits[0].Commit.Message.Split("\n")[0];

            CommitSha = commits[0].Sha.Substring(0, 7);

            CommitUpdatedAtHumanized = commits[0].Commit.Author.Date.Humanize();

            CommitsCount = commits.Count().ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }
    }
}
