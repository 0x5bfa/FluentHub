using Humanizer;
using Octokit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace FluentHub.ViewModels.UserControls.Repository
{
    public class LatestCommitBlockViewModel : INotifyPropertyChanged
    {
        private long repositoryId;
        public long RepositoryId { get => repositoryId; set => repositoryId = value; }

        private string path;
        public string Path { get => path; set => path = value; }

        private string avatarUrl;
        public string AvatarUrl { get => avatarUrl; set => SetProperty(ref avatarUrl, value); }

        private bool isUser;
        public bool IsUser { get => isUser; set => SetProperty(ref isUser, value); }

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

        public async void SetContents()
        {
            if (repositoryId == 0) return;

            CommitRequest request = new CommitRequest();
            request.Path = Path;

            var commits = await App.Client.Repository.Commit.GetAll(repositoryId, request);

            AuthorLoginName = commits[0].Author.Login;

            AvatarUrl = commits[0].Author.AvatarUrl;

            IsUser = commits[0].Author.Type == "User" ? true : false;

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
